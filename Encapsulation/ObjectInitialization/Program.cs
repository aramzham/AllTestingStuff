using System;

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

            var p4 = new Point(70, 80) { X = 90, Y = 100 }; //not a great use
            p4.Display();

            var p5 = new Point(PointColor.LightBlue) {X = 110,Y = 120}; //there is already a sense
            p5.Display();

            var rectangle = new Rectangle()
            {
                BottomRight = new Point() {X = 10, Y = 20,PointColor = PointColor.Gold},
                TopLeft = new Point() {X = 100, Y = 200, PointColor = PointColor.BloodRed}
            };
            rectangle.DisplayStats();

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

        public Point() : this(PointColor.BloodRed)
        {
            Console.WriteLine("Default ctor is called");
        }

        public Point(PointColor pointColor)
        {
            Console.WriteLine("One parameter ctor is called");
            PointColor = pointColor;
        }
        public int X { get; set; }
        public int Y { get; set; }
        public PointColor PointColor { get; set; }

        public void Display()
        {
            Console.WriteLine($"X = {X}, Y = {Y}");
        }
    }
    class Rectangle
    {
        public Point TopLeft { get; set; } = new Point();
        public Point BottomRight { get; set; } = new Point();

        public void DisplayStats()
        {
            Console.WriteLine("[TopLeft: {0}, {1}, {2} BottomRight: {3}, {4}, {5}]",
            TopLeft.X, TopLeft.Y, TopLeft.PointColor,
            BottomRight.X, BottomRight.Y, BottomRight.PointColor);
        }
    }

    enum PointColor
    {
        LightBlue,
        BloodRed,
        Gold
    }
}
