using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main
{
    //Class for containing threads that are intended to contain file manipulating methods
    class ThreadContainer
    {
        public Mutex locker = new();
        Thread? _writer;
        Thread? _reader;
        public volatile bool stopThreads = false; 
        public Thread? Writer 
        {  
            get { if (_writer != null) return _writer; else return null; }
            set { _writer = value; }
        }
        public Thread? Reader
        {
            get { if (_reader != null)  return _reader; else return null; }
            set {  _reader = value; }
        }
        public ThreadContainer(Action writerAction, Action listenerAction) 
        {
            Writer = new Thread(new ThreadStart(writerAction));
            Reader = new Thread(new ThreadStart(listenerAction));
            Writer.Name = "Writer";
            Reader.Name = "Reader";
            Writer.Priority = ThreadPriority.Normal;
            Reader.Priority = ThreadPriority.Normal;
        }
    }
}
