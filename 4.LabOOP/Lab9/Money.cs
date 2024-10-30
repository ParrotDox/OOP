using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab9
{
    internal class Point
    {
        //Делегат для инициализации события
        public delegate void PointEventHandler(Point sender, PointEventArgs e);
        public event PointEventHandler Notify;
        public void RegisterEventNotify(PointEventHandler del) 
        {
            Notify = del;
        }
        //Объявление и инициализация полей через посредника
        private double x {  get; set; }
        private double X 
        {
            get { return x; }
            set 
            {
                x = value; 
            }
        }
        private double y { get; set; }
        private double Y 
        {
            get { return y; }
            set { y = value; }
        }
        //Конструктор с передачей сообщения о событии
        public Point(double x, double y) 
        {
            X = x;
            Y = y;
            Notify(this, new PointEventArgs($"Creating point->cord.:[{x}:{y}]", x, y));
        }
    }
    //Класс сообщения при вызове событий в классе Point
    internal class PointEventArgs 
    {
        private string msg;
        private double x, y;
        public PointEventArgs(string msg, double x, double y) 
        {
            this.msg = msg;
            this.x = x;
            this.y = y;
        }
    }
}
