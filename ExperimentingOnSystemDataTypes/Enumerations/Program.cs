﻿using System;

namespace Enumerations
{
    class Program
    {
        static void Main(string[] args)
        {
            var task = TodoList.CallNarek;
            Console.WriteLine(Enum.GetUnderlyingType(task.GetType()));
            Console.WriteLine(Enum.GetUnderlyingType(typeof(TodoList))); //without using a variable of the type

            //Type todoType = Enum.GetUnderlyingType(typeof (TodoList));
            Console.WriteLine($"Value of {task.ToString()} is {(byte)task}");

            Console.ReadKey();
        }
        private enum TodoList : byte
        {
            GetFriends = 2,
            SubscribeInGroups,
            CallNarek = 6,
            StickerOnDesktop
        }
    }
}