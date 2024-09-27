using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Math;

namespace Lab1_3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            double a = 1000;
            double b = 0.0001;
            //Получаем double результаты
            double ansDouble = (Pow(a - b, 4) - (Pow(a, 4) + 6 * Pow(a, 2) * Pow(b, 2) + Pow(b, 4))) /
                (-4 * a * Pow(b,3) - 4 * Pow(a,3) * b);
            Console.WriteLine($"Double answer: {ansDouble}");

            //Получаем float результаты
            float ansFloat = (((float)Pow(a - b, 4) - (float)(Pow(a, 4) + (float)6 * (float)Pow(a, 2) * (float)Pow(b, 2) + (float)Pow(b, 4))) /
                (-4 * (float)a * (float)Pow(b, 3) - 4 * (float)Pow(a, 3) * (float)b));
            Console.WriteLine($"Float answer: {ansFloat}");
        }
    }
}
