using System;
using System.Collections.Generic;

using StoreDB.Models;
using StoreDB.Repos;

namespace StoreLib
{
    public class ProductService : IProductService
    {
        private IProductRepo repo;

        public ProductService(IProductRepo repo)
        {
            this.repo = repo;
        }

        public List<Product> GetAllProducts()
        {
            return repo.GetAllProducts();
        }

        public int GetNewProductId()
        {
            return repo.GetLastProductId() + 1;
        }

        public int GetNewProductStockId()
        {
            return repo.GetLastProductStockId() + 1;
        }

        public Product GetProductById(int productId)
        {
            return repo.GetProductById(productId);
        }

        public List<ProductStock> GetProductStockByLocation(int locationId)
        {
            return repo.GetProductStockByLocation(locationId);
        }

        public List<ProductStock> GetProductStockByProductId(int productId)
        {
            return repo.GetProductStockByProductId(productId);
        }

        public ProductStock GetProductStockByLocationProductId(int locationId, int productId)
        {
            return repo.GetProductStockByLocationProductId(locationId, productId);
        }

        public void AddProduct(Product newProduct)
        {
            newProduct.ProductId = GetNewProductId();
            repo.AddProduct(newProduct);
        }

        public void UpdateProductStocksBatch(int locationId, Dictionary<int, int> cart)
        {
            foreach (KeyValuePair<int, int> item in cart)
            {
                if (item.Value < 0)
                {
                    throw new Exception("Quantity must be non-negative");
                } else if (item.Value > 0)
                {
                    UpdateProductStock(locationId, item.Key, item.Value * -1);
                }
            }
            repo.SaveProductStockChanges();
        }

        public void UpdateProductStockSingle(int locationId, int productId, int quantityChange)
        {
            UpdateProductStock(locationId, productId, quantityChange);
            repo.SaveProductStockChanges();
        }

        public void UpdateProductStock(int locationId, int productId, int quantityChange)
        {
            ProductStock productStock = GetProductStockByLocationProductId(locationId, productId);
            if (productStock == null && quantityChange > 0)
            {
                ProductStock newProductStock = new ProductStock();
                newProductStock.Id = GetNewProductStockId();
                newProductStock.LocationId = locationId;
                newProductStock.ProductId = productId;
                newProductStock.QuantityStocked = quantityChange;
                repo.AddProductStock(newProductStock);
            }
            else if (productStock.QuantityStocked + quantityChange == 0)
            {
                repo.RemoveProductStock(productStock);
            }
            else if (productStock.QuantityStocked + quantityChange < 0)
            {
                throw new Exception("Quantity must remain non-negative");
            }
            else
            {
                productStock.QuantityStocked += quantityChange;
                repo.UpdateProductStock(productStock);
            }
        }
    }
}