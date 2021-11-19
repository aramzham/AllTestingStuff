using System;
using System.Collections.Generic;

namespace ReverseLinkedListByRecursion
{
    class Program
    {
        static void Main(string[] args)
        {
            var node1 = new Node<int>(1);
            var node2 = new Node<int>(2);
            var node3 = new Node<int>(3);
            var node4 = new Node<int>(4);
            var node5 = new Node<int>(5);

            node1.Next = node2;
            node2.Next = node3;
            node3.Next = node4;
            node4.Next = node5;

            Console.WriteLine(ToString(node1));

            var reverse = Reverse(node1);

            Console.WriteLine(ToString(node5));
        }

        private static Node<T> Reverse<T>(Node<T> node)
        {
            if (node?.Next is null) return node;
            var listNode = Reverse<T>(node.Next);
            node.Next.Next = node;
            node.Next = null;
            return listNode;
        }

        private static string ToString<T>(Node<T> node)
        {
            var list = new List<T>();
            while (node is not null)
            {
                list.Add(node.Data);
                node = node.Next;
            }

            return string.Join(", ", list);
        }
    }
}
