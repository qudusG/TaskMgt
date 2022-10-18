using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManagement.Enums;

namespace TaskManagement.DTOs
{
    public class EditComment
    {
        public long Id { get; set; }
        public string Text { get; set; }
        public CommentType Type { get; set; }
        public DateTime RequiredDate { get; set; }
    }
}
