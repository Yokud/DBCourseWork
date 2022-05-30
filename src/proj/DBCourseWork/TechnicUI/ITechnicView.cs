using DataBaseUI.SysEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnicUI
{
    public interface ITechnicView
    {
        delegate void AddShopDel(Shop shop);
        event AddShopDel AddShopEvent;

        delegate void DeleteShopDel(Shop shop);
        event DeleteShopDel DeleteShopEvent;

        delegate void UpdateShopDel(Shop shop);
        event UpdateShopDel UpdateShopEvent;

        delegate void GetShopDel(int id);
        event GetShopDel GetShopEvent;

        delegate void GetAllShopsDel();
        event GetAllShopsDel GetAllShopsEvent;

        void AddShop();
        void DeleteShop();
        void UpdateShop();
        void GetShop();
        void GetAllShops();


        delegate void AddProductDel(Product shop);
        event AddProductDel AddProductEvent;

        delegate void DeleteProductDel(Product shop);
        event DeleteProductDel DeleteProductEvent;

        delegate void UpdateProductDel(Product shop);
        event UpdateProductDel UpdateProductEvent;

        delegate void GetProductDel(int id);
        event GetProductDel GetProductEvent;

        delegate void GetAllProductsDel();
        event GetAllProductsDel GetAllProductsEvent;

        void AddProduct();
        void DeleteProduct();
        void UpdateProduct();
        void GetProduct();
        void GetAllProducts();

        void AddAvailability();
        void DeleteAvailability();
        void UpdateAvailability();
        void GetAvailability();
        void GetAllAvailabilities();

        void AddSaleReceipt();
        void DeleteSaleReceipt();
        void UpdateSaleReceipt();
        void GetSaleReceipt();
        void GetAllSaleReceipts();

        void AddSaleReceiptPosition();
        void DeleteSaleReceiptPosition();
        void UpdateSaleReceiptPosition();
        void GetSaleReceiptPosition();
        void GetAllSaleReceiptPositions();

        void AddCost();
        void DeleteCost();
        void UpdateCost();
        void GetCost();
        void GetAllCosts();

        void AddCostStory();
        void DeleteCostStory();
        void UpdateCostStory();
        void GetCostStory();
        void GetAllCostStories();
        void Show(string message);
    }
}
