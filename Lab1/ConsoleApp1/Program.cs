using System;
using System.Linq;
using ClassLibrary1;
using System.Collections;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace ConsoleApp1
{
    /// <summary>
    /// Возможные действия с PersonList
    /// </summary>
    public enum ListMenuItems
    {
        AddPerson,
        PrintList,
        DeletePersonByIndex,
        DeletePersonByAnthroponym,
        ClearList,
        Exit
    }

    class Program
    {
        //TODO: | Дописано summary
        /// <summary>
        /// Массив листов для работы программы
        /// </summary>
        static PersonList[] Lists;

        public static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            Console.InputEncoding = System.Text.Encoding.Unicode;
            string action;
            do
            {
                Console.WriteLine("Хотите протестировать программу? (Y/N)");
                Console.WriteLine("Если хотите завершить работу программы введите \"exit\"");
                action = Console.ReadLine();
                //Тест программы по заданию л.р.   
                if (action == "Y")      
                {
                    TestProgram();
                }
                //Ручная работа с PersonList
                else if (action == "N") 
                {
                    CreateAndFillingLists(1);
                    ListMenuItems actionList;
                    do
                    {
                        Console.WriteLine("Введите номер действия, которое хотите выполнить:");
                        Console.WriteLine("1.Добавить нового человека");
                        Console.WriteLine("2.Вывести сохраненный список людей");
                        Console.WriteLine("3.Удалить человека по индексу в списке");
                        Console.WriteLine("4.Удалить человека по фамилии и имени");
                        Console.WriteLine("5.Полностью очистить список");
                        Console.WriteLine("6.Назад");
                        //TODO: | done
                        actionList = (ListMenuItems)int.Parse(Console.ReadLine()) - 1;
                        switch (actionList)
                        {
                            case ListMenuItems.AddPerson:                                                              
                                try
                                {
                                    Lists[0].AddPerson(CreatePerson()); 
                                }
                                catch (Exception)
                                {
                                    Console.WriteLine("Не удалось создать запись о человеке!");
                                    Console.WriteLine("Пожалуйста, следуйте требованиям формата.");
                                }
                                break;
                            case ListMenuItems.PrintList: 
                                PrintList(new int[] {0});
                                break;
                            case ListMenuItems.DeletePersonByIndex:
                                //TODO: дубль | Ликвидирован
                                int index = -1;
                                var actionsTuple = Tuple.Create<Action, string>
                                (
                                    () =>
                                    {
                                        if (int.TryParse(Console.ReadLine(), out index)) { }
                                        else
                                        {
                                            throw new FormatException("Не удалось распознать номер");
                                        }
                                        Lists[0].DeleteByIndex(index - 1);
                                    },
                                    $"Введите номер человека в списке, которого необходимо удалить"
                                );
                                ActionHandler(actionsTuple.Item1, actionsTuple.Item2);
                                break;
                            case ListMenuItems.DeletePersonByAnthroponym:
                                Console.WriteLine("Введите имя человека, которого необходимо удалить");
                                string mySuperName = Console.ReadLine();
                                Console.WriteLine("Введите фамилию человека, которого необходимо удалить");
                                string lastName = Console.ReadLine();
                                Console.WriteLine(Lists[0].DeletePersonByAnthroponym(mySuperName, lastName)
                                    ? $"Запись о человека \"{mySuperName} {lastName}\" удалена"
                                    : $"Данных о введеном человеке не обнаружено");
                                break;
                            case ListMenuItems.ClearList:
                                Lists[0].DeleteAllPeople();
                                Console.WriteLine($"Список успешно очищен");
                                break;
                            case ListMenuItems.Exit:  
                                break;
                            default:  
                                Console.WriteLine("Не удалось определить команду, повторите ввод...");
                                break;
                        }
                        Console.WriteLine();
                    //Заверешние ручной работы с PersonList
                    } while (actionList != ListMenuItems.Exit);    
                }
                else if (action != "exit")
                {
                    Console.WriteLine("Не удалось определить ответ.");
                }
            //Завершение работы с программой
            } while (action != "exit");   

            Console.WriteLine("==================ЦеноК=======================");
            Console.ReadLine();
        }

        /// <summary>
        /// Процедура демонстрации функционала класса
        /// </summary>
        static void TestProgram()
        {
            const int countList = 2;
            const int countElements = 3;
            Console.WriteLine("=================CREATE 2 LISTS===============");
            //TODO: to const | ->const
            CreateAndFillingLists(countList, countElements);
            //TODO: дубль | Ликвидирован (?)
            PrintList(new int[] { 0, 1 });

            Console.WriteLine("==============ADD PERSON -> 1 LIST============");
            Lists[0].AddPerson(Person.GetRandomPerson());
            PrintList(new int[] { 0 });

            Console.WriteLine("=============COPY PERSON -> 2 LIST============");
            Lists[1].AddPerson(Lists[0].GetByIndex(1));
            //TODO: дубль | Ликвидирован (?)
            PrintList(new int[] { 0, 1 });

            Console.WriteLine("==============DELETE FROM 1 LIST=============");
            Lists[0].DeleteByIndex(1);
            //TODO: дубль | Ликвидирован (?)
            PrintList(new int[] { 0, 1 });

            Console.WriteLine("==============CLEARE ALL 2 LIST===============");
            Lists[1].DeleteAllPeople();
            //TODO: дубль | Ликвидирован (?)
            PrintList(new int[] { 0, 1 });
        }

        /// <summary>
        /// Action Handler
        /// </summary>
        /// <param name="action">Обрабатываемое событие</param>
        /// <param name="inputMessage">Сообщение в консоль</param>
        static void ActionHandler(Action action, string inputMessage)
        {
            while (true)
            {
                Console.WriteLine(inputMessage);
                try
                {
                    action.Invoke();
                    return;
                }
                catch (Exception e)
                {
                    if (e is FormatException
                        || e is ArgumentException)
                    {
                        Console.WriteLine(e.Message);
                    }
                    else
                    {
                        throw;
                    }
                }
            }
        }

        /// <summary>
        /// Функция обработки чтения имени/фамилии
        /// </summary>
        /// <param name="parameter">Строка "Имя" или "Фамилия"</param>
        /// <returns>Строка с именем/фамилией</returns>
        static string ReadNames(string parameter, out List<string> languageContained)
        {
            Console.WriteLine($"{parameter} нового человека (кириллица или латиница):");
            string name;
            languageContained = new List<string>(); 
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
                    Console.WriteLine("В строке содержится(-атся) цифра(-ы), повторите ввод без них");
                    continue;
                }
                languageContained = CheckLanguage(name);
                if (languageContained.Count > 1)
                {
                    badData = true;
                    Console.WriteLine($"{parameter} содержит символы нескольких алфавитов, повторите ввод");
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

        //TODO: | Перелопачено
        /// <summary>
        /// Функция определения алфавита(-ов) символов в имени/фамилии
        /// </summary>
        /// <param name="name">Имя или фамилия</param>
        /// <returns>Лист языков из которых состоит вводимая строка</returns>
        static List<string> CheckLanguage(string name)
        {
            Dictionary<string, string> languagesDictionary 
                                = new Dictionary<string, string>();
            languagesDictionary.Add("Russian", @"[а-я]+");
            languagesDictionary.Add("English", @"[a-z]+");

            List<string> languageContained = new List<string>();
            foreach (var language in languagesDictionary)
            {
                if (Regex.IsMatch(name.ToLower(), language.Value))
                {
                    languageContained.Add(language.Key);
                }
            }
            return languageContained;
        }

        /// <summary>
        /// Функция преобразования в "правильный" регистр
        /// </summary>
        /// <param name="name">Имя или фамилия</param>
        /// <returns>Имя/Фамилия в "правильном" регистре</returns>
        static string NormalizationNames(string name)
        {
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
        /// Процедура считывания, обработки данных и создания объекта Person
        /// </summary>
        /// <returns>Экземпляр класса Person</returns>
        static Person CreatePerson()
        {
            string mySuperName = ReadNames("Имя", out var checkFirstName);
            string lastName = ReadNames("Фамилия", out var checkLastName);
            while (!checkFirstName.SequenceEqual(checkLastName))
            {
                Console.WriteLine("Имя и фамилия заданы на разных языках!");
                Console.WriteLine("Что хотите задать заново? " +
                                    "(F-FirstName/L-LastName)");
                string actionChangeName = Console.ReadLine();
                switch (actionChangeName)
                {
                    case "F":
                        mySuperName = ReadNames("Имя", out checkFirstName);
                        break;
                    case "L":
                        lastName = ReadNames("Фамилия", out checkLastName);
                        break;
                    default:
                        Console.WriteLine("Не удалось Вас понять");
                        break;
                }
            }
            //TODO: дубль | Ликвидирован
            int age = -1;
            var actionsTuple = Tuple.Create<Action, string>
            (
                () =>
                {
                    if (int.TryParse(Console.ReadLine(), out age)) { }
                    else
                    {
                        throw new FormatException("Не удалось распознать возраст, повторите ввод...");
                    }
                    if (age < Person.AgeMin || age > Person.AgeMax)
                    {
                        throw new ArgumentException("Некоректный возраст");
                    }
                },
                $"Введите возраст человека от 0 до {Person.AgeMax}"
            );
            ActionHandler(actionsTuple.Item1, actionsTuple.Item2);
            
            Gender gender = PossibleGender.Indefinite;
            actionsTuple = Tuple.Create<Action, string>
            (
                () =>
                {
                    if (Gender.TryParse(int.Parse(Console.ReadLine()), out gender))  { }
                    else
                    {
                        throw new FormatException("Не удалось распознать гендер, повторите ввод...");
                    }
                },
                $"Введите гендер человека, 1 - Мужчина, 2 - Женщина"
            );
            ActionHandler(actionsTuple.Item1, actionsTuple.Item2);

            return new Person(mySuperName, lastName, age, (PossibleGender) gender);
        }

        /// <summary>
        /// Вывод в консоль содержимого листов
        /// </summary>
        /// <param name="indexs">Номера листов в глобальном массиве</param>
        static void PrintList(int [] indexs)  
        {
            foreach (var index in indexs)
            {
                Console.WriteLine($"Список №{index + 1}:");
                for (int i = 0; i < Lists[index].CountOfPersons; i++)
                {
                    Console.WriteLine($"{i + 1})" + Lists[index].GetByIndex(i).PersonInfo);
                }
                Console.WriteLine();
            }
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
