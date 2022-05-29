using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using DataBaseUI.DB;
using DataBaseUI.Models;
using DataBaseUI.SysEntities;
using DataBaseUI.Views.DialogWindows.SaleReceiptsView;

namespace DataBaseUI.ViewModels
{
    internal class SaleReceiptsViewModel : INotifyPropertyChanged
    {
        ISaleReceiptsRepository saleReceipts;
        ISaleReceiptPositionsRepository saleReceiptPositions;
        IAvailabilityRepository availavilities;
        Shop selectedShop;
        SaleReceipt selectedSaleReceipt;
        Product selectedSaleReceiptPosition;

        public SaleReceiptsViewModel(SpsrLtDbContext dbContext)
        {
            saleReceipts = new PgSQLSaleReceiptsRepository(dbContext);
            saleReceiptPositions = new PgSQLSaleReceiptPositionsRepository(dbContext);
            availavilities = new PgSQLAvailabilityRepository(dbContext);
        }

        public Shop SelectedShop
        {
            get { return selectedShop; }
            set 
            { 
                selectedShop = value; 
                OnPropertyChanged("SelectedShop");
                OnPropertyChanged("SaleReceipts");
            }
        }

        public SaleReceipt SelectedSaleReceipt
        {
            get { return selectedSaleReceipt; }
            set 
            { 
                selectedSaleReceipt = value; 
                OnPropertyChanged("SelectedSaleReceipt");
                OnPropertyChanged("SaleReceiptPositions");
            }
        }

        public Product SelectedSaleReceiptPosition
        {
            get { return selectedSaleReceiptPosition; }
            set { selectedSaleReceiptPosition = value; OnPropertyChanged("SelectedSaleReceiptPosition"); }
        }

        public IEnumerable<SaleReceipt> SaleReceipts
        {
            get
            {
                if (selectedShop == null)
                    return null;

                var saleReceiptsInShop = saleReceipts.GetAllFromShop(selectedShop);

                foreach (var sr in saleReceiptsInShop)
                {
                    var positions = saleReceiptPositions.GetAllFromSaleReceipt(sr);
                    sr.SummaryCost = positions.Sum(x => x.Cost);
                }

                return saleReceiptsInShop;
            }
        }

        public IEnumerable <Product> SaleReceiptPositions
        {
            get
            {
                return selectedSaleReceipt == null ? null : saleReceiptPositions.GetAllFromSaleReceipt(selectedSaleReceipt);
            }
        }

        RelayCommand addSRCommand;
        public RelayCommand AddSRCommand
        {
            get
            {
                return addSRCommand ??= new RelayCommand(obj => 
                {
                    AddSaleReceiptWindow wnd = new AddSaleReceiptWindow();

                    if (wnd.ShowDialog() == true)
                    {
                        SaleReceipt sr = wnd.NewSaleReceipt;

                        if (sr != null)
                        {
                            sr.ShopId = selectedShop.Id;
                            AddSaleReceipt(sr);
                        }    
                    }
                });
            }
        }

        RelayCommand addSRPCommand;
        public RelayCommand AddSRPCommand
        {
            get
            {
                return addSRPCommand ??= new RelayCommand(obj =>
                {
                    AddSaleReceiptPositionWindow wnd = new AddSaleReceiptPositionWindow(new PgSQLProductsRepository().GetAllFromShop(SelectedShop));

                    if (wnd.ShowDialog() == true)
                    {
                        Product prod = wnd.SelectedProduct;

                        if (prod != null)
                        {
                            int id = availavilities.GetAll().Where(x => x.ProductId == prod.Id && x.ShopId == selectedShop.Id).First().Id;
                            AddSaleReceiptPosition(new SaleReceiptPosition(id, selectedSaleReceipt.Id));
                        }
                    }
                });
            }
        }

        RelayCommand deleteSRCommand;
        public RelayCommand DeleteSRCommand
        {
            get
            {
                return deleteSRCommand ??= new RelayCommand(obj =>
                {
                    SaleReceipt sr = obj as SaleReceipt;

                    if (sr != null)
                        DeleteSaleReceipt(sr);
                }, obj => saleReceipts.GetAll().Count() > 0);
            }
        }

        RelayCommand deleteSRPCommand;
        public RelayCommand DeleteSRPCommand
        {
            get
            {
                return deleteSRPCommand ??= new RelayCommand(obj =>
                {
                    Product prod = obj as Product;

                    if (prod != null)
                    {
                        int id = availavilities.GetAll().Where(x => x.ProductId == prod.Id && x.ShopId == selectedShop.Id).First().Id;
                        DeleteSaleReceiptPosition(saleReceiptPositions.GetAll().Where(x => x.AvailabilityId == id).First());
                    }
                }, obj => saleReceiptPositions.GetAll().Count() > 0);
            }
        }

        public void AddSaleReceipt(SaleReceipt receipt)
        {
            try
            {
                saleReceipts.Create(receipt);
                OnPropertyChanged("SaleReceipts");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public void AddSaleReceiptPosition(SaleReceiptPosition pos)
        {
            try
            {
                saleReceiptPositions.Create(pos);
                OnPropertyChanged("SaleReceiptPositions");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public void DeleteSaleReceipt(SaleReceipt receipt)
        {
            try
            {
                saleReceipts.Delete(receipt);
                OnPropertyChanged("SaleReceipts");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public void DeleteSaleReceiptPosition(SaleReceiptPosition pos)
        {
            try
            {
                saleReceiptPositions.Delete(pos);
                OnPropertyChanged("SaleReceiptPositions");
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
