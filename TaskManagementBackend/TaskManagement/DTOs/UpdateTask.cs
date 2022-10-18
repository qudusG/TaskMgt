using System;
using System.Collections.Generic;
using System.Linq;
using TaskManagement.Enums;

namespace TaskManagement.DTOs
{
    public class UpdateTask
    {
        public long Id { get; set; }
        public TaskStatus Status { get; set; }
    }
}
