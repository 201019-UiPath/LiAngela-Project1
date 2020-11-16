using StoreDB.Models;
using System.Collections.Generic;

namespace StoreLib
{
    public interface IProductService
    {
        List<Product> GetAllProducts();

        int GetNewProductId();

        int GetNewProductStockId();

        Product GetProductById(int productId);

        List<ProductStock> GetProductStockByLocation(int locationId);

        List<ProductStock> GetProductStockByProductId(int productId);

        ProductStock GetProductStockByLocationProductId(int locationId, int productId);

        void AddProduct(Product newProduct);

        void UpdateProductStocksBatch(int locationId, Dictionary<int, int> cart);

        void UpdateProductStockSingle(int locationId, int productId, int quantityChange);

        void UpdateProductStock(int locationId, int productId, int quantityChange);
    }
}