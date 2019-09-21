using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace TestsOfAllKinds
{
    // Math.Floor() function:   The two numbers 123.456 and 123.987 are rounded down to the nearest integer.This means that regardless of how close they are close to 124, they are rounded to 123.
    //Note:
    //Floor can be useful when rounding numbers that are part of a larger representation of another number.

    //Discussion.The Math.Floor method when given a positive number will erase the digits after the decimal place. But when it receives a negative number, it will erase the digits and increase the number's negativity by 1.
    //So:
    //Using Math.Floor on a negative number will still decrease the total number.This means it will always become smaller.
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Clone p1 and store new one in p2");
            var p1 = new Point(1, 2, "Peto");
            var p2 = (Point) p1.Clone();

            Console.WriteLine("Before modifications...");
            Console.WriteLine($"p1 = {p1}");
            Console.WriteLine($"p2 = {p2}");

            p2.Description.PetName = "Gayshut";
            p2.X = 3;

            Console.WriteLine("After modification");
            Console.WriteLine($"p1 = {p1}");
            Console.WriteLine($"p2 = {p2}");

            Console.WriteLine("Հարց հանդիսատեսին. ինչի փոխվավ p1-ի անունը ու ինչի չփոխվավ X կոորդինատի արժեքը: Ժամանակ...");

            Console.ReadLine();
        }

        private static bool IsSquare(int n)
        {
            int i = 1;
            while (true)
            {
                if (n < 0)
                    return false;
                if (n == 0)
                    return true;
                n -= i;
                i += 2;
            }
        }
    }

    class Point : ICloneable
    {
        public int X { get; set; }
        public int Y { get; set; }
        public PointDescription Description { get; set; } = new PointDescription();

        public Point(int x, int y, string petName)
        {
            X = x;
            Y = y;
            Description.PetName = petName;
        }

        public Point(int x, int y)
        {
            X = x; Y = y;
        }

        public override string ToString() => $"X = {X}; Y = {Y}; Name = {Description.PetName}; Id = {Description.PointId}";

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }

    internal class PointDescription
    {
        public string PetName { get; set; }
        public Guid PointId { get; set; }

        public PointDescription()
        {
            PetName = "No-name";
            PointId = Guid.NewGuid();
        }
    }
}
