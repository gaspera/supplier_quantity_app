using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WarehouseServerApp.Models;

namespace WarehouseServerApp.Data
{
    public class WarehouseServerAppContext : DbContext
    {
        public WarehouseServerAppContext (DbContextOptions<WarehouseServerAppContext> options)
            : base(options)
        {
        }

        public DbSet<WarehouseServerApp.Models.Movie> Movie { get; set; } = default!;
    }
}
