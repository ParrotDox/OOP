using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ClassIerarchyLib
{
    public class PersonHashTableEnumerator: IEnumerator<KeyValuePair<string, Person>>
    {
        private HashPoint[] _beg;
        private HashPoint _cur;
        private int _index;
        public KeyValuePair<string, Person> Current 
        {
            get { return new KeyValuePair<string, Person>(_cur.key, _cur.value); }
        }
        object IEnumerator.Current => Current;
        //Constructors
        public PersonHashTableEnumerator(PersonHashTable personHashTable) 
        {
            _beg = personHashTable.table;
            _cur = null;
            _index = -1;
        }

        //Methods
        public bool MoveNext()
        {
            if (_index == -1 && _beg[0] != null)
            {
                ++_index;
                _cur = _beg[0];
                return true;
            }
            if (_cur.Link_to_next != null) 
            {
                _cur = _cur.Link_to_next;
                return true;
            }
            if (_beg[_index + 1] != null) 
            {
                _cur = _beg[_index + 1];
                return true;
            }
            Reset();
            return false;
        }
        public void Reset() 
        {
            if(_beg == null) 
            {
                _cur = null;
            }
            else 
            {
                _cur = _beg[0];
            }
            _index = -1;
        }
        public void Dispose()
        {

        }
    }
}
