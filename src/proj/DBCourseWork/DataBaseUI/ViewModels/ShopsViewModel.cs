using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Threading.Tasks;
using DataBaseUI.Models;
using DataBaseUI.SysEntities;
using DataBaseUI.DB;

namespace DataBaseUI.ViewModels
{
    internal class ShopsViewModel : INotifyPropertyChanged
    {
        IShopsRepository shopsRepository;
        Shop selectedShop;
        internal Delegate del;

        public ShopsViewModel(SpsrLtDbContext spsr)
        {
            shopsRepository = new PgSQLShopsRepository(spsr);
        }

        public IEnumerable<Shop> Shops
        {
            get
            {
                return shopsRepository.GetAll();
            }
        }

        public Shop SelectedShop
        {
            get { return selectedShop; }
            set
            {
                selectedShop = value;
                SetSelectedShop(value);
                OnPropertyChanged("SelectedShop");
            }
        }

        internal void SetSelectedShop(Shop selectedShop)
        {
            del.DynamicInvoke(selectedShop);
        }

        public void AddShop(Shop shop)
        {
            try
            {
                shopsRepository.Create(shop);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public void DeleteShop(Shop shop)
        {
            try
            {
                shopsRepository.Delete(shop);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public void UpdateShop(Shop shop)
        {
            try
            {
                shopsRepository.Update(shop);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
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
