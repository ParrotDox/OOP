using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassIerarchyLib
{
    //Меню для работы с обобщенной коллекцией
    public class MenuGeneralized
    {
        //Массив доступных классов
        string[] classes = new string[] { "Person", "Employee", "Engineer", "Admin" };
        //Переменные для вызова комманд
        private uint commandKey = 0;
        //ВАЖНО: значение переменной должно быть равно кол-ву элементов enum Commands
        private uint commandAmount = 14;
        //Основной ArrayList для работы
        List<Person> mainList = new List<Person>();
        //Доп.ArrayList'ы для демонстрации копирований
        List<Person> shallowCopyList;
        List<Person> deepCopyList;
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
                                if (mainList.Count == 0)
                                {
                                    Console.WriteLine($"mainList is empty");
                                }
                                else
                                {
                                    Console.WriteLine($"mainList:");
                                    ForEachEnum(mainList);
                                }
                                if (shallowCopyList is null)
                                {
                                    Console.WriteLine($"shallowCopyList is not init");
                                }
                                else
                                {
                                    if (shallowCopyList.Count == 0)
                                    {
                                        Console.WriteLine($"shallowCopyList is empty");
                                    }
                                    else
                                    {
                                        Console.WriteLine($"shallowCopyList:");
                                        ForEachEnum(shallowCopyList);
                                    }
                                }
                                if (deepCopyList is null)
                                {
                                    Console.WriteLine($"deepCopyList is not init");
                                }
                                else
                                {
                                    if (deepCopyList.Count == 0)
                                    {
                                        Console.WriteLine($"deepCopyList is empty");
                                    }
                                    else
                                    {
                                        Console.WriteLine($"deepCopyList:");
                                        ForEachEnum(deepCopyList);
                                    }
                                }
                                break;
                            }
                        case (uint)Commands.Add:
                            {
                                //Запрашиваем требуемый класс для создания экземпляра класса
                                string queryClass = GetQueryClass();
                                //Создаем требуемый экземпляр класса
                                Person queryObject = CreateClassObject(queryClass);
                                mainList.Add(queryObject);
                                break;
                            }
                        case (uint)Commands.DeleteIndex:
                            {
                                int queryInt = GetQueryIndex();
                                mainList.RemoveAt(queryInt);
                                break;
                            }
                        case (uint)Commands.Sort:
                            {
                                mainList.Sort(new SortByAge());
                                break;
                            }
                        case (uint)Commands.CountType:
                            {
                                //Обращение к отдельному классу с запросом (из 10 лаб.)
                                QueryMaker.ShowQuantityOfClassObjects(mainList);
                                break;
                            }
                        case (uint)Commands.CountDepartmentEngineers:
                            {
                                QueryMaker.ShowQuantityOfEngineersOfChosenDepartment(mainList);
                                break;
                            }
                        case (uint)Commands.CalcTotalSalaryByType:
                            {
                                QueryMaker.CalcTotalSalaryToPayByClassObject(mainList);
                                break;
                            }
                        case (uint)Commands.Enumeration:
                            {
                                ForEachEnum(mainList);
                                break;
                            }
                        case (uint)Commands.ShallowCopy:
                            {
                                shallowCopyList = new List<Person>(mainList);
                                break;
                            }
                        case (uint)Commands.DeepCopy:
                            {
                                if (deepCopyList is not null)
                                {
                                    deepCopyList.Clear();
                                }
                                deepCopyList = DeepCopy(mainList);
                                break;
                            }
                        case (uint)Commands.FindBySample:
                            {
                                int queryInt = GetQueryIndex();
                                int index = mainList.IndexOf(mainList[queryInt]);
                                Console.WriteLine($"Query Index = {queryInt} | Object was found on {index}");
                                break;
                            }
                        case (uint)Commands.MainIsShallow:
                            {
                                if (shallowCopyList is null)
                                {
                                    Console.WriteLine("Initialize shallowCopyList!");
                                    break;
                                }
                                AreLinksOfArraysEqual(mainList, shallowCopyList);
                                AreLinksOfItemsInArraysEqual(mainList, shallowCopyList);
                                break;
                            }
                        case (uint)Commands.MainIsDeep:
                            {
                                if (deepCopyList is null)
                                {
                                    Console.WriteLine("Initialize deepCopyList!");
                                    break;
                                }
                                AreLinksOfArraysEqual(mainList, deepCopyList);
                                AreLinksOfItemsInArraysEqual(mainList, deepCopyList);
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
        private Person CreateClassObject(string queryClass)
        {
            Person obj;
            switch (queryClass)
            {
                case "Person":
                    {
                        obj = new Person();
                        obj.RandomInit();
                        break;
                    }
                case "Employee":
                    {
                        obj = new Employee();
                        obj.RandomInit();
                        break;
                    }
                case "Engineer":
                    {
                        obj = new Engineer();
                        obj.RandomInit();
                        break;
                    }
                case "Admin":
                    {
                        obj = new Admin();
                        obj.RandomInit();
                        break;
                    }
                default:
                    {
                        obj = new Person();
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
        //Итерация по List<Person> с вызовом метода Show у каждого экземпляра
        private void ForEachEnum(List<Person> array)
        {
            foreach (Person obj in array)
            {
                Console.Write("----:----:----\n");
                obj.Show();
            }
        }
        //Глубокое копирование с возвратом нового List<Person>
        private List<Person> DeepCopy(List<Person> array)
        {
            List<Person> tempArrayList = new List<Person>();
            foreach (Person obj in array)
            {
                tempArrayList.Add((Person)obj.Clone());
            }
            return tempArrayList;
        }
        private bool AreLinksOfArraysEqual(List<Person> a, List<Person> b)
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
          1)Оригинального List<Person>
          2)У копии оригинала List<Person> (копирование List<Person> может быть как глубоким, так и поверхностным)
        */
        private bool AreLinksOfItemsInArraysEqual(List<Person> a, List<Person> b)
        {
            bool result = false;
            Person toCompareItem1 = a[0];
            Person toCompareItem2 = b[0];
            if (toCompareItem1.link == toCompareItem2.link)
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
