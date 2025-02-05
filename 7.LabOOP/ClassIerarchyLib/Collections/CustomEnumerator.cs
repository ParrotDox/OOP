using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassIerarchyLib
{
    public class CustomEnumerator: IEnumerator<Person>
    {
        private Point _beg;
        private Point _cur;
        private bool _firstStep;
        public Person Current 
        {
            get { return _cur.Info; }
        }
        object IEnumerator.Current => Current;

        //Constructors
        public CustomEnumerator(PersonList pointList)
        {
            _beg = pointList.beg;
            _cur = null;
            _firstStep = false;
        }
        //Methods
        public bool MoveNext() 
        {
            if (_beg == null)
                return false;

            if (_firstStep == false && _beg != null) 
            {
                _cur = _beg;
                _firstStep = true;
                return true;
            }
            if (_cur.Link_to_next != null)
            {
                _cur = _cur.Link_to_next;
                return true;
            }
            else 
            {
                Reset();
                return false;
            } 
        }
        public void Reset() 
        {
            _cur = null;
            _firstStep = false;
        }
        public void Dispose() 
        {
            
        }
    }
}
