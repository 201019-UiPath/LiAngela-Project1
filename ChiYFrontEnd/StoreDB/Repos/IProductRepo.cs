using System.Collections.Generic;

using StoreDB.Models;

namespace StoreDB.Repos
{
    public interface IProductRepo
    {
         List<Product> GetAllProducts();

         int GetLastProductId();

         int GetLastProductStockId();

         Product GetProductById(int productId);

         List<ProductStock> GetProductStockByLocation(int locationId);

         List<ProductStock> GetProductStockByProductId(int productId);

         ProductStock GetProductStockByLocationProductId(int locationId, int productId);

         void AddProduct(Product product);

         void AddProductStock(ProductStock productStock);

         void UpdateProductStock(ProductStock productStock);

         void RemoveProductStock(ProductStock productStock);

         void SaveProductStockChanges();
    }
}