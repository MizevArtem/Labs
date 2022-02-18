using System;
using System.Linq;
using ClassLibrary1;
using System.Collections;
using System.Text.RegularExpressions;

namespace ConsoleApp1
{
    class Program
    {
        static PersonList[] Lists;

        public static void Main(string[] args)
        {
            string action;
            do
            {
                Console.WriteLine("Хотите протестировать программу? (Y/N)");
                Console.WriteLine("Если хотите завершить работу программы введите \"exit\"");
                action = Console.ReadLine();
                if (action == "Y")      //Тест программы по заданию л.р.   
                {
                    Console.WriteLine("=================CREATE 2 LISTS===============");
                    CreateAndFillingLists(2, 3);
                    for (int i = 0; i < 2; i++)
                        PrintList(i);

                    Console.WriteLine("==============ADD PERSON -> 1 LIST============");
                    Lists[0].AddPerson(Person.GetRandomPerson());
                    PrintList(0);

                    Console.WriteLine("=============COPY PERSON -> 2 LIST============");
                    Lists[1].AddPerson(Lists[0].GetByIndex(1));
                    for (int i = 0; i < 2; i++)
                        PrintList(i);

                    Console.WriteLine("==============DELETE FROM 1 LIST=============");
                    Lists[0].DeleteByIndex(1);
                    for (int i = 0; i < 2; i++)
                        PrintList(i);

                    Console.WriteLine("==============CLEARE ALL 2 LIST===============");
                    Lists[1].DeleteAllPeople();
                    for (int i = 0; i < 2; i++)
                        PrintList(i);
                }
                else if (action == "N") //Ручная работа с PersonList
                {
                    CreateAndFillingLists(1);
                    do
                    {
                        Console.WriteLine("Введите номер действия, которое хотите выполнить:");
                        Console.WriteLine("1.Добавить нового человека");
                        Console.WriteLine("2.Вывести сохраненный список людей");
                        Console.WriteLine("3.Удалить человека по индексу в списке");
                        Console.WriteLine("4.Удалить человека по фамилии и имени");
                        Console.WriteLine("5.Полностью очистить список");
                        Console.WriteLine("6.Назад");
                        action = Console.ReadLine();
                        switch (action)
                        {
                            case "1":  // Добавление нового человека
                                string name = ReadNames("Имя");
                                string lastName = ReadNames("Фамилия");
                                BitArray checkFirstName = CheckLanguage(name);
                                BitArray checkLastName = CheckLanguage(lastName);
                                //     RE       RE        RE        RE
                                //FN   01       10        10        01
                                //LN   10       01        10        01
                                //Res  00(0)    00(0)     10(1)     01(1) 
                                while (!(checkFirstName[0] & checkLastName[0] ||
                                         checkFirstName[1] & checkLastName[1]))
                                {
                                    Console.WriteLine("Имя и фамилия заданы на разных языках!");
                                    Console.WriteLine("Что хотите задать заново? " +
                                                        "(F-FirstName/L-LastName)");
                                    action = Console.ReadLine();
                                    if (action == "F")
                                    {
                                        name = ReadNames("Имя");
                                        checkFirstName = CheckLanguage(name);
                                    }
                                    else if (action == "L")
                                    {
                                        lastName = ReadNames("Фамилия");
                                        checkLastName = CheckLanguage(lastName);
                                    }
                                    else { Console.WriteLine("Не удалось Вас понять"); }
                                }
                                
                                int age;
                                Console.WriteLine($"Введите возраст человека от 0 до {Person._ageMax}");
                                bool converted = int.TryParse(Console.ReadLine(), out age);
                                while (!converted || age < 0 || age > Person._ageMax)
                                {
                                    Console.WriteLine("Не корректный возраст, повторите ввод");
                                    converted = int.TryParse(Console.ReadLine(), out age);
                                }
                                
                                try
                                {
                                    Lists[0].AddPerson(new Person(name, lastName, age, Gender.Male));
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine(e.Message);
                                }
                                break;
                            case "2":  // Выведение списка в консоль
                                PrintList(0);
                                break;
                            case "3": // Удаление Person по индексу
                                Console.WriteLine("Введите номер человека в списке, которого необходимо удалить");
                                int index;
                                converted = int.TryParse(Console.ReadLine(), out index);
                                while(!converted)
                                {
                                    Console.WriteLine("Не удалось распознать номер, повторите ввод");
                                    converted = int.TryParse(Console.ReadLine(), out index);
                                }
                                try
                                {
                                    Lists[0].DeleteByIndex(index - 1);
                                    Console.WriteLine($"Запись человека под номером {index} успешно удалена");
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine(e.Message);
                                }
                                break;
                            case "4":  // Удаление Person по Имени и Фамилии 
                                Console.WriteLine("Введите имя человека, которого необходимо удалить");
                                name = Console.ReadLine();
                                Console.WriteLine("Введите фамилию человека, которого необходимо удалить");
                                lastName = Console.ReadLine();
                                if (Lists[0].DeletePersonByAnthroponym(name, lastName))
                                {
                                    Console.WriteLine($"Запись о человека \"{name} {lastName}\" удалена");
                                }
                                else
                                {
                                    Console.WriteLine($"Данных о введеном человеке не обнаружено");
                                }
                                break;
                            case "5":  // Полная очистка списка
                                Lists[0].DeleteAllPeople();
                                Console.WriteLine($"Список успешно очищен");
                                break;
                            case "6":  // Выход назад
                                break;
                            default:  // Иное
                                Console.WriteLine("Не удалось определить команду, повторите ввод");
                                break;
                        }
                        Console.WriteLine();
                    } while (action != "6");    //Заверешние ручной работы с PersonList
                }
                else if (action != "exit")
                {
                    Console.WriteLine("Не удалось определить ответ.");
                }
            } while (action != "exit");   //Завершение работы с программой

            Console.WriteLine("==================ЦеноК=======================");
            Console.ReadLine();
        }

        /// <summary>
        /// Функция обработки чтения имени/фамилии
        /// </summary>
        /// <param name="parameter">Строка "Имя" или "Фамилия"</param>
        /// <returns>Строка с именем/фамилией</returns>
        static string ReadNames(string parameter)
        {
            Console.WriteLine($"{parameter} нового человека:");
            string name;
            bool badData;
            do
            {
                badData = false;
                name = Console.ReadLine();
                if (name == string.Empty)
                {
                    badData = true;
                    Console.WriteLine("Введена пустая строка, повторите ввод");
                    continue;
                }
                if (name.Any(char.IsNumber))
                {
                    badData = true;
                    Console.WriteLine("В строке содержится(-атся) цифра(-ы), повторите ввод");
                    continue;
                }
                BitArray checkName = CheckLanguage(name);
                // XOR(^):
                // 00 -> 0
                // 01 -> 1
                // 10 -> 1
                // 11 -> 0
                if (!(checkName[0] ^ checkName[1]))
                {
                    badData = true;
                    Console.WriteLine($"{parameter} содержит и латиницу и кирилицу, повторите ввод");
                    continue;
                }
            } while (badData);
            
            string normalizedName = NormalizationNames(name);
            if (normalizedName != name)
            {
                Console.WriteLine($"Возможно вы ввели ошиблись при введении, " +
                    $"не хотите изменить на \"{normalizedName}\"? (Y/N)");
                string action = Console.ReadLine();
                if (action == "Y")
                {
                    name = normalizedName;
                }
            }
            return name;
        }

        /// <summary>
        /// Функция определения алфавита символов в имени/фамилии
        /// </summary>
        /// <param name="name">Имя или фамилия</param>
        /// <returns>Два бита отвечающие за 2 языка</returns>
        static BitArray CheckLanguage(string name)
        {
            BitArray bitArray = new BitArray(2, false); // 00
            var language = Regex.IsMatch(name.ToLower(), @"\p{IsCyrillic}");
            if (language)
            {
                bitArray[0] = true;   //Russian -> 10
            }
            //language = Regex.IsMatch(name.ToLower(), @"\p{IsBasicLatin}");
            language = name.ToLower().Any(c => (int)c > 96 && (int)c < 123);
            if (language)
            {
                bitArray[1] = true; // English -> 01
            }
            return bitArray;
        }

        /// <summary>
        /// Функция преобразования в "правильный" регистр
        /// </summary>
        /// <param name="name">Имя или фамилия</param>
        /// <returns>Имя/Фамилия в "правильном" регистре</returns>
        static string NormalizationNames(string name)
        {
            /*name = name.Substring(0, 1).ToUpper() +
                    name.Substring(1).ToLower();*/
            name = name.ToLower();
            var symbols = new[] { "-", " " };
            foreach (var symbol in symbols)
            {
                string buffer = "";
                string[] nameParts = name.Split(symbol);
                foreach (string part in nameParts)
                {
                    buffer += part.Substring(0,1).ToUpper() +
                        part.Substring(1) + symbol;
                }
                buffer = buffer.Remove(buffer.Length - 1);
                name = buffer;
            }
            return name;
        }

        /// <summary>
        /// Вывод в консоль 
        /// </summary>
        /// <param name="index">Номер листа в глобальном массиве</param>
        static void PrintList(int index)  //Надо бы поменять на ссылку на PersonList
        {
            Console.WriteLine($"Список №{index + 1}:");
            for (int i = 0; i < Lists[index].CountOfPersons; i++)
            {
                Console.WriteLine($"{i + 1})" + Lists[index].GetByIndex(i).PersonInfo);
            }
            Console.WriteLine();
        }

        /// <summary>
        /// Создание нужного количества листов в массиве с 
        /// определенным количеством элементов внутри
        /// </summary>
        /// <param name="сountLists">Необходимое количество PersonList-ов</param>
        /// <param name="сountElements">Количество элементов в листах</param>
        static void CreateAndFillingLists(int сountLists, int сountElements = 0)
        {
            Lists = new PersonList[сountLists];
            for (int i = 0; i < сountLists; i++)
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
