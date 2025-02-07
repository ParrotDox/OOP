using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassIerarchyLib
{
    public class HashPoint 
    {
        public string key;
        public Person value;
        private HashPoint _link_to_next;
        public HashPoint Link_to_next
        {
            get { return _link_to_next; }
            set
            {
                if (value is HashPoint || value is null)
                {
                    _link_to_next = value;
                }
                else
                {
                    throw new ArgumentException("set _link_to_next: Wrong data type");
                }
            }
        }

        //Constructors
        public HashPoint(Person sample) 
        {
            value = sample;
            key = sample.ToString();
            Link_to_next = null;
        }
        public HashPoint(string key, Person sample) 
        {
            value = sample;
            this.key = key;
            Link_to_next = null;
        }
        //Methods
        static public HashPoint Create(Person sample) 
        {
            HashPoint temp = new HashPoint(sample);
            return temp;
        }
        static public HashPoint Create(string key, Person sample)
        {
            HashPoint temp = new HashPoint(key, sample);
            return temp;
        }
    }
    public class PersonHashTable : IDictionary<string, Person>
    {
        public HashPoint[] table;
        private int _size;
        public int Size 
        {
            set 
            {
                if (value < 0)
                    throw new ArgumentException("Set _size: size is under zero!");
                _size = value;
            }
            get { return _size; }
        }
        public Person this[string key] 
        {
            get 
            {
                Person temp;
                bool isFound = TryGetValue(key, out temp);
                if (isFound)
                    return temp;
                else
                    return null;
            }
            set 
            {
                Person temp;
                bool isFound = TryGetValue(key, out temp);
                if(isFound)
                    temp = value;
            }
        }
        public ICollection<string> Keys 
        {
            get 
            {
                List<string> keys = new List<string>();
                foreach(HashPoint hashpoint in table) 
                {
                    HashPoint cur_hashpoint = hashpoint;
                    while(cur_hashpoint != null) 
                    {
                        keys.Add(cur_hashpoint.key);
                        cur_hashpoint = cur_hashpoint.Link_to_next;
                    }
                }
                return keys;
            }
        }
        public ICollection<Person> Values
        {
            get
            {
                List<Person> values = new List<Person>();
                foreach (HashPoint hashpoint in table)
                {
                    HashPoint cur_hashpoint = hashpoint;
                    while (cur_hashpoint != null)
                    {
                        values.Add(cur_hashpoint.value);
                        cur_hashpoint = cur_hashpoint.Link_to_next;
                    }
                }
                return values;
            }
        }
        public int Count 
        {
            get 
            {
                int count = 0;
                foreach (HashPoint hashpoint in table)
                {
                    HashPoint cur_hashpoint = hashpoint;
                    while (cur_hashpoint != null)
                    {
                        ++count;
                        cur_hashpoint = cur_hashpoint.Link_to_next;
                    }
                }
                return count;
            }
        }
        private bool _is_read_only;
        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        //Constructors
        public PersonHashTable(int capacity) 
        {
            this.Size = capacity;
            table = new HashPoint[Size];
        }

        //Methods
        public void Add(string key, Person sample) 
        {
            if (sample == null || key == "")
                return;

            HashPoint temp = HashPoint.Create(key, sample);
            int index = Math.Abs(temp.key.GetHashCode() % Size);
            if(table[index] == null)
                table[index] = temp;
            else 
            {
                HashPoint cur_hashpoint = table[index];

                while (cur_hashpoint.Link_to_next != null) 
                {
                    cur_hashpoint = cur_hashpoint.Link_to_next;
                }
                cur_hashpoint.Link_to_next = temp;
            }
            return;
        }
        public void Add(KeyValuePair<string, Person> sample) 
        {
            Add(sample.Key, sample.Value);
        }
        public bool ContainsKey(string key) 
        {
            int index = Math.Abs(key.GetHashCode() % Size);
            if(table[index] == null)
                return false;
            else 
            {
                HashPoint cur_hashpoint = table[index];
                if(cur_hashpoint.key == key)
                    return true;
                while(cur_hashpoint != null) 
                {
                    if (cur_hashpoint.key == key)
                        return true;
                    cur_hashpoint = cur_hashpoint.Link_to_next;
                }
                return false;
            }
        }
        public bool Contains(Person sample) 
        {
            foreach(HashPoint hashPoint in table) 
            {
                HashPoint cur_point = hashPoint;
                while(cur_point != null) 
                {
                    if(cur_point.value.Equals(sample))
                        return true;
                    cur_point = cur_point.Link_to_next;
                }
            }
            return false;
        }
        public bool Contains(KeyValuePair<string, Person> sample) 
        {
            int index = Math.Abs(sample.Key.GetHashCode() % Size);
            if (table[index] == null)
                return false;
            else
            {
                HashPoint cur_hashpoint = table[index];
                while (cur_hashpoint != null)
                {
                    if (cur_hashpoint.value.Equals(sample.Value))
                        return true;
                    cur_hashpoint = cur_hashpoint.Link_to_next;
                }
                return false;
            }
        }
        public bool Remove(string key) 
        {
            int index = Math.Abs(key.GetHashCode() % Size);
            if (table[index] == null)
                return false;
            else
            {
                HashPoint cur_hashpoint = table[index];
                HashPoint prev_hashpoint;
                if(cur_hashpoint.key == key && cur_hashpoint.Link_to_next == null) 
                {
                    table[index] = null;
                    return true;
                }
                if (cur_hashpoint.key == key && cur_hashpoint.Link_to_next != null)
                {
                    table[index] = cur_hashpoint.Link_to_next;
                    return false;
                }
                while (cur_hashpoint != null) 
                {
                    if(cur_hashpoint.key == key) 
                    {
                        prev_hashpoint = cur_hashpoint.Link_to_next;
                        cur_hashpoint = null;
                        return true;
                    }
                    prev_hashpoint = cur_hashpoint;
                    cur_hashpoint = cur_hashpoint.Link_to_next;
                }
                return false;
            }
        }
        public bool Remove(KeyValuePair<string, Person> pair_sample) 
        {
            bool isRemoved = Remove(pair_sample.Key);
            return isRemoved;
        }
        public void Clear() 
        {
            table = new HashPoint[Size];
        }
        public bool TryGetValue(string key, out Person value)
        { 
            int index = Math.Abs(key.GetHashCode() % Size);
            HashPoint cur_hashpoint = table[index];

            if (cur_hashpoint == null) 
            {
                value = null;
                return false;
            }
            while (cur_hashpoint != null) 
            {
                if(cur_hashpoint.key == key) 
                {
                    value = cur_hashpoint.value;
                    return true;
                }
                cur_hashpoint = cur_hashpoint.Link_to_next;
            }
            value = null;
            return false;
        }
        public void CopyTo(KeyValuePair<string, Person>[] array, int array_index) 
        {
            if (array == null)
                throw new ArgumentNullException("array argument: not initialized");
            if (array_index < 0 || array_index >= array.Length)
                throw new ArgumentOutOfRangeException("array index argument: index is out of range");
            if (array_index + Count > array.Length)
                throw new ArgumentOutOfRangeException("copyTo: can't copy, remain array part < Collection.Count");

            if(table == null)
                return;
            
            int index = array_index;
            foreach(HashPoint hashPoint in table) 
            {
                HashPoint cur_hashpoint = hashPoint;
                //Going throught list of HashPoints
                while (cur_hashpoint != null) 
                {
                    //Creating new individual pair
                    KeyValuePair<string, Person> pair = new KeyValuePair<string, Person>(cur_hashpoint.key, cur_hashpoint.value);
                    array[index] = pair;
                    ++index;
                    cur_hashpoint = cur_hashpoint.Link_to_next;
                }
            }
        }
        IEnumerator<KeyValuePair<string, Person>> IEnumerable<KeyValuePair<string, Person>>.GetEnumerator()
        {
            return new PersonHashTableEnumerator(this);
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return new PersonHashTableEnumerator(this);
        }
    }
}
