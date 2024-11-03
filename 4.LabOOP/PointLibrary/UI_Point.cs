using System.Linq.Expressions;

namespace PointLibrary
{
    enum Commands 
    {
        Continue = 100,
        Enter = 200,

        Print = 0,
        InitArrayM = 1,
        InitArrayR = 2,
        InitArrayE = 3,
        ShowStaticCtrs = 4,
        InitPointOne = 5,
        InitPointTwo = 6,
        IncreaseX = 7,
        DecreaseX = 8,
        TypeCastIntExplicit = 9,
        TypeCastDoubleImplicit = 10,
        FindDistance = 11,
        AddIntValue = 12,
        Exit = 13,
    }
    //[UI для демонстрации программы]
    public class UI_Point
    {
        //Переменные для вызова комманд
        private uint commandKey = 0;
        private uint commandAmount = 14;
        //Переменные для записи данных
        Point pointOne;
        Point pointTwo;
        PointArray arrayManual;
        PointArray arrayRandom;
        PointArray arrayEmpty;
        public void Execute()
        {
            Start:
            try 
            {
                bool exitFlag = false;
                Commands request = Commands.Print;
                while(!exitFlag) 
                {
                    do
                    {
                        Console.Clear();
                        ShowMenu(commandKey);
                        request = KeyboardHandler(Console.ReadKey().KeyChar);
                    } while (request != Commands.Enter);
                    switch (commandKey)
                    {
                        //Вывод всех переменных класса UI_Point
                        case (uint)Commands.Print:
                            {
                                pointOne.Print();
                                Console.Write('\n');
                                pointTwo.Print();
                                Console.Write('\n');
                                arrayManual.Print();
                                Console.Write('\n');
                                arrayRandom.Print();
                                Console.Write('\n');
                                arrayEmpty.Print();
                                break;
                            }
                        case (uint)Commands.InitArrayM:
                            {
                                uint len = InputUInt();
                                arrayManual = new PointArray(len, true);
                                //Наглядный вывод
                                arrayManual.Print();
                                break;
                            }
                        case (uint)Commands.InitArrayR:
                            {
                                uint len = InputUInt();
                                arrayRandom = new PointArray(len, false);
                                //Наглядный вывод
                                arrayRandom.Print();
                                break;
                            }
                        case (uint)Commands.InitArrayE:
                            {
                                //Для примера будет создаваться пустой массив
                                arrayEmpty = new PointArray();
                                //Наглядный вывод
                                arrayEmpty.Print();
                                break;
                            }
                        case (uint)Commands.ShowStaticCtrs:
                            {
                                Console.WriteLine("Points:");
                                Console.WriteLine(PointStatic.pointCounter);
                                Console.WriteLine("PointArrays:");
                                Console.WriteLine(PointStatic.pointArrayCounter);
                                break;
                            }
                        case (uint)Commands.InitPointOne:
                            {
                                double coordX = InputDouble();
                                double coordY = InputDouble();
                                pointOne = new Point(coordX, coordY);
                                pointOne.Print();
                                break;
                            }
                        case (uint)Commands.InitPointTwo:
                            {
                                double coordX = InputDouble();
                                double coordY = InputDouble();
                                pointTwo = new Point(coordX, coordY);
                                pointTwo.Print();
                                break;
                            }
                        case (uint)Commands.IncreaseX:
                            {
                                Console.WriteLine("Before ++ operation");
                                pointOne.Print();
                                ++pointOne;
                                Console.WriteLine("After ++ operation");
                                pointOne.Print();
                                break;
                            }
                        case (uint)Commands.DecreaseX:
                            {
                                Console.WriteLine("Before -- operation");
                                pointOne.Print();
                                --pointOne;
                                Console.WriteLine("After -- operation");
                                pointOne.Print();
                                break;
                            }
                        case (uint)Commands.TypeCastIntExplicit:
                            {
                                pointOne.Print();
                                int x = (int)pointOne;
                                Console.WriteLine($"INT x value: {x}");
                                break;
                            }
                        case (uint)Commands.TypeCastDoubleImplicit:
                            {
                                pointOne.Print();
                                double y = pointOne;
                                Console.WriteLine($"DOUBLE y value: {y}");
                                break;
                            }
                        case (uint)Commands.FindDistance:
                            {
                                Console.WriteLine("[ POINT ONE INPUT ]");
                                double coordX = InputDouble();
                                double coordY = InputDouble();
                                Point tempPoint1 = new Point(coordX, coordY);
                                tempPoint1.Print();

                                Console.WriteLine("[ POINT TWO INPUT ]");
                                coordX = InputDouble();
                                coordY = InputDouble();
                                Point tempPoint2 = new Point(coordX, coordY);
                                tempPoint2.Print();

                                Console.WriteLine("[ (POINT CLASS) OVERRIDE+ /// (STATIC CLASS) FUNC ]");
                                Console.WriteLine("(POINT CLASS)");
                                double result = tempPoint1 + tempPoint2;
                                Console.WriteLine($"Distance between two points: {result}");
                                Console.WriteLine("(STATIC CLASS)");
                                result = PointStatic.FindDistance(tempPoint1, tempPoint2);
                                Console.WriteLine($"Distance between two points: {result}");
                                break;
                            }
                        case (uint)Commands.AddIntValue:
                            {
                                Console.WriteLine("[ POINT ONE INPUT ]");
                                double coordX = InputDouble();
                                double coordY = InputDouble();
                                Point tempPoint1 = new Point(coordX, coordY);
                                tempPoint1.Print();
                                Console.WriteLine("[ INT VAL. INPUT ]");
                                int x = InputInt();
                                Console.WriteLine("[ (1) = POINT + INT /// (2) = INT + POINT ]");
                                Console.WriteLine("(1)");
                                Point result = tempPoint1 + x;
                                result.Print();
                                Console.WriteLine("(2)");
                                result = x + tempPoint1;
                                result.Print();
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
            catch(NullReferenceException e) 
            {
                Console.WriteLine("NullReferenceError:");
                Console.WriteLine("Initialize all points and arrays!");
                Console.ReadKey();
                goto Start;
            }
            catch(Exception e) 
            {
                Console.WriteLine(e);
                Console.ReadKey();
                goto Start;
            }
        }
        private int InputInt()
        {
            bool isInt;
            int n;
            do
            {
                Console.WriteLine("Enter val.(INT):");
                isInt = int.TryParse(Console.ReadLine(), out n);
                if (!isInt)
                {
                    throw new Exception("Unexpected type of data");
                }
            } while (!isInt);
            return n;
        }
        private uint InputUInt() 
        {
            bool isInt;
            int n;
            do
            {
                Console.WriteLine("Enter val.(value > 0)(UINT):");
                isInt = int.TryParse(Console.ReadLine(), out n);
                if (!isInt)
                {
                    throw new Exception("Unexpected type of data");
                }
                if (n < 0)
                {
                    throw new Exception("Input is < 0");
                }
            } while (!isInt || n < 0);
            return (uint)n;
        }
        private double InputDouble()
        {
            bool isDouble;
            double n;
            do
            {
                Console.WriteLine("Enter val.(DOUBLE):");
                isDouble = double.TryParse(Console.ReadLine(), out n);
                if (!isDouble)
                {
                    throw new Exception("Unexpected type of data");
                }
            } while (!isDouble);
            return n;
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
            else if(key == 115 && commandKey < commandAmount - 1) 
            {
                ++commandKey;
                return Commands.Continue;
            }
            //Enter
            else if(key == 13) 
            {
                
                return Commands.Enter;
            }
            //Other keyChars
            else 
            {
                return Commands.Continue;
            }
        }
        private void ShowMenu(uint key) 
        {
        //Print = 0,
        //InitArrayM = 1,
        //InitArrayR = 2,
        //InitArrayE = 3,
        //ShowStaticCtrs = 4,
        //InitPointOne = 5,
        //InitPointTwo = 6,
        //IncreaseX = 7,
        //DecreaseX = 8,
        //TypeCastIntExplicit = 9,
        //TypeCastDoubleImplicit = 10,
        //FindDistance = 11,
        //AddIntValue = 12,
        //Exit = 13,
            string menuInfo = key == 0 ? "Print everything <--" : "Print everything";
            menuInfo += '\n';
            menuInfo += key == 1 ? "Initialize array manualy <--" : "Initialize array manualy";
            menuInfo += '\n';
            menuInfo += key == 2 ? "Initialize array randomly <--" : "Initialize array randomly";
            menuInfo += '\n';
            menuInfo += key == 3 ? "Initialize empty array <--" : "Initialize empty array";
            menuInfo += '\n';
            menuInfo += key == 4 ? "Show static class counters <--" : "Show static class counters";
            menuInfo += '\n';
            menuInfo += key == 5 ? "Initialize point one <--" : "Initialize point one";
            menuInfo += '\n';
            menuInfo += key == 6 ? "Initialize point two <--" : "Initialize point two";
            menuInfo += '\n';
            menuInfo += key == 7 ? "Increase point X coordinate <--" : "Increase point X coordinate";
            menuInfo += '\n';
            menuInfo += key == 8 ? "Decrease point X coordinate <--" : "Increase point X coordinate";
            menuInfo += '\n';
            menuInfo += key == 9 ? "Type cast INT explicit <--" : "Type cast INT explicit";
            menuInfo += '\n';
            menuInfo += key == 10 ? "Type cast DOUBLE implicit <--" : "Type cast DOUBLE implicit";
            menuInfo += '\n';
            menuInfo += key == 11 ? "Find distance between points <--" : "Find distance between points";
            menuInfo += '\n';
            menuInfo += key == 12 ? "Add INT value to point <--" : "Add INT value to point";
            menuInfo += '\n';
            menuInfo += key == 13 ? "Exit <--" : "Exit";
            menuInfo += '\n';
            Console.WriteLine(menuInfo);
        }
    }
}
