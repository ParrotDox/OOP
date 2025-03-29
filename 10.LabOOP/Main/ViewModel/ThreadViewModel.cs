using Main;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

namespace Main
{
    class ThreadViewModel : INotifyPropertyChanged
    {
        //Window properties
        public Func<bool> IsRunning { get; set; }
        //XAML binding info
        private string _textBlockInfoChosenThread = "None";
        private bool _isTextBlockInfoChosenThreadPressed = false;
        private string _textBlockInfoWriterPriority;
        private string _textBlockInfoReaderPriority;
        public string TextBlockInfoChosenThread 
        {
            get { return _textBlockInfoChosenThread; }
            set { _textBlockInfoChosenThread = value; OnPropertyChanged(); }
        }
        public bool TextBlockInfoChosenThreadPressed
        {
            get { return _isTextBlockInfoChosenThreadPressed; }
            set { _isTextBlockInfoChosenThreadPressed = value; OnPropertyChanged(); }
        }
        public string TextBlockInfoWriterPriority
        {
            get { return _textBlockInfoWriterPriority; }
            set { _textBlockInfoWriterPriority = value; OnPropertyChanged(); }
        }
        public string TextBlockInfoReaderPriority
        {
            get { return _textBlockInfoReaderPriority; }
            set { _textBlockInfoReaderPriority = value; OnPropertyChanged(); }
        }
        //Properties
        private Filer? _filer;
        private Thread? _currentThread;
        public Filer? filer
        {
            get { if (_filer != null) return _filer; else return null; }
            set { _filer = value; OnPropertyChanged(); }
        }
        public Thread? CurrentThread 
        {
            get { if (_currentThread != null) return _currentThread; else return null; }
            set { OnPropertyChanged(); _currentThread = value; }
        }
        //Commands
        public ICommand ChooseThreadCommand { get; set; }
        public ICommand ChangeThreadPriorityCommand { get; set; }
        public ICommand StartOrStopThreadCommand { get; set; }
        public ThreadViewModel()
        {
            filer = new Filer();
            ChooseThreadCommand = new RelayCommand(ExecuteChooseThreadCommand, CanExecuteChooseThreadCommand);
            ChangeThreadPriorityCommand = new RelayCommand(ExecuteChangeThreadPriorityCommand, CanExecuteChangeThreadPriorityCommand);
            StartOrStopThreadCommand = new RelayCommand(ExecuteStartOrStopThreadCommand, CanExecuteStartOrStopThreadCommand);

            TextBlockInfoWriterPriority = $"Writer thread: {filer.Threads.Writer.Priority}";
            TextBlockInfoReaderPriority = $"Reader thread: {filer.Threads.Reader.Priority}";

            IsRunning = filer.GetState;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string propertyName="") 
        {
            if (PropertyChanged != null) 
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        //ICommand ChangeThreadPriorityCommand
        private bool CanExecuteChooseThreadCommand(object? parameter) 
        {
            return true;
        }
        private void ExecuteChooseThreadCommand(object? parameter) 
        {
            string? param = parameter as string;
            if(param != null) 
            {
                if (param == "WriteThread")
                {
                    TextBlockInfoChosenThread = "Chosen thread: Write thread";
                    CurrentThread = filer.Threads.Writer;
                }     
                if (param == "ReadThread") 
                {
                    TextBlockInfoChosenThread = "Chosen thread: Read thread";
                    CurrentThread = filer.Threads.Reader; 
                }

                TextBlockInfoChosenThreadPressed = true;
                ((RelayCommand)ChangeThreadPriorityCommand).RaiseCanExecuteChanged();

                if (param == "Reset")
                {
                    TextBlockInfoChosenThread = "Chosen thread: Write thread";
                    CurrentThread = filer.Threads.Writer;
                }
            }
        }
        //ICommand ChangePriorityCommand
        private bool CanExecuteChangeThreadPriorityCommand(object? parameter) 
        {
            return TextBlockInfoChosenThreadPressed;
        }
        private void ExecuteChangeThreadPriorityCommand(object? parameter) 
        {
            string param = parameter as string;
            switch (param)
            {
                case ("Lowest"): { CurrentThread.Priority = ThreadPriority.Lowest; break; }
                case ("BelowNormal"): { CurrentThread.Priority = ThreadPriority.BelowNormal; break; }
                case ("Normal"): { CurrentThread.Priority = ThreadPriority.Normal; break; }
                case ("AboveNormal"): { CurrentThread.Priority = ThreadPriority.AboveNormal; break; }
                case ("Highest"): { CurrentThread.Priority = ThreadPriority.Highest; break; }
            }

            
            //This is for reset in code
            if(param == "Reset") 
            {
                filer.Threads.Writer.Priority = ThreadPriority.Normal;
                filer.Threads.Reader.Priority = ThreadPriority.Normal;
                TextBlockInfoWriterPriority = CurrentThread.Name + $" Thread: {CurrentThread.Priority}";
                TextBlockInfoReaderPriority = CurrentThread.Name + $" Thread: {CurrentThread.Priority}";
            }
            //This is for normal setting
            else 
            {
                if (CurrentThread.Name == "Writer")
                {
                    TextBlockInfoWriterPriority = CurrentThread.Name + $" Thread: {CurrentThread.Priority}";
                }
                if (CurrentThread.Name == "Reader")
                {
                    TextBlockInfoReaderPriority = CurrentThread.Name + $" Thread: {CurrentThread.Priority}";
                }
            }
        }
        //ICommand StartThreadCommand
        private bool CanExecuteStartOrStopThreadCommand(object? parameter) 
        {
            return true;
        }
        private void ExecuteStartOrStopThreadCommand(object? parameter) 
        {
            string action = parameter as string;
            if(action == "Start") 
            {
                if(filer.Threads.Writer.IsAlive || 
                    filer.Threads.Reader.IsAlive) 
                {
                    MessageBox.Show("Threads already working!", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else 
                {
                    RunThreads();
                }
            }
            if(action == "Stop") 
            {
                if(filer.Threads.Writer.ThreadState == ThreadState.Unstarted ||
                    filer.Threads.Reader.ThreadState == ThreadState.Unstarted) 
                {
                    MessageBox.Show("Threads already stopped!", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else 
                {
                    filer.Threads.stopThreads = true;
                    if (filer.Threads.Writer.IsAlive)
                    {
                        filer.Threads.Writer.Join();
                        filer.Threads.Writer.Interrupt();
                    }
                    if (filer.Threads.Reader.IsAlive)
                    {
                        filer.Threads.Reader.Join();
                        filer.Threads.Reader.Interrupt();
                    }
                    InitNewThreads();
                }
            }
        }

        private void InitNewThreads() 
        {
            filer.InitNewThreadContainer();
            CurrentThread = filer.Threads.Writer;
            ExecuteChooseThreadCommand("Reset");
            ExecuteChangeThreadPriorityCommand("Reset");
        }
        private void RunThreads() 
        {
            filer.Threads.stopThreads = false;
            filer.Threads.Reader.Start();
            filer.Threads.Writer.Start();
        }
    }
}
