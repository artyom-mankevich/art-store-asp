using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using MyWebApp.Data.Entities;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace MyWebApp.Data
{
    public class DutchSeeder
    {
        private readonly DutchContext _context;
        private readonly IWebHostEnvironment _environment;
        private readonly UserManager<StoreUser> _userManager;

        public DutchSeeder(DutchContext context, IWebHostEnvironment environment, UserManager<StoreUser> userManager)
        {
            _context = context;
            _environment = environment;
            _userManager = userManager;
        }

        public async Task SeedAsync()
        {
            _context.Database.EnsureCreated();

            StoreUser user = await _userManager.FindByEmailAsync("artyom@mail.com");
            if (user == null)
            {
                user = new StoreUser()
                {
                    FirstName = "Artyom",
                    LastName = "Mankevich",
                    Email = "artyom@mail.com",
                    UserName = "artyom"
                };

                var result = await _userManager.CreateAsync(user, "Password1!");
                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create new user in seeder");
                }
            }
            
            if (!_context.Products.Any())
            {
                var filepath = Path.Combine(_environment.ContentRootPath, "Data/art.json");
                var json = File.ReadAllText(filepath);
                var products = JsonSerializer.Deserialize<IEnumerable<Product>>(json);
                
                _context.Products.AddRange(products);
                var order = new Order()
                {
                    User = user,
                    OrderDate = DateTime.Today,
                    OrderNumber = "10000",
                    Items = new List<OrderItem>()
                    {
                        new OrderItem()
                        {
                            Product = products.First(),
                            Quantity = 5,
                            UnitPrice = products.First().Price
                        }
                    }
                };
                _context.Orders.Add(order);
                _context.SaveChanges();
            }
        }
    }
}