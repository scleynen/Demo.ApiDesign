using Demo.ApiDesign.Cache.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.ApiDesign.Cache.Context
{
    public class DemoDbContext: DbContext
    {
        public DbSet<Store> Stores { get; set; }
        public DbSet<Product> Products { get; set; }

        public DemoDbContext(DbContextOptions<DemoDbContext> options) : base(options)
        { }
    }
}
