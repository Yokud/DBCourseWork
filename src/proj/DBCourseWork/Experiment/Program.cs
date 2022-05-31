using System;
using TrendLineLib;

namespace Experiment
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<int> costs = new List<int>() { 16883, 17283, 15860, 17194, 20262, 16655, 16983, 13709, 19365, 17504, 18579, 14264, 14831, 19490, 16791, 22657, 21632, 18796 };
            List<Point> points = new List<Point>();

            for (int i = 0; i < costs.Count(); i++)
                points.Add(new Point(i + 1, costs[i]));

            PolynomialTrendLine line = new PolynomialTrendLine();

            for (int i = 0; i < costs.Count; i++)
            {
                line.GetCoefs(points.GetRange(0, i + 1));
                int val = (int)line.F(i + 1);
                int real_val = (int)points[Math.Min(i + 1, points.Count() - 1)].Y;
                Console.WriteLine($"{i + 1} {val} {real_val} {Math.Sqrt(Math.Pow(real_val - val, 2) / (real_val * real_val)) * 100}");
            }
        }
    }
}