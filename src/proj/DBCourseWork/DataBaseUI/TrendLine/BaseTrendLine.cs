using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseUI.TrendLine
{
    internal abstract class BaseTrendLine
    {
        double[] coefs = null!;

        protected double[] GetCoefs(List<Point> points)
        {
            throw new NotImplementedException();
        }

        public abstract double[] GetLinesPoints();
    }
}
