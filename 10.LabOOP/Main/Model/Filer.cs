using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.IO;

namespace Main
{
    //Class is intended to contain ThreadContainer with methods to manipulate file
    class Filer
    {
        private string _path = "../Files/Info.json";
        private string? _file;
        public string File 
        {
            get 
            {
                if (_file == null)
                    return "File is empty";
                return _file;
            }
        }
        private ThreadContainer? _threads;
        public ThreadContainer? Threads 
        {
            get { return _threads; }
            set { _threads = value; }
        }
        public Filer()
        {
            GetJsonFile(); //TODO
            Threads = new ThreadContainer(AddRandomInfo, ReadInfo);
        }

        private void AddRandomInfo() 
        {
            Random rnd = new Random();
            string garbage = rnd.Next(0, 300).ToString();
            using (FileStream stream = new FileStream(_path, FileMode.Append, FileAccess.Write))
            {
                JsonSerializer.Serialize<string>(stream, garbage);
            }
        }
        private void ReadInfo() 
        {
            using (FileStream stream = new FileStream(_path, FileMode.Open, FileAccess.Read))
            {
                _file = JsonSerializer.Deserialize<string>(stream);
            }
        }
        private void GetJsonFile() 
        {
            //TO DO
        }
    }
}
