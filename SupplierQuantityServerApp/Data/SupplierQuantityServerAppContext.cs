using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SupplierQuantityServerApp.Models;

namespace SupplierQuantityServerApp.Data
{
    public class SupplierQuantityServerAppContext : DbContext
    {
        public SupplierQuantityServerAppContext (DbContextOptions<SupplierQuantityServerAppContext> options)
            : base(options)
        {
        }

        public DbSet<SupplierQuantityServerApp.Models.Customer> Customer { get; set; } = default!;
    }
}
