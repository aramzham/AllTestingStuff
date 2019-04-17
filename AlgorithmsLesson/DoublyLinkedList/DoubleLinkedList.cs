using System;
using System.Collections.Generic;
using System.Text;

namespace DoublyLinkedList
{
    public class DoubleLinkedList<T>
    {
        private Node<T> _head;

        public Node<T> Head { get => _head;
            set
            {
                if (value is null)
                    throw new Exception("cannot insert null to head");

                if (Head.Next != null)
                {
                    Head.Next.Previous = value;
                    value.Next = Head.Next;
                }

                value.Previous = null;
            } }

        public bool IsEmpty => Head is null;

        public int Count { get; private set; } = 0;

        public DoubleLinkedList()
        {
            Head = null;
        }

        public void InsertBeginning(T input)
        {
            var insertedNode = new Node<T>(input);
            insertedNode.Next = Head;

            if (Head != null)
                Head.Previous = insertedNode;

            Head = insertedNode;

            Count++;
        }

        public void InsertEnd(T input)
        {
            var insertedNode = new Node<T>(input);

            if (Head is null)
            {
                Head = insertedNode;
                return;
            }

            var lastNode = GetLastNode();
            lastNode.Next = insertedNode;
            insertedNode.Previous = lastNode;

            Count++;
        }

        public void InsertEnd(Node<T> node)
        {
            if (Head is null)
            {
                Head = node;
                return;
            }

            var lastNode = GetLastNode();
            lastNode.Next = node;
            node.Previous = lastNode;

            Count++;
        }

        public void InsertAfter(Node<T> node, T value)
        {
            if (node is null)
                return;

            var insertedNode = new Node<T>(value);
            insertedNode.Next = node.Next;
            node.Next = insertedNode;
            insertedNode.Previous = node;

            if (insertedNode.Next != null)
                insertedNode.Next.Previous = insertedNode;

            Count++;
        }

        private Node<T> SearchNode(Node<T> n) //Find a given node
        {
            var index = Head;
            while (index != null)
            {
                if (index.Data.Equals(n.Data))
                    break;

                index = index.Next;
            }

            return index ?? null;
        }

        public bool FindNode(Node<T> node)
        {
            return SearchNode(node) != null;
        }

        public int ListLength()
        {
            return Count;
        }

        public Node<T> RemoveBeginning()
        {
            var temp = Head;
            if (Head != null)
            {
                Head = Head.Next;

                if (Head != null)
                    Head.Previous = null;
            }

            Count--;

            return temp;
        }

        public void RemoveNode(Node<T> n)
        {
            if (Head is null)
                return;

            var node = SearchNode(n);

            if (node is null)
                return;

            if (node != Head)
                node.Previous.Next = node.Next;

            if (node.Next != null)
                node.Next.Previous = node.Previous;

            Count--;
        }

        public void SwapNodes(Node<T> b, Node<T> f)
        {
            var a = b.Previous;
            var c = b.Next;
            var e = f.Previous;
            var g = f.Next;


            if (Head == b)
                Head = f;
            else if (Head == f)
                Head = b;

            if (a != null)
                a.Next = f;

            f.Previous = a;
            f.Next = c;
            if (c != null)
                c.Previous = f;

            if (e != null)
                e.Next = b;

            b.Previous = e;
            b.Next = g;
            if (g != null)
                g.Previous = b;

            //var last = GetLastNode();

            //if (last == b)
            //    last = f;
            //else if (last == f)
            //    last = b;
        }

        public void AppendLists(DoubleLinkedList<T> list)
        {
            if (list is null || list.Count == 0)
                return;

            Node<T> p_curr = _head, listCurrent = list.Head;
            Node<T> p_next, listNext;

            // While there are available positions in p;  
            while (p_curr != null && listCurrent != null)
            {
                p_next = p_curr.Next;
                listNext = listCurrent.Next;

                listCurrent.Next = p_next;
                p_curr.Next = listCurrent;

                p_curr = p_next;
                listCurrent = listNext;
            }
            list.Head = listCurrent;
        }

        public void PrintList()
        {
            Console.WriteLine(this);
        }

        public override string ToString()
        {
            if (Head is null)
                return "[]";
            else
            {
                var current = Head;
                var sb = new StringBuilder("[");

                while (current != null)
                {
                    if (current.Data != null)
                        sb.Append($"{current.Data}, ");
                    else
                        sb.Append("null, ");

                    current = current.Next;
                }

                sb = sb.Remove(sb.Length - 2, 2);
                sb.Append("]");

                return sb.ToString();
            }
        }

        private Node<T> GetLastNode()
        {
            var temp = Head;

            while (temp.Next != null)
            {
                temp = temp.Next;
            }

            return temp;
        }
    }
}
