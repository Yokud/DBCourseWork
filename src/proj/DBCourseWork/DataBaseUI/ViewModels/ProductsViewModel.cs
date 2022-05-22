using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using DataBaseUI.DB;
using DataBaseUI.Models;
using DataBaseUI.SysEntities;

namespace DataBaseUI.ViewModels
{
    internal class ProductsViewModel : INotifyPropertyChanged
    {
        IProductsRepository products;
        ICostsRepository costs;
        IAvailabilityRepository availabilities;
        Shop selectedShop;

        public ProductsViewModel(SpsrLtDbContext spsr)
        {
            products = new PgSQLProductsRepository(spsr);
            costs = new PgSQLCostsRepository(spsr);
            availabilities = new PgSQLAvailabilityRepository(spsr);
        }

        public Shop SelectedShop
        {
            get { return selectedShop; }
            set 
            { 
                selectedShop = value;
                OnPropertyChanged("SelectedShop");
                OnPropertyChanged("Products");
            }
        }

        public IEnumerable<Product> Products
        {
            get
            {
                if (SelectedShop == null)
                    return null;

                var shopsProducts = products.GetAllFromShop(SelectedShop);
                //var shopsAvails = availabilities.GetAll().Where(x => x.ShopId == SelectedShop.Id);
                //var costs1 = costs.GetAll();

                //foreach (Product product in shopsProducts)
                //    product.Cost = costs1.Where(y => y.AvailabilityId == shopsAvails.Where(x => x.ProductId == product.Id).First().Id).First().CostValue;

                return shopsProducts;
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
