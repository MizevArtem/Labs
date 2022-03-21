using System;
using System.Linq;
using ClassLibrary1;
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
       
        public static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            Console.InputEncoding = System.Text.Encoding.Unicode;
            
            (PersonList List, string Name)[] namedLists;

            string action;
            string exitAction = "exit";
            do
            {
                Console.WriteLine("Хотите протестировать программу? (Y/N)");
                Console.WriteLine($"Если хотите завершить работу программы введите \"{exitAction}\"");
                action = Console.ReadLine();
                //Тест программы по заданию л.р.   
                if (action == "Y")      
                {
                    TestProgram(out namedLists);
                }
                //Ручная работа с PersonList
                else if (action == "N") 
                {
                    CreateAndFillingLists(out namedLists, 1, new string[] { "Лист №1" });
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
                        
                        if (!int.TryParse(Console.ReadLine(), out int input))
                        {
                            Console.WriteLine("Не удалось распознать число");
                            actionList = ListMenuItems.PrintList;
                            continue;
                        }
                        actionList = (ListMenuItems)(input - 1);
                        switch (actionList)
                        {
                        case ListMenuItems.AddPerson:                                                              
                            try
                            {
                                namedLists[0].List.AddPerson(CreatePerson()); 
                            }
                            catch (Exception)
                            {
                                Console.WriteLine("Не удалось создать запись о человеке!");
                                Console.WriteLine("Пожалуйста, следуйте требованиям формата.");
                            }
                            break;
                        case ListMenuItems.PrintList: 
                            PrintList(new (PersonList List, string Name)[] { namedLists[0] });
                            break;
                        case ListMenuItems.DeletePersonByIndex:
                            DeletePersonByIndex(namedLists[0]);
                            break;
                        case ListMenuItems.DeletePersonByAnthroponym:
                            DeletePersonByAnthroponym(namedLists[0]);
                            break;
                        case ListMenuItems.ClearList:
                                ClearList(namedLists[0]);
                            break;
                        case ListMenuItems.Exit:  
                            break;
                        default:  
                            Console.WriteLine("Не удалось определить команду, повторите ввод...");
                            break;
                        }
                        Console.WriteLine();
                    } while (actionList != ListMenuItems.Exit);    
                }
                else if (action != exitAction)
                {
                    Console.WriteLine("Не удалось определить ответ.");
                }
            } while (action != exitAction);   

            Console.WriteLine("==================ЦеноК=======================");
            Console.ReadLine();
        }

        /// <summary>
        /// Процедура демонстрации функционала класса
        /// </summary>
        /// <param name="namedLists">Проименовыние листы для теста программы</param>
        public static void TestProgram(out (PersonList List, string Name)[] namedLists)
        {
            const int countList = 2;
            const int countElements = 3;
            Console.WriteLine("=================CREATE 2 LISTS===============");
            CreateAndFillingLists(out namedLists, countList, 
                                new string[] { "СЭР", "САСДУ" }, countElements);
            PrintList(namedLists);

            Console.WriteLine("==============ADD PERSON -> 1 LIST============");
            namedLists[0].List.AddPerson(Person.GetRandomPerson());
            PrintList(new (PersonList List, string Name)[] { namedLists[0] });

            Console.WriteLine("=============COPY PERSON -> 2 LIST============");
            namedLists[1].List.AddPerson(namedLists[0].List.GetByIndex(1));
            PrintList(new (PersonList List, string Name)[] { namedLists[1] });

            Console.WriteLine("==============DELETE FROM 1 LIST=============");
            namedLists[0].List.DeleteByIndex(1);
            PrintList(namedLists);

            Console.WriteLine("==============CLEARE ALL 2 LIST===============");
            namedLists[1].List.DeleteAllPeople();
            PrintList(namedLists);
        }

        /// <summary>
        /// Action Handler
        /// </summary>
        /// <param name="action">Обрабатываемое событие</param>
        /// <param name="inputMessage">Сообщение в консоль</param>
        public static void ActionHandler(Action action, string inputMessage)
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
        /// Процедура удаление человека из списка по индексу
        /// </summary>
        /// <param name="NamedList">Список людей</param>
        public static void DeletePersonByIndex((PersonList List, string Name) NamedList)
        {
            if (NamedList.List.CountOfPersons == 0)
            {
                Console.WriteLine("Лист пуст: нет людей для удаления");
                return;
            }
            var actionsTuple = Tuple.Create<Action, string>
            (
                () =>
                {
                    if (!int.TryParse(Console.ReadLine(), out int index))
                    {
                        throw new FormatException("Не удалось распознать номер");
                    }
                    if (index != -1)
                    {
                        NamedList.List.DeleteByIndex(index - 1);
                    }
                },
                $"Введите номер человека в списке, которого необходимо удалить. \n" +
                $"Чтобы вернуться назад введите \"-1\""
            );
            ActionHandler(actionsTuple.Item1, actionsTuple.Item2);
        }

        /// <summary>
        /// Процедура удаление человека из списка по имени и фамилии
        /// </summary>
        /// <param name="NamedList">Список людей</param>
        public static void DeletePersonByAnthroponym((PersonList List, string Name) NamedList)
        {
            if (NamedList.List.CountOfPersons == 0)
            {
                Console.WriteLine("Лист пуст: нет людей для удаления");
                return;
            }
            (string FirstName, string SecondName) Anthroponym = ReadFirstSecondName();
            Console.WriteLine(NamedList.List.DeletePersonByAnthroponym
                                               (Anthroponym.FirstName, Anthroponym.SecondName)
                                    ? $"Запись о человека \"{Anthroponym.FirstName}" +
                                                         $" {Anthroponym.SecondName}\" удалена"
                                    : $"Данных о введеном человеке не обнаружено");
        }

        /// <summary>
        /// Процедура полной очистки листа
        /// </summary>
        /// <param name="NamedList">Список людей</param>
        public static void ClearList((PersonList List, string Name) NamedList)
        {
            if (NamedList.List.CountOfPersons == 0)
            {
                Console.WriteLine("Лист пуст: нет людей для удаления");
                return;
            }
            NamedList.List.DeleteAllPeople();
            Console.WriteLine($"Список успешно очищен");
        }

        /// <summary>
        /// Функция считывания имени и фамилии
        /// </summary>
        /// <returns>Кортеж из имени и фамилии</returns>
        public static (string, string) ReadFirstSecondName()
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
            return (mySuperName, lastName);
        }
        
        /// <summary>
        /// Функция обработки чтения имени/фамилии
        /// </summary>
        /// <param name="parameter">Строка "Имя" или "Фамилия"</param>
        /// <param name="languageContained">Список языков из которых состоит имя или фамилия</param>
        /// <returns>Строка с именем/фамилией</returns>
        public static string ReadNames(string parameter, out List<string> languageContained)
        {
            Console.WriteLine($"{parameter} человека (кириллица или латиница):");
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
                    Console.WriteLine("В строке содержится(-атся) цифра(-ы), " +
                                                        "повторите ввод без них");
                    continue;
                }
                badData = CheckCountOfNameLanguages(name, out languageContained);
                if (badData)
                {
                    Console.WriteLine($"{parameter} содержит символы " +
                                        $"нескольких алфавитов, повторите ввод");
                    continue;
                }
            } while (badData);
            return CheckNeedNormalization(name);
        }

        /// <summary>
        /// Проверка на количество языков, из которых состоит имя/фамилия
        /// </summary>
        /// <param name="name">Имя или фамилия</param>
        /// <param name="languageContained">Список языков из которых состоит имя или фамилия</param> 
        /// <returns>True если имя/фамилия состоит из символов нескольких алфавитов</returns>
        public static bool CheckCountOfNameLanguages(string name, out List<string> languageContained)
        {
            languageContained = CheckLanguage(name);
            return languageContained.Count > 1;
        }
        
        /// <summary>
        /// Функция определения алфавита(-ов) символов в имени/фамилии
        /// </summary>
        /// <param name="name">Имя или фамилия</param>
        /// <returns>Лист языков из которых состоит вводимая строка</returns>
        public static List<string> CheckLanguage(string name)
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
        /// Проверка на необходимость нормализации имени/фамилии
        /// </summary>
        /// <param name="name">Имя или фамилия</param>
        /// <returns>Строка с корректным именем или фамилией</returns>
        public static string CheckNeedNormalization(string name)
        {
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
        /// Функция преобразования в "правильный" регистр
        /// </summary>
        /// <param name="name">Имя или фамилия</param>
        /// <returns>Имя/Фамилия в "правильном" регистре</returns>
        public static string NormalizationNames(string name)
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
        public static Person CreatePerson()
        {
            (string FirstName, string SecondName) Anthroponym = ReadFirstSecondName();
            int age = -1;
            var actionsTuple = Tuple.Create<Action, string>
            (
                () =>
                {
                    if (int.TryParse(Console.ReadLine(), out age)) { }
                    else
                    {
                        throw new FormatException("Не удалось распознать возраст," +
                                                                " повторите ввод...");
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
                    if (!Gender.TryParse(int.Parse(Console.ReadLine()), out gender))
                    {
                        throw new FormatException("Не удалось распознать гендер," +
                                                                " повторите ввод...");
                    }
                },
                $"Введите гендер человека, 1 - Мужчина, 2 - Женщина"
            );
            ActionHandler(actionsTuple.Item1, actionsTuple.Item2);

            return new Person(Anthroponym.FirstName, Anthroponym.SecondName,
                                                age, (PossibleGender) gender);
        }

        /// <summary>
        /// Вывод в консоль содержимого листов
        /// </summary>
        /// <param name="namedLists">Проименованные листы для вывода в консоль</param>ля
        public static void PrintList(in (PersonList List, string Name)[] namedLists)  
        {
            foreach (var namedList in namedLists)
            {
                Console.WriteLine(namedList.Name + ":");
                for (int j = 0; j < namedList.List.CountOfPersons; j++)
                {
                    Console.WriteLine($"{j + 1})" + namedList.List.GetByIndex(j).PersonInfo);
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Создание нужного количества листов в массиве с 
        /// определенным количеством элементов внутри
        /// </summary>
        /// <param name="namedLists">Проименовынные листы для создания и заполнения</param>
        /// <param name="сountLists">Необходимое количество PersonList-ов</param>
        /// <param name="names">Названия для листов</param>
        /// <param name="сountElements">Количество элементов в листах</param>
        public static void CreateAndFillingLists(out (PersonList List, string Name)[] namedLists, int сountLists, 
                                                string[] names, int сountElements = 0)
        {
            namedLists = new (PersonList List, string Name)[сountLists];
            for (int i = 0; i < сountLists; i++)
            {
                namedLists[i].List = new PersonList();
                try
                {
                    namedLists[i].Name = names[i];
                }
                catch (IndexOutOfRangeException e)
                {
                    namedLists[i].Name = "Indefinite name";
                }
                for (int j = 0; j < сountElements; j++)
                {
                    namedLists[i].List.AddPerson(Person.GetRandomPerson());
                }
            }
            //TODO: return
        }
    }
}