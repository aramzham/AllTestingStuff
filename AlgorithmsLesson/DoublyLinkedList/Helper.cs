using System;
using System.Collections.Generic;
using System.Text;

namespace DoublyLinkedList
{
    /// <summary>
    /// Helper class
    /// </summary>
    public class Helper
    {
        private int _l;
        private int _r;

        /// <summary>
        /// Generates a random integer number
        /// </summary>
        /// <returns>random number</returns>
        public int GenerateRandomInt()
        {
            return new Random().Next(_l += 5, 100 + (_r += 5));
        }
    }
}
