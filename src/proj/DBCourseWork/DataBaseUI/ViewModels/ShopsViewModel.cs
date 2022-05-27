﻿using System;
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

        RelayCommand addCommand;
        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ?? new RelayCommand(obj =>
                {
                    Shop shop = obj as Shop;

                    if (shop != null)
                        AddShop(shop);
                });
            }
        }

        RelayCommand deleteCommand;
        public RelayCommand DeleteCommand
        {
            get
            {
                return deleteCommand ?? new RelayCommand(obj =>
                {
                    Shop shop = obj as Shop;

                    if (shop != null)
                        DeleteShop(shop);
                },
                (obj => shopsRepository.GetAll().Count() > 0)
                );
            }
        }

        RelayCommand updateCommand;
        public RelayCommand UpdateCommand
        {
            get
            {
                return updateCommand ?? new RelayCommand(obj =>
                {
                    Shop shop = obj as Shop;

                    if (shop != null)
                        UpdateShop(shop);
                });
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
