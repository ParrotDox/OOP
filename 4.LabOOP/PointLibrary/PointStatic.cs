using static System.Math;
namespace PointLibrary
{
    public static class PointStatic
    {
        public static uint pointCounter = 0;
        public static uint pointArrayCounter = 0;
        public static double FindDistance(Point point1, Point point2)
        {
            double result = Sqrt(Pow(point1.X - point2.X, 2) + Pow(point1.Y - point2.Y, 2));
            return result;
        }
    }
}
