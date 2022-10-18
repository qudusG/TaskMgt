using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManagement.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace TaskManagement
{
    public class AppContext : IdentityDbContext<User>
    {
        public AppContext(DbContextOptions<AppContext> options) : base(options)
        {
        }
        public DbSet<Task_> Tasks { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}
