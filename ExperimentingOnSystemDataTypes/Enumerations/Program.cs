using System;

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

            PrintEnum(task);

            Console.ReadKey();
        }
        private enum TodoList : byte
        {
            GetFriends = 2,
            SubscribeInGroups,
            CallNarek = 6,
            StickerOnDesktop,
            NotObijnik = 255
        }

        static void PrintEnum(Enum e)
        {
            Console.WriteLine($"The underlying storage type is {Enum.GetUnderlyingType(e.GetType())}");
            var arr = Enum.GetValues(e.GetType());

            for (int i = 0; i < arr.Length; i++)
            {
                Console.WriteLine($"Text: {arr.GetValue(i)}, numeric value: {arr.GetValue(i):D}");
            }
        }
    }
}
