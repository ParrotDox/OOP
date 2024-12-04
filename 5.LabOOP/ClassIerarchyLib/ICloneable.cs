using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassIerarchyLib
{
    public interface ICloneable
    {
        //Реализовать поверхностное и глубокое клонирование (Создание ссылки на поля в стеке и создание дубликата полей в стеке)
        object Clone();
    }
}
