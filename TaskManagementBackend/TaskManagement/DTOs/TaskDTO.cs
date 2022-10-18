using System;
using System.Collections.Generic;
using System.Linq;
using TaskManagement.Enums;
using TaskManagement.Models;

namespace TaskManagement.DTOs
{
    public class TaskDTO
    {
        public TaskDTO()
        {
            Comments = new List<CommentDTO> { };
        }
        public string Id { get; set; }
        public string DateCreated { get; set; }
        public string RequiredDate { get; set; }
        public string UpdatedDate { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }
        public int Type { get; set; }
        public string CreatedById { get; set; }
        public string AssignedToId { get; set; }
        public string AssignedTo { get; set; }
        public string NextActionDate { get; set; }
        public List<CommentDTO> Comments { get; set; }
    }
}
