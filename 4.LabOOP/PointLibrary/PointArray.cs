using static System.Math;
namespace PointLibrary
{
    //[КЛАСС КОЛЛЕКЦИЯ]
    public class PointArray<T>
    {
        //Делегат для события
        public delegate void PointArrayEventHandler(PointArray<T> sender, PointArrayEventArgs e);
        public event PointArrayEventHandler notify = (PointArray<T> sender, PointArrayEventArgs e) =>
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
        public Point<double>[] array;
        public uint length;
        //Границы рандома
        private static int min = -50;
        private static int max = 50;
        private static Random rnd = new Random();
        //Конструкторы Деструктор
        public PointArray()
        {
            notify(this, new PointArrayEventArgs($"Creating empty array", length));
            length = 0;
            array = new Point<double>[length];
            ++PointStatic<T>.pointArrayCounter;
        }

        public PointArray(T len, bool isManual)
        {
            notify(this, new PointArrayEventArgs($"Creating array [Len: {length}]", (uint)length));
            array = CreateArray(len, isManual);
            this.length = (uint)array.Length;
            ++PointStatic<T>.pointArrayCounter;
        }
        //Создание массива и его возврат
        public Point<double>[] CreateArray(T len, bool isManual) 
        {
            if (len is double || len is float || len is int)
            {
                int length = Convert.ToInt32(len);
                if (length >= 0 && Convert.ToDouble(len) >= 0 && (Convert.ToDouble(len) - length == 0))
                {
                    this.length = (uint)length;
                    array = new Point<double>[length];
                    switch (isManual)
                    {
                        //Ручной ввод
                        case true:
                            {
                                notify(this, new PointArrayEventArgs($"Manual fill:", this.length));
                                dynamic coordX, coordY;
                                for (int i = 0; i < length; ++i)
                                {
                                    Console.Write("X:");
                                    coordX = InputDouble(i);
                                    Console.Write("Y:");
                                    coordY = InputDouble(i);
                                    array[i] = new Point<double>(coordX, coordY);
                                }
                                ++PointStatic<T>.pointArrayCounter;
                                return array;
                            }
                        //Случайный ввод
                        case false:
                            {
                                dynamic coordX, coordY;
                                for (int i = 0; i < length; ++i)
                                {
                                    notify(this, new PointArrayEventArgs($"Random fill:", this.length));
                                    coordX = rnd.NextDouble() * (max - min) + min;
                                    coordY = rnd.NextDouble() * (max - min) + min;
                                    array[i] = new Point<double>(coordX, coordY);
                                }
                                ++PointStatic<T>.pointArrayCounter;
                                return array;
                            }
                    }
                }
                else
                {
                    throw new Exception("Length is under zero or value is not integer!");
                }
            }
            else
            {
                throw new Exception("Wrong type of data!");
            }
        }
        ~PointArray()
        {
            notify(this, new PointArrayEventArgs($"Deleting array [Len: {length}]", length));

            --PointStatic<T>.pointArrayCounter;
        }
        //Индексатор
        public Point<double> this[int index]
        {
            get
            {
                if (index >= 0 && index < array.Length)
                    return array[index];
                else
                    throw new IndexOutOfRangeException("Index is out of range");
            }
            set
            {
                if (index >= 0 && index < array.Length)
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
