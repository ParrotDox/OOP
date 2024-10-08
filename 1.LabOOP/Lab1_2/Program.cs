﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1_2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            double X1 = 0.0;
            double Y1 = 0.0;
            bool X1IsDouble = false;
            bool Y1IsDouble = false;
            //Ввод и проверка на тип double переменных X1,Y1
            do
            {
                Console.WriteLine("Enter X1 (double): ");
                X1IsDouble = Double.TryParse(Console.ReadLine(), out X1);
                Console.WriteLine("Enter Y1 (double): ");
                Y1IsDouble = Double.TryParse(Console.ReadLine(), out Y1);
                if (!X1IsDouble || !Y1IsDouble) Console.WriteLine($"Types do not match\nX1: {X1IsDouble}\nY1: {Y1IsDouble}\n");

            }
            while (!X1IsDouble || !Y1IsDouble);

            //Проверка на включение точки в обл.значений
            ///comment
            bool isContained = false;
            if((X1 >= 0.0 && X1 <= 5.0 && Y1 <= -X1 + 5 && Y1 >= 0) || (X1 >= 0 && X1 <= 5 && Y1 <= 0 && Y1 >= -7)) 
            {
                Console.WriteLine($"X1 >= 0.0: {X1 >= 0.0}");
                Console.WriteLine($"X1 <= 5.0: {X1 <= 5.0}");
                Console.WriteLine($"Y1 <= -X1 + 5: {Y1 <= -X1 + 5}");
                Console.WriteLine($"Y1 >= 0: {Y1 >= 0}");
                Console.WriteLine("-------------------------------");
                Console.WriteLine($"X1 >= 0: {X1 >= 0}");
                Console.WriteLine($"X1 <= 5: {X1 <= 5}");
                Console.WriteLine($"Y1 <= 0: {Y1 <= 0}");
                Console.WriteLine($"Y1 >= -7: {Y1 >= -7}");
                isContained = true;
            }
            else 
            {
                Console.WriteLine($"X1 >= 0.0: {X1 >= 0.0}");
                Console.WriteLine($"X1 <= 5.0: {X1 <= 5.0}");
                Console.WriteLine($"Y1 <= -X1 + 5: {Y1 <= -X1 + 5}");
                Console.WriteLine($"Y1 >= 0: {Y1 >= 0}");
                Console.WriteLine("-------------------------------");
                Console.WriteLine($"X1 >= 0: {X1 >= 0}");
                Console.WriteLine($"X1 <= 5: {X1 <= 5}");
                Console.WriteLine($"Y1 <= 0: {Y1 <= 0}");
                Console.WriteLine($"Y1 >= -7: {Y1 >= -7}");
            }

            if(isContained == true) 
            {
                Console.WriteLine($"Point with coordinates X1: {X1} Y1: {Y1} is in area");
                Console.WriteLine(isContained);
            }
            else 
            {
                Console.WriteLine($"Point with coordinates X1: {X1} Y1: {Y1} is not in area");
                Console.WriteLine(isContained);
            }
        }
    }
}
