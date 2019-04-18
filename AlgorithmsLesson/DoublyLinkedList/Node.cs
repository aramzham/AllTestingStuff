using System;
using System.Collections.Generic;
using System.Text;

namespace DoublyLinkedList
{
    public class Node<T>
    {
        public T Data { get; set; }
        public Node<T> Previous { get; set; }
        public Node<T> Next { get; set; }

        public Node(T input)
        {
            Data = input;
            Previous = null;
            Next = null;
        }

        public override string ToString()
        {
            return this.Data.ToString();
        }
    }
}
