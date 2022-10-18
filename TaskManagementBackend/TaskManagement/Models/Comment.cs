using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManagement.Enums;

namespace TaskManagement.Models
{
    public class Comment
    {
        public long Id { get; set; }
        public long TaskId { get; set; }
        public Task_ Task { get; set; }
        public string PosterId { get; set; }
        public User Poster { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime DateUpdated { get; set; }
        public string Text { get; set; }
        public CommentType Type { get; set; }
        public DateTime? ReminderDate { get; set; }
    }
}
