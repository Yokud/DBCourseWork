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
using Microsoft.Extensions.Logging;

namespace DataBaseUI.ViewModels
{
    internal class ShopsViewModel : INotifyPropertyChanged
    {
        IShopsRepository shopsRepository;
        Shop selectedShop;
        internal Delegate del;
        ILogger<ShopsViewModel> logger;

        public ShopsViewModel(SpsrLtDbContext spsr, ILogger<ShopsViewModel> logger)
        {
            shopsRepository = new PgSQLShopsRepository(spsr);
            this.logger = logger;
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
                logger.LogInformation("Selected shop was updated.\n");
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
                logger.LogInformation(String.Format("Shop with id = {0} was added.\n", shop.Id));
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                logger.LogError(e.Message);
            }
        }

        public void DeleteShop(Shop shop)
        {
            try
            {
                shopsRepository.Delete(shop);
                logger.LogInformation(String.Format("Shop with id = {0} was deleted.\n", shop.Id));
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                logger.LogError(e.Message);
            }
        }

        public void UpdateShop(Shop shop)
        {
            try
            {
                shopsRepository.Update(shop);
                logger.LogInformation(String.Format("Shop with id = {0} was updated.\n", shop.Id));
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                logger.LogError(e.Message);
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
