using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1_1
{
    internal class Program
    {
        static void Main(string[] args)
        {

            int n = 0;
            int m = 0;
            bool nIsInt = false;
            bool mIsInt = false;
            //Ввод и проверка на целочисленность переменных n,m
            do
            {
                Console.WriteLine("Enter n (int):");
                nIsInt = int.TryParse(Console.ReadLine(), out n);
                Console.WriteLine("Enter m (int):");
                mIsInt = int.TryParse(Console.ReadLine(), out m);
                if (!nIsInt || !mIsInt) Console.WriteLine($"Types do not match\n n:{nIsInt} \n m:{mIsInt}\n");
            }
            while (!nIsInt || !mIsInt);

            //Вывод результатов с переменными n,m
            Console.WriteLine($"Values (n) : (m) are {n} : {m}");
            Console.WriteLine($"1)n++ *m: {n++ *m}");
            Console.WriteLine($"Values (n) : (m) are {n} : {m}");
            Console.WriteLine($"2)m-- <n: {m-- <n}");
            Console.WriteLine($"Values (n) : (m) are {n} : {m}");
            Console.WriteLine($"3)++m >n: {++m >n}");
            Console.WriteLine($"Values (n) : (m) are {n} : {m}");

            double x = 0.0;
            var xIsDouble = false;
            //Ввод и проверка на тип double переменных x
            do
            {
                Console.WriteLine("Enter x (double): ");
                xIsDouble = double.TryParse(Console.ReadLine(), out x);
                if (!xIsDouble) Console.WriteLine($"Type do not match\n x: {xIsDouble}\n");
            }
            while (!xIsDouble);

            Console.WriteLine($"4)cos(arctg(x)): {(double)Math.Cos(Math.Atan(x))}");

        }
    }
}
