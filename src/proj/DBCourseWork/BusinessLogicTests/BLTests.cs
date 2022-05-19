namespace BusinessLogicTests
{
    public class BLTests
    {
        [Fact]
        public void TestDefault()
        {
            List<Point> data = new List<Point>();
            data.Add(new Point(0, 0));
            data.Add(new Point(1, 2));
            data.Add(new Point(2, 1));
            data.Add(new Point(3, 1.2));
            data.Add(new Point(4, 3));
            data.Add(new Point(5, 4));

            BaseTrendLine line = new PolynomialTrendLine();
            var res = line.GetCoefs(data);
            Assert.Equal(res.Select(x => Math.Round(x, 3)), new List<double>() { 0.579, 0.109, 0.111 });
        }

        [Fact]
        public void TestDirectLine()
        {
            List<Point> data = new List<Point>();
            data.Add(new Point(0, 0));
            data.Add(new Point(1, 1));
            data.Add(new Point(2, 2));
            data.Add(new Point(3, 3));
            data.Add(new Point(4, 4));
            data.Add(new Point(5, 5));

            BaseTrendLine line = new PolynomialTrendLine();
            var res = line.GetCoefs(data);
            Assert.Equal(res.Select(x => Math.Round(x, 3)), new List<double>() { 1 });
        }

        [Fact]
        public void TestMaxExtremums()
        {
            List<Point> data = new List<Point>();
            data.Add(new Point(0, 0));
            data.Add(new Point(1, -1));
            data.Add(new Point(2, 1));
            data.Add(new Point(3, -1));
            data.Add(new Point(4, 1));
            data.Add(new Point(5, -1));
            data.Add(new Point(6, 0));
            data.Add(new Point(7, -1));
            data.Add(new Point(8, 1));
            data.Add(new Point(9, -1));
            data.Add(new Point(10, 1));
            data.Add(new Point(11, -1));

            BaseTrendLine line = new PolynomialTrendLine();
            var res = line.GetCoefs(data);
            Assert.Equal(res.Select(x => Math.Round(x, 4)), new List<double>() { -0.1344, -0.5040, 0.3348, -0.0392, -0.0071, 0.0016, -0.0001 });
        }

        [Fact]
        public void TestEmptyData()
        {
            List<Point> data = new List<Point>();

            BaseTrendLine line = new PolynomialTrendLine();
            var res = line.GetCoefs(data);
            Assert.Equal(res, null);
        }

        [Fact]
        public void TestNullData()
        {
            BaseTrendLine line = new PolynomialTrendLine();
            var res = line.GetCoefs(null);
            Assert.Equal(res, null);
        }
    }
}