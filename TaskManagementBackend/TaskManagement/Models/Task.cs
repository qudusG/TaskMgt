using System;
using System.Collections.Generic;
using System.Linq;
using TaskManagement.Enums;

namespace TaskManagement.Models
{
    public class Task_
    {
        public long Id { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime RequiredDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public TaskStatus Status { get; set; }
        public TaskType Type { get; set; }
        public string CreatedById { get; set; }
        public User CreatedBy { get; set; }
        public string AssignedToId { get; set; }
        public User AssignedTo { get; set; }
        public DateTime? NextActionDate { get; set; }
        public virtual IEnumerable<Comment> Comments { get; set; }
    }
}
