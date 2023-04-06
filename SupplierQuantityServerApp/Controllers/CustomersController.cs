using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SupplierQuantityServerApp.Data;
using SupplierQuantityServerApp.Models;

namespace SupplierQuantityServerApp.Controllers
{
    /*
     * TODO:
     * Reorganize (multiple controllers)
     * Move logic from controller to server
     * Add unit test (AddQuantity, GetCustomers)
     * Improve Login logic (e.g. add cookies and validate every call)
     * Add different error messages for different cases on the controller
     * Remove unneeded methods in controller
     * Password hashing should be done on the server
     * Improve on naming - customer, supplier, username is all the same
     */

    public class CustomersController : Controller
    {
        private readonly SupplierQuantityServerAppContext _context;

        public CustomersController(SupplierQuantityServerAppContext context)
        {
            _context = context;
        }

        // GET: Customers
        public async Task<IActionResult> Index()
        {
              return _context.Customer != null ? 
                          View(await _context.Customer.ToListAsync()) :
                          Problem("Entity set 'SupplierQuantityServerAppContext.Customer'  is null.");
        }

        // GET: Customers/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Customer == null)
            {
                return NotFound();
            }

            var customer = await _context.Customer
                .FirstOrDefaultAsync(m => m.Name == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }
         

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] Customer customer)
        {
            if (customer.Name == null || customer.Password == null)
            {
                return BadRequest();
            }

            var existingCustomer = await _context.Customer
                .FirstOrDefaultAsync(m => m.Name == customer.Name && m.Password == customer.Password);
            if (existingCustomer == null)
            {
                return Unauthorized();
            }

            return Ok();
        }


        [HttpPost]
        public async Task<IActionResult> AddQuantity([FromBody]Customer customer)
        {
            var existingCustomer = await _context.Customer
                .FirstOrDefaultAsync(m => m.Name == customer.Name);
            if (existingCustomer == null)
            {
                return BadRequest();
            }

            if (customer.Quantity == null)
            {
                return BadRequest();
            }

            existingCustomer.Quantity += customer.Quantity;

            try
            {
                _context.Update(existingCustomer);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(customer.Name))
                {
                    return BadRequest();
                }
                else
                {
                    throw;
                }
            }
            return Ok();
        }

        private bool CustomerExists(string id)
        {
          return (_context.Customer?.Any(e => e.Name == id)).GetValueOrDefault();
        }
    }
}
