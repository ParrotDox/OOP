using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5LAB
{
    //Класс Программы
    internal static class Application
    {
        static public int request = 0; 
        public static void Execute() 
        {
            CallMenu();
        }
        private static void CallMenu() 
        {
            do
            {
                bool isRequestInt = Int32.TryParse(Console.ReadLine(), out request);
                switch (request)
                {
                    case 1:
                        //CreateArray();
                        break;
                    case 2:
                        //ChooseArray();
                        break;
                    case 3:
                        //AddKElements();
                        break;
                    case 4:
                        //DeleteZeroColumns();
                        break;
                    case 5:
                        //AddCertainRow();
                        break;
                    default:
                        if (!isRequestInt)
                            Console.WriteLine("Unknown data type");
                        else
                            Console.WriteLine("Unknown command");
                        break;
                }
            } while (request != -1);
        }
    }
    //Класс 
    public class Arrays 
    {

    }
}
