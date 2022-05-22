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
        Product selectedProduct;

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

        public Product SelectedProduct
        {
            get { return selectedProduct; }
            set
            {
                selectedProduct = value;
                OnPropertyChanged("SelectedProduct");
            }
        }

        public IEnumerable<Product> Products
        {
            get
            {
                return SelectedShop == null ? null : products.GetAllFromShop(SelectedShop);
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
