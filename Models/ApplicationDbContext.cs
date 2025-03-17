using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace MadeByMe.Models
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
