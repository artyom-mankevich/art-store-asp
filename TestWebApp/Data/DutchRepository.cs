using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyWebApp.Data.Entities;

namespace MyWebApp.Data
{
    public class DutchRepository : IDutchRepository
    {
        private readonly DutchContext _dutchContext;
        private readonly ILogger<DutchRepository> _logger;

        public DutchRepository(DutchContext dutchContext, ILogger<DutchRepository> logger)
        {
            _dutchContext = dutchContext;
            _logger = logger;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            try
            {
                _logger.LogInformation("GetAllProducts was called");

                return _dutchContext.Products
                    .OrderBy(p => p.Title)
                    .ToList();
            }
            catch (Exception e)
            {
                _logger.LogError($"Failed to get all products: {e}");
                return null;
            }
        }

        public IEnumerable<Product> GetProductsByCategory(string category)
        {
            return _dutchContext.Products.Where(product => product.Category == category).ToList();
        }

        public Order GetOrderById(string username, int id)
        {
            return _dutchContext.Orders
                .Include(order => order.Items)
                .ThenInclude(item => item.Product)
                .Where(o => o.Id == id && o.User.UserName == username)
                .FirstOrDefault(order => order.Id == id);
        }

        public IEnumerable<Order> GetAllOrders(bool includeItems)
        {
            if (includeItems)
            {
                return _dutchContext.Orders
                    .Include(order => order.Items)
                    .ThenInclude(item => item.Product)
                    .ToList();
            }
            else
            {
                return _dutchContext.Orders.ToList();
            }
        }

        public bool SaveAll()
        {
            return _dutchContext.SaveChanges() > 0;
        }

        public void AddEntity(object model)
        {
            _dutchContext.Add(model);
        }

        public IEnumerable<Order> GetAllOrdersByUser(string username, bool includeItems)
        {
            if (includeItems)
            {
                return _dutchContext.Orders
                    .Where(o => o.User.UserName == username)
                    .Include(order => order.Items)
                    .ThenInclude(item => item.Product)
                    .ToList();
            }
            else
            {
                return _dutchContext.Orders
                    .Where(o => o.User.UserName == username)
                    .ToList();
            }
        }
    }
}