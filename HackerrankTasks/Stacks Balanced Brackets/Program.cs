using System;
using System.Collections.Generic;
using System.Linq;

namespace Stacks_Balanced_Brackets
{
    class Program
    {
        static void Main(string[] args) //not fully done!
        {
            int t = Convert.ToInt32(Console.ReadLine());
            var list = new List<string>();
            for (int a0 = 0; a0 < t; a0++)
            {
                string expression = Console.ReadLine();
                list.Add(expression);
            }
            foreach (var item in list)
            {
                Console.WriteLine(CheckWellFormed(item) ? "Yes" : "No");
            }
            Console.ReadKey();
        }
        private static bool CheckWellFormed(string input)
        {
            var stack = new Stack<char>();
            // dictionary of matching starting and ending pairs
            var allowedChars = new Dictionary<char, char>() { { '(', ')' }, { '[', ']' }, { '{', '}' } };

            var wellFormated = true;
            foreach (var chr in input)
            {
                if (allowedChars.ContainsKey(chr))
                {
                    // if starting char then push on stack
                    stack.Push(chr);
                }
                // ContainsValue is linear but with a small number is faster than creating another object
                else if (allowedChars.ContainsValue(chr))
                {
                    // make sure something to pop if not then know it's not well formated
                    wellFormated = stack.Any();
                    if (wellFormated)
                    {
                        // hit ending char grab previous starting char
                        var startingChar = stack.Pop();
                        // check it is in the dictionary
                        wellFormated = allowedChars.Contains(new KeyValuePair<char, char>(startingChar, chr));
                    }
                    // if not wellformated exit loop no need to continue
                    if (!wellFormated)
                    {
                        break;
                    }
                }
            }
            return wellFormated;
        }
    }
}
