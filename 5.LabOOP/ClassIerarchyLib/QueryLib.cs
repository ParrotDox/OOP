using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//[Класс формирования запросов для демонстрации динамической идентификации]
namespace ClassIerarchyLib
{
    public static class QueryMaker
    {
        public static int companyLength = 10;
        public static Person[] company = new Person[companyLength];

        public static void InitCompanyArray() 
        {
            Random rnd = new Random();
            for (int i = 0; i < company.Length; ++i) 
            {
                int position = rnd.Next(0, 4);
                if (position == 0) 
                {
                    company[i] = new Person();
                }
                else if (position == 1)
                {
                    company[i] = new Employee();
                }
                else if (position == 2) 
                {
                    company[i] = new Engineer(); 
                }
                else if (position == 3) 
                {
                    company[i] = new Admin();
                }
                company[i].RandomInit();
            }
        }
        public static void ShowCompanyArray() 
        {
            foreach (Person person in company)
            {
                Console.WriteLine("-- : -- : -- : --");
                person.Show();
            }
        }
        //Использован is, полиморфизм учитывается, алгоритм работает от наследников к родителям,
        //чтобы не посчитать несколько раз 1 объект для унаследованных классов
        public static void ShowQuantityOfClassObjects() 
        {
            int persons, employees, engineers, admins;
            persons = 0;
            employees = 0;
            engineers = 0;
            admins = 0;

            foreach (dynamic person in company) 
            {
                if(person is Admin) 
                {
                    ++admins;
                    continue;
                }
                if (person is Engineer) 
                {
                    ++engineers;
                    continue;
                }
                if (person is Employee) 
                {
                    ++employees;
                    continue;
                }
                ++persons;
            }
            Console.WriteLine("[RESULTS]");
            Console.WriteLine($"Persons:{persons}");
            Console.WriteLine($"Employees:{employees}");
            Console.WriteLine($"Engineers:{engineers}");
            Console.WriteLine($"Admins:{admins}");
        }
        public static void ShowQuantityOfClassObjects(Person[] array)
        {
            int persons, employees, engineers, admins;
            persons = 0;
            employees = 0;
            engineers = 0;
            admins = 0;

            foreach (dynamic person in array)
            {
                if (person is Admin)
                {
                    ++admins;
                    continue;
                }
                if (person is Engineer)
                {
                    ++engineers;
                    continue;
                }
                if (person is Employee)
                {
                    ++employees;
                    continue;
                }
                ++persons;
            }
            Console.WriteLine("[RESULTS]");
            Console.WriteLine($"Persons:{persons}");
            Console.WriteLine($"Employees:{employees}");
            Console.WriteLine($"Engineers:{engineers}");
            Console.WriteLine($"Admins:{admins}");
        }
        //Использован as, полиморфизм учитывается, Engineer крайний класс в наследовании +
        //Унаследованные от него классы в случае чего тоже подойдут под условие "является инженером"
        public static void ShowQuantityOfEngineersOfChosenDepartment() 
        {
            string queryDepartment = ChooseDepartment();
            
            int engineers = 0;
            foreach (dynamic person in company)
            {
                var checkIfEngineer = person as Engineer;
                if (checkIfEngineer != null)
                {
                    if(checkIfEngineer.__Department__ == queryDepartment) 
                    {
                        Console.WriteLine("[MATCH TO YOUR REQUEST]");
                        checkIfEngineer.Show();
                        ++engineers;
                        continue;
                    }
                }
            }
            Console.WriteLine("[RESULTS]");
            Console.WriteLine($"{engineers} engineers work in {queryDepartment}");
        }
        public static void ShowQuantityOfEngineersOfChosenDepartment(Person[] array)
        {
            string queryDepartment = ChooseDepartment();

            int engineers = 0;
            foreach (dynamic person in array)
            {
                var checkIfEngineer = person as Engineer;
                if (checkIfEngineer != null)
                {
                    if (checkIfEngineer.__Department__ == queryDepartment)
                    {
                        Console.WriteLine("[MATCH TO YOUR REQUEST]");
                        checkIfEngineer.Show();
                        ++engineers;
                        continue;
                    }
                }
            }
            Console.WriteLine("[RESULTS]");
            Console.WriteLine($"{engineers} engineers work in {queryDepartment}");
        }
        //Использованы person.GetType() и typeOf(`argument`), полиморфизм не учитывается,
        //происходит строгая проверка под соответствие классу, ведь может быть выбран родительский класс "Employee",
        //тогда ошибочно будут учитываться все классы наследники (Engineer/Admin)
        public static int CalcTotalSalaryToPayByClassObject() 
        {
            string queryPosition = ChoosePosition();
            int totalSum = 0;
            if(queryPosition == "Employee") 
            {
                foreach (dynamic person in company)
                {
                    if (person.GetType() == typeof(Employee))
                    {
                        Console.WriteLine("[MATCH TO YOUR REQUEST]");
                        person.Show();
                        totalSum += person.__Salary__;
                        continue;
                    }
                }
            }
            else if (queryPosition == "Engineer")
            {
                foreach (dynamic person in company)
                {
                    if (person.GetType() == typeof(Engineer))
                    {
                        Console.WriteLine("[MATCH TO YOUR REQUEST]");
                        person.Show();
                        totalSum += person.__Salary__;
                        continue;
                    }
                }
            }
            else if (queryPosition == "Admin")
            {
                foreach (dynamic person in company)
                {
                    if (person.GetType() == typeof(Admin))
                    {
                        Console.WriteLine("[MATCH TO YOUR REQUEST]");
                        person.Show();
                        totalSum += person.__Salary__;
                        continue;
                    }
                }
            }
            Console.WriteLine("[RESULTS]");
            Console.WriteLine($"Total sum to pay is {totalSum}");
            return totalSum;
        }
        public static int CalcTotalSalaryToPayByClassObject(Person[] array)
        {
            string queryPosition = ChoosePosition();
            int totalSum = 0;
            if (queryPosition == "Employee")
            {
                foreach (dynamic person in array)
                {
                    if (person.GetType() == typeof(Employee))
                    {
                        Console.WriteLine("[MATCH TO YOUR REQUEST]");
                        person.Show();
                        totalSum += person.__Salary__;
                        continue;
                    }
                }
            }
            else if (queryPosition == "Engineer")
            {
                foreach (dynamic person in array)
                {
                    if (person.GetType() == typeof(Engineer))
                    {
                        Console.WriteLine("[MATCH TO YOUR REQUEST]");
                        person.Show();
                        totalSum += person.__Salary__;
                        continue;
                    }
                }
            }
            else if (queryPosition == "Admin")
            {
                foreach (dynamic person in array)
                {
                    if (person.GetType() == typeof(Admin))
                    {
                        Console.WriteLine("[MATCH TO YOUR REQUEST]");
                        person.Show();
                        totalSum += person.__Salary__;
                        continue;
                    }
                }
            }
            Console.WriteLine("[RESULTS]");
            Console.WriteLine($"Total sum to pay is {totalSum}");
            return totalSum;
        }
        //БИНАРНЫЙ ПОИСК
        public static int BinarySearch(List<Person> listToAnalyze, string targetName) 
        {
            //Левый, правый индексы
            int left = 0;
            int right = listToAnalyze.Count - 1;
            while (left <= right) 
            {
                int middle = (left + right) / 2;
                int comparison = string.Compare(targetName, listToAnalyze[middle].__Name__);
                Console.WriteLine($"{comparison} : {left} : {right} :{middle}");
                if (comparison == 0)
                {
                    Console.WriteLine("Found");
                    return middle;
                }
                    
                else if (comparison < 0)
                {
                    Console.WriteLine("Comparison is under zero");
                    right = middle - 1;
                }
                    
                else
                {
                    Console.WriteLine("Comparison is above zero");
                    left = middle + 1;
                }   
                Console.ReadLine();
            }
            return -1;
        }
        public static string ChooseDepartment() 
        {
            string queryDepartment = "";
            do
            {
                Console.WriteLine("--- DEPARTMENTS ---");
                for (int i = 0; i < Engineer.rndDepartments.Count; ++i)
                {
                    Console.WriteLine($"{i + 1}. {Engineer.rndDepartments[i]}");
                }
                Console.Write("Input department name:");
                queryDepartment = Console.ReadLine();
            } while (!Engineer.rndDepartments.Contains(queryDepartment));
            return queryDepartment;
        }
        public static string ChoosePosition() 
        {
            string queryPosition = "";
            string[] positionArray = new string[] {"Employee", "Engineer", "Admin" };
            do
            {
                Console.WriteLine("--- Job Positions ---");
                for (int i = 0; i < positionArray.Length; ++i)
                {
                    Console.WriteLine($"{i + 1}. {positionArray[i]}");
                }
                Console.Write("Input position name:");
                queryPosition = Console.ReadLine();
            } while (!positionArray.Contains(queryPosition));
            return queryPosition;
        }
    }
}
