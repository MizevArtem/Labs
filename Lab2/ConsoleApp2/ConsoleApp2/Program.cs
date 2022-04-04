using System;
using ClassLibrary2;
using System.Threading;

namespace ConsoleApp2
{
    public class Program
    {
        static void Main(string[] args)
        {
            MyRandom rnd = new MyRandom(System.DateTime.Now.Millisecond);
            PersonList personList = new PersonList();
            const int countElements = 15;
            for (int i = 0; i < countElements; i++)
            {
                Thread.Sleep(5);
                if (rnd.Next(false, true))
                {
                    personList.AddPerson(Adult.GetRandomPerson(rnd));
                }
                else
                {
                    personList.AddPerson(Child.GetRandomPerson(rnd));
                }
            }
            FormationFamilies(ref personList);
            PrintList(personList);
            var person = personList.GetByIndex(3);
            switch (person)
            {
                case Adult adult:
                    Adult partner = adult.GetPartner();
                    PrintPerson("Partner is ", partner);
                    break;
                case Child child:
                    (Adult Father, Adult Mother) = child.GetParents();
                    PrintPerson("Father: ", Father);
                    PrintPerson("Mother: ", Mother);
                    break;
            }
            Console.ReadLine();
        }

        /// <summary>
        /// Вывод на экран листа с людьми
        /// </summary>
        /// <param name="personList">Выводимый лист</param>
        static void PrintList(PersonList personList)
        {
            for (int i = 0; i < personList.CountOfPersons; i++)
            {
                PrintPerson($"{i + 1}) ", personList.GetByIndex(i));
            }
        }

        /// <summary>
        /// Вывод на экран информации о человеке
        /// </summary>
        /// <param name="prefix">Сообщение перед ифнормацией</param>
        /// <param name="person">Человек для вывода его информации</param>
        static void PrintPerson(string prefix, PersonBase person)
        {
            if (person is null)
            { 
                Console.WriteLine(prefix + "missing person");
            }
            else
            {
                Console.WriteLine(prefix + person.PersonInfo());
            }
        }

        /// <summary>
        /// Распределение людей по семьям
        /// </summary>
        /// <param name="personList">выводимый лист</param>
        static void FormationFamilies(ref PersonList personList)
        {
            Random rnd = new Random();
            SortingList(personList, 
                out var mansList, out var womansList, out var childrensList);
            
            int countMan = mansList.CountOfPersons;
            Adult husband = null;
            int countChildren = 0;
            for (int i = 0; i < countMan; i++)
            {
                int countWomans = womansList.CountOfPersons;
                countChildren = childrensList.CountOfPersons;
                husband = mansList.GetByIndex(i) as Adult;
                Adult wife = null;
                if (countWomans != 0)
                {
                    int nextWoman = rnd.Next(0, countWomans);
                    wife = womansList.GetByIndex(nextWoman) as Adult;
                    husband.MaritalStatus = MaritalStatus.Married;
                    husband.Partner = wife;
                    wife.MaritalStatus = MaritalStatus.Married;
                    wife.Partner = husband;
                    womansList.DeleteByIndex(nextWoman);
                }
                int chilrenInFamily = rnd.Next(0, 3);
                if (chilrenInFamily > countChildren)
                {
                    chilrenInFamily = countChildren;
                }
                for (int j = 0; j < chilrenInFamily; j++)
                {
                    int numberOfChild = rnd.Next(0, countChildren);
                    Child child = childrensList.GetByIndex(numberOfChild) as Child;
                    child.Father = husband;
                    child.Mother = wife;
                    childrensList.DeleteByIndex(numberOfChild);
                    countChildren = childrensList.CountOfPersons;
                }
            }
            if (countChildren > 0)
            {
                for (int i = 0; i < countChildren; i++)
                {
                    Child child = childrensList.GetByIndex(i) as Child;
                    child.Father = husband;
                    if (!(husband.Partner is null))
                    {
                        child.Mother = husband.Partner;
                    }
                }
            }
        }

        /// <summary>
        /// Распределение людей по группам
        /// </summary>
        /// <param name="personList">Распредяемые лист</param>
        /// <param name="mansList">Лист мужчин</param>
        /// <param name="womansList">Лист женщин</param>
        /// <param name="childrensList">Лист детей</param>
        static void SortingList(PersonList personList, 
        out PersonList mansList, out PersonList womansList, out PersonList childrensList)
        {
            mansList = new PersonList();
            womansList = new PersonList();
            childrensList = new PersonList();
            for (int i = 0; i < personList.CountOfPersons; i++)
            {
                PersonBase person = personList.GetByIndex(i);
                if (person is Adult)
                {
                    if (person.Gender == PossibleGender.Male)
                    {
                        mansList.AddPerson(person);
                    }
                    else
                    {
                        womansList.AddPerson(person);
                    }
                }
                else
                {
                    childrensList.AddPerson(person);
                }
            }
        }
    }
}