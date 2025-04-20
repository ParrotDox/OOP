using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassIerarchyLib
{
    [Serializable]
    public class HashPoint<TKey, TVal>
    {
        public TKey Key { get; set; }
        public TVal Value { get; set; }
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
            Value = sample;
            this.Key = key;
            Link_to_next = null;
        }
        //Methods
        static public HashPoint<TKey, TVal> Create(TKey key, TVal sample)
        {
            HashPoint<TKey, TVal> temp = new HashPoint<TKey, TVal>(key, sample);
            return temp;
        }
    }
    [Serializable]
    public class CustomHashTable<TKey, TVal> : IDictionary<TKey, TVal>
    {
        public HashPoint<TKey, TVal>[] Table { get; set; }
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
        public virtual TVal this[TKey key] 
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
                TVal temp;
                bool isFound = TryGetValue(key, out temp);
                if(isFound)
                    temp = value;
            }
        }
        public ICollection<TKey> Keys 
        {
            get 
            {
                List<TKey> keys = new List<TKey>();
                foreach(HashPoint<TKey, TVal> hashpoint in Table) 
                {
                    HashPoint<TKey, TVal> cur_hashpoint = hashpoint;
                    while(cur_hashpoint != null) 
                    {
                        keys.Add(cur_hashpoint.Key);
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
                foreach (HashPoint<TKey, TVal> hashpoint in Table)
                {
                    HashPoint<TKey, TVal> cur_hashpoint = hashpoint;
                    while (cur_hashpoint != null)
                    {
                        values.Add(cur_hashpoint.Value);
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
                foreach (HashPoint<TKey, TVal> hashpoint in Table)
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
        public CustomHashTable()
        {
            this.Size = 10;
            Table = new HashPoint<TKey, TVal>[10];
        }
        public CustomHashTable(int capacity) 
        {
            this.Size = capacity;
            Table = new HashPoint<TKey, TVal>[Size];
        }

        //Methods
        public virtual void Add(TKey key, TVal sample) 
        {
            if (IsReadOnly)
                throw new InvalidOperationException("IsReadOnly: true. Collection is not changeable");

            if (sample == null || key == null)
                return;

            HashPoint<TKey, TVal> temp = HashPoint<TKey, TVal>.Create(key, sample);
            int index = Math.Abs(temp.Key.GetHashCode() % Size);
            if(Table[index] == null)
                Table[index] = temp;
            else 
            {
                HashPoint<TKey, TVal> cur_hashpoint = Table[index];

                while (cur_hashpoint.Link_to_next != null) 
                {
                    cur_hashpoint = cur_hashpoint.Link_to_next;
                }
                cur_hashpoint.Link_to_next = temp;
            }
            return;
        }
        public virtual void Add(KeyValuePair<TKey, TVal> sample) 
        {
            if (IsReadOnly)
                throw new InvalidOperationException("IsReadOnly: true. Collection is not changeable");

            Add(sample.Key, sample.Value);
        }
        public virtual bool ContainsKey(TKey key) 
        {
            if(Count == 0) return false;

            if(key is null)
                return false;

            int index = Math.Abs(key.GetHashCode() % Size);
            if (Table[index] == null)
                return false;

            HashPoint<TKey, TVal> cur_hashpoint = Table[index];
            if (cur_hashpoint.Key.Equals(key))
                return true;
            while (cur_hashpoint != null)
            {
                if (cur_hashpoint.Key.Equals(key))
                    return true;
                cur_hashpoint = cur_hashpoint.Link_to_next;
            }
            return false;
        }
        public virtual bool Contains(TVal sample) 
        {
            foreach(HashPoint<TKey, TVal> hashPoint in Table) 
            {
                HashPoint<TKey, TVal> cur_point = hashPoint;
                while(cur_point != null) 
                {
                    if(cur_point.Value.Equals(sample))
                        return true;
                    cur_point = cur_point.Link_to_next;
                }
            }
            return false;
        }
        public virtual bool Contains(KeyValuePair<TKey, TVal> sample) 
        {
            int index = Math.Abs(sample.Key.GetHashCode() % Size);
            if (Table[index] == null)
                return false;
            else
            {
                HashPoint<TKey, TVal> cur_hashpoint = Table[index];
                while (cur_hashpoint != null)
                {
                    if (cur_hashpoint.Value.Equals(sample.Value))
                        return true;
                    cur_hashpoint = cur_hashpoint.Link_to_next;
                }
                return false;
            }
        }
        public virtual bool Remove(TKey key) 
        {
            if (IsReadOnly)
                throw new InvalidOperationException("IsReadOnly: true. Collection is not changeable");

            int index = Math.Abs(key.GetHashCode() % Size);
            if (Table[index] == null)
                return false;
            else
            {
                HashPoint<TKey, TVal> cur_hashpoint = Table[index];
                HashPoint<TKey, TVal> prev_hashpoint = null;
                if(cur_hashpoint.Key.Equals(key) && cur_hashpoint.Link_to_next == null) 
                {
                    Table[index] = null;
                    return true;
                }
                if (cur_hashpoint.Key.Equals(key) && cur_hashpoint.Link_to_next != null)
                {
                    Table[index] = cur_hashpoint.Link_to_next;
                    return true;
                }
                while (cur_hashpoint != null) 
                {
                    if(cur_hashpoint.Key.Equals(key)) 
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
        public virtual bool Remove(KeyValuePair<TKey, TVal> pair_sample) 
        {
            if (IsReadOnly)
                throw new InvalidOperationException("IsReadOnly: true. Collection is not changeable");

            bool isRemoved = Remove(pair_sample.Key);
            return isRemoved;
        }
        public virtual void Clear() 
        {
            if (IsReadOnly)
                throw new InvalidOperationException("IsReadOnly: true. Collection is not changeable");

            Table = new HashPoint<TKey, TVal>[Size];
        }
        public virtual bool TryGetValue(TKey key, out TVal value)
        { 
            if(Size == 0) 
            {
                value = default;
                return false;
            }
            int index = Math.Abs(key.GetHashCode() % Size);
            HashPoint<TKey, TVal> cur_hashpoint = Table[index];
            while (cur_hashpoint != null) 
            {
                if(cur_hashpoint.Key.Equals(key)) 
                {
                    value = cur_hashpoint.Value;
                    return true;
                }
                cur_hashpoint = cur_hashpoint.Link_to_next;
            }
            value = default;
            return false;
        }
        public virtual void CopyTo(KeyValuePair<TKey, TVal>[] array, int array_index) 
        {
            if (array == null)
                throw new ArgumentNullException("array argument: not initialized");
            if (array_index < 0 || array_index >= array.Length)
                throw new ArgumentOutOfRangeException("array index argument: index is out of range");
            if (array_index + Count > array.Length)
                throw new ArgumentOutOfRangeException("copyTo: can't copy, remain array part < Collection.Count");

            if(Table == null)
                return;
            
            int index = array_index;
            foreach(HashPoint<TKey, TVal> hashPoint in Table) 
            {
                HashPoint<TKey, TVal> cur_hashpoint = hashPoint;
                //Going throught list of HashPoints
                while (cur_hashpoint != null) 
                {
                    //Creating new individual pair
                    KeyValuePair<TKey, TVal> pair = new KeyValuePair<TKey, TVal>(cur_hashpoint.Key, cur_hashpoint.Value);
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
                HashPoint<TKey, TVal> cur_hashpoint = Table[i];
                while (cur_hashpoint != null) 
                {
                    yield return new KeyValuePair<TKey, TVal>(cur_hashpoint.Key, cur_hashpoint.Value);
                    cur_hashpoint = cur_hashpoint.Link_to_next;
                }
            }
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            for (int i = 0; i < Size; ++i)
            {
                HashPoint<TKey, TVal> cur_hashpoint = Table[i];
                while (cur_hashpoint != null)
                {
                    yield return new KeyValuePair<TKey, TVal>(cur_hashpoint.Key, cur_hashpoint.Value);
                    cur_hashpoint = cur_hashpoint.Link_to_next;
                }
            }
        }
    }
}
