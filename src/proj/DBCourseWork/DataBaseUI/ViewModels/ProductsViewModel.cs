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

        public ProductsViewModel()
        {
            products = new PgSQLProductsRepository();
            costs = new PgSQLCostsRepository();
            availabilities = new PgSQLAvailabilityRepository();
        }

        public Shop SelectedShop
        {
            get { return selectedShop; }
            set { selectedShop = value; }
        }

        public IEnumerable<Product> Products
        {
            get
            {
                return products.GetAllFromShop(SelectedShop);
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
