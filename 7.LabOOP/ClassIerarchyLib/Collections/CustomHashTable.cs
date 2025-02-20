using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassIerarchyLib
{
    public class HashPoint<TKey, TVal>
    {
        public TKey key;
        public TVal value;
        private HashPoint<TKey, TVal> _link_to_next;
        public HashPoint<TKey, TVal> Link_to_next
        {
            get { return _link_to_next; }
            set
            {
                if (value is HashPoint<TKey, TVal> || value is null)
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
        public HashPoint(TKey key, TVal sample) 
        {
            value = sample;
            this.key = key;
            Link_to_next = null;
        }
        //Methods
        static public HashPoint<TKey, TVal> Create(TKey key, TVal sample)
        {
            HashPoint<TKey, TVal> temp = new HashPoint<TKey, TVal>(key, sample);
            return temp;
        }
    }
    public class CustomHashTable<TKey, TVal> : IDictionary<TKey, TVal>
    {
        public HashPoint<TKey, TVal>[] table;
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
        public TVal this[TKey key] 
        {
            get 
            {
                TVal temp;
                bool isFound = TryGetValue(key, out temp);
                if (isFound)
                    return temp;
                else
                    return default;
            }
            set 
            {
                if (IsReadOnly)
                    throw new InvalidOperationException("IsReadOnly: true. Collection is not changeable");

                if (value == null || key == null)
                    return;
                Add(key, value);
            }
        }
        public ICollection<TKey> Keys 
        {
            get 
            {
                List<TKey> keys = new List<TKey>();
                foreach(HashPoint<TKey, TVal> hashpoint in table) 
                {
                    HashPoint<TKey, TVal> cur_hashpoint = hashpoint;
                    while(cur_hashpoint != null) 
                    {
                        keys.Add(cur_hashpoint.key);
                        cur_hashpoint = cur_hashpoint.Link_to_next;
                    }
                }
                return keys;
            }
        }
        public ICollection<TVal> Values
        {
            get
            {
                List<TVal> values = new List<TVal>();
                foreach (HashPoint<TKey, TVal> hashpoint in table)
                {
                    HashPoint<TKey, TVal> cur_hashpoint = hashpoint;
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
                foreach (HashPoint<TKey, TVal> hashpoint in table)
                {
                    HashPoint<TKey, TVal> cur_hashpoint = hashpoint;
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
            set 
            {
                _is_read_only = value;
            }
        }

        //Constructors
        public CustomHashTable(int capacity) 
        {
            this.Size = capacity;
            table = new HashPoint<TKey, TVal>[Size];
        }

        //Methods
        public void Add(TKey key, TVal sample) 
        {
            if (IsReadOnly)
                throw new InvalidOperationException("IsReadOnly: true. Collection is not changeable");

            if (sample == null || key == null)
                return;

            HashPoint<TKey, TVal> temp = HashPoint<TKey, TVal>.Create(key, sample);
            int index = Math.Abs(temp.key.GetHashCode() % Size);
            if(table[index] == null)
                table[index] = temp;
            else 
            {
                HashPoint<TKey, TVal> cur_hashpoint = table[index];

                while (cur_hashpoint.Link_to_next != null) 
                {
                    cur_hashpoint = cur_hashpoint.Link_to_next;
                }
                cur_hashpoint.Link_to_next = temp;
            }
            return;
        }
        public void Add(KeyValuePair<TKey, TVal> sample) 
        {
            if (IsReadOnly)
                throw new InvalidOperationException("IsReadOnly: true. Collection is not changeable");

            Add(sample.Key, sample.Value);
        }
        public bool ContainsKey(TKey key) 
        {
            if(Count == 0) return false;

            if(key is null)
                return false;

            int index = Math.Abs(key.GetHashCode() % Size);
            if (table[index] == null)
                return false;

            HashPoint<TKey, TVal> cur_hashpoint = table[index];
            if (cur_hashpoint.key.Equals(key))
                return true;
            while (cur_hashpoint != null)
            {
                if (cur_hashpoint.key.Equals(key))
                    return true;
                cur_hashpoint = cur_hashpoint.Link_to_next;
            }
            return false;
        }
        public bool Contains(TVal sample) 
        {
            foreach(HashPoint<TKey, TVal> hashPoint in table) 
            {
                HashPoint<TKey, TVal> cur_point = hashPoint;
                while(cur_point != null) 
                {
                    if(cur_point.value.Equals(sample))
                        return true;
                    cur_point = cur_point.Link_to_next;
                }
            }
            return false;
        }
        public bool Contains(KeyValuePair<TKey, TVal> sample) 
        {
            int index = Math.Abs(sample.Key.GetHashCode() % Size);
            if (table[index] == null)
                return false;
            else
            {
                HashPoint<TKey, TVal> cur_hashpoint = table[index];
                while (cur_hashpoint != null)
                {
                    if (cur_hashpoint.value.Equals(sample.Value))
                        return true;
                    cur_hashpoint = cur_hashpoint.Link_to_next;
                }
                return false;
            }
        }
        public bool Remove(TKey key) 
        {
            if (IsReadOnly)
                throw new InvalidOperationException("IsReadOnly: true. Collection is not changeable");

            int index = Math.Abs(key.GetHashCode() % Size);
            if (table[index] == null)
                return false;
            else
            {
                HashPoint<TKey, TVal> cur_hashpoint = table[index];
                HashPoint<TKey, TVal> prev_hashpoint = null;
                if(cur_hashpoint.key.Equals(key) && cur_hashpoint.Link_to_next == null) 
                {
                    table[index] = null;
                    return true;
                }
                if (cur_hashpoint.key.Equals(key) && cur_hashpoint.Link_to_next != null)
                {
                    table[index] = cur_hashpoint.Link_to_next;
                    return true;
                }
                while (cur_hashpoint != null) 
                {
                    if(cur_hashpoint.key.Equals(key)) 
                    {
                        prev_hashpoint.Link_to_next = cur_hashpoint.Link_to_next;
                        cur_hashpoint = null;
                        return true;
                    }
                    prev_hashpoint = cur_hashpoint;
                    cur_hashpoint = cur_hashpoint.Link_to_next;
                }
                return false;
            }
        }
        public bool Remove(KeyValuePair<TKey, TVal> pair_sample) 
        {
            if (IsReadOnly)
                throw new InvalidOperationException("IsReadOnly: true. Collection is not changeable");

            bool isRemoved = Remove(pair_sample.Key);
            return isRemoved;
        }
        public void Clear() 
        {
            if (IsReadOnly)
                throw new InvalidOperationException("IsReadOnly: true. Collection is not changeable");

            table = new HashPoint<TKey, TVal>[Size];
        }
        public bool TryGetValue(TKey key, out TVal value)
        { 
            if(Size == 0) 
            {
                value = default;
                return false;
            }
            int index = Math.Abs(key.GetHashCode() % Size);
            HashPoint<TKey, TVal> cur_hashpoint = table[index];
            while (cur_hashpoint != null) 
            {
                if(cur_hashpoint.key.Equals(key)) 
                {
                    value = cur_hashpoint.value;
                    return true;
                }
                cur_hashpoint = cur_hashpoint.Link_to_next;
            }
            value = default;
            return false;
        }
        public void CopyTo(KeyValuePair<TKey, TVal>[] array, int array_index) 
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
            foreach(HashPoint<TKey, TVal> hashPoint in table) 
            {
                HashPoint<TKey, TVal> cur_hashpoint = hashPoint;
                //Going throught list of HashPoints
                while (cur_hashpoint != null) 
                {
                    //Creating new individual pair
                    KeyValuePair<TKey, TVal> pair = new KeyValuePair<TKey, TVal>(cur_hashpoint.key, cur_hashpoint.value);
                    array[index] = pair;
                    ++index;
                    cur_hashpoint = cur_hashpoint.Link_to_next;
                }
            }
        }
        IEnumerator<KeyValuePair<TKey, TVal>> IEnumerable<KeyValuePair<TKey, TVal>>.GetEnumerator()
        {
            for(int i = 0; i < Size; ++i) 
            {
                HashPoint<TKey, TVal> cur_hashpoint = table[i];
                while (cur_hashpoint != null) 
                {
                    yield return new KeyValuePair<TKey, TVal>(cur_hashpoint.key, cur_hashpoint.value);
                    cur_hashpoint = cur_hashpoint.Link_to_next;
                }
            }
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            for (int i = 0; i < Size; ++i)
            {
                HashPoint<TKey, TVal> cur_hashpoint = table[i];
                while (cur_hashpoint != null)
                {
                    yield return new KeyValuePair<TKey, TVal>(cur_hashpoint.key, cur_hashpoint.value);
                    cur_hashpoint = cur_hashpoint.Link_to_next;
                }
            }
        }
    }
}
