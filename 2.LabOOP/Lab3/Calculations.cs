using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculations
{
    static public class Application 
    {
        public static void Execute() 
        {
            //Метод перебирает значения x и вызывает вспомогательные методы для получения результатов
            for(double x = 0.1; x <= 0.81; x += 0.07) 
            {
                double y = 0.5 - (Math.PI / 4 * Math.Abs(Math.Sin(x)));
                double sEps = CalcTaylorSinEps(x,0.0001);
                double sN = CalcTaylorSinN(x,50);
                PrintResults(x, sEps, sN, y);
            }    
        }
        public static double CalcTaylorSinEps(double x, double eps) 
        {
            //Вычисление значения ряда Тейлора для заданной точности
            double resultPrev = Math.Cos(2 * 1 * x) / (4 * Power(1, 2) - 1);
            double result = resultPrev + Math.Cos(2 * 2 * x) / (4 * Power(2, 2) - 1);
            for(int i = 3; Math.Abs(result - resultPrev) > eps; ++i) 
            {
                double term = Math.Cos(2 * i * x) / (4 * Power(i, 2) - 1);
                resultPrev = result;
                result += term;
            }
            return result;
        }
        public static double CalcTaylorSinN(double x, int n)
        {
            //Вычисление значения ряда Тейлора для n элементов
            double result = 0;
            for (int i = 1; i <= n; i++)
            {
                double term = Math.Cos(2 * i * x) / (4 * Power(i, 2) - 1);
                result += term;
            }
            return result;
        }
        public static double Power(double n, int p) 
        {
            //Степень через рекурсию
            if (p == 0)
                return 1;
            else
                return n * Power(n, p-1);
        }
        public static void PrintResults(double x, double sEps, double sN, double y) 
        {
            //Вывод результатов в консоль
            Console.WriteLine($"X = {x} SE = {Math.Round(sEps, 4)} SN = {Math.Round(sN, 4)} Y = {Math.Round(y, 4)}");
            return;
        }
    }
}
