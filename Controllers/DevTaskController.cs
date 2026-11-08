using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using TasksApi.Data;
using TasksApi.Models;
using WebApplication4.Extinsion;
using WebApplication4.Services;

namespace TasksApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DevTaskController : ControllerBase
    {
        private readonly ApplicationDbContext db;
        private readonly IUploadService upload;

        public DevTaskController(ApplicationDbContext db, IUploadService upload)
        {
            this.db = db;
            this.upload = upload;
        }
        [HttpGet]
        [Route("devtasks")]
        [Authorize]
        public async Task<ActionResult<List<DevTask>>> Get([FromQuery] string? search, [FromQuery] int page =1, [FromQuery] int paeSize =1, [FromQuery] string sortBy ="title", [FromQuery] string sortOrder ="asc")
        {
            var query = db.tblTasks.AsQueryable();
            if (!string.IsNullOrEmpty(search))
            {
                //  query = query.Where(p => p.ProductName.Contains(SeatchTerm));
                query = query.Where(p => EF.Functions.Like(p.Title, $"%{search}%"));

            }
           
            var sordor = sortOrder.Equals("desc", StringComparison.OrdinalIgnoreCase) ? "desc" : "asc";
            query = query.ApplySorting(sortBy, sordor);


            var Tasks = await PaginatedList<DevTask>.CreateAsync(query, page, paeSize);
            return Ok(Tasks);


        }
        [HttpPost]
        [Route("devtasks/add")]
        [Authorize(Roles = "Supervisor")]
        public async Task<ActionResult> Add([FromForm] DevTask devTask)
        {
            try
            {
                await db.tblTasks.AddAsync(devTask);
                await db.SaveChangesAsync();
                return CreatedAtAction(nameof(Get), new { id = devTask.Id }, devTask);

            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
        [HttpPut]
        [Route("devtasks/{id}/cmplete-rate")]
        [Authorize(Roles = "Supervisor")]
        public async Task<ActionResult> UpdateCompleteRate(int id, int rate)
        {
            var task = db.tblTasks.FirstOrDefault(t => t.Id == id);
            if (task is null) return NotFound();
            task.CompleteRate = rate;
            db.SaveChanges();
            return Ok(task);
        }
        [HttpPut]
        [Route("{id}/document")]
        [Authorize(Roles = "Supervisor")]
        public async Task<IActionResult> Upload(int id, IFormFile file)
        {
            if (file is null || file.Length == 0)
                return BadRequest("No file was provided.");

            var task = await db.tblTasks.FirstOrDefaultAsync(e => e.Id == id);
            if (task is null) 
                return NotFound();
            string? path;
            try
            {
                path = upload.Upload(file, "TaskDocuments");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            task.Document = path;
            await db.SaveChangesAsync();

            return NoContent();
        }



    }
    }

