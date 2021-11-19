namespace ReverseLinkedListByRecursion
{
    public class Node<T>
    {
        public T Data { get; set; }
        public Node<T> Next { get; set; }

        public Node(T input)
        {
            Data = input;
            Next = null;
        }

        public override string ToString()
        {
            return this.Data.ToString();
        }
    }
}
