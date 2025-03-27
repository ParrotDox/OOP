using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Main
{
    //Class is intended to contain ThreadContainer with methods to manipulate file
    class Filer : INotifyPropertyChanged
    {
        private string _path = @"B:\GIT\OOP\10.LabOOP\Main\Files\Info.txt";
        private string? _file;
        public string file 
        {
            get 
            {
                if (_file == null)
                    return null;
                return _file;
            }
            set 
            {
                _file = value;
                OnPropertyChanged();
            }
        }
        private ThreadContainer? _threads;
        public ThreadContainer? Threads
        {
            get { return _threads; }
            set { _threads = value; }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public Filer()
        {
            Threads = new ThreadContainer(AddRandomInfo, ReadInfo);
            GetFile();
        }

        public void InitNewThreadContainer() 
        {
            Threads = new ThreadContainer(AddRandomInfo, ReadInfo);
        }
        private void AddRandomInfo() 
        {
            Random rnd = new Random();
            string garbage;
            while (!Threads.stopThreads) 
            {
                Threads.locker.WaitOne();
                try
                {
                    garbage = rnd.Next(100, 300).GetHashCode().ToString();
                    using (StreamWriter streamWriter = File.AppendText(_path))
                    {
                        streamWriter.WriteLine(garbage);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                finally
                {
                    Threads.locker.ReleaseMutex();
                }
                Thread.Sleep(1000);
            }
        }
        private void ReadInfo() 
        {
            while (!Threads.stopThreads) 
            {
                Threads.locker.WaitOne();
                try
                {
                    string data = "";
                    using (StreamReader streamReader = File.OpenText(_path))
                    {
                        string? line;
                        while ((line = streamReader.ReadLine()) != null)
                        {
                            data += line + '\n';
                        }
                    }
                    file = data;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                finally
                {
                    Threads.locker.ReleaseMutex();
                }
                Thread.Sleep(500);
            }
        }
        private void GetFile() 
        {
            Threads.locker.WaitOne();
            try
            {
                using (StreamWriter streamWriter = File.CreateText(_path))
                {
                    streamWriter.WriteLine("New file created");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                Threads.locker.ReleaseMutex();
            }
        }
    }
}
