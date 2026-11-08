using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TasksApi.Models;

namespace TasksApi.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public  DbSet<DevTask> tblTasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<DevTask>(entity =>
            {
                entity.Property(e => e.StartDate)
                   .HasColumnType("date")
                   .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CompleteRate).HasDefaultValueSql("((0))");
            });


            // add users and roles 
            modelBuilder.Seed();
        }
    }
}