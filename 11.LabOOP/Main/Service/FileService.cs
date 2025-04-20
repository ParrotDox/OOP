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
            dialog.Filter = "Text files(*.txt)|*.txt";
            if(dialog.ShowDialog() == false) 
            {
                return false;
            }
            //else
            _filePathCollection = dialog.FileName;
            string[] postfixes = {"Binary", "XML", "JS"};
            string? postfixType = "";
            bool hasPostfix = postfixes.Any(postfix => 
            {
                bool flag = _filePathCollection.Contains(postfix);
                postfixType = flag ? postfix : null;
                return flag; 
            });
            if (!hasPostfix)
            {
                return false;
            }
            //else
            switch (postfixType) 
            {
                case "Binary":
                    {
                        //Read info about collection
                        using (FileStream stream = new(_filePathCollection, FileMode.Open))
                        {
                            BinaryFormatter formatter = new BinaryFormatter();
                            _container.Persons = (NewCustomHashTable<string, Person>)formatter.Deserialize(stream);
                        }
                        //Get the journal path
                        _filePathJournal = dialog.FileName.Replace("Binary", "Journal");
                        //Read info about journal
                        using (FileStream stream = new(_filePathJournal, FileMode.Open))
                        {
                            BinaryFormatter formatter = new BinaryFormatter();
                            _container.Journal = (Journal<Person>)formatter.Deserialize(stream);
                        }
                        return true;
                    }
                case "XML": 
                    {
                        using (FileStream stream = new(_filePathCollection, FileMode.Open))
                        {
                            XmlSerializer formatter = new XmlSerializer(typeof(NewCustomHashTable<string, Person>));
                            _container.Persons = (NewCustomHashTable<string, Person>)formatter.Deserialize(stream);
                        }
                        _filePathJournal = dialog.FileName.Replace("XML", "Journal");
                        using (FileStream stream = new(_filePathJournal, FileMode.Open))
                        {
                            XmlSerializer formatter = new XmlSerializer(typeof(Journal<Person>));
                            _container.Journal = (Journal<Person>)formatter.Deserialize(stream);
                        }
                        return true;
                    }
                case "JS": 
                    {
                        using (FileStream stream = new(_filePathCollection, FileMode.Open))
                        {
                            _container.Persons = JsonSerializer.Deserialize<NewCustomHashTable<string, Person>>(stream);
                        }
                        _filePathJournal = dialog.FileName.Replace("JS", "Journal");
                        using (FileStream stream = new(_filePathJournal, FileMode.Open))
                        {
                            _container.Journal = JsonSerializer.Deserialize<Journal<Person>>(stream);
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
            dialog.Filter = "Text files(*.txt)|*.txt|XML files(*.xml)|*.xml|Json files(*.js)|*.js";
            if (dialog.ShowDialog() == false)
            {
                return false;
            }
            //else
            _filePathCollection = dialog.FileName;
            string[] postfixes = { "Binary", "XML", "JS" };
            string? postfixType = "";
            bool hasPostfix = postfixes.Any(postfix =>
            {
                bool flag = _filePathCollection.Contains(postfix);
                postfixType = flag ? postfix : null;
                return flag;
            });
            if (!hasPostfix)
            {
                return false;
            }
            //else
            switch (postfixType)
            {
                case "Binary":
                    {
                        //Save info about collection
                        using (FileStream stream = new(_filePathCollection, FileMode.Create))
                        {
                            BinaryFormatter formatter = new BinaryFormatter();
                            formatter.Serialize(stream, _container.Persons);
                        }
                        //Get the journal path
                        _filePathJournal = dialog.FileName.Replace("Binary", "Journal");
                        //Save info about journal
                        using (FileStream stream = new(_filePathJournal, FileMode.Create))
                        {
                            BinaryFormatter formatter = new BinaryFormatter();
                            formatter.Serialize(stream, _container.Journal);
                        }
                        return true;
                    }
                case "XML":
                    {
                        using (FileStream stream = new(_filePathCollection, FileMode.Create))
                        {
                            XmlSerializer formatter = new XmlSerializer(typeof(NewCustomHashTable<string, Person>));
                            formatter.Serialize(stream, _container.Persons);
                        }
                        _filePathJournal = dialog.FileName.Replace("XML", "Journal");
                        using (FileStream stream = new(_filePathJournal, FileMode.Create))
                        {
                            XmlSerializer formatter = new XmlSerializer(typeof(Journal<Person>));
                            formatter.Serialize(stream, _container.Journal);
                        }
                        return true;
                    }
                case "JS":
                    {
                        using (FileStream stream = new(_filePathCollection, FileMode.Create))
                        {
                            JsonSerializer.Serialize<NewCustomHashTable<string, Person>>(stream, _container.Persons);
                        }
                        _filePathJournal = dialog.FileName.Replace("JS", "Journal");
                        using (FileStream stream = new(_filePathJournal, FileMode.Create))
                        {
                            JsonSerializer.Serialize<Journal<Person>>(stream, _container.Journal);
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
