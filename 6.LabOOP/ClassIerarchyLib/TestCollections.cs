using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassIerarchyLib
{
    public class TestCollections
    {
        //Параметр кол-ва экземпляров для генерации
        private int count = 1000;
        //Обобщенные Коллекции
        public List<Employee> personList;
        public List<String> stringList;
        public Dictionary<Person, Employee> dictPE;
        public Dictionary<string, Employee> dictSE;
        //Конструктор
        public TestCollections() 
        {
            personList = new List<Employee>(count);
            stringList = new List<String>(count);
            dictPE = new Dictionary<Person, Employee>(count);
            dictSE = new Dictionary<string, Employee>(count);

            for (int i = 0; i < count; i++) 
            {
                var employee = new Employee();
                employee.RandomInit();
                personList.Add(employee);
                stringList.Add(employee.ToString());
                dictPE.Add(employee.basePerson(), employee);
                dictSE.Add(employee.ToString(), employee);
            }
        }
        //Операции работы над обобщенными коллекциями
        public void Add(Employee employee) 
        {
            personList.Add(employee);
            stringList.Add(employee.ToString());
            dictPE.Add(employee.basePerson(), employee);
            dictSE.Add(employee.ToString(), employee);
        }
        public void Remove(Employee employee) 
        {
            personList.Remove(employee);
            stringList.Remove(employee.ToString());
            dictPE.Remove(employee.basePerson());
            dictSE.Remove(employee.ToString());
        }
        //Метод тестирования производительности, возвращает string с результатами замеров
        public string TestPerformance() 
        {
            //Экземпляры для поиска
            Employee first = personList[0];
            first = (Employee)first.Clone();
            Employee middle = personList[count/2];
            middle = (Employee)middle.Clone();
            Employee last = personList[^1];
            last = (Employee)last.Clone();
            Employee absence = new Employee("KarlLongBearded", 48, "SpaceRig", 1001828, 16, 55000);

            //Коллекция с экземплярами для поиска
            List<Employee> searchList = new List<Employee>() { first, middle, last, absence };

            //Переменные для записи результатов
            string resultPL = "";
            string resultSL = "";
            string resultDPE = "";
            string resultDSE = "";
            uint counter = 1;
            //Header для понимания, какой тест за поиск какого экземпляра отвечает
            resultPL += $"1 - first | 2 - middle | 3 - last | 4 - absence\n";
            resultSL += $"1 - first | 2 - middle | 3 - last | 4 - absence\n";
            resultDPE += $"1 - first | 2 - middle | 3 - last | 4 - absence\n";
            resultDSE += $"1 - first | 2 - middle | 3 - last | 4 - absence\n";
            //Вызов тестов
            foreach (Employee emp in searchList) 
            {
                //Нумерация тестов в результате
                resultPL += $"{counter})\n";
                resultSL += $"{counter})\n";
                resultDPE += $"{counter})\n";
                resultDSE += $"{counter})\n";
                //Запись каждого из результатов поиска
                resultPL += MeasurePersonList(personList, emp);
                resultSL += MeasureStringList(stringList, emp.ToString());
                resultDPE += MeasureDictPE(dictPE, emp.basePerson());
                resultDSE += MeasureDictSE(dictSE, emp.ToString());
                //Обновление счетчика
                counter++;
            }
            //Возврат результата тестов
            string log = resultPL + resultSL + resultDPE + resultDSE;
            return log;
        }
        //Метод возвращает результат замера времени при поиске экземпляра Employee в List<Employee>
        private string MeasurePersonList(List<Employee> pL, Employee em) 
        {
            var sw = new Stopwatch();
            sw.Start();
            bool isFound = pL.Contains(em);
            sw.Stop();

            string result = $"pL: ";
            result += isFound ? "Found " : "notFound ";
            //result += $", time: {sw.Elapsed}\n";
            result += $", time: {sw.Elapsed.Ticks}\n";
            return result;
        }
        //Метод возвращает результат замера времени при поиске экземпляра string в List<string>
        private string MeasureStringList(List<string> sL, string st)
        {
            var sw = new Stopwatch();
            sw.Start();
            bool isFound = sL.Contains(st);
            sw.Stop();

            string result = $"sL: ";
            result += isFound ? "Found " : "notFound ";
            //result += $", time: {sw.Elapsed}\n";
            result += $", time: {sw.Elapsed.Ticks}\n";
            return result;
        }
        //Метод возвращает результат замера времени при поиске экземпляра по ключу Person в Dictionary<Person, Employee>
        private string MeasureDictPE(Dictionary<Person, Employee> dPE, Person personKey)
        {
            var sw = new Stopwatch();
            sw.Start();
            bool isFound = dPE.ContainsKey(personKey);
            sw.Stop();

            string result = $"dPE: ";
            result += isFound ? "Found " : "notFound ";
            //result += $", time: {sw.Elapsed}\n";
            result += $", time: {sw.Elapsed.Ticks}\n";
            return result;
        }
        //Метод возвращает результат замера времени при поиске экземпляра по ключу string в Dictionary<string, Employee>
        private string MeasureDictSE(Dictionary<string, Employee> dPE, string strKey)
        {
            var sw = new Stopwatch();
            sw.Start();
            bool isFound = dPE.ContainsKey(strKey);
            sw.Stop();

            string result = $"dPE: ";
            result += isFound ? "Found " : "notFound ";
            //result += $", time: {sw.Elapsed}\n";
            result += $", time: {sw.Elapsed.Ticks}\n";
            return result;
        }
    }
}
