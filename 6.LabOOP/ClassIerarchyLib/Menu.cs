using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
enum Commands
{
    Continue = 100,
    Enter = 200,

    Print = 0,
    Add = 1,
    DeleteIndex = 2,
    Sort = 3,
    //Специальные Запросы
    CountType = 4,
    CountDepartmentEngineers = 5,
    CalcTotalSalaryByType = 6,
    //foreach-перебор
    Enumeration = 7,
    //Клонирование
    ShallowCopy = 8,
    DeepCopy = 9,
    //Поиск элемента
    FindBySample = 10,
    //Проверка равнозначности ссылок
    MainIsShallow = 11,
    MainIsDeep = 12,

    Exit = 13,
}

namespace ClassIerarchyLib
{
    //Меню для работы с коллекцией
    public class Menu
    {
        //Массив доступных классов
        string[] classes = new string[] { "Person", "Employee", "Engineer", "Admin"};
        //Переменные для вызова комманд
        private uint commandKey = 0;
        //ВАЖНО: значение переменной должно быть равно кол-ву элементов enum Commands
        private uint commandAmount = 14;
        //Основной ArrayList для работы
        ArrayList mainArray = new ArrayList();
        //Доп.ArrayList'ы для демонстрации копирований
        ArrayList shallowCopyArray;
        ArrayList deepCopyArray;
        public void Execute()
        {
        Start:
            try
            {
                bool exitFlag = false;
                Commands request = Commands.Print;
                while (!exitFlag)
                {
                    //Счетчик на основе W и S, +1 и -1 соответственно, при Enter завершает цикл выбора
                    do
                    {
                        Console.Clear();
                        ShowMenu(commandKey);
                        request = KeyboardHandler(Console.ReadKey().KeyChar);
                    } while (request != Commands.Enter);

                    switch (commandKey)
                    {
                        case (uint)Commands.Print:
                            {
                                if(mainArray.Count == 0) 
                                {
                                    Console.WriteLine($"mainArray is empty");
                                }
                                else 
                                {
                                    Console.WriteLine($"mainArray:");
                                    ForEachEnum(mainArray);
                                }
                                if(shallowCopyArray is null) 
                                {
                                    Console.WriteLine($"shallowCopyArray is not init");
                                }
                                else 
                                {
                                    if (shallowCopyArray.Count == 0) 
                                    {
                                        Console.WriteLine($"shallowCopyArray is empty");
                                    }
                                    else 
                                    {
                                        Console.WriteLine($"shallowCopyArray:");
                                        ForEachEnum(shallowCopyArray);
                                    }
                                }
                                if (deepCopyArray is null)
                                {
                                    Console.WriteLine($"deepCopyArray is not init");
                                }
                                else 
                                {
                                    if (deepCopyArray.Count == 0)
                                    {
                                        Console.WriteLine($"deepCopyArray is empty");
                                    }
                                    else
                                    {
                                        Console.WriteLine($"deepCopyArray:");
                                        ForEachEnum(deepCopyArray);
                                    }
                                }
                                break;
                            }
                        case (uint)Commands.Add:
                            {
                                //Запрашиваем требуемый класс для создания экземпляра класса
                                string queryClass = GetQueryClass();
                                //Создаем требуемый экземпляр класса
                                object queryObject = CreateClassObject(queryClass);
                                mainArray.Add(queryObject);
                                break;
                            }
                        case (uint)Commands.DeleteIndex:
                            {
                                int queryInt = GetQueryIndex();
                                mainArray.RemoveAt(queryInt);
                                break;
                            }
                        case (uint)Commands.Sort:
                            {
                                mainArray.Sort();
                                break;
                            }
                        case (uint)Commands.CountType:
                            {
                                //Обращение к отдельному классу с запросом (из 10 лаб.)
                                QueryMaker.ShowQuantityOfClassObjects(mainArray);
                                break;
                            }
                        case (uint)Commands.CountDepartmentEngineers:
                            {
                                QueryMaker.ShowQuantityOfEngineersOfChosenDepartment(mainArray);
                                break;
                            }
                        case (uint)Commands.CalcTotalSalaryByType:
                            {
                                QueryMaker.CalcTotalSalaryToPayByClassObject(mainArray);
                                break;
                            }
                        case (uint)Commands.Enumeration:
                            {
                                ForEachEnum(mainArray);
                                break;
                            }
                        case (uint)Commands.ShallowCopy:
                            {
                                shallowCopyArray = new ArrayList(mainArray);
                                break;
                            }
                        case (uint)Commands.DeepCopy:
                            {
                                if(deepCopyArray is not null) 
                                {
                                    deepCopyArray.Clear();
                                }
                                deepCopyArray = DeepCopy(mainArray);
                                break;
                            }
                        case (uint)Commands.FindBySample:
                            {
                                int queryInt = GetQueryIndex();
                                int index = mainArray.IndexOf(mainArray[queryInt]);
                                Console.WriteLine($"Query Index = {queryInt} | Object was found on {index}");
                                break;
                            }
                        case (uint)Commands.MainIsShallow:
                            {
                                if(shallowCopyArray is null) 
                                {
                                    Console.WriteLine("Initialize shallowCopyArray!");
                                    break;
                                }
                                AreLinksOfArraysEqual(mainArray, shallowCopyArray);
                                AreLinksOfItemsInArraysEqual(mainArray, shallowCopyArray);
                                break;
                            }
                        case (uint)Commands.MainIsDeep:
                            {
                                if (deepCopyArray is null)
                                {
                                    Console.WriteLine("Initialize deepCopyArray!");
                                    break;
                                }
                                AreLinksOfArraysEqual(mainArray, deepCopyArray);
                                AreLinksOfItemsInArraysEqual(mainArray, deepCopyArray);
                                break;
                            }
                        case (uint)Commands.Exit:
                            {
                                exitFlag = true;
                                break;
                            }
                    }
                    Console.WriteLine("Press any button to continue");
                    Console.ReadKey();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.ReadKey();
                goto Start;
            }
        }
        private Commands KeyboardHandler(int key)
        {
            //w
            if (key == 119 && commandKey > 0)
            {
                --commandKey;
                return Commands.Continue;
            }
            //s
            else if (key == 115 && commandKey < commandAmount - 1)
            {
                ++commandKey;
                return Commands.Continue;
            }
            //Enter
            else if (key == 13)
            {

                return Commands.Enter;
            }
            //Other keyChars
            else
            {
                return Commands.Continue;
            }
        }
        //Метод для вывода консольного меню
        private void ShowMenu(uint key)
        {
            /*
            Continue = 100,
            Enter = 200,

            Print = 0,
            Add = 1,
            DeleteIndex = 2,
            Sort = 3,
            //Специальные Запросы
            CountType = 4,
            CountDepartmentEngineer = 5,
            CalcTotalSalaryByType = 6,
            //foreach-перебор
            Enumeration = 7,
            //Клонирование
            ShallowCopy = 8,
            DeepCopy = 9,
            //Поиск элемента
            FindByIndex = 10,
            //Проверка равнозначности ссылок
            MainIsShallow = 11,
            MainIsDeep = 12,

            Exit = 13,
            */

            //Для вывода выбранного действия использованы тернарные операторы
            string menuInfo = key == 0 ? "Print everything <--" : "Print everything";
            menuInfo += '\n';
            menuInfo += key == 1 ? "Add element <--" : "Add element";
            menuInfo += '\n';
            menuInfo += key == 2 ? "DeleteByIndex <--" : "DeleteByIndex";
            menuInfo += '\n';
            menuInfo += key == 3 ? "Sort by age <--" : "Sort by age";
            menuInfo += '\n';
            menuInfo += key == 4 ? "Count type's objects <--" : "Count type's objects";
            menuInfo += '\n';
            menuInfo += key == 5 ? "Count Depart.'s engineers <--" : "Count Depart.'s engineers";
            menuInfo += '\n';
            menuInfo += key == 6 ? "Calc.Total Salary By Type <--" : "Calc.Total Salary By Type";
            menuInfo += '\n';
            menuInfo += key == 7 ? "Enumeration <--" : "Enumeration";
            menuInfo += '\n';
            menuInfo += key == 8 ? "Shallow Copy <--" : "Shallow Copy";
            menuInfo += '\n';
            menuInfo += key == 9 ? "Deep Copy <--" : "Deep Copy";
            menuInfo += '\n';
            menuInfo += key == 10 ? "Find By Index <--" : "Find By Index";
            menuInfo += '\n';
            menuInfo += key == 11 ? "Check if mainArray link == shallowCopyArray link <--" : "Check if mainArray link == shallowCopyArray link";
            menuInfo += '\n';
            menuInfo += key == 12 ? "Check if mainArray link == deepCopyArray link <--" : "Check if mainArray link == deepCopyArray link";
            menuInfo += '\n';
            menuInfo += key == 13 ? "Exit <--" : "Exit";
            menuInfo += '\n';
            Console.WriteLine(menuInfo);
        }
        //Выбор класса
        private string GetQueryClass() 
        {
            string choice = "";
            Console.Write("Classes: ");
            foreach (string cl in classes) 
            {
                Console.Write(cl + ", ");
            }
            Console.Write('\n');
            do
            {
                Console.Write("Enter query class:");
                choice = Console.ReadLine();
                if (!classes.Contains(choice))
                {
                    Console.WriteLine("ERROR: Query class doesn't exist");
                }
            } while (!classes.Contains(choice));
            return choice;
        }
        //Создание экземпляра
        private object CreateClassObject(string queryClass) 
        {
            object obj;
            switch (queryClass) 
            {
                case "Person": 
                    {
                        obj = new Person();
                        ((Person)obj).RandomInit();
                        break;
                    }
                case "Employee":
                    {
                        obj = new Employee();
                        ((Employee)obj).RandomInit();
                        break;
                    }
                case "Engineer":
                    {
                        obj = new Engineer();
                        ((Engineer)obj).RandomInit();
                        break;
                    }
                case "Admin":
                    {
                        obj = new Admin();
                        ((Admin)obj).RandomInit();
                        break;
                    }
                default: 
                    {
                        obj = new object();
                        break;
                    }  
            }
            return obj;
        }
        //Выбор индекса (Нет проверки на "< 0")
        private int GetQueryIndex() 
        {
            int choice = -1;
            bool isInt = false;
            do
            {
                Console.Write("Enter index:");
                isInt = int.TryParse(Console.ReadLine(), out choice);
                if (!isInt)
                {
                    Console.WriteLine("ERROR: Wrong Type");
                }
            } while (isInt == false);
            return choice;
        }
        //Итерация по ArrayList с вызовом метода Show у каждого экземпляра
        private void ForEachEnum(ArrayList array) 
        {
            foreach (object obj in array) 
            {
                Console.Write("----:----:----\n");
                if (obj is Admin)
                {
                    ((Admin)obj).Show();
                    continue;
                }
                if (obj is Engineer)
                {
                    ((Engineer)obj).Show();
                    continue;
                }
                if (obj is Employee)
                {
                    ((Employee)obj).Show();
                    continue;
                }
                ((Person)obj).Show();
            }
        }
        //Глубокое копирование с возвратом нового ArrayList
        private ArrayList DeepCopy(ArrayList array) 
        {
            ArrayList tempArrayList = new ArrayList();
            foreach (object obj in array)
            {
                if (obj is Admin)
                {
                    var ad = (Admin)obj;
                    tempArrayList.Add(ad.Clone());
                    continue;
                }
                if (obj is Engineer)
                {
                    var en = (Engineer)obj;
                    tempArrayList.Add(en.Clone());
                    continue;
                }
                if (obj is Employee)
                {
                    var em = (Employee)obj;
                    tempArrayList.Add(em.Clone());
                    continue;
                }
                var pe = (Person)obj;
                tempArrayList.Add(pe.Clone());
            }
            return tempArrayList;
        }
        private bool AreLinksOfArraysEqual(ArrayList a, ArrayList b) 
        {
            bool result = false;
            if (a == b)
            {
                result = true;
                Console.WriteLine("Links of Arrays are the same");
            }
            else
            {
                result = false;
                Console.WriteLine("Links of Arrays are different");
            }
            return result;
        }
        /*Сравнение ссылок ссылочных объектов у:
          1)Оригинального ArrayList
          2)У копии оригинала ArrayList (копирование ArrayList может быть как глубоким, так и поверхностным)
        */
        private bool AreLinksOfItemsInArraysEqual(ArrayList a, ArrayList b) 
        {
            bool result = false;
            Person toCompareItem1 = ((Person)a[0]);
            Person toCompareItem2 = ((Person)b[0]);
            if (toCompareItem1.lnk == toCompareItem2.lnk)
            {
                result = true;
                Console.WriteLine("Links of items are the same");
            }
            else
            {
                result = false;
                Console.WriteLine("Links of items are different");
            }
            return result;
        }
    }
}
