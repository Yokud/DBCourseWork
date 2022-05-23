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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        internal delegate void ValuePassDelegate(Shop selectedShop);
        internal event ValuePassDelegate ValuePassEvent;

        public MainWindow()
        {
            InitializeComponent();

            SpsrLtDbContext dbContext = new SpsrLtDbContext();

            ShopsContentControl.Content = new ShopsView();
            ShopsContentControl.DataContext = new ShopsViewModel(dbContext, new Logger<ShopsViewModel>(new LoggerFactory()));

            ProductsContentControl.Content = new ProductsView();
            ProductsContentControl.DataContext = new ProductsViewModel(dbContext);

            SaleReceiptsContentControl.Content = new SaleReceiptsView();
            SaleReceiptsContentControl.DataContext = new SaleReceiptsViewModel();

            CostStoriesContentControl.Content = new CostStoryView();
            CostStoriesContentControl.DataContext = new CostStoryViewModel();

            ValuePassEvent += new ValuePassDelegate(SetSelectedShop);
            ((ShopsViewModel)ShopsContentControl.DataContext).del = ValuePassEvent;
        }

        internal void SetSelectedShop(Shop selectedShop)
        {
            ((ProductsViewModel)ProductsContentControl.DataContext).SelectedShop = selectedShop;
        }
    }
}
