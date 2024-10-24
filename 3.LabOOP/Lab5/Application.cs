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
                Console.Clear();

                Console.WriteLine
                    ("Menu:\n" +
                    "print - output all arrays\n" +
                    "1r - create random one dimensional array\n" +
                    "2r - create random two dimensional array\n" +
                    "2tr - create random torn array\n" +
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
                        ArrayManager.Print("All");
                        break;
                    case "1r":
                        ArrayManager.CreateOneDimArr("Random");
                        break;
                    case "2r":
                        ArrayManager.CreateTwoDimArr("Random");
                        break;
                    case "2tr":
                        ArrayManager.CreateTornArr("Random");
                        break;
                    case "1":
                        ArrayManager.CreateOneDimArr("User");
                        break;
                    case "2":
                        ArrayManager.CreateTwoDimArr("User");
                        break;
                    case "2t":
                        ArrayManager.CreateTornArr("User");
                        break;
                    case "k":
                        ArrayManager.AddElements();
                        break;
                    case "-col0":
                        ArrayManager.DeleteAllZeroColumns();
                        break;
                    case "+row":
                        ArrayManager.AddRowToPos();
                        break;
                    case "quit":
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Error: Unknown command");
                        Console.ResetColor();
                        break;
                }
                Console.WriteLine("Press any button to continue");
                Console.ReadKey();
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
            public static void Print(string key)
            {
                int ctr = 1;
                //Выполняет итерирование по определенным массивам в зависимости от ключа
                switch (key) 
                {
                    case "One":
                        {
                            Console.WriteLine("OneDim.Arrays:");
                            OneDimensionalArray cur = oneDimLnk;
                            while (cur != null)
                            {
                                Console.Write($"{ctr})");
                                cur.Print();
                                cur = (OneDimensionalArray)cur.lnkToNext;
                                ++ctr;
                                Console.Write('\n');
                            }
                            break;
                        }
                    case "Two":
                        {
                            Console.WriteLine("TwoDim.Arrays:");
                            TwoDimensionalArray cur = twoDimLnk;
                            while (cur != null)
                            {
                                Console.WriteLine($"{ctr})");
                                cur.PrintMatrix();
                                cur = (TwoDimensionalArray)cur.lnkToNext;
                                ++ctr;
                                Console.Write('\n');
                            }
                            break;
                        }
                    case "Torn":
                        {
                            Console.WriteLine("Torn Arrays:");
                            TornArray cur = tornLnk;
                            while (cur != null)
                            {
                                Console.WriteLine($"{ctr})");
                                cur.Print();
                                cur = (TornArray)cur.lnkToNext;
                                ++ctr;
                                Console.Write('\n');
                            }
                            break;
                        }
                    case "All":
                        {
                            Console.WriteLine("OneDim.Arrays:");
                            OneDimensionalArray cur1 = oneDimLnk;
                            while (cur1 != null)
                            {
                                Console.Write($"{ctr})");
                                cur1.Print();
                                cur1 = (OneDimensionalArray)cur1.lnkToNext;
                                ++ctr;
                                Console.Write('\n');
                            }
                            ctr = 1;
                            Console.WriteLine("TwoDim.Arrays:");
                            TwoDimensionalArray cur2 = twoDimLnk;
                            while (cur2 != null)
                            {
                                Console.WriteLine($"{ctr})");
                                cur2.PrintMatrix();
                                cur2 = (TwoDimensionalArray)cur2.lnkToNext;
                                ++ctr;
                                Console.Write('\n');
                            }
                            ctr = 1;
                            Console.WriteLine("Torn Arrays:");
                            TornArray curT = tornLnk;
                            while (curT != null)
                            {
                                Console.WriteLine($"{ctr})");
                                curT.Print();
                                curT = (TornArray)curT.lnkToNext;
                                ++ctr;
                                Console.Write('\n');
                            }
                            break;
                        }
                    default:
                        Console.WriteLine("Error: Unknown key");
                        break;
                }
            }
            public static int[] CreateOneDimArr(string key)
            {
                OneDimensionalArray buffer = new OneDimensionalArray(key);
                if (oneDimLnk is null)
                {
                    oneDimLnk = buffer;
                }
                else
                {
                    //С помощью метода получаем ссылку на конечный узел
                    OneDimensionalArray curLnk = (OneDimensionalArray)IterateThroughArrObj(-1, (ArrayObj)oneDimLnk);
                    curLnk.lnkToNext = buffer;
                }
                Print("One");
                return buffer.vault;
            }
            public static int[,] CreateTwoDimArr(string key)
            {
                TwoDimensionalArray buffer = new TwoDimensionalArray(key);
                if (twoDimLnk is null)
                {
                    twoDimLnk = buffer;
                }
                else
                {
                    TwoDimensionalArray curLnk = (TwoDimensionalArray)IterateThroughArrObj(-1, (ArrayObj)twoDimLnk);
                    curLnk.lnkToNext = buffer;
                }
                Print("Two");
                return buffer.vault;
            }
            public static int[][] CreateTornArr(string key)
            {
                TornArray buffer = new TornArray(key);
                if (tornLnk is null)
                {
                    tornLnk = buffer;
                }
                else
                {
                    TornArray curLnk = (TornArray)IterateThroughArrObj(-1, (ArrayObj)tornLnk);
                    curLnk.lnkToNext = buffer;
                }
                Print("Torn");
                return buffer.vault;
            }
            public static int[] AddElements() 
            {
                if (oneDimLnk is null)
                {
                    Console.WriteLine("No oneDim.arrays have found");
                    return null;
                }
                else 
                {
                    OneDimensionalArray curLnk = null;
                    do
                    {
                        Print("One");
                        int request = InputPositive("Choose oneDim.array:");
                        curLnk = (OneDimensionalArray)IterateThroughArrObj(request, (ArrayObj)oneDimLnk);
                    } while (curLnk == null);
                    //Если request попадает на узел
                    int K = InputPositive("How many elements to add:");
                    int[] newArr = new int[curLnk.vault.Length + K * 2];
                    bool flag = false;
                    do
                    {
                        Console.WriteLine("Input random or manually?");
                        Console.WriteLine("r - random | m - manually");
                        string option = Console.ReadLine().ToLower();
                        switch (option) 
                        {
                            case "r":
                                    Random rnd = new Random();
                                    for (int i = 0; i < newArr.Length; i++)
                                    {
                                        if (i < K)
                                        {
                                            newArr[i] = rnd.Next(lowestVal, highestVal);
                                        }
                                        else if (K <= i && i < newArr.Length - K)
                                        {
                                            newArr[i] = curLnk.vault[i - K];
                                        }
                                        else
                                        {
                                            newArr[i] = rnd.Next(-50, 50);
                                        }
                                    }
                                    flag = true;
                                    break;
                            case "m":
                                    for (int i = 0; i < newArr.Length; i++)
                                    {
                                        if (i < K)
                                        {
                                            newArr[i] = InputElem($"Index {i}:");
                                        }
                                        else if (K <= i && i < newArr.Length - K)
                                        {
                                            newArr[i] = curLnk.vault[i - K];
                                        }
                                        else
                                        {
                                            newArr[i] = InputElem($"Index {i}:");
                                        }
                                    }
                                    flag = true;
                                    break;
                            default:
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Error: Unknown Command");
                                Console.ResetColor();
                                break;
                        }
                    } while (!flag);
                    curLnk.vault = newArr;
                    Print("One");
                    return newArr;
                }
            }
            public static int[,] DeleteAllZeroColumns() 
            {
                if (twoDimLnk is null)
                {
                    Console.WriteLine("No twoDim.arrays have found");
                    return null;
                }
                else 
                {
                    TwoDimensionalArray curLnk = null;
                    do
                    {
                        Print("Two");
                        int request = InputPositive("Choose twoDim.array:");
                        curLnk = (TwoDimensionalArray)IterateThroughArrObj(request, (ArrayObj)twoDimLnk);
                    } while (curLnk == null);

                    int rows = curLnk.vault.GetLength(0);
                    int cols = curLnk.vault.GetLength(1);
                    List<int> zeroCols = new List<int>();
                    //Записываем номера нулевых столбцов
                    for(int i = 0; i < rows; ++i) 
                    {
                        for(int j = 0; j < cols; ++j) 
                        {
                            if (curLnk.vault[i, j] == 0 && !zeroCols.Contains(j))
                            {
                                zeroCols.Add(j);
                            }
                        }
                    }
                    //Если кол-во удаляемых столбиков < кол-ва столбцов изменяемой матрицы
                    if (zeroCols.Count != cols)
                    {
                        int[,] newMatrix = new int[rows, cols - zeroCols.Count];
                        //Счетчик колонки для правильной вставки копированных элементов матрицы
                        int colCtr = 0;
                        for (int i = 0; i < rows; ++i)
                        {
                            colCtr = 0;
                            for (int j = 0; j < cols; ++j)
                            {
                                if (!zeroCols.Contains(j)) 
                                {
                                    newMatrix[i, colCtr] = curLnk.vault[i, j];
                                    ++colCtr;
                                }
                            }
                        }
                        curLnk.vault = newMatrix;
                        Print("Two");
                        return newMatrix;
                    }
                    //Если кол-во удаляемых столбиков == кол-ву столбцов изменяемой матрицы, то возвращаем пустую матрицу
                    else
                    {
                        int[,] newMatrix = new int[0,0];
                        curLnk.vault = newMatrix;
                        Print("Two");
                        return newMatrix;
                    }
                }
            }
            public static int[][] AddRowToPos()
            {
                if (tornLnk is null)
                {
                    Console.WriteLine("No torn arrays have found");
                    return null;
                }
                else 
                {
                    //Итерирование до нужного узла
                    TornArray curLnk = null;
                    do
                    {
                        Print("Torn");
                        int request = InputPositive("Choose torn array:");
                        curLnk = (TornArray)IterateThroughArrObj(request, (ArrayObj)tornLnk);
                    } while (curLnk == null);
                    //Размерность(1) текущего массива
                    int rows = curLnk.vault.GetLength(0);
                    //Запрос позиции для вставки элемента в массив
                    int insertPos = -1;
                    //Вставка может быть от нуля до последней позиции + 1
                    do
                    {
                        Console.WriteLine($"Choose gap position between 0 and {rows}");
                        insertPos = InputElem("Position to insert:");
                        if (insertPos < 0 || insertPos > rows)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Error: Position is out of range");
                            Console.ResetColor();
                        }
                    } while (insertPos < 0 || insertPos > rows);

                    int[][] newArr = new int[rows + 1][];
                    int newColLen = InputPositive("Columns:");
                    //Сдвиг. После вставки нового элемента все последующие элем. будут сдвинуты вправо на +1
                    int shift = 0;
                    bool flag = false;
                    do
                    {
                        Console.WriteLine("Input random or manually?");
                        Console.WriteLine("r - random | m - manually");
                        string option = Console.ReadLine().ToLower();
                        switch (option)
                        {
                            case "r":
                                Random rnd = new Random();
                                for(int i = 0; i < rows + 1; ++i) 
                                {
                                    int cols = 0;
                                    //Совпадение с позицией вставки
                                    if (i == insertPos) 
                                    {
                                        //Следующие элементы после вставки будут сдвинуты на +1 позицию
                                        ++shift;
                                        cols = newColLen;
                                        newArr[i] = new int[newColLen];
                                        for(int j = 0; j < cols; ++j) 
                                        {
                                            newArr[i][j] = rnd.Next(lowestVal, highestVal);
                                        }
                                    }
                                    //Копирование элементов
                                    else
                                    {
                                        cols = curLnk.vault[i - shift].Length;
                                        newArr[i] = new int[cols];
                                        for(int j = 0; j < cols; ++j) 
                                        {
                                            newArr[i][j] = curLnk.vault[i - shift][j];
                                        }
                                    }
                                    
                                }
                                flag = true;
                                break;
                            case "m":
                                for (int i = 0; i < rows + 1; ++i)
                                {
                                    int cols = 0;
                                    //Совпадение с позицией вставки
                                    if (i == insertPos)
                                    {
                                        //Следующие элементы после вставки будут сдвинуты на +1 позицию
                                        ++shift;
                                        cols = newColLen;
                                        newArr[i] = new int[newColLen];
                                        for (int j = 0; j < cols; ++j)
                                        {
                                            newArr[i][j] = InputElem($"Index {j}:");
                                        }
                                    }
                                    //Копирование элементов
                                    else
                                    {
                                        cols = curLnk.vault[i - shift].Length;
                                        newArr[i] = new int[cols];
                                        for (int j = 0; j < cols; ++j)
                                        {
                                            newArr[i][j] = curLnk.vault[i - shift][j];
                                        }
                                    }

                                }
                                flag = true;
                                break;
                            default:
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Error: Unknown Command");
                                Console.ResetColor();
                                break;
                        }
                    } while (!flag);
                    curLnk.vault = newArr;
                    Print("Torn");
                    return newArr;
                }
            }
            //Итерация по узлам до номера {request}
            private static ArrayObj IterateThroughArrObj(int request, ArrayObj lnk) 
            {
                //Если request -1, то итерирование идет до последнего узла
                ArrayObj curLnk = lnk;
                if (request == -1)
                {
                    while(curLnk.lnkToNext != null) 
                    {
                        curLnk = curLnk.lnkToNext;
                        request -= 1;
                    }
                }
                //Если request != -1, то итерирование идет до указанного узла
                else
                {
                    while (request != 1 && curLnk.lnkToNext != null)
                    {
                        curLnk = curLnk.lnkToNext;
                        request -= 1;
                    }
                    if (request != 1)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Error: Request is out of range");
                        Console.ResetColor();
                        return null;
                    }
                }
                return curLnk;
            }
            private static int InputPositive(string msg)
            {
                int val = 0;
                bool isValueInt = false;
                do
                {
                    Console.WriteLine(msg);
                    isValueInt = Int32.TryParse(Console.ReadLine(), out val);
                    if(!isValueInt)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Error: Wrong type of data");
                        Console.ResetColor();
                    }
                        
                    else if (val < 1 && isValueInt) 
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Error: Number is <= 0");
                        Console.ResetColor();
                    }
                        
                } while (val < 1 || !isValueInt);
                return val;
            }
            private static int InputElem(string msg)
            {
                int val = 0;
                bool isValueInt = false;
                do
                {
                    Console.WriteLine(msg);
                    isValueInt = Int32.TryParse(Console.ReadLine(), out val);
                    if (!isValueInt)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Error: Wrong type of data");
                        Console.ResetColor();
                    }
                } while (!isValueInt);
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
                public OneDimensionalArray(string key) : base()
                {
                    int len = InputPositive("Elements in oneDim.Array:");
                    int[] temp = new int[len];
                    switch (key)
                    {
                        case "Random":
                            Random rnd = new Random();
                            for (int i = 0; i < len; i++)
                            {
                                temp[i] = rnd.Next(lowestVal, highestVal);
                            }
                            vault = temp;
                            break;
                        case "User":
                            for (int i = 0; i < len; i++)
                            {
                                temp[i] = InputElem($"Index {i}:");
                            }
                            vault = temp;
                            break;
                        default:
                            Console.WriteLine("Unknown key");
                            vault = temp;
                            break;
                    }
                }
                //Методы
                public override void Print()
                {
                    int rows = vault.GetLength(0);
                    Console.Write("[");
                    for (int i = 0; i < rows; i++)
                    {
                        Console.Write($"{vault[i]} ");
                    }
                    Console.Write("]");
                }
            }
            //Двуразмерный массив
            private class TwoDimensionalArray : ArrayObj
            {
                //Поля
                public int[,] vault;
                //Конструкторы
                public TwoDimensionalArray(string key) : base()
                {
                    int rows = InputPositive("Rows in twoDim.Array:");
                    int cols = InputPositive("Columns in twoDim.Array:");
                    int[,] temp = new int[rows, cols];
                    switch (key)
                    {
                        case "Random":
                            Random rnd = new Random();
                            for (int i = 0; i < rows; i++)
                            {
                                for (int j = 0; j < cols; ++j)
                                {
                                    temp[i, j] = rnd.Next(lowestVal, highestVal);
                                }
                            }
                            vault = temp;
                            break;
                        case "User":
                            for (int i = 0; i < rows; i++)
                            {
                                Console.WriteLine($"--- ROW {i} --- :");
                                for (int j = 0; j < cols; ++j)
                                {
                                    temp[i, j] = InputElem($"Column {j}:");
                                }
                            }
                            vault = temp;
                            break;
                        default:
                            Console.WriteLine("Unknown key");
                            vault = temp;
                            break;
                    }
                }
                //Методы
                public override void Print()
                {
                    int rows = vault.GetLength(0);
                    int cols = vault.GetLength(1);
                    for (int i = 0; i < rows; i++)
                    {
                        Console.Write("[");
                        for (int j = 0; j < cols; ++j)
                        {
                            Console.Write($"{vault[i, j]} ");
                        }
                        Console.Write("]");
                        Console.Write("\n");
                    }
                }
                public void PrintMatrix() 
                {
                    int rows = vault.GetLength(0);
                    int cols = vault.GetLength(1);
                    int maxLen = 0;
                    //Находим максимальную длину ячейки на вывод
                    foreach(int item in vault) 
                    {
                        maxLen = Math.Max(maxLen, item.ToString().Length);
                    }
                    for(int i = 0; i < rows; ++i) 
                    {
                        for(int j = 0; j < cols; ++j) 
                        {
                            Console.Write($"{vault[i,j].ToString().PadLeft(maxLen,' ')}");
                            if (j + 1 != cols)
                                Console.Write('|');
                        }
                        Console.Write('\n');
                        if(i + 1 != rows) 
                        {
                            string divLine = string.Concat(Enumerable.Repeat("-", (maxLen + 1) * cols));
                            Console.Write(divLine);
                        }
                        Console.Write('\n');
                    }
                }
            }
            //Зубчатый массив
            private class TornArray : ArrayObj
            {
                //Поля
                public int[][] vault;
                //Конструкторы
                public TornArray(string key) : base()
                {
                    int rows = InputPositive("Rows in Torn Array:");
                    int[][] temp = new int[rows][];
                    switch (key) 
                    {
                        case "Random":

                            Random rnd = new Random();
                            for (int i = 0; i < rows; i++)
                            {
                                temp[i] = new int[rnd.Next(1, 5)];
                                int cols = temp[i].Length;
                                for (int j = 0; j < cols; ++j)
                                {
                                    temp[i][j] = rnd.Next(lowestVal, highestVal);
                                }
                            }
                            vault = temp;
                            break;
                        case "User":
                            for (int i = 0; i < rows; i++)
                            {
                                Console.WriteLine($"--- ROW {i} --- :");
                                int cols = InputPositive($"Columns in ROW {i}:");
                                temp[i] = new int[cols];
                                for (int j = 0; j < cols; ++j)
                                {
                                    temp[i][j] = InputElem($"Column {j}:");
                                }
                            }
                            vault = temp;
                            break;
                        default:
                            Console.WriteLine("Unknown key");
                            vault = temp;
                            break;
                    }  
                }
                //Методы
                public override void Print()
                {
                    int rows = vault.GetLength(0);
                    for (int i = 0; i < rows; ++i)
                    {

                        Console.Write("[");
                        for (int j = 0; j < vault[i].Length; ++j)
                        {
                            Console.Write($"{vault[i][j]} ");
                        }
                        Console.Write("]");
                        Console.Write("\n");
                    }
                }
            }
        }
    }
}