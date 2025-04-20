using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassIerarchyLib
{
    //Enum для меню
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
}
