using System;

namespace DoublyLinkedList
{
    class Program
    {
        static void Main(string[] args)
        {
            var list = new DoubleLinkedList<string>();
            list.InsertBeginning("Aram");
            list.InsertEnd("Vanik");
            list.InsertBeginning("First");
            list.InsertEnd("Last");
            list.InsertAfter(list.Head, "after_head");

            var head = list.FindNode(list.Head);
            var notExists = list.FindNode(new Node<string>(""));

            var removed_from_beginning = list.RemoveBeginning();
            list.RemoveNode(list.Head);
            list.PrintList();

            var otherList = new DoubleLinkedList<string>();
            otherList.InsertEnd("otherHead");
            otherList.InsertAfter(otherList.Head, "otherMiddle");
            otherList.InsertEnd("otherEnd");
            list.AppendLists(otherList);
            list.PrintList();

            list.SwapNodes(list.Head, otherList.Head);

            Console.ReadKey();
        }
    }
}
