using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManagement.Enums;

namespace TaskManagement.DTOs
{
    public class CreateTask
    {
        public DateTime RequiredDate { get; set; }
        public string Description { get; set; }
        public TaskType Type { get; set; }
        public string AssignedToId { get; set; }
        public string Title { get; set; }
    }
}
