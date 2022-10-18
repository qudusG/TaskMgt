using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManagement.DTOs
{
    public class CommentDTO
    {
        public string Id { get; set; }
        public string TaskId { get; set; }
        public string Poster { get; set; }
        public string DateAdded { get; set; }
        public string Text { get; set; }
        public int Type { get; set; }
        public string ReminderDate { get; set; }
    }
}
