using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectInitialization
{
    class Program
    {
        static void Main(string[] args)
        {
            var p1 = new Point();
            p1.X = 10;
            p1.Y = 20;
            p1.Display();

            var p2 = new Point(30, 40);
            p2.Display();

            var p3 = new Point { X = 50 }; //in this case default ctor is called behind the scenes, then X property is assigned
            p3.Display();
            p3.Y = 60;
            p3.Display();

            Console.ReadKey();
        }
    }

    class Point
    {
        public Point(int x, int y)
        {
            Console.WriteLine("Parametrized ctor is called");
            X = x;
            Y = y;
        }

        public Point()
        {
            Console.WriteLine("Default ctor is called");
        }
        public int X { get; set; }
        public int Y { get; set; }

        public void Display()
        {
            Console.WriteLine($"X = {X}, Y = {Y}");
        }

    }
}
