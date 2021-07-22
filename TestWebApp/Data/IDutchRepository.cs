using System.Collections;
using System.Collections.Generic;
using MyWebApp.Data.Entities;

namespace MyWebApp.Data
{
    public interface IDutchRepository
    {
        IEnumerable<Product> GetAllProducts();
        IEnumerable<Product> GetProductsByCategory(string category);
        bool SaveAll();
    }
}