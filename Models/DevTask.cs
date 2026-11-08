using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TasksApi.Models
{
    public class DevTask
    {
        public int Id { get; set; }

        [Required, StringLength(100, MinimumLength = 5, ErrorMessage = "This field must be betweeb 5 and 100 chars.")]

        public string Title { get; set; }

        [Required(ErrorMessage = "This field is required.")]

        public string Details { get; set; }

        public string? Document { get; set; } = "No-Image.png";

        [Required]
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime? EndDate { get; set; }
        [Required]
        public DateTime? DeadLine { get; set; }

        public double CompleteRate { get; set; } = 0;
        public string IdentityUserId { get; set; }
        public virtual IdentityUser ApplicationUser { get; set; }


    }
}
