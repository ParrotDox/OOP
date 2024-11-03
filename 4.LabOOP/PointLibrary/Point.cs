using static System.Math;
namespace PointLibrary
{
    //[ОБЪЕКТ КОЛЛЕКЦИИ]
    public class Point
    {
        //Делегат для события
        public delegate void PointEventHandler(Point sender, PointEventArgs e);
        public PointEventHandler notify = (Point sender, PointEventArgs e) =>
        {
            Console.WriteLine(e.msg);
        };
        //Посредник для события
        public event PointEventHandler Notify
        {
            add
            {
                notify += value;
                Console.WriteLine($"{value.Method.Name} added");
            }
            remove
            {
                notify -= value;
                Console.WriteLine($"{value.Method.Name} deleted");
            }
        }
        //Объявление и инициализация полей через посредника
        private double x { get; set; }
        public double X
        {
            get { return x; }
            set
            {
                x = value;
            }
        }
        private double y { get; set; }
        public double Y
        {
            get { return y; }
            set { y = value; }
        }
        //Конструкторы Деструктор
        public Point()
        {
            X = 0;
            Y = 0;
            notify(this, new PointEventArgs($"Creating empty point->cord.:[{x}:{y}]", x, y));
            ++PointStatic.pointCounter;
        }
        public Point(double x, double y)
        {
            X = x;
            Y = y;
            notify(this, new PointEventArgs($"Creating point->cord.:[{x}:{y}]", x, y));
            ++PointStatic.pointCounter;
        }
        ~Point() 
        {
            notify(this, new PointEventArgs($"Deleting point->cord.:[{x}:{y}]", x, y));
            --PointStatic.pointCounter;
        }
        //Методы
        //Вывод информации о точке
        public void Print()
        {
            notify(this, new PointEventArgs($"Point->cord.:[{x}:{y}]", x, y));
        }
        //Вычисление дистанции между точками
        public double FindDistance(Point point1)
        {
            double result = Sqrt(Pow(this.X - point1.X, 2) + Pow(this.X - point1.Y, 2));
            return result;
        }
        //Приведение
        public static explicit operator int(Point point)
        {
            return (int)point.X;
        }
        public static implicit operator double(Point point)
        {
            return point.Y;
        }
        //Бинарные операции
        public static double operator +(Point point1, Point point2)
        {
            return Sqrt(Pow(point1.X - point2.X, 2) + Pow(point1.Y - point2.Y, 2));
        }
        public static Point operator +(int val, Point point)
        {
            return new Point(point.X + val, point.Y);
        }
        public static Point operator +(Point point, int val)
        {
            return new Point(point.X + val, point.Y);
        }
        //Унарные операции
        public static Point operator ++(Point point)
        {
            return new Point(point.X + 1, point.Y);
        }
        public static Point operator --(Point point)
        {
            return new Point(point.X - 1, point.Y);
        }
    }
    //Класс сообщения при вызове событий в классе Point
    public class PointEventArgs
    {
        public string msg;
        public double x, y;
        public PointEventArgs(string msg, double x, double y)
        {
            this.msg = msg;
            this.x = x;
            this.y = y;
        }
    }
}
