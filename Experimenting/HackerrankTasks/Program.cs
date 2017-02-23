using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackerrankTasks
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] tokens_n = Console.ReadLine().Split(' ');
            int n = Convert.ToInt32(tokens_n[0]);
            int k = Convert.ToInt32(tokens_n[1]);
            string[] a_temp = Console.ReadLine().Split(' ');
            int[] a = Array.ConvertAll(a_temp, Int32.Parse);
            
            var queue = new Queue<int>();
            foreach (int t in a)
                queue.Enqueue(t);

            for (int i = 0; i < k; i++)
            {
                queue.Enqueue(queue.Peek());
                queue.Dequeue();
            }
            foreach (var i in queue)
            {
                Console.Write($"{i} ");
            }

            Console.ReadKey();
        }
        private static IEnumerable<T> RotateLeft<T>(IEnumerable<T> container)
        {
            return container.Skip(1).Concat(container.Take(1));
        }
    }
}
