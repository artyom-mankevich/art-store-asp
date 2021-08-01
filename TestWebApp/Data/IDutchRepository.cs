using System.Collections;
using System.Collections.Generic;
using MyWebApp.Data.Entities;

namespace MyWebApp.Data
{
    public interface IDutchRepository
    {
        IEnumerable<Product> GetAllProducts();
        IEnumerable<Product> GetProductsByCategory(string category);
        IEnumerable<Order> GetAllOrders(bool includeItems);
        Order GetOrderById(string username, int id);
        bool SaveAll();
        void AddEntity(object model);
        IEnumerable<Order> GetAllOrdersByUser(string username, bool includeItems);
    }
}