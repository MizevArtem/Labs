using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    /// <summary>
    /// Класс Person 
    /// Включает в себя информацию о имени, фамилии, возрасте и поле человека
    /// </summary>
    public class Person
    {
        /// <summary>
        /// Имя
        /// </summary>
        private string _name;
        
        /// <summary>
        /// Фамилия
        /// </summary>
        private string _lastName;

        /// <summary>
        /// Возраст 
        /// </summary>
        private int _age;

        //TODO: RSDN
        /// <summary>
        /// Максимально допустимый возраст 
        /// </summary>
        public const int _ageMax = 100;
        
        /// <summary>
        /// Метод для работы с именем 
        /// </summary>
        public string Name
        {
            get => _name;
            set
            {
                //TODO: {}
                if (CheckNames(value))
                    _name = value;
            }
        }

        /// <summary>
        /// Метод для работы с фамилией 
        /// </summary>
        public string LastName
        {
            get => _lastName;

            set
            {
                //TODO: {}
                if (CheckNames(value))
                    _lastName = value;
            }
        }

        /// <summary>
        /// Метод для работы с возрастом 
        /// </summary>
        public int Age
        {
            get => _age;

            set
            {
                //TODO: bug
                if (value < 0 && value > _ageMax)
                {
                    throw new Exception("Некоректный возраст");
                }
                _age = value;
            }
        }
        
        /// <summary>
        /// Метод для работы с полом 
        /// </summary>
        public Gender Gender { get; set; }
        
        /// <summary>
        /// Констукрутор класса
        /// </summary>
        /// <param name="name">Имя человека</param>
        /// <param name="lastName">Фамилия человека</param>
        /// <param name="age">Возраст человека</param>
        /// <param name="gender">Пол человека</param>
        public Person(string name, string lastName, int age, Gender gender)
        {
            Name = name;
            LastName = lastName;
            Age = age;
            Gender = gender;
        }

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public Person() : this("Dab", "Bergnight", 23, Gender.Male) { }
        
        /// <summary>
        /// Проверка формата имени и фамилии
        /// </summary>
        /// <param name="name">Имя или фамилия для проверки</param>
        /// <returns>True если корректная строка</returns>
        private bool CheckNames(string name)
        {
            //TODO: {}
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException("Исключение! Задана пустое(-ая) имя(фамилия)");
            if (name.Any(char.IsNumber))
                throw new FormatException("Исключение! В имени(фамилии) содержится цифра(-ы)");

            return true;
        }

        /*
        /// <summary>
        /// Приведение необходимых символов имени/фамилии к верхнему регистру
        /// </summary>
        /// <param name="name">Фамилия или имя для преобразования</param>
        /// <returns>Имя/фамилия с корректным регистром</returns>
        private string ToNormalCase(string name)
        {
            name = name.Substring(0, 1).ToUpper() +
                    name.Substring(1).ToLower();
            int position = name.IndexOf("-");
            //TODO: | Переименовано
            if (position != -1)
                name = name.Substring(0, position) +
                    name.Substring(position + 1, 1).ToUpper() +
                    name.Substring(position + 2).ToLower();
            return name;
        }
        */
        
        /// <summary>
        /// Вывод информации о человеке
        /// </summary>
        public string PersonInfo => $"{Name} {LastName}; Age: {Age}; Gender: {Gender}";

        /// <summary>
        /// Генерирует случайного человека
        /// </summary>
        /// <returns>Случайный человек</returns>
        public static Person GetRandomPerson()
        {
            string[] maleNames = 
            {
                "David", "James", "Charles", "Donald",
                "William", "Lawrence", "Cody",
                "John", "Kenneth", "Scott", "Samuel",
                "Craig", "Willie", "Gregory", "Robert", "George"
            };

            string[] femaleNames = 
            {
                "Frances", "Shannon", "Patricia", "Barbara",
                "Hazel", "Roberta", "Gloria",
                "Teresa", "Donna", "Violet", "Anna",
                "Tracy", "Lois", "Carolyn", "Kimberly", "Virginia"
            };

            string[] allSurnames = 
            {
                "Spencer", "Parker", "Butler", "Nelson",
                "Daniels", "Miller", "Riley",
                "Nelson", "Roberts", "Watts", "Lestrange",
                "King", "Patrick", "Jenkins",
                "Fisher", "Luna", "Collins", "Sanders",
                "Wilson", "Gonzales", "Tran", "Morgan",
            };
            Random random = new Random();
            Gender gender = (Gender)random.Next(0, 2);
            string name;
            switch (gender)
            {
                case Gender.Male:
                    name = maleNames[random.Next(maleNames.Length)];
                    break;
                case Gender.Female:
                    name = femaleNames[random.Next(femaleNames.Length)];
                    break;
                default:
                    return new Person("John", "Potter", 0, Gender.Male);
            }
            string lastName = allSurnames[random.Next(allSurnames.Length)];
            int age = random.Next(0, Person._ageMax);
            return new Person(name, lastName, age, gender);
        }
    }
}