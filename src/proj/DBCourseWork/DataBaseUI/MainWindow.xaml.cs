using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using DataBaseUI.DB;
using DataBaseUI.Views;
using DataBaseUI.ViewModels;
using DataBaseUI.SysEntities;
using Microsoft.Extensions.Logging;

namespace DataBaseUI
{
    enum Users
    {
        User,
        Analyst,
        Admin
    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        internal delegate void ShopValuePassDelegate(Shop selectedShop);
        internal event ShopValuePassDelegate ShopValuePassEvent;

        internal delegate void ProductValuePassDelegate(Product product);
        internal event ProductValuePassDelegate ProductValuePassEvent;

        public MainWindow()
        {
            InitializeComponent();

            SpsrLtDbContext dbContext = new SpsrLtDbContext();

            ShopsContentControl.Content = new ShopsView();
            ShopsContentControl.DataContext = new ShopsViewModel(dbContext, new Logger<ShopsViewModel>(new LoggerFactory()));

            ProductsContentControl.Content = new ProductsView();
            ProductsContentControl.DataContext = new ProductsViewModel(dbContext);

            SaleReceiptsContentControl.Content = new SaleReceiptsView();
            SaleReceiptsContentControl.DataContext = new SaleReceiptsViewModel(dbContext);

            CostStoriesContentControl.Content = new CostStoryView();
            CostStoriesContentControl.DataContext = new CostStoryViewModel(dbContext);

            ShopValuePassEvent += new ShopValuePassDelegate(SetSelectedShop);
            ((ShopsViewModel)ShopsContentControl.DataContext).del = ShopValuePassEvent;

            ProductValuePassEvent += new ProductValuePassDelegate(SetSelectedProduct);
            ((ProductsViewModel)ProductsContentControl.DataContext).del = ProductValuePassEvent;
        }

        internal void SetSelectedShop(Shop selectedShop)
        {
            ((ProductsViewModel)ProductsContentControl.DataContext).SelectedShop = selectedShop;
            ((SaleReceiptsViewModel)SaleReceiptsContentControl.DataContext).SelectedShop = selectedShop;

            ((CostStoryViewModel)CostStoriesContentControl.DataContext).SelectedShop = selectedShop;
        }

        internal void SetSelectedProduct(Product product)
        {
            ((CostStoryViewModel)CostStoriesContentControl.DataContext).SelectedProduct = product;
        }
    }
}
