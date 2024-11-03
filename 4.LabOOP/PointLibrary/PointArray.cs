using static System.Math;
namespace PointLibrary
{
    //[КЛАСС КОЛЛЕКЦИЯ]
    public class PointArray
    {
        //Делегат для события
        public delegate void PointArrayEventHandler(PointArray sender, PointArrayEventArgs e);
        public event PointArrayEventHandler notify = (PointArray sender, PointArrayEventArgs e) =>
        {
            Console.WriteLine(e.msg);
        };
        //Посредник для события
        public event PointArrayEventHandler Notify
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
        //Одномерный массив точек
        private Point[] array;
        private uint length;
        //Границы рандома
        private static int min = -50;
        private static int max = -50;
        private static Random rnd = new Random();
        //Конструкторы Деструктор
        public PointArray()
        {
            notify(this, new PointArrayEventArgs($"Creating empty array", length));
            length = 0;
            array = new Point[length];
            ++PointStatic.pointArrayCounter;
        }

        public PointArray(uint length, bool isManual)
        {
            notify(this, new PointArrayEventArgs($"Creating array [Len: {length}]", length));
            this.length = length;
            array = new Point[length];
            ++PointStatic.pointArrayCounter;
            if (length > 0)
            {
                switch (isManual)
                {
                    //Ручной ввод
                    case true:
                        {
                            notify(this, new PointArrayEventArgs($"Manual fill:", this.length));
                            double coordX, coordY;
                            for (int i = 0; i < length; ++i)
                            {
                                Console.WriteLine("X:");
                                coordX = InputDouble(i);
                                Console.WriteLine("Y:");
                                coordY = InputDouble(i);
                                array[i] = new Point(coordX, coordY);
                            }
                            break;
                        }
                    //Случайный ввод
                    case false:
                        {
                            double coordX, coordY;
                            for (int i = 0; i < length; ++i)
                            {
                                notify(this, new PointArrayEventArgs($"Random fill:", this.length));
                                coordX = rnd.NextDouble() * (max - min) + min;
                                coordY = rnd.NextDouble() * (max - min) + min;
                                array[i] = new Point(coordX,coordY);
                            }
                            break;
                        }
                }
            }
        }
        ~PointArray()
        {
            notify(this, new PointArrayEventArgs($"Deleting array [Len: {length}]", length));
            --PointStatic.pointArrayCounter;
        }
        //Индексатор
        public Point this[int index]
        {
            get
            {
                if (index > 0 && index < array.Length)
                    return array[index];
                else
                    throw new IndexOutOfRangeException("Index is out of range");
            }
            set
            {
                if (index > 0 && index < array.Length)
                    array[index] = value;
                else
                    throw new ArgumentOutOfRangeException("Index is out of range");
            }
        }
        //Проверка и возврат double
        private double InputDouble(int position)
        {
            double input;
            bool isDouble = false;
            do
            {
                notify(this, new PointArrayEventArgs($"Input double [{position}]:", length));
                isDouble = double.TryParse(Console.ReadLine(), out input);
                if (!isDouble)
                {
                    //notify(this, new PointArrayEventArgs($"Unexpected type of data!", length));
                    throw new Exception("Wrong type of data");
                }
            } while (!isDouble);
            return input;
        }
        //Вывод полей объекта текущего класса
        public void Print()
        {
            if(length == 0) 
            {
                notify(this, new PointArrayEventArgs($"PRINTING POINT_ARRAY", length));
                notify(this, new PointArrayEventArgs($"Array is empty!", length));
            }
            else 
            {
                notify(this, new PointArrayEventArgs($"PRINTING POINT_ARRAY", length));
                notify(this, new PointArrayEventArgs($"Length: {length}", length));
                for (int i = 0; i < length; ++i)
                {
                    notify(this, new PointArrayEventArgs($"Point[{i}]: X={array[i].X} Y={array[i].Y}", length));
                }
            }
        }
    }
    public class PointArrayEventArgs
    {
        public string msg;
        public uint length;
        public PointArrayEventArgs(string msg, uint length)
        {
            this.msg = msg;
            this.length = length;
        }
    }
}
