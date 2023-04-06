using Microsoft.EntityFrameworkCore;
using SupplierQuantityServerApp.Data;
using SupplierQuantityServerApp.Helpers;

namespace SupplierQuantityServerApp.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new SupplierQuantityServerAppContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<SupplierQuantityServerAppContext>>()))
            {
                // Look for any movies.
                if (context.Customer.Any())
                {
                    return;   // DB has been seeded
                }
                context.Customer.AddRange(
                    new Customer
                    {
                        Name = "Coca cola",
                        Password = CryptographyHelper.SimpleEncrypt("test123"),
                        Quantity = 104.5m
                    },
                    new Customer
                    {
                        Name = "Elan",
                        Password = CryptographyHelper.SimpleEncrypt("test123"),
                        Quantity = 24.5m
                    },
                    new Customer
                    {
                        Name = "Spar",
                        Password = CryptographyHelper.SimpleEncrypt("test123"),
                        Quantity = 10456.523m
                    },
                    new Customer
                    {
                        Name = "Union",
                        Password = CryptographyHelper.SimpleEncrypt("test123"),
                        Quantity = 8
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
