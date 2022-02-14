using System;
using ClassLibrary1;

namespace ConsoleApp1
{
    class Program
    {
        static PersonList[] Lists;

        public static void Main(string[] args)
        {
            if (true)
            {
                FillingLists(3);
                for (int i = 0; i < 2; i++)
                    PrintList(i);
                Console.WriteLine("===============================================");

                Lists[0].AddPerson(Person.GetRandomPerson());
                PrintList(0);

                Console.WriteLine("===============================================");
                Lists[1].AddPerson(Lists[0].GetByIndex(1));
                for (int i = 0; i < 2; i++)
                    PrintList(i);

                Console.WriteLine("===============================================");
                Lists[0].DeleteByIndex(1);
                for (int i = 0; i < 2; i++)
                    PrintList(i);

                Console.WriteLine("===============================================");
                Lists[1].DeleteAllPeople();
                for (int i = 0; i < 2; i++)
                    PrintList(i);
                Console.ReadLine();
            }
        }

        static void PrintList(int index)
        {
            Console.WriteLine($"Список №{index + 1}:");
            for (int i = 0; i < Lists[index].CountOfPersons; i++)
            {
                Console.WriteLine($"{i + 1})" + Lists[index].GetByIndex(i).GetPersonInfo);
            }
            Console.WriteLine();
        }

        static void FillingLists(int сountElements)
        {
            Lists = new PersonList[2];
            for (int i = 0; i < 2; i++)
            {
                Lists[i] = new PersonList();
                for (int j = 0; j < сountElements; j++)
                {
                    Lists[i].AddPerson(Person.GetRandomPerson());
                }
            }
        }
    }
}
