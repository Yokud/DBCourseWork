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
using TrendLineLib;
using OxyPlot;

namespace DataBaseUI.ViewModels
{
    internal class CostStoryViewModel : INotifyPropertyChanged
    {
        ICostStoryRepository costStories;
        Shop selectedShop;
        Product selectedProduct;
        CostStory selectedCostStory;

        BaseTrendLine trend;
        PlotModel trendLinePlot;
        int nextCostValue;
        string polString;

        public CostStoryViewModel(SpsrLtDbContext dbContext)
        {
            costStories = new PgSQLCostStoryRepository(dbContext);
            trend = new PolynomialTrendLine();
        }

        public IEnumerable<CostStory> ProductCostStory
        {
            get
            {
                return selectedShop != null && selectedProduct != null ? costStories.GetFullCostStory(selectedShop, selectedProduct) : null;
            }
        }

        public Shop SelectedShop
        {
            get { return selectedShop; }
            set
            {
                selectedShop = value;
                OnPropertyChanged("SelectedShop");
            }
        }

        public Product SelectedProduct
        {
            get { return selectedProduct; }
            set
            {
                selectedProduct = value;
                OnPropertyChanged("SelectedProduct");
                OnPropertyChanged("ProductCostStory");
                OnPropertyChanged("TrendLinePlot");
            }
        }

        public CostStory SelectedCostStory
        {
            get => selectedCostStory;
            set
            {
                selectedCostStory = value;
                OnPropertyChanged("SelectedCostStory");
            }
        }

        public PlotModel TrendLinePlot
        {
            get
            {
                if (ProductCostStory == null)
                    return null;

                trendLinePlot = new PlotModel();

                List<double> coefs = trend.GetCoefs(ProductCostStory);
                List<Point> points = trend.GetLinePoints(ProductCostStory);

                NextCostValue = (int)trend.F(ProductCostStory.Count() + 1);

                StringBuilder builder = new StringBuilder("y =");

                for (int i = coefs.Count - 1; i > -1; i--)
                {
                    char sign = coefs[i] >= 0 ? '+' : ' ';

                    builder.Append(string.Format(" {0}{1:f3}x^{2}", sign, coefs[i], i));
                }

                PolynomeString = builder.ToString();

                trendLinePlot.Title = "Тренд цены";

                var line = new OxyPlot.Series.LineSeries();
                var costPoints = new OxyPlot.Series.ScatterSeries();

                foreach (var p in points)
                    line.Points.Add(new DataPoint(p.X, p.Y));

                trendLinePlot.Series.Add(line);

                foreach (var p in CostStoryPoints.FromCostStory(ProductCostStory))
                    costPoints.Points.Add(new OxyPlot.Series.ScatterPoint(p.X, p.Y));

                trendLinePlot.Series.Add(costPoints);

                return trendLinePlot;
            }
        }

        public int NextCostValue
        {
            get
            { 
                return nextCostValue; 
            }
            set
            {
                nextCostValue = value;
                OnPropertyChanged("NextCostValue");
            }
        }

        public string PolynomeString
        {
            get
            {
                return polString;
            }
            set 
            { 
                polString = value; 
                OnPropertyChanged("PolynomeString"); 
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }

    internal static class CostStoryPoints
    {
        public static IEnumerable<Point> FromCostStory(IEnumerable<CostStory> cs)
        {
            List<int> costs = cs.ToList().Select(x => x.Cost).ToList();
            List<DateOnly> dates = cs.Select(x => new DateOnly(x.Year, x.Month, 1)).ToList<DateOnly>();

            List<Point> points = new List<Point>();

            for (int i = 0; i < Math.Max(costs.Count, dates.Count); i++)
            {
                points.Add(new Point(i + 1, costs[i]));
            }

            return points;
        }

        public static List<double> GetCoefs(this BaseTrendLine line, IEnumerable<CostStory> costStories)
        {
            var points = FromCostStory(costStories);

            return line.GetCoefs(points.ToList());
        }

        public static List<Point> GetLinePoints(this BaseTrendLine line, IEnumerable<CostStory> costStories)
        {
            var points = FromCostStory(costStories);

            return line.GetLinePoints(points);
        }
    }
}
