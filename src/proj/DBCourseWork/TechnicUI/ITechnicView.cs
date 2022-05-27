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
        ITechnicPresenter Presenter { get; set; }

        void AddShop();
        void DeleteShop();
        void UpdateShop();
        void GetShop();
        void GetAllShops();

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
