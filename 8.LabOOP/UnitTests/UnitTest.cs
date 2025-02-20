using ClassIerarchyLib;

namespace UnitTests
{
    public class UnitTest
    {
        [Fact]
        public void Add_ShouldTriggerCollectionCountChangedEvent()
        {
            // Arrange
            var hashTable = new NewCustomHashTable<string, Person>(10);
            hashTable.collection_name = "TestCollection";
            bool eventTriggered = false;
            var person = new Person();
            person.RandomInit();

            hashTable.CollectionCountChanged += (sender, args) =>
            {
                eventTriggered = true;
                Assert.Equal("TestCollection", args.collection_name);
                Assert.Equal("Add", args.method_name);
                Assert.Equal(person, args.item_link);
            };

            // Act
            hashTable.Add("person1", person);

            // Assert
            Assert.True(eventTriggered);
        }

        [Fact]
        public void Remove_ShouldTriggerCollectionCountChangedEvent()
        {
            // Arrange
            var hashTable = new NewCustomHashTable<string, Person>(10);
            hashTable.collection_name = "TestCollection";
            var person = new Person();
            person.RandomInit();
            hashTable.Add("person1", person);
            bool eventTriggered = false;

            hashTable.CollectionCountChanged += (sender, args) =>
            {
                eventTriggered = true;
                Assert.Equal("TestCollection", args.collection_name);
                Assert.Equal("Remove(person1)", args.method_name);
                Assert.Equal(person, args.item_link);
            };

            // Act
            hashTable.Remove("person1");

            // Assert
            Assert.True(eventTriggered);
        }

        [Fact]
        public void Indexator_Set_ShouldTriggerCollectionRefChangedEvent()
        {
            // Arrange
            var hashTable = new NewCustomHashTable<string, Person>(10);
            hashTable.collection_name = "TestCollection";
            var person = new Person();
            person.RandomInit();
            hashTable.Add("person1", person);
            bool eventTriggered = false;
            Person newPerson = new Person();
            newPerson.RandomInit();

            hashTable.CollectionRefChanged += (sender, args) =>
            {
                eventTriggered = true;
                Assert.Equal("TestCollection", args.collection_name);
                Assert.Equal("Indexator[person1]", args.method_name);
                Assert.Equal(newPerson, args.item_link);
            };

            // Act
            hashTable["person1"] = newPerson;

            // Assert
            Assert.True(eventTriggered);
        }

        [Fact]
        public void AddPair_ShouldTriggerCollectionCountChangedEvent()
        {
            // Arrange
            var hashTable = new NewCustomHashTable<string, Person>(10);
            hashTable.collection_name = "TestCollection";
            bool eventTriggered = false;
            Person person = new Person();
            person.RandomInit();
            KeyValuePair<string, Person> pair = new KeyValuePair<string, Person>("person2", person);

            hashTable.CollectionCountChanged += (sender, args) =>
            {
                eventTriggered = true;
                Assert.Equal("TestCollection", args.collection_name);
                Assert.Equal("Add", args.method_name);
                Assert.Equal(person, args.item_link);
            };

            // Act
            hashTable.Add(pair);

            // Assert
            Assert.True(eventTriggered);
        }

        [Fact]
        public void RemovePair_ShouldTriggerCollectionCountChangedEvent()
        {
            // Arrange
            var hashTable = new NewCustomHashTable<string, Person>(10);
            hashTable.collection_name = "TestCollection";
            var person = new Person();
            person.RandomInit();
            hashTable.Add("person1", person);
            bool eventTriggered = false;

            hashTable.CollectionCountChanged += (sender, args) =>
            {
                eventTriggered = true;
                Assert.Equal("TestCollection", args.collection_name);
                Assert.Equal("Remove(person1)", args.method_name);
                Assert.Equal(person, args.item_link);
            };

            // Act
            hashTable.Remove(new KeyValuePair<string, Person>("person1", person));

            // Assert
            Assert.True(eventTriggered);
        }
    }
}