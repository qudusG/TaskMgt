using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManagement.Enums;

namespace TaskManagement.DTOs
{
    public class CreateComment
    {
        public long TaskId { get; set; }
        public string Text { get; set; }
        public CommentType Type { get; set; }
        public DateTime ReminderDate { get; set; }
    }
}
