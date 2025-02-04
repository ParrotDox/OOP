using System;
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
                if(value is Person) 
                {
                    _link_to_next = value;
                }
                else 
                {
                    throw new ArgumentException("set _link_to_next: Wrong data type or nullable data");
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

    public class PointList: ICollection<Point>
    {
        private Point _beg;
        private Point _end;
        private uint _capacity;
        private uint Capacity 
        {
            set 
            {
                if (value == 0)
                    throw new ArgumentException("set _capacity: can't set capacity = 0, use constructor without parameters");
            }
            get { return _capacity; }
        }
        private int _count;
        public int Count 
        {
            get 
            {
                if (_beg == null)
                    return 0;
                
                Point cur_point = _beg;
                int ctr = 1;
                while (cur_point != null) 
                {
                    cur_point = cur_point.Link_to_next;
                    ctr++;
                }
                return ctr;
            }
        }
        //Constructors
        public PointList() 
        {
            _beg = null;
            _end = null;
            _capacity = 0;
        }
        public PointList(uint capacity) 
        {
            //Checking if atribute correct using setter
            Capacity = capacity;

            Point sample = Point.Create();
            sample.Info.RandomInit();

            //capacity = 1
            if (capacity == 1) 
            {
                _beg = sample;
                _end = sample;
            }
            else if (capacity > 1)
            {
                _beg = sample;
                //Temp is used to iterate through list
                Point cur_list_point = _beg;
                for(int c = 1; c < capacity; c++) 
                {
                    while (cur_list_point.Link_to_next != null)
                        cur_list_point = cur_list_point.Link_to_next;

                    sample = Point.Create();
                    sample.Info.RandomInit();

                    cur_list_point.Link_to_next = sample;
                }
                _end = cur_list_point.Link_to_next;
            }
        }
        public PointList(PointList pointList_sample) 
        {
            //User may try to copy empty collection. Using setter is incorrect
            _capacity = pointList_sample.Capacity;

            if(pointList_sample.Capacity == 0) 
            {
                _beg = null;
                _end = null;
                _capacity = 0;
            }
            else if(pointList_sample.Capacity == 1) 
            {
                Point sample = Point.Create(pointList_sample._beg.Info);
                _beg = sample;
                _end = sample;
            }
            else if(pointList_sample.Capacity > 1) 
            {
                Point sample = Point.Create(pointList_sample._beg.Info);
                _beg = sample;
                //temp is used to iterate through creating list
                Point cur_list_point = _beg;
                //temp_sample is used to iterate through pointList_sample
                Point cur_sample_point = pointList_sample._beg;
                for(int c = 1; c < Capacity; ++c)
                {
                    while (cur_list_point.Link_to_next != null) 
                    {
                        cur_list_point = cur_list_point.Link_to_next;
                        cur_sample_point = cur_sample_point.Link_to_next;
                    }

                    sample = Point.Create(cur_sample_point.Info);

                    cur_list_point.Link_to_next = sample;
                }
                _end = cur_list_point.Link_to_next;
            }
        }
        //Create and return PointList object
        //Empty
        static public PointList Create() 
        {
            PointList pointList = new PointList();
            return pointList;
        }
        //With specified length
        static public PointList Create(uint capacity)
        {
            PointList pointList = new PointList(capacity);
            return pointList;
        }
        
        //ICollection methods
        public void Add(Point sample) 
        {
            Point cur_point = _beg;
            while(cur_point.Link_to_next != null)
                cur_point = cur_point.Link_to_next;
            cur_point.Link_to_next = sample;
            ++Capacity;
        }
        public void Clear() 
        {
            _beg = null;
            _end = null;
            _capacity = 0;
        }
        public bool Contains(Point sample) 
        {
            //Checking using Equals
            bool isEquals = false;
            
            Point cur_point = _beg;
            while (cur_point.Link_to_next != null) 
            {
                isEquals = sample.Equals(cur_point.Info);
                cur_point = cur_point.Link_to_next;
            }

            return isEquals;
        }
        public void CopyTo(Point[] array, int arrayIndex) 
        {
            if (array == null)
                throw new ArgumentNullException("array argument: not initialized");
            if (arrayIndex < 0 || arrayIndex >= array.Length)
                throw new ArgumentOutOfRangeException("arrayIndex argument: index is out of range");
            if(arrayIndex + Count > array.Length)
                throw new ArgumentOutOfRangeException("copyTo: can't copy, remain array part < Collection.Count");
            Point cur_point = _beg;
            for (int i = 0; i < Count; ++i) 
            {
                array[arrayIndex + i] = cur_point;
                cur_point = cur_point.Link_to_next;
            }
        }
    }
}
