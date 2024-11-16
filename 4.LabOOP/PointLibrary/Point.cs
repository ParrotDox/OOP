using static System.Math;
namespace PointLibrary
{
    //[ОБЪЕКТ КОЛЛЕКЦИИ]
    public class Point<T>
    {
        //Делегат для события
        public delegate void PointEventHandler(Point<T> sender, PointEventArgs e);
        public PointEventHandler notify = (Point<T> sender, PointEventArgs e) =>
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
        private double x;
        public double X
        {
            get { return x; }
            set
            {
                x = value;
            }
        }
        private double y;
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
            ++PointStatic<T>.pointCounter;
        }
        public Point(T x, T y)
        {
            if((x is double || x is int || x is float) && (y is double || y is int || y is float)) 
            {
                X = Convert.ToDouble(x);
                Y = Convert.ToDouble(y);
                notify(this, new PointEventArgs($"Creating point->cord.:[{X}:{Y}]", X, Y));
                ++PointStatic<T>.pointCounter;
            }
            else 
            {
                throw new Exception("Wrong type of data!");
            }
        }
        ~Point() 
        {
            notify(this, new PointEventArgs($"Deleting point->cord.:[{x}:{y}]", x, y));
            --PointStatic<T>.pointCounter;
        }
        //Методы
        //Вывод информации о точке
        public void Print()
        {
            notify(this, new PointEventArgs($"Point->cord.:[{x}:{y}]", x, y));
        }
        //Вычисление дистанции между точками
        public double FindDistance(Point<T> point1)
        {
            double result = Sqrt(Pow(this.X - point1.X, 2) + Pow(this.X - point1.Y, 2));
            return result;
        }
        //Приведение
        public static explicit operator int(Point<T> point)
        {
            return (int)point.X;
        }
        public static implicit operator double(Point<T> point)
        {
            return point.Y;
        }
        //Бинарные операции
        public static double operator +(Point<T> point1, Point<T> point2)
        {
            return Sqrt(Pow(point1.X - point2.X, 2) + Pow(point1.Y - point2.Y, 2));
        }
        public static Point<T> operator +(int val, Point<T> point)
        {
            dynamic newX = point.X + val;
            dynamic oldY = point.Y;
            return new Point<T>(newX, oldY);
        }
        public static Point<T> operator +(Point<T> point, int val)
        {
            dynamic newX = point.X + val;
            dynamic oldY = point.Y;
            return new Point<T>(newX, oldY);
        }
        //Унарные операции
        public static Point<T> operator ++(Point<T> point)
        {
            dynamic newX = point.X + 1;
            dynamic oldY = point.Y;
            return new Point<T>(newX, oldY);
        }
        public static Point<T> operator --(Point<T> point)
        {
            dynamic newX = point.X - 1;
            dynamic oldY = point.Y;
            return new Point<T>(newX, oldY);
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
