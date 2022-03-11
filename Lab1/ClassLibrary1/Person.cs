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
        /// Возраст 
        /// </summary>
        private int _age;

        /// <summary>
        /// Имя
        /// </summary>
        private string _name;

        /// <summary>
        /// Фамилия
        /// </summary>
        private string _lastName;

        //TODO: RSDN | Переименовано
        /// <summary>
        /// Максимально допустимый возраст 
        /// </summary>
        public const int AgeMax = 100;

        /// <summary>
        /// Минимально допустимый возраст 
        /// </summary>
        public const int AgeMin = 0;

        /// <summary>
        /// Метод для работы с именем 
        /// </summary>
        public string Name
        {
            get => _name;
            set
            {
                //TODO: {} | Добавлены
                if (CheckNames(value))
                {
                    _name = value;
                }
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
                //TODO: {} | Добавлены
                if (CheckNames(value))
                {
                    _lastName = value;
                }
                    
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
                //TODO: bug | Устранен
                if (value < AgeMin || value > AgeMax)
                {
                    throw new Exception($"Некоректный возраст, задайте возраст от {AgeMin} до {AgeMax}");
                }
                _age = value;
            }
        }
        
        /// <summary>
        /// Метод для работы с полом 
        /// </summary>
        public PossibleGender Gender { get; set; }
        
        /// <summary>
        /// Констукрутор класса
        /// </summary>
        /// <param name="name">Имя человека</param>
        /// <param name="lastName">Фамилия человека</param>
        /// <param name="age">Возраст человека</param>
        /// <param name="gender">Пол человека</param>
        public Person(string name, string lastName, int age, PossibleGender gender)
        {
            Name = name;
            LastName = lastName;
            Age = age;
            Gender = gender;
        }

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public Person() : this("Dab", "Bergnight", 23, PossibleGender.Male) { }
        
        /// <summary>
        /// Проверка формата имени и фамилии
        /// </summary>
        /// <param name="name">Имя или фамилия для проверки</param>
        /// <returns>True если корректная строка</returns>
        private bool CheckNames(string name)
        {
            //TODO: {} | Добавлены
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("Исключение! Задана пустое(-ая) имя(фамилия)");
            }
            if (name.Any(char.IsNumber))
            {
                throw new FormatException("Исключение! В имени(фамилии) содержится цифра(-ы)");
            }
                

            return true;
        }
        
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
            PossibleGender gender = (PossibleGender)random.Next(0, 2);
            string name;
            switch (gender)
            {
                case PossibleGender.Male:
                    name = maleNames[random.Next(maleNames.Length)];
                    break;
                case PossibleGender.Female:
                    name = femaleNames[random.Next(femaleNames.Length)];
                    break;
                default:
                    return new Person("John", "Potter", 0, PossibleGender.Male);
            }
            string lastName = allSurnames[random.Next(allSurnames.Length)];
            int age = random.Next(0, Person.AgeMax);
            return new Person(name, lastName, age, (PossibleGender) gender);
        }
    }
}