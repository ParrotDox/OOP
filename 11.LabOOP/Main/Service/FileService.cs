using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Serialization;
using ClassIerarchyLib;
using Main.Model;
using Microsoft.Win32;

namespace Main.Service
{
    [Serializable]
    [XmlInclude(typeof(XMLpair))]
    public class XMLpair
    {
        public string Key;
        public Person Value;
        public XMLpair()
        {

        }
        public XMLpair(string key, Person val)
        {
            Key = key;
            Value = val;
        }
    }
    public static class FileService
    {

        public static string? CollectionPath;
        public static string? JournalPath;

        //Checks path accuracy, choose appropriate extension depending on path extension and save info into a file
        public static void SaveIntoFile(NewCustomHashTable<string, Person> collection, Journal journal) 
        {
            bool IsRequestedPathOK = AskSavePath();

            if (IsRequestedPathOK) 
            {
                string? extension = Path.GetExtension(CollectionPath);
                switch (extension) 
                {
                    case ".js": 
                        {
                            JSSerialize(collection, journal);
                            break;
                        }
                    case ".xml": 
                        {
                            XMLSerialize(collection, journal);
                            break;
                        }
                    case ".bin": 
                        {
                            BinarySerizalize(collection, journal);
                            break;
                        }
                    default:
                        {
                            MessageBox.Show("Save into file extension is wrong");
                            break;
                        }
                }
            }
            else 
            {
                return;
            }
        }

        //Checks path accuracy, choose appropriate extension depending on path extension and load info from a file
        public static void LoadFile(out NewCustomHashTable<string, Person>? collection, out Journal? journal)
        {
            bool IsRequestedPathOK = AskLoadPath();

            if (IsRequestedPathOK)
            {
                string? extension = Path.GetExtension(CollectionPath);
                switch (extension)
                {
                    case ".js":
                        {
                            JSDeserialize(out collection, out journal);
                            break;
                        }
                    case ".xml":
                        {
                            XMLDeserialize(out collection, out journal);
                            break;
                        }
                    case ".bin":
                        {
                            BinaryDeserialize(out collection, out journal);
                            break;
                        }
                    default:
                        {
                            collection = null;
                            journal = null;
                            MessageBox.Show("Save into file extension is wrong");
                            break;
                        }
                }
            }
            else
            {
                collection = null;
                journal = null;
                return;
            }
        }
        //Just save file depending on used before path
        public static void AutoSave(NewCustomHashTable<string, Person> collection, Journal journal) 
        {
            string? extension = Path.GetExtension(CollectionPath);
            switch (extension)
            {
                case ".js":
                    {
                        JSSerialize(collection, journal);
                        break;
                    }
                case ".xml":
                    {
                        XMLSerialize(collection, journal);
                        break;
                    }
                case ".bin":
                    {
                        BinarySerizalize(collection, journal);
                        break;
                    }
                default:
                    {
                        MessageBox.Show("Save into file extension is wrong");
                        break;
                    }
            }
        }
        //Serializators, just write or read data
        private static void JSSerialize(NewCustomHashTable<string,Person> collection, Journal journal) 
        {
            using(FileStream stream = new(CollectionPath, FileMode.Create)) 
            {
                JsonSerializer.Serialize<IDictionary<string, Person>>(stream, collection);
            }
            using (FileStream stream = new(JournalPath, FileMode.Create))
            {
                JsonSerializer.Serialize<Journal>(stream, journal);
            }
        }
        private static void JSDeserialize(out NewCustomHashTable<string, Person>? collection, out Journal? journal) 
        {
            using (FileStream stream = new(CollectionPath, FileMode.Open))
            {
                //Get data as a dictionary because JS serializator can't properly work with custom hashtable
                IDictionary<string, JsonElement> data = JsonSerializer.Deserialize<IDictionary<string, JsonElement>>(stream);
                List<KeyValuePair<string, Person>> unpackedData = UnpackJSData(data);
                collection = new(100);
                foreach(KeyValuePair<string, Person> pair in unpackedData) 
                {
                    collection.Add(pair.Key,pair.Value);
                }
            }
            using (FileStream stream = new(JournalPath, FileMode.Open))
            {
                journal = JsonSerializer.Deserialize<Journal>(stream);
            }
        }
        private static void XMLSerialize(NewCustomHashTable<string, Person>? collection, Journal? journal) 
        {
            using (FileStream stream = new(CollectionPath, FileMode.Create))
            {
                //Convert hashTable into a list of custom pairs. Xaml can't work with dictionaries and with keyValuePair
                List<XMLpair> pairs = new();
                foreach(KeyValuePair<string,Person> pair in collection)
                {
                    XMLpair unit = new(pair.Key,pair.Value);
                    pairs.Add(unit);
                }
                XmlSerializer serializer = new(typeof(List<XMLpair>));
                serializer.Serialize(stream, pairs);
            }
            using (FileStream stream = new(JournalPath, FileMode.Create))
            {
                XmlSerializer serializer = new(typeof(Journal));
                serializer.Serialize(stream, journal);
            }
        }
        private static void XMLDeserialize(out NewCustomHashTable<string, Person>? collection, out Journal? journal)
        {
            using (FileStream stream = new(CollectionPath, FileMode.Open))
            {
                XmlSerializer serializer = new(typeof(List<XMLpair>));
                List<XMLpair> pairs = (List<XMLpair>)serializer.Deserialize(stream);
                collection = new(100);
                foreach(XMLpair pair in pairs) 
                {
                    collection.Add(pair.Key, pair.Value);
                }
            }
            using (FileStream stream = new(JournalPath, FileMode.Open))
            {
                XmlSerializer serializer = new(typeof(Journal));
                journal = (Journal)serializer.Deserialize(stream);
            }
        }
        private static void BinarySerizalize(NewCustomHashTable<string, Person>? collection, Journal? journal) 
        {
            using (FileStream stream = new(CollectionPath, FileMode.Create)) 
            {
                BinaryWriter writer = new BinaryWriter(stream);
                foreach(KeyValuePair<string,Person> pair in collection) 
                {
                    string data = pair.Value.ToString();
                    writer.Write(data);
                }
            }
            using (FileStream stream = new(JournalPath, FileMode.Create))
            {
                BinaryWriter writer = new BinaryWriter(stream);
                foreach (JournalEntry entry in journal.Entries)
                {
                    string data = entry.ToString();
                    writer.Write(data);
                }
            }
        }
        private static void BinaryDeserialize(out NewCustomHashTable<string, Person>? collection, out Journal? journal) 
        {
            using (FileStream stream = new(CollectionPath, FileMode.Open))
            {
                BinaryReader reader = new BinaryReader(stream);
                collection = new(100);
                while (reader.BaseStream.Position < stream.Length) 
                {
                    string line = reader.ReadString();
                    string[] data = line.Split(',');
                    switch (data.Count()) 
                    {
                        case 6: 
                            {
                                Person person = new Person();
                                person.Key = data[0];
                                person.Name = data[1];
                                person.Age = int.Parse(data[2]);
                                person.Residence = data[3];
                                person.link.Notes = data[4];
                                person.link.Data = data[5];
                                collection.Add(person.Key, person);
                                break;
                            }
                        case 8:
                            {
                                Employee employee = new Employee();
                                employee.Key = data[0];
                                employee.Name = data[1];
                                employee.Age = int.Parse(data[2]);
                                employee.Residence = data[3];
                                employee.link.Notes = data[4];
                                employee.link.Data = data[5];
                                employee.Experience = int.Parse(data[6]);
                                employee.Salary = int.Parse(data[7]);
                                collection.Add(employee.Key, employee);
                                break;
                            }
                        case 9: 
                            {
                                Admin admin = new Admin();
                                admin.Key = data[0];
                                admin.Name = data[1];
                                admin.Age = int.Parse(data[2]);
                                admin.Residence = data[3];
                                admin.link.Notes = data[4];
                                admin.link.Data = data[5];
                                admin.Experience = int.Parse(data[6]);
                                admin.Salary = int.Parse(data[7]);
                                admin.HeadOffice = data[8];
                                collection.Add(admin.Key, admin);
                                break;
                            }
                        case 10: 
                            {
                                Engineer engineer = new Engineer();
                                engineer.Key = data[0];
                                engineer.Name = data[1];
                                engineer.Age = int.Parse(data[2]);
                                engineer.Residence = data[3];
                                engineer.link.Notes = data[4];
                                engineer.link.Data = data[5];
                                engineer.Experience = int.Parse(data[6]);
                                engineer.Salary = int.Parse(data[7]);
                                engineer.Department = data[8];
                                collection.Add(engineer.Key, engineer);
                                break;
                            }
                    }
                }
            }
            using (FileStream stream = new(JournalPath, FileMode.Open))
            {
                BinaryReader reader = new BinaryReader(stream);
                journal = new();
                while (reader.BaseStream.Position < stream.Length)
                {
                    string line = reader.ReadString();
                    string[] data = line.Split(',');
                    JournalEntry entry = new(data[0], data[1], data[2]);
                    journal.Entries.Add(entry);
                }
            }
        }
        //Dialog show, check accuracy. If all smoothly, static var.paths get string values
        public static bool AskSavePath() 
        {
            SaveFileDialog dialog = new();
            dialog.Filter = "Binary files(*.bin)|*.bin|XML files(*.xml)|*.xml|Json files(*.js)|*.js";
            
            if(dialog.ShowDialog() == true) 
            {
                string? fullPath = dialog.FileName;
                //Divide path on parts
                string? directory = Path.GetDirectoryName(fullPath);
                string? name = Path.GetFileNameWithoutExtension(fullPath);
                string? extension = Path.GetExtension(fullPath);
                //Create appropriate paths automatically
                string? collectionPath = fullPath;
                string? journalPath = Path.Combine(directory, $"{name}Journal{extension}");
                //Check if paths correct
                bool arePathsCorrect = CheckSavePathAccuracy(collectionPath, journalPath);
                //Pin paths at Service
                if (!arePathsCorrect) 
                {
                    return false;
                }
                else 
                {
                    CollectionPath = collectionPath;
                    JournalPath = journalPath;
                    return true;
                }
            }
            else 
            {
                return false;
            }
        }
        //Dialog show, check accuracy. If all smoothly, static var.paths get string values
        public static bool AskLoadPath()
        {
            OpenFileDialog dialog = new();
            dialog.Filter = "Binary files(*.bin)|*.bin|XML files(*.xml)|*.xml|Json files(*.js)|*.js";

            if (dialog.ShowDialog() == true)
            {
                string? fullPath = dialog.FileName;
                //Divide path on parts
                string? directory = Path.GetDirectoryName(fullPath);
                string? name = Path.GetFileNameWithoutExtension(fullPath);
                string? extension = Path.GetExtension(fullPath);
                //Create appropriate paths automatically
                string? collectionPath = fullPath;
                string? journalPath = Path.Combine(directory, $"{name}Journal{extension}");
                //Check if paths correct
                bool arePathsCorrect = CheckLoadPathAccuracy(collectionPath, journalPath);
                //Pin paths at Service
                if (!arePathsCorrect)
                {
                    return false;
                }
                else
                {
                    CollectionPath = collectionPath;
                    JournalPath = journalPath;
                    return true;
                }
            }
            else
            {
                return false;
            }
        }
        //Path checkers, if something bad with a path they 'll tell you
        private static bool CheckSavePathAccuracy(string collectionPath, string journalPath) 
        {
            if (collectionPath == null) 
            {
                MessageBox.Show("Collection FilePath is null");
                return false;
            }
            if (journalPath == null)
            {
                MessageBox.Show("Journal FilePath is null");
                return false;
            }
            return true;
        }
        private static bool CheckLoadPathAccuracy(string collectionPath, string journalPath)
        {
            if (!File.Exists(collectionPath))
            {
                MessageBox.Show("Collection FilePath is wrong");
                return false;
            }
            if (!File.Exists(journalPath))
            {
                MessageBox.Show("Journal FilePath is wrong");
                return false;
            }
            if (collectionPath == null)
            {
                MessageBox.Show("Collection FilePath is null");
                return false;
            }
            if (journalPath == null)
            {
                MessageBox.Show("Journal FilePath is null");
                return false;
            }
            return true;
        }
        //Unpacker
        private static List<KeyValuePair<string, Person>> UnpackJSData(IDictionary<string, JsonElement> data) 
        {
            List<KeyValuePair<string, Person>> unpackedData = new();
            foreach (KeyValuePair<string, JsonElement> pair in data) 
            {
                string key = pair.Key;
                Person? val;
                JsonElement element = pair.Value;
                if(element.TryGetProperty("Department", out _)) 
                {
                    val = element.Deserialize<Engineer>();
                    unpackedData.Add(new KeyValuePair<string, Person>(key, val));
                    continue;
                }
                if (element.TryGetProperty("HeadOffice", out _))
                {
                    val = element.Deserialize<Admin>();
                    unpackedData.Add(new KeyValuePair<string, Person>(key, val));
                    continue;
                }
                if (element.TryGetProperty("Salary", out _))
                {
                    val = element.Deserialize<Admin>();
                    unpackedData.Add(new KeyValuePair<string, Person>(key, val));
                    continue;
                }
                val = element.Deserialize<Person>();
                unpackedData.Add(new KeyValuePair<string, Person>(key, val));
            }
            return unpackedData;
        }
    }
}
