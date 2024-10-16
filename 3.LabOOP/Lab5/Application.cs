using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5
{
    internal class Application
    {
        //Execute - запускает работу консольного приложения
        static public void Execute()
        {
            string request = "None";
            do
            {
                Console.ReadKey();
                Console.Clear();

                Console.WriteLine
                    ("Menu:\n" +
                    "print - output all arrays\n" +
                    "1 - create one dimensional array\n" +
                    "2 - create two dimensional array\n" +
                    "2t - create torn array\n" +
                    "k - add k elements to chosen array\n" +
                    "-col0 - del columns with zero\n" +
                    "+row - add chosen row to certain array\n" +
                    "quit - close the application");
                Console.WriteLine("Option:");
                request = Console.ReadLine().ToLower();
                switch (request)
                {
                    case "print":
                        ArrayManager.Print();
                        break;
                    case "1":
                        ArrayManager.CreateOneDimArr();
                        break;
                    case "2":
                        ArrayManager.CreateTwoDimArr();
                        break;
                    case "2t":
                        ArrayManager.CreateTornArr();
                        break;
                    case "k":
                        ArrayManager.AddElements();
                        break;
                    case "-col0":
                        ArrayManager.DeleteAllZeroColumns();
                        break;
                    case "+row":
                        ArrayManager.AddCertainRow();
                        break;
                    case "quit":
                        break;
                    default:
                        Console.WriteLine("Unknown command");
                        break;
                }
            } while (request != "quit");
            return;
        }
        //Вспомогательный класс для работы с массивами
        protected static class ArrayManager
        {
            //Поля
            //Ссылки на первые объекты в списках массивов
            static private OneDimensionalArray oneDimLnk;
            static private TwoDimensionalArray twoDimLnk;
            static private TornArray tornLnk;
            //Границы генератора случайных чисел
            static private int lowestVal = -10;
            static private int highestVal = 10;
            //Методы
            public static void Print()
            {
                Console.WriteLine("OneDim Arrays:");
                if (oneDimLnk is null)
                {
                    Console.WriteLine("Empty");
                }
                else
                {
                    OneDimensionalArray curOneDimLnk = oneDimLnk;
                    while (curOneDimLnk.lnkToNext != null)
                    {
                        curOneDimLnk.Print();
                        curOneDimLnk = (OneDimensionalArray)curOneDimLnk.lnkToNext;
                    }
                    curOneDimLnk.Print();
                }
                Console.WriteLine("TwoDim Arrays:");
                if (twoDimLnk is null)
                {
                    Console.WriteLine("Empty");
                }
                else
                {
                    TwoDimensionalArray curTwoDimLnk = twoDimLnk;
                    while (curTwoDimLnk.lnkToNext != null)
                    {
                        curTwoDimLnk.Print();
                        curTwoDimLnk = (TwoDimensionalArray)curTwoDimLnk.lnkToNext;
                    }
                    curTwoDimLnk.Print();
                }
                Console.WriteLine("Torn Arrays:");
                if (tornLnk is null)
                {
                    Console.WriteLine("Empty");
                }
                else
                {
                    TornArray curTornLnk = tornLnk;
                    while (curTornLnk.lnkToNext != null)
                    {
                        curTornLnk.Print();
                        curTornLnk = (TornArray)curTornLnk.lnkToNext;
                    }
                    curTornLnk.Print();
                }
            }
            public static int[] CreateOneDimArr()
            {
                OneDimensionalArray buffer = new OneDimensionalArray();
                if (oneDimLnk is null)
                {
                    oneDimLnk = buffer;
                }
                else
                {
                    OneDimensionalArray curLnk = oneDimLnk;
                    while (curLnk.lnkToNext != null)
                    {
                        curLnk = (OneDimensionalArray)curLnk.lnkToNext;
                    }
                    curLnk.lnkToNext = buffer;
                }
                return buffer.vault;
            }
            public static int[,] CreateTwoDimArr()
            {
                TwoDimensionalArray buffer = new TwoDimensionalArray();
                if (twoDimLnk is null)
                {
                    twoDimLnk = buffer;
                }
                else
                {
                    TwoDimensionalArray curLnk = twoDimLnk;
                    while (curLnk.lnkToNext != null)
                    {
                        curLnk = (TwoDimensionalArray)curLnk.lnkToNext;
                    }
                    curLnk.lnkToNext = buffer;
                }
                return buffer.vault;
            }
            public static int[][] CreateTornArr()
            {
                TornArray buffer = new TornArray();
                if (tornLnk is null)
                {
                    tornLnk = buffer;
                }
                else
                {
                    TornArray curLnk = tornLnk;
                    while (curLnk.lnkToNext != null)
                    {
                        curLnk = (TornArray)curLnk.lnkToNext;
                    }
                    curLnk.lnkToNext = buffer;
                }
                return buffer.vault;
            }
            public static int[] AddElements() 
            {
                if (oneDimLnk is null)
                {
                    Console.WriteLine("No arrays have found");
                    return null;
                }
                else 
                {
                    Print();
                    //request - номер узла с изменяемым списком и счетчик для цикла
                    int request = InputInt("Choose one dimensional array:");
                    OneDimensionalArray curLnk = oneDimLnk;
                    while (request != 1 && curLnk.lnkToNext != null) 
                    {
                        curLnk = (OneDimensionalArray)curLnk.lnkToNext;
                        request -= 1;
                    }
                    if(request != 1) 
                    {
                        Console.WriteLine("Request is out of range");
                        return null;
                    }
                    else 
                    {
                        int K = InputInt("How many elements to add:");
                        Random rnd = new Random();
                        int[] newArr = new int[curLnk.vault.Length + K * 2];
                        for(int i = 0; i < newArr.Length; i++) 
                        {
                            if(i < K) 
                            {
                                newArr[i] = rnd.Next(lowestVal, highestVal);
                            }
                            else if (K <= i && i < newArr.Length - K) 
                            {
                                newArr[i] = curLnk.vault[i-K];
                            }
                            else 
                            {
                                newArr[i] = rnd.Next(-50, 50);
                            }
                        }
                        curLnk.vault = newArr;
                        return newArr;
                    }
                }
            }
            public static int[,] DeleteAllZeroColumns() 
            {
                if (twoDimLnk is null)
                {
                    Console.WriteLine("No arrays have found");
                    return null;
                }
                else 
                {
                    Print();
                    //request - номер узла с изменяемым списком и счетчик для цикла
                    int request = InputInt("Choose two dimensional array:");
                    TwoDimensionalArray curLnk = (TwoDimensionalArray)IterateThroughArrObj(request, twoDimLnk);
                    if (curLnk == null)
                    {
                        return null;
                    }
                    else
                    {
                        //Размерности будущего редактированного массива
                        int lenOuter = curLnk.vault.GetLength(0);
                        int lenInner = curLnk.vault.GetLength(1);
                        //Массив разницы, какие столбики будут включены в измененный массив
                        //1 - оставить, 0 - удалить (Значения массива по умолчанию - 0)
                        bool[] dif = new bool[lenOuter];
                        for(int i = 0; i < curLnk.vault.GetLength(0); ++i) 
                        {
                            bool isZero = false;
                            for(int j = 0; j < curLnk.vault.GetLength(1); ++j) 
                            {
                                if (curLnk.vault[i,j] == 0) 
                                {
                                    isZero = true;
                                    --lenOuter;
                                    break;
                                }
                            }
                            //Если столбец прошел проверку (не нашлось 0)
                            if (!isZero) 
                            {
                                dif[i] = true;
                            }
                        }

                        //На основе массива разницы формируем новый массив без ячеек с нулями
                        int[,] newArr = new int[lenOuter, lenInner];
                        for(int iNewOut = 0; iNewOut < lenOuter; ++iNewOut) 
                        {
                            for(int iDif = 0; iDif < dif.GetLength(0); ++iDif) 
                            {
                                //Если 1 - переписываем соответствующую ячейку
                                if (dif[iDif] == true) 
                                {
                                    //Зачеркиваем 1 нулем, чтобы не допустить повторение ячейки
                                    dif[iDif] = false; 
                                    for(int k = 0; k < lenInner; ++k) 
                                    {
                                        newArr[iNewOut, k] = curLnk.vault[iDif, k];
                                    }
                                    break;
                                }
                            }
                        }
                        curLnk.vault = newArr;
                        return newArr;
                    }
                }
            }
            public static int[][] AddCertainRow()
            {
                if (tornLnk is null)
                {
                    Console.WriteLine("No arrays have found");
                    return null;
                }
                else
                {
                    Print();
                    //request - номер узла с изменяемым списком
                    int request = InputInt("Choose torn array:");
                    TornArray curLnk = (TornArray)IterateThroughArrObj(request, tornLnk);
                    if (curLnk == null)
                    {
                        return null;
                    }
                    else 
                    {
                        //request - индекс строки для дублирования
                        request = InputInt("Choose row to duplicate:") - 1;
                        if(request >= curLnk.vault.GetLength(0)) 
                        {
                            Console.WriteLine("Request is out of range");
                            return null;
                        }
                        else
                        {
                            //Дублирование всех элементов из старого массива в новый + [копированная ячейка]
                            int[][] newArr = new int[curLnk.vault.GetLength(0) + 1][];
                            for(int i = 0; i < curLnk.vault.GetLength(0); ++i) 
                            {
                                newArr[i] = new int[curLnk.vault[i].Length];
                                for(int j = 0; j < curLnk.vault[i].Length; ++j) 
                                {
                                    newArr[i][j] = curLnk.vault[i][j];
                                }
                            }
                            newArr[newArr.GetLength(0) - 1] = new int[curLnk.vault[request].Length];
                            for(int i = 0; i < curLnk.vault[request].Length; ++i) 
                            {
                                newArr[newArr.GetLength(0) - 1][i] = curLnk.vault[request][i];
                            }
                            curLnk.vault = newArr;
                            return newArr;
                        }
                    }
                }
            }
            private static ArrayObj IterateThroughArrObj(int request, ArrayObj lnk) 
            {
                ArrayObj curLnk = lnk;
                while (request != 1 && curLnk.lnkToNext != null)
                {
                    curLnk = curLnk.lnkToNext;
                    request -= 1;
                }
                if (request != 1)
                {
                    Console.WriteLine("Request is out of range");
                    return null;
                }
                else 
                {
                    return curLnk;
                }
            }
            private static int InputInt(string msg)
            {
                int val = 0;
                bool isValueInt = false;
                do
                {
                    Console.WriteLine(msg);
                    isValueInt = Int32.TryParse(Console.ReadLine(), out val);
                } while (val < 1 || !isValueInt);
                return val;
            }

            //Внутренний базовый класс, задает базовые методы и структуру объекта - массив
            private abstract class ArrayObj
            {
                //Поля
                public ArrayObj lnkToNext = null;
                //Конструкторы
                protected ArrayObj()
                {
                    lnkToNext = null;
                }
                //Методы
                public abstract void Print();
            }
            //Одноразмерный массив
            private class OneDimensionalArray : ArrayObj
            {
                //Поля
                public int[] vault;
                //Конструкторы
                public OneDimensionalArray() : base()
                {
                    int len = InputInt("Length:");
                    int[] temp = new int[len];
                    Random rnd = new Random();
                    for (int i = 0; i < len; i++)
                    {
                        temp[i] = rnd.Next(lowestVal, highestVal);
                    }
                    vault = temp;
                }
                //Методы
                public override void Print()
                {
                    Console.Write("[");
                    for (int i = 0; i < vault.GetLength(0); i++)
                    {
                        Console.Write($"{vault[i]} ");
                    }
                    Console.Write("]");
                    Console.Write("\n");
                }
            }
            //Двуразмерный массив
            private class TwoDimensionalArray : ArrayObj
            {
                //Поля
                public int[,] vault;
                //Конструкторы
                public TwoDimensionalArray() : base()
                {
                    int lenOuter = InputInt("Outer Length:");
                    int lenInner = InputInt("Inner Length:");
                    int[,] temp = new int[lenOuter, lenInner];
                    Random rnd = new Random();
                    for (int i = 0; i < lenOuter; i++)
                    {
                        for (int j = 0; j < lenInner; ++j)
                        {
                            temp[i, j] = rnd.Next(lowestVal, highestVal);
                        }
                    }
                    vault = temp;
                }
                //Методы
                public override void Print()
                {
                    for (int i = 0; i < vault.GetLength(0); i++)
                    {
                        Console.Write("[");
                        for (int j = 0; j < vault.GetLength(1); ++j)
                        {
                            Console.Write($"{vault[i, j]} ");
                        }
                        Console.Write("]");
                    }
                    Console.Write("\n");
                }
            }
            private class TornArray : ArrayObj
            {
                //Поля
                public int[][] vault;
                //Конструкторы
                public TornArray() : base()
                {
                    int lenOuter = InputInt("Outer Length:");
                    int[][] temp = new int[lenOuter][];
                    Random rnd = new Random();
                    for (int i = 0; i < lenOuter; i++)
                    {
                        temp[i] = new int[rnd.Next(1, 5)];
                        for (int j = 0; j < temp[i].Length; ++j)
                        {
                            temp[i][j] = rnd.Next(lowestVal, highestVal);
                        }
                    }
                    vault = temp;
                }
                //Методы
                public override void Print()
                {
                    for (int i = 0; i < vault.GetLength(0); ++i)
                    {

                        Console.Write("[");
                        for (int j = 0; j < vault[i].Length; ++j)
                        {
                            Console.Write($"{vault[i][j]} ");
                        }
                        Console.Write("]");
                    }
                    Console.Write("\n");
                }
            }
        }
    }
}
