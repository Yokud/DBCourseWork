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


namespace DataBaseUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            ShopsContentControl.Content = new ShopsView();
            ShopsContentControl.DataContext = new ShopsViewModel();

            ProductsContentControl.Content = new ProductsView();
            ProductsContentControl.DataContext = new ProductsViewModel();

            SaleReceiptsContentControl.Content = new SaleReceiptsView();
            SaleReceiptsContentControl.DataContext = new SaleReceiptsViewModel();

            CostStoriesContentControl.Content = new CostStoryView();
            CostStoriesContentControl.DataContext = new CostStoryViewModel();
        }
    }
}
