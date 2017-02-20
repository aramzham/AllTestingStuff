using System;

namespace ValueTypeVSReferenceType
{
    class Program
    {
        static void Main(string[] args)
        {
            //var p1 = new Point(10,20);  //value type
            //var p2 = p1;
            //p1.Display();
            //p2.Display();

            //p1.X = 77;
            //p1.Display();
            //p2.Display();
            Console.WriteLine(new string('-',50));

            //var p3 = new PointRef(30,40); //reference type
            //var p4 = p3;
            //p3.Display();
            //p4.Display();

            //p3.X = 88;
            //p3.Display();
            //p4.Display();

            Console.ReadKey();
        }
    }

    struct Point
    {
        public int X;
        public int Y;

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public void Display()
        {
            Console.WriteLine($"X: {X}, Y: {Y}");
        }
    }

    class PointRef
    {
        public int X;
        public int Y;

        public PointRef(int X, int Y)
        {
            this.X = X;
            this.Y = Y;
        }

        public void Display()
        {
            Console.WriteLine($"X: {X}, Y: {Y}");
        }
    }


}
