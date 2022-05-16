using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using DataBaseUI.Models;

namespace DataBaseUI.ViewModels
{
    internal class ShopsViewModel : INotifyPropertyChanged
    {
        IShopsRepository shopsRepository;

        public ShopsViewModel()
        {
            shopsRepository = new PgSQLShopsRepository();
        }

        public IEnumerable<Shop> Shops
        {
            get
            {
                return shopsRepository.GetAll();
            }
        }

        public void AddShop(Shop shop)
        {
            shopsRepository.Create(shop);
        }

        public void DeleteShop(Shop shop)
        {
            shopsRepository.Delete(shop);
        }

        public void UpdateShop(Shop shop)
        {
            shopsRepository.Update(shop);
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
