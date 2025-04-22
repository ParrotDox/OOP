using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Serialization;
using ClassIerarchyLib;
using Main.Model;
using Microsoft.Win32;

namespace Main.Service
{
    public class FileService
    {
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
        private string? _filePathCollection;
        private string? _filePathJournal;
        private CollectionContainer? _container;
        public CollectionContainer? Container { get { return _container; } set { _container = value; } }
        public FileService()
        {
            _filePathCollection = null;
            _filePathJournal = null;
            _container = new CollectionContainer();
        }
        public bool FileOpen() 
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Binary files(*.bin)|*.bin|XML files(*.xml)|*.xml|Json files(*.js)|*.js";

            if (dialog.ShowDialog() == false) 
            {
                return false;
            }

            string collectionPath = dialog.FileName;
            string? directory = Path.GetDirectoryName(collectionPath);
            string? filenameCollection = Path.GetFileNameWithoutExtension(collectionPath);
            string? extension = Path.GetExtension(collectionPath);
            string? journalPath = Path.Combine(directory,$"{filenameCollection}Journal{extension}");
            _filePathCollection = collectionPath;
            _filePathJournal = journalPath;

            switch (extension)
            {
                case ".bin":
                    {
                        using(FileStream stream = new(collectionPath, FileMode.Open)) 
                        {
                            BinaryReader reader = new BinaryReader(stream);
                            _container.Persons = new(100);
                            while (stream.Position < stream.Length) 
                            {
                                string personString = reader.ReadString();
                                var data = personString.Split(',');
                                if (data.Length == 4) 
                                {
                                    Person person = new();
                                    person.Key = data[0];
                                    person.Name = data[1];
                                    person.Age = int.Parse(data[2]);
                                    person.Residence = data[3];
                                    _container.Persons.Add(data[0], person);
                                }
                                if (data.Length == 6)
                                {
                                    Employee person = new();
                                    person.Key = data[0];
                                    person.Name = data[1];
                                    person.Age = int.Parse(data[2]);
                                    person.Residence = data[3];
                                    person.Experience = int.Parse(data[4]);
                                    person.Salary = int.Parse(data[5]);
                                    _container.Persons.Add(data[0], person);
                                }
                                if (data.Length == 7)
                                {
                                    Admin person = new();
                                    person.Key = data[0];
                                    person.Name = data[1];
                                    person.Age = int.Parse(data[2]);
                                    person.Residence = data[3];
                                    person.Experience = int.Parse(data[4]);
                                    person.Salary = int.Parse(data[5]);
                                    person.HeadOffice = data[6];
                                    _container.Persons.Add(data[0], person);
                                }
                                if (data.Length == 8)
                                {
                                    Engineer person = new();
                                    person.Key = data[0];
                                    person.Name = data[1];
                                    person.Age = int.Parse(data[2]);
                                    person.Residence = data[3];
                                    person.Experience = int.Parse(data[4]);
                                    person.Salary = int.Parse(data[5]);
                                    person.Department = data[6];
                                    _container.Persons.Add(data[0], person);
                                }
                            }
                        }
                        using(FileStream stream = new(journalPath, FileMode.Open)) 
                        {
                            BinaryReader reader = new BinaryReader(stream);
                            Journal journal = new();
                            while (stream.Position < stream.Length) 
                            {
                                string personString = reader.ReadString();
                                var data = personString.Split(',');
                                JournalEntry unit = new(data[0], data[1], data[2]);
                                journal.AddEntry(unit);
                            }
                            _container.Journal = journal;
                        }
                        return true;
                    }
                case ".xml": 
                    {
                        using (FileStream stream = new(collectionPath, FileMode.Open))
                        {
                            XmlSerializer deserializer = new XmlSerializer(typeof(List<XMLpair>));
                            var list = (List<XMLpair>)deserializer.Deserialize(stream);
                            _container.Persons = new(100);
                            //Move all pairs into custom collection
                            foreach(var pair in list) 
                            {
                                _container.Persons.Add(pair.Key, pair.Value);
                            }
                        }
                        using (FileStream stream = new(journalPath, FileMode.Open))
                        {
                            XmlSerializer deserializer = new(typeof(Journal));
                            _container.Journal = (Journal)deserializer.Deserialize(stream);
                        }
                        return true;
                    }
                case ".js": 
                    {
                        using (FileStream stream = new(collectionPath, FileMode.Open))
                        {
                            var list = JsonSerializer.Deserialize<List<KeyValuePair<string, Person>>>(stream);
                            _container.Persons = new(100);
                            //Move all pairs into custom collection
                            foreach (var pair in list)
                            {
                                _container.Persons.Add(pair.Key, pair.Value);
                            }
                        }
                        using (FileStream stream = new(journalPath, FileMode.Open))
                        {
                            _container.Journal = JsonSerializer.Deserialize<Journal>(stream);
                        }
                        return true;
                    }
                default: 
                    {
                        return false;
                    }
            }
        }
        public bool FileSave() 
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Binary files(*.bin)|*.bin|XML files(*.xml)|*.xml|Json files(*.js)|*.js";

            if (dialog.ShowDialog() == false)
            {
                return false;
            }

            string collectionPath = dialog.FileName;
            string? directory = Path.GetDirectoryName(collectionPath);
            string? filenameCollection = Path.GetFileNameWithoutExtension(collectionPath);
            string? extension = Path.GetExtension(collectionPath);
            string? journalPath = Path.Combine(directory, $"{filenameCollection}Journal{extension}");
            _filePathCollection = collectionPath;
            _filePathJournal = journalPath;

            //Name collection before saving
            _container.Persons.CollectionName = collectionPath;
            switch (extension)
            {
                case ".bin":
                    {
                        using (FileStream stream = new(collectionPath, FileMode.Create))
                        {
                            BinaryWriter writer = new BinaryWriter(stream);
                            foreach(var pair in _container.Persons) 
                            {
                                string pairData = $"{pair.Key},{pair.Value.ToString()}";
                                writer.Write(pairData);
                            }
                        }
                        using (FileStream stream = new(journalPath, FileMode.Create))
                        {
                            BinaryWriter writer = new BinaryWriter(stream);
                            foreach (var entry in _container.Journal.Entries)
                            {
                                string pairData = $"{entry.ToString()}";
                                writer.Write(pairData);
                            }
                        }
                        return true;
                    }
                case ".xml":
                    {
                        using (FileStream stream = new(collectionPath, FileMode.Create))
                        {
                            XmlSerializer serializer = new(typeof(List<XMLpair>));
                            List<XMLpair> list = new();
                            //Pack custom collection into a list
                            foreach (var pair in _container.Persons)
                            {
                                XMLpair unit = new(pair.Key, pair.Value);
                                list.Add(unit);
                            }
                            serializer.Serialize(stream, list);
                        }
                        using (FileStream stream = new(journalPath, FileMode.Create))
                        {
                            XmlSerializer serializer = new(typeof(Journal));
                            serializer.Serialize(stream, _container.Journal);
                        }
                        return true;
                    }
                case ".js":
                    {
                        using (FileStream stream = new(collectionPath, FileMode.Create))
                        {
                            List<KeyValuePair<string, Person>> list = new();
                            //Pack custom collection into a list
                            foreach (var pair in _container.Persons)
                            {
                                list.Add(pair);
                            }
                            JsonSerializer.Serialize<List<KeyValuePair<string, Person>>>(stream, list);
                        }
                        using (FileStream stream = new(journalPath, FileMode.Create))
                        {
                            JsonSerializer.Serialize<Journal>(stream, _container.Journal);
                        }
                        return true;
                    }
                default:
                    {
                        return false;
                    }
            }
        }
    }
}
