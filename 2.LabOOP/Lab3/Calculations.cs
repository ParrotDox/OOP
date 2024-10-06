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
            double? x = Inputx();
            if (x is null)
            {
                Console.WriteLine("Exiting...");
                return;
            }
            else 
            {
                double sumN = 0, sumE = 0;
                uint n = 50;
                double eps = 0.0001;
                double yPrev = 0, yCur = 0;
                double y = 1 / 2 - Math.PI / 4 * Math.Abs(Math.Sin((double)x));
                for(int i = 1; i <= n; ++i) 
                {
                    CalculateFuncValues((double)x, i, eps, ref sumN, ref sumE, ref yPrev, ref yCur);
                    PrintResults((double)x,i, sumN, sumE, y);
                }
                return;
            }
        }
        private static double? Inputx() 
        {
            bool isInputNull = false;
            double? x = null;
            bool isxDouble = false;
            do 
            {
                isxDouble = false;
                Console.WriteLine($"Input x (0.1 <= x <= 0.8) or left empty space to exit");
                string input = Console.ReadLine();
                if (input == "")
                    isInputNull = true;
                else
                    if (isxDouble = double.TryParse(input, out double parsedValue))
                        x = parsedValue;
            }
            while ((isInputNull == isxDouble) || (x < 0.1 || x > 0.8));
            return x;
        }
        private static void CalculateFuncValues(double x, int n, double eps, ref double sumN, ref double sumE, ref double yPrev, ref double yCur) 
        {
            yPrev = yCur;
            yCur = CalculateTerm(n, x);
            if (Math.Abs(yCur - yPrev) < eps)
            {
                sumE += yCur;
                sumN += yCur;
            }
            else
                sumN += yCur;
            return;
        }
        private static double CalculateTerm(int n, double x) 
        {
            double result = Math.Cos(2 * n * x) / (4 * Math.Pow(n,2) - 1);
            return result;
        }
        private static void PrintResults(double x, int n, double sumN, double sumE, double y) 
        {
            Console.WriteLine("--------------------------------------------------------------------------------------------");
            Console.WriteLine($"{n}) X = {x} SN = {sumN} SE = {sumE} Y = {y}");
        }
    }
}
