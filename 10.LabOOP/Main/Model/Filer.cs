using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Main
{
    //Class is intended to contain ThreadContainer with methods to manipulate file
    class Filer
    {
        private string _path = @"B:\GIT\OOP\10.LabOOP\Main\Files";
        private string? _file;
        public string file 
        {
            get 
            {
                if (_file == null)
                    return null;
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
            Threads = new ThreadContainer(AddRandomInfo, ReadInfo);
            GetFile();
            ReadInfo();
        }

        private void AddRandomInfo() 
        {
            Random rnd = new Random();
            string garbage = rnd.Next(0, 300).ToString();
            while (!Threads.stopThreads) 
            {
                Threads.locker.WaitOne();
                using (StreamWriter streamWriter = File.AppendText(_path))
                { 
                    streamWriter.WriteLine(garbage);
                }
                Threads.locker.ReleaseMutex();
            }
        }
        private void ReadInfo() 
        {
            while (!Threads.stopThreads)
            {
                Threads.locker.WaitOne();
                string data = "";
                using (StreamReader streamReader = File.OpenText(_path)) 
                {
                    while (streamReader.ReadLine() != null) 
                    {
                        data += streamReader.ReadLine();
                    }
                    
                }
                _file = data;
                Threads.locker.ReleaseMutex();
            }
        }
        //ACCESS DENIED TO DO!!!
        private void GetFile() 
        {
            while (!Threads.stopThreads) 
            {
                Threads.locker.WaitOne();
                using(StreamWriter streamWriter = File.CreateText(_path)) 
                {
                    streamWriter.WriteLine("New file created\n");
                }
            }
            Threads.locker.ReleaseMutex();
        }
    }
}
