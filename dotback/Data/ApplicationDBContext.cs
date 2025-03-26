using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotback.Models;
using Microsoft.EntityFrameworkCore;

namespace dotback.Data
{
    public class ApplicationDBContext : DbContext
    {
        // base(): similar with super()
        public ApplicationDBContext(DbContextOptions dbContextOptions)
        : base(dbContextOptions) 
        {
            
        }

        // to query data
        public DbSet<Stock> Stock { get; set; }
        public DbSet<Comment> Comment { get; set; }
    }
}