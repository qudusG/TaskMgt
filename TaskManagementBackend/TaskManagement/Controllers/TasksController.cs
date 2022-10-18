using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManagement.Data;
using TaskManagement.DTOs;
using TaskManagement.Models;


namespace TaskManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TasksController : ControllerBase
    {
        private readonly AppContext _context;
        private readonly IDatabase _database;
        public TasksController(AppContext context, IDatabase database)
        {
            _context = context;
            _database = database;
        }
        [HttpPost]
        public IActionResult Create([FromBody] CreateTask createTask)
        {
            var authenticatedUserId = User.GetAuthenticatedUserId();
            var sql = "INSERT tasks (Description, AssignedToId, DateCreated," +
                " RequiredDate, UpdatedDate, Type, Title, CreatedById, Status) VALUES (@Description, @AssignedToId, " +
                "@DateCreated, @RequiredDate, @UpdatedDate, @Type, @Title, @CreatedById, @Status)";

            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Description", createTask.Description),
                new SqlParameter("@AssignedToId", createTask.AssignedToId),
                new SqlParameter("@DateCreated", DateTime.UtcNow),
                new SqlParameter("@RequiredDate", createTask.RequiredDate),
                new SqlParameter("@UpdatedDate", DateTime.UtcNow),
                new SqlParameter("@Type", createTask.Type),
                new SqlParameter("@Title", createTask.Title),
                new SqlParameter("@CreatedById", authenticatedUserId),
                new SqlParameter("@Status", Enums.TaskStatus.New)
            };
            _database.Write(sql, parameters);
            return Ok(new { Message = "Task created." });
        }
        [HttpPost("Update")]
        public async Task<IActionResult> Update([FromBody] UpdateTask updateTask)
        {
            var task = await _context.Tasks.FromSqlRaw("SELECT * From Tasks Where Id = {0}", updateTask.Id).FirstOrDefaultAsync();
            if (task == null)
                return BadRequest(new { Message = "Task does not exist." });
            
            var authenticatedUserId = User.GetAuthenticatedUserId();
            if (task.CreatedById != authenticatedUserId && task.AssignedToId != authenticatedUserId)
                return BadRequest(new { Message = "Cannot edit task not related to user." });
            
            var sql = $"update tasks set UpdatedDate = @UpdatedDate, Status = @Status where Id = {task.Id}";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Status", updateTask.Status),
                new SqlParameter("@UpdatedDate", DateTime.UtcNow)
            };
            _database.Write(sql, parameters);
            return Ok(new { Message = "Task status updated." });
        }
        [HttpGet("Stats")]
        public async Task<IActionResult> Stats()
        {
            var tasks = await _context.Tasks.FromSqlRaw("SELECT * From Tasks").ToListAsync();
            return Ok(new
            {
                New = tasks.Count(c => c.Status == Enums.TaskStatus.New),
                NewPercentage = Math.Round(tasks.Count(c => c.Status == Enums.TaskStatus.New) / (decimal)tasks.Count * 100, 2),
                InProgress = tasks.Count(c => c.Status == Enums.TaskStatus.InProgress),
                InProgressPercentage = Math.Round(tasks.Count(c => c.Status == Enums.TaskStatus.InProgress) / (decimal)tasks.Count * 100, 2),
                Completed = tasks.Count(c => c.Status == Enums.TaskStatus.Completed),
                CompletedPercentage = Math.Round(tasks.Count(c => c.Status == Enums.TaskStatus.Completed) / (decimal)tasks.Count * 100, 2),
                Total = tasks.Count
            });
        }

        [HttpGet]
        public IActionResult Get()
        {
            var data = _database.Read("SELECT t.id, t.DateCreated, t.RequiredDate, t.UpdatedDate, t.Title, t.Description," +
                "t.Status, t.Type, a.name, t.NextActionDate, t.AssignedToId, t.CreatedById " +
                "From Tasks t left join aspnetusers a on a.Id = t.assignedToId");
            var tasks = new List<object>();
            var count = data.Rows.Count;
            for (int j = 0; j < count; j++)
            {
                tasks.Add(new 
                { 
                    id = data.Rows[j]["id"].ToString(),
                    DateCreated = data.Rows[j]["DateCreated"].ToString(),
                    RequiredDate = data.Rows[j]["RequiredDate"].ToString(),
                    UpdatedDate = data.Rows[j]["UpdatedDate"].ToString(),
                    Title = data.Rows[j]["Title"].ToString(),
                    Description = data.Rows[j]["Description"].ToString(),
                    Status = data.Rows[j]["Status"].ToString(),
                    Type = data.Rows[j]["Type"].ToString(),
                    AssignedTo = data.Rows[j]["Name"].ToString(),
                    NextActionDate = data.Rows[j]["NextActionDate"].ToString()
                });
            }
            return Ok(tasks);
        }
        [HttpGet("{id}")]
        public IActionResult GetById(long id)
        {
            var taskData = _database.Read($"SELECT * from tasks t left join aspnetusers a on a.id = t.AssignedToId where t.id = {id}");
            if (taskData.Rows.Count < 1)
                return BadRequest(new { Message = "Task not found." });
            var task = new TaskDTO
            {
                Id = taskData.Rows[0]["Id"].ToString(),
                DateCreated = taskData.Rows[0]["DateCreated"].ToString(),
                CreatedById = taskData.Rows[0]["CreatedById"].ToString(),
                RequiredDate = taskData.Rows[0]["RequiredDate"].ToString(),
                UpdatedDate = taskData.Rows[0]["UpdatedDate"].ToString(),
                Title = taskData.Rows[0]["Title"].ToString(),
                Description = taskData.Rows[0]["Description"].ToString(),
                Status = int.Parse(taskData.Rows[0]["Status"].ToString()),
                Type = int.Parse(taskData.Rows[0]["Type"].ToString()),
                AssignedTo = taskData.Rows[0]["name"].ToString(),
                NextActionDate = taskData.Rows[0]["NextActionDate"].ToString()
            };

            var commentData = _database.Read($"SELECT * from comments c left join aspnetusers a on a.id = c.posterId where c.taskId = {id}");
            if (commentData.Rows.Count > 0)
            {
                for (int j = 0; j < commentData.Rows.Count; j++)
                {
                    task.Comments.Add(new CommentDTO
                    {
                        Id = commentData.Rows[j]["Id"].ToString(),
                        TaskId = task.Id,
                        DateAdded = commentData.Rows[j]["DateAdded"].ToString(),
                        Text = commentData.Rows[j]["text"].ToString(),
                        Poster = commentData.Rows[j]["name"].ToString(),
                        ReminderDate = commentData.Rows[j]["reminderDate"].ToString(),
                        Type = int.Parse(commentData.Rows[j]["type"].ToString())
                    });
                }
            }
            return Ok(task);
        }
    }
}
