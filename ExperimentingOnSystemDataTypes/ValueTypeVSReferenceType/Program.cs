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
            //Console.WriteLine(new string('-',50));

            //var r1 = new Rectangle("First rec",1,1,4,4);
            //var r2 = r1;

            //r2.rectInfo.infoString = "This is a new info";  //reference type goes reference, so if we change at a place, everything wiil be changed
            //r2.bottom = 777;  //unlike reference types, we create a copy of it and change that copy, not the original one

            //r1.Display();
            //r2.Display();
            Console.WriteLine(new string('-', 50));
            
            var anthony = new Person("Martial", 21);
            SendPersonByValue(anthony);
            anthony.Display();

            SendPersonByReference(ref anthony);
            anthony.Display();

            Console.ReadKey();
        }

        static void SendPersonByValue(Person p) // a copy of the reference to the caller's object is passed
        {
            p.age = 100;
            p = new Person("Nikki",20);
        }

        static void SendPersonByReference(ref Person p)
        {
            p.age = 888;
            p = new Person("Nikki", 20);
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

    class ShapeInfo
    {
        public string infoString;

        public ShapeInfo(string infoString)
        {
            this.infoString = infoString;
        }
    }

    struct Rectangle
    {
        public ShapeInfo rectInfo;
        public int top, left, bottom, right;

        public Rectangle(string rectInfo, int top, int left, int bottom, int right)
        {
            this.rectInfo = new ShapeInfo(rectInfo);
            this.top = top;
            this.left = left;
            this.bottom = bottom;
            this.right = right;
        }

        public void Display()
        {
            Console.WriteLine($"String = {rectInfo.infoString}, top = {top}, bottom = {bottom}, left = {left}, right = {right}");
        }
    }

    class Person
    {
        public string name;
        public int age;

        public Person(string name, int age)
        {
            this.name = name;
            this.age = age;
        }

        public Person()
        {
            
        }

        public void Display()
        {
            Console.WriteLine($"Name: {name}, Age: {age}");
        }
    }
}
