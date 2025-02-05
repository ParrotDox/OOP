using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ClassIerarchyLib
{
    public class Point
    {
        private Person _info;
        public Person Info 
        {
            get { return _info; }
            set
            {
                if (value is Person)
                    _info = value;
                else
                    throw new ArgumentException("set _info: Wrong data type or nullable data");
            }
        }
        private Point _link_to_next;
        public Point Link_to_next 
        {
            get { return _link_to_next;  }
            set
            {
                if(value is Point || value is null) 
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
        public Point() 
        {
            Info = new Person();
            Link_to_next = null;
        }
        public Point(Person info_sample) 
        {
            Info = (Person)info_sample.Clone();
            Link_to_next = null;
        }

        //Create and return point object
        //Empty
        static public Point Create() 
        {
            Point point = new Point();
            return point;
        }
        //Based on data
        static public Point Create(Person person_sample)
        {
            Point point = new Point(person_sample);
            return point;
        }
    }

    public class PersonList: ICollection<Person>
    {
        public Point beg;
        public int Count 
        {
            get 
            {
                if (beg == null)
                    return 0;
                
                Point cur_point = beg;
                int ctr = 1;
                while (cur_point.Link_to_next != null) 
                {
                    cur_point = cur_point.Link_to_next;
                    ctr++;
                }
                return ctr;
            }
        }
        bool isReadOnly;
        public bool IsReadOnly 
        {
            get 
            {
                return false;
            }
        }
        //Constructors
        public PersonList() 
        {
            beg = null;
        }
        public PersonList(uint capacity) 
        {
            if (capacity == 0)
            {
                beg = null;
            }
            else
            {
                Point sample = Point.Create();
                sample.Info.RandomInit();

                if (capacity == 1)
                {
                    beg = sample;
                }
                else if (capacity > 1)
                {
                    beg = sample;
                    //cur_list_point is used to iterate through list
                    Point cur_list_point = beg;
                    for (int c = 1; c < capacity; c++)
                    {
                        while (cur_list_point.Link_to_next != null)
                            cur_list_point = cur_list_point.Link_to_next;

                        sample = Point.Create();
                        sample.Info.RandomInit();

                        cur_list_point.Link_to_next = sample;
                    }
                }
            }
        }
        public PersonList(PersonList pointList_sample) 
        {
            if(pointList_sample.Count == 0) 
            {
                beg = null;
            }
            else if(pointList_sample.Count == 1) 
            {
                Point sample = Point.Create(pointList_sample.beg.Info);
                beg = sample;
            }
            else if(pointList_sample.Count > 1) 
            {
                Point sample = Point.Create(pointList_sample.beg.Info);
                beg = sample;
                //temp is used to iterate through creating list
                Point cur_list_point = beg;
                //temp_sample is used to iterate through pointList_sample
                Point cur_sample_point = pointList_sample.beg;
                for(int c = 1; c < Count; ++c)
                {
                    while (cur_list_point.Link_to_next != null) 
                    {
                        cur_list_point = cur_list_point.Link_to_next;
                        cur_sample_point = cur_sample_point.Link_to_next;
                    }

                    sample = Point.Create(cur_sample_point.Info);

                    cur_list_point.Link_to_next = sample;
                }
            }
        }
        //Create and return PointList object
        //Empty
        static public PersonList Create() 
        {
            PersonList pointList = new PersonList();
            return pointList;
        }
        //With specified length
        static public PersonList Create(uint capacity)
        {
            PersonList pointList = new PersonList(capacity);
            return pointList;
        }
        
        //ICollection methods
        public void Add(Person sample) 
        {
            Point cur_point = beg;
            if(beg == null) 
            {
                beg = Point.Create(sample);
            }
            else 
            {
                while (cur_point.Link_to_next != null)
                    cur_point = cur_point.Link_to_next;
                cur_point.Link_to_next = Point.Create(sample);
            }
        }
        public void Clear() 
        {
            beg = null;
        }
        public bool Contains(Person sample) 
        {
            //Checking using Equals
            bool isEquals = false;
            
            Point cur_point = beg;
            while (cur_point.Link_to_next != null) 
            {
                if(sample.Equals(cur_point.Info)) 
                {
                    isEquals = true;
                    break;
                }
                cur_point = cur_point.Link_to_next;
            }

            return isEquals;
        }
        public void CopyTo(Person[] array, int arrayIndex) 
        {
            if (array == null)
                throw new ArgumentNullException("array argument: not initialized");
            if (arrayIndex < 0 || arrayIndex >= array.Length)
                throw new ArgumentOutOfRangeException("arrayIndex argument: index is out of range");
            if(arrayIndex + Count > array.Length)
                throw new ArgumentOutOfRangeException("copyTo: can't copy, remain array part < Collection.Count");

            Point cur_point = beg;
            for (int i = 0; i < Count; ++i) 
            {
                array[arrayIndex + i] = cur_point.Info;
                cur_point = cur_point.Link_to_next;
            }
        }
        public bool Remove(Person sample) 
        {
            Point cur_point = beg;
            Point prev_point = null;

            // Seeking queue-point
            while (cur_point != null && !cur_point.Info.Equals(sample))
            {
                prev_point = cur_point;
                cur_point = cur_point.Link_to_next;
            }

            // If haven't found
            if (cur_point == null)
            {
                return false;
            }

            // If queue-point at beggining
            if (prev_point == null)
            {
                beg = cur_point.Link_to_next;
            }
            else
            {
                prev_point.Link_to_next = cur_point.Link_to_next; // Reconnecting links
            }

            return true;
        }
        IEnumerator<Person> IEnumerable<Person>.GetEnumerator() 
        {
            return new CustomEnumerator(this);
        }
        IEnumerator IEnumerable.GetEnumerator() 
        {
            return new CustomEnumerator(this);
        }
    }
}
