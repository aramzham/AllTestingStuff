using System;

namespace DLinkedList
{
    class Program
    {
        static void Main(string[] args)
        {
            var helper = new Helper();
            var list = new DoubleLinkedList<int>();
            list.InsertBeginning(helper.GenerateRandomInt());
            list.InsertEnd(helper.GenerateRandomInt());
            list.InsertBeginning(helper.GenerateRandomInt());
            list.InsertEnd(helper.GenerateRandomInt());
            list.InsertAfter(list.Head, helper.GenerateRandomInt());

            var head = list.FindNode(list.Head);
            Console.WriteLine($"head = {head}");

            var notExists = list.FindNode(new Node<int>(int.MinValue));
            Console.WriteLine($"Is exist = {notExists}");

            var removedFromBeginning = list.RemoveBeginning();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"removed from beginning {removedFromBeginning.Data}");
            Console.ForegroundColor = ConsoleColor.Gray;

            list.RemoveNode(list.Head);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("After removing list head it became");
            Console.ForegroundColor = ConsoleColor.Gray;

            list.PrintList();

            var otherList = new DoubleLinkedList<int>();
            otherList.InsertEnd(helper.GenerateRandomInt());
            otherList.InsertAfter(otherList.Head, helper.GenerateRandomInt());
            otherList.InsertEnd(helper.GenerateRandomInt());

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Created another list");
            Console.ForegroundColor = ConsoleColor.Gray;

            otherList.PrintList();

            list.AppendLists(otherList);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("After merging two lists we have");
            Console.ForegroundColor = ConsoleColor.Gray;

            list.PrintList();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Swapping {list.Head} with {otherList.Head}");
            Console.ForegroundColor = ConsoleColor.Gray;

            list.SwapNodes(list.Head, otherList.Head);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("After swapping we have");
            Console.ForegroundColor = ConsoleColor.Gray;

            list.PrintList();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Sorting the list");
            Console.ForegroundColor = ConsoleColor.Gray;

            // choose one of sorting methods
            list.QuickSort();
            //list.InsertionSort();
            list.PrintList();

            Console.ReadKey();
        }
    }
}
