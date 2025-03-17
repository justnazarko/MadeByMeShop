using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace MadeByMe.src.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
           : base(options)
        {
        }

        public DbSet<Post> Posts { get; set; }
    }
}
