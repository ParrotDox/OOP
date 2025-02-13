using ClassIerarchyLib;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;

namespace UnitTests
{
    public class UnitTest
    {
        [Fact]
        public void TestGetKeys() 
        {
            //Arrange
            List<string> keys = new List<string>();
            PersonHashTable myHashTable = new PersonHashTable(10);
            Person p = new Person();
            p.RandomInit();
            //Act
            myHashTable.Add("KeyOne", p);
            myHashTable.Add("KeyTwo", p);
            myHashTable.Add("KeyThree", p);
            myHashTable.Add("GGGBBB", p);
            keys = (List<string>)myHashTable.Keys;
            //Assert
            Assert.Equal(4, keys.Count);
        }
        public void TestGetValues()
        {
            //Arrange
            List<Person> vals = new List<Person>();
            PersonHashTable myHashTable = new PersonHashTable(10);
            Person p = new Person();
            p.RandomInit();
            //Act
            myHashTable.Add("KeyOne", p);
            myHashTable.Add("KeyTwo", p);
            myHashTable.Add("KeyThree", p);
            myHashTable.Add("GGGBBB", p);
            vals = (List<Person>)myHashTable.Values;
            //Assert
            Assert.Equal(4, vals.Count);
        }
        [Fact]
        public void TestAddMethods()
        {
            //Arrange
            PersonHashTable myHashTable = new PersonHashTable(10);
            Person p = new Person();
            p.RandomInit();
            KeyValuePair<string, Person> pair = new KeyValuePair<string, Person>("KeyThree", p);
            //Act
            myHashTable.Add("KeyOne", p);
            myHashTable.Add("KeyOne", p);
            myHashTable.Add("KeyOne", p);
            myHashTable.Add("KeyOne", p);
            myHashTable.Add(pair);
            //Assert
            Assert.NotNull(myHashTable);
            Assert.Equal(5, myHashTable.Count);
        }
        [Fact]
        public void TestContainsKeyMethods()
        {
            //Arrange
            PersonHashTable myHashTable = new PersonHashTable(10);
            PersonHashTable emptyHashTable = new PersonHashTable(0);
            Person p = new Person();
            p.RandomInit();
            KeyValuePair<string, Person> pair = new KeyValuePair<string, Person>("KeyThree", p);
            //Act
            myHashTable.Add("KeyOne", p);
            myHashTable.Add("GGGBBB", p);
            myHashTable.Add("Aa", p);
            myHashTable.Add("BB", p);
            myHashTable.Add(pair);
            bool keyOne = myHashTable.ContainsKey("KeyOne");
            bool keyThree = myHashTable.ContainsKey("KeyThree");
            bool keyGGGBBB = myHashTable.ContainsKey("GGGBBB");
            bool emptyCheck = emptyHashTable.ContainsKey("Anykey");
            bool notAkey = myHashTable.ContainsKey("ThisKeyIsNotUsed");
            bool eqHash1 = myHashTable.ContainsKey("BB");
            bool eqHash2 = myHashTable.ContainsKey("Aa");
            //Assert
            Assert.True(keyOne);
            Assert.True(keyThree);
            Assert.True(keyGGGBBB);
            Assert.False(emptyCheck);
            Assert.False(notAkey);
            Assert.True(eqHash1 && eqHash2);
        }
        [Fact]
        public void TestContainsMethods() 
        {
            //Arrange
            PersonHashTable myHashTable = new PersonHashTable(10);
            Person p = new Person();
            p.RandomInit();
            KeyValuePair<string, Person> pair = new KeyValuePair<string, Person>("KeyThree", p);
            //Act
            myHashTable.Add("KeyOne", p);
            myHashTable.Add("GGGBBB", p);
            myHashTable.Add(pair);
            Person copy_p = (Person)p.Clone();
            bool contains_p = myHashTable.Contains(copy_p);
            bool contains_pair = myHashTable.Contains(pair);
            //Assert
            Assert.True(contains_p);
            Assert.True(contains_pair);
        }
        [Fact]
        public void TestRemoveMethods() 
        {
            //Arrange
            PersonHashTable myHashTable = new PersonHashTable(10);
            Person p = new Person();
            p.RandomInit();
            KeyValuePair<string, Person> pair = new KeyValuePair<string, Person>("KeyThree", p);
            //Act
            myHashTable.Add("KeyOne", p);
            myHashTable.Add(pair);
            myHashTable.Add("GGGBBB", p);
            myHashTable.Add(p.ToString(), p);
            myHashTable.Remove("KeyOne");
            myHashTable.Remove("GGGBBB");
            myHashTable.Remove(p.ToString());
            myHashTable.Remove(pair);
            myHashTable.Add("Aa", p);
            myHashTable.Add("Aa", p);
            myHashTable.Add("BB", p);
            myHashTable.Add("Aa", p);
            myHashTable.Add("Aa", p);
            myHashTable.Remove("BB");
            myHashTable.Remove("Aa");
            myHashTable.Remove("Aa");
            myHashTable.Remove("Aa");
            myHashTable.Remove("Aa");
            //Assert
            Assert.Equal(0, myHashTable.Count);
        }
        [Fact]
        public void TestClearMethod()
        {
            //Arrange
            PersonHashTable myHashTable = new PersonHashTable(10);
            Person p = new Person();
            p.RandomInit();
            KeyValuePair<string, Person> pair = new KeyValuePair<string, Person>("KeyThree", p);
            //Act
            myHashTable.Add("KeyOne", p);
            myHashTable.Add(pair);
            myHashTable.Clear();
            //Assert
            Assert.Equal(0, myHashTable.Count);
        }
        [Fact]
        public void TestTryGetValueMethod()
        {
            //Arrange
            PersonHashTable myHashTable = new PersonHashTable(10);
            PersonHashTable emptyHashTable = new PersonHashTable(0);
            Person p = new Person();
            p.RandomInit();
            //Act
            myHashTable.Add("KeyOne", p);
            Person get_p;
            Person get_nothing;
            Person get_nothing_again;
            myHashTable.TryGetValue("KeyOne", out get_p);
            bool areTheSame = get_p.Equals(p);
            bool falze = emptyHashTable.TryGetValue("KeyOne", out get_nothing);
            bool end_false = myHashTable.TryGetValue("DunnoSomeKey", out get_nothing_again);
            //Assert
            Assert.True(areTheSame);
            Assert.False(falze);
            Assert.False(end_false);
        }
        [Fact]
        public void CopyToMethod() 
        {
            //Arrange
            int ctr = 0;
            PersonHashTable myHashTable = new PersonHashTable(10);
            Person p = new Person();
            p.RandomInit();
            KeyValuePair<string, Person>[] key_array = new KeyValuePair<string, Person>[5];
            KeyValuePair<string, Person>[] not_init_arr = null;
            //Act
            myHashTable.Add("KeyOne", p);
            myHashTable.Add("KeyTwo", p);
            myHashTable.Add("KeyThree", p);
            myHashTable.CopyTo(key_array, 2);
            foreach(KeyValuePair<string, Person> pair in key_array) 
            {
                if(pair.Value != null) 
                {
                    ++ctr;
                }
            }
            //Assert
            Assert.Equal(3, ctr);
            Assert.ThrowsAny<ArgumentNullException>(() => myHashTable.CopyTo(not_init_arr, 2));
            Assert.ThrowsAny<ArgumentOutOfRangeException>(() => myHashTable.CopyTo(key_array, 222));
            Assert.ThrowsAny<ArgumentOutOfRangeException>(() => myHashTable.CopyTo(key_array, 3));
        }
        [Fact]
        public void GetEnumeratorMethod() 
        {
            //Arrange
            int iterations = 0;
            PersonHashTable myHashTable = new PersonHashTable(10);
            //Act
            for (int i = 0; i < 10; ++i) 
            {
                Person p = new Person();
                p.RandomInit();
                myHashTable.Add(p.ToString(), p);
            }
            foreach(KeyValuePair<string , Person> pair in myHashTable) 
            {
                ++iterations;
            }
            //Assert
            Assert.Equal(10, iterations);
        }
    }
}