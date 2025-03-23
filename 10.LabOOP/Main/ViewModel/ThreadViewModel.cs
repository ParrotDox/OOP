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
        //XAML binding info
        private string _textBlockInfoChosenThread = "None";
        private bool _isTextBlockInfoChosenThreadPressed = false;
        private string _textBlockInfoWriterPriority = "Writer thread: Normal";
        private string _textBlockInfoReaderPriority = "Reader thread: Normal";
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
        public ThreadViewModel()
        {
            filer = new Filer();
            ChooseThreadCommand = new RelayCommand(ExecuteChooseThreadCommand, CanExecuteChooseThreadCommand);
            ChangeThreadPriorityCommand = new RelayCommand(ExecuteChangeThreadPriorityCommand, CanExecuteChangeThreadPriorityCommand);
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
            string? action = parameter as string;
            if(action != null) 
            {
                if (action == "WriteThread")
                {
                    TextBlockInfoChosenThread = "Chosen thread: Write thread";
                    CurrentThread = filer.Threads.Writer;
                }     
                if (action == "ReadThread") 
                {
                    TextBlockInfoChosenThread = "Chosen thread: Read thread";
                    CurrentThread = filer.Threads.Reader; 
                }
                TextBlockInfoChosenThreadPressed = true;
                ((RelayCommand)ChangeThreadPriorityCommand).RaiseCanExecuteChanged();
            }
        }
        //ICommand ChangePriorityCommand
        private bool CanExecuteChangeThreadPriorityCommand(object? parameter) 
        {
            return TextBlockInfoChosenThreadPressed;
        }
        private void ExecuteChangeThreadPriorityCommand(object? parameter) 
        {
            switch (parameter)
            {
                case ("Lowest"): { CurrentThread.Priority = ThreadPriority.Lowest; break; }
                case ("BelowNormal"): { CurrentThread.Priority = ThreadPriority.BelowNormal; break; }
                case ("Normal"): { CurrentThread.Priority = ThreadPriority.Normal; break; }
                case ("AboveNormal"): { CurrentThread.Priority = ThreadPriority.AboveNormal; break; }
                case ("Highest"): { CurrentThread.Priority = ThreadPriority.Highest; break; }
            }

            if (CurrentThread.Name == "Writer")
            {
                TextBlockInfoWriterPriority = CurrentThread.Name + $" Thread: {parameter}";
            }
            if (CurrentThread.Name == "Reader")
            {
                TextBlockInfoReaderPriority = CurrentThread.Name + $" Thread: {parameter}";
            }
        }
    }
}
