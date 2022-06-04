using System;
using TrendLineLib;

namespace Experiment
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<int> costs = new List<int>() { 69990,
69990,
69990,
64990,
52990,
59990,
64990,
62990,
67990,
59990,
69990,
61990,
61990,
61990,
61990,
61990,
61990,
61990
                                                 };
            List<Point> points = new List<Point>();

            for (int i = 0; i < costs.Count(); i++)
                points.Add(new Point(i + 1, costs[i]));

            PolynomialTrendLine line = new PolynomialTrendLine();

            for (int i = 0; i < costs.Count; i++)
            {
                line.GetCoefs(points.GetRange(0, i + 1));
                int val = (int)line.F(i + 1);
                int real_val = (int)points[Math.Min(i + 1, points.Count() - 1)].Y;
                Console.WriteLine($"{i + 1} {val} {real_val} {Math.Abs(real_val - val) / (double)real_val * 100}");
            }
        }
    }
}