using DataBaseUI.SysEntities;

namespace TrendLineLib
{
    public class Point
    {
        public Point(double x, double y, double p = 1)
        {
            X = x;
            Y = y;
            P = p;
        }

        public double X { get; set; }
        public double Y { get; set; }
        public double P { get; set; }
    }

    public static class CostStoryPoints
    {
        public static IEnumerable<Point> FromCostStory(IEnumerable<CostStory> cs)
        {
            List<int> costs = cs.ToList().Select(x => x.Cost).ToList();
            List<DateOnly> dates = ((List<CostStory>)cs).Select(x => new DateOnly(x.Year, x.Month, 1)).ToList();            

            List<Point> points = new List<Point>();

            for (int i = 0; i < Math.Max(costs.Count, dates.Count); i++)
            {
                points.Add(new Point(i, costs[i]));
            }

            return points;
        }
    }
}