using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManagement.DTOs;
using TaskManagement.Models;

namespace TaskManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CommentsController : ControllerBase
    {
        private readonly AppContext _context;
        public CommentsController(AppContext context)
        {
            _context = context;
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateComment createComment)
        {
            var task = await _context.Tasks.FromSqlRaw("SELECT * From Tasks Where Id = {0}", createComment.TaskId).FirstOrDefaultAsync();
            if (task == null)
            {
                return BadRequest(new { Message = "Task does not exist." });
            }
            var authenticatedUserId = User.GetAuthenticatedUserId();
            var comment = new Comment
            {
                ReminderDate = createComment.ReminderDate,
                DateAdded = DateTime.UtcNow,
                TaskId = createComment.TaskId,
                PosterId = authenticatedUserId,
                Text = createComment.Text,
                Type = createComment.Type
            };
            _context.Add(comment);
            await _context.SaveChangesAsync();

            if (comment.ReminderDate != null)
            {
                task.NextActionDate = comment.ReminderDate;
            }
            task.UpdatedDate = comment.DateAdded;

            _context.Update(task);
            await _context.SaveChangesAsync();
            return Ok(new { Message = "Comment added." });
        }
        [HttpPost("Update")]
        public async Task<IActionResult> Update([FromBody] EditComment editComment)
        {
            var comment = await _context.Comments.Where(c => c.Id == editComment.Id).FirstOrDefaultAsync();
            if (comment == null)
            {
                return BadRequest(new { Message = "Comment does not exist." });
            }
            var authenticatedUserId = User.GetAuthenticatedUserId();
            if (comment.PosterId != authenticatedUserId)
            {
                return BadRequest(new { Message = "Cannot edit comment which does not belong to user." });
            }
            comment.DateUpdated = DateTime.UtcNow;
            comment.Text = editComment.Text;
            comment.Type = editComment.Type;
            comment.ReminderDate = editComment.RequiredDate;


            var task = await _context.Tasks.Where(c => c.Id == comment.TaskId).FirstOrDefaultAsync();
            task.UpdatedDate = comment.DateUpdated;
            if (comment.ReminderDate != null)
            {
                task.NextActionDate = comment.ReminderDate;
            }
            _context.Update(comment);
            _context.Update(task);
            await _context.SaveChangesAsync();
            return Ok(new { Message = "Comment updated." });
        }
    }
}
