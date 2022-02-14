﻿using System;
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
    public class Person : ICloneable
    {
        /// <summary>
        /// Имя
        /// </summary>
        private string _name;

        //TODO:
        /// <summary>
        /// Afvbkbz
        /// </summary>
        private string _lastName;

        /// <summary>
        /// Возраст 
        /// </summary>
        private int _age;

        //TODO: to const
        /// <summary>
        /// Максимально допустимый возраст 
        /// </summary>
        public static int _ageMax = 100;
        
        /// <summary>
        /// Метод для работы с именем 
        /// </summary>
        public string Name
        {
            get => _name;
            set
            {
                //TODO:
                if (CheckNames(value))
                    _name = ToNormalCase(value);
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
                //TODO:
                if (CheckNames(value))
                    _lastName = ToNormalCase(value);
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
                if (value < 0 && value > _ageMax)
                {
                    throw new Exception("Некоректный возраст");
                }
                _age = value;
            }
        }

        //TODO: RSDN
        /// <summary>
        /// Метод для работы с полом 
        /// </summary>
        public gender _gender { get; set; }

        /// <summary>
        /// Констукрутор класса
        /// </summary>
        /// <param name="name">Имя человека</param>
        /// <param name="lastName">Фамилия человека</param>
        /// <param name="age">Возраст человека</param>
        /// <param name="gender">Пол человека</param>
        public Person(string name, string lastName, int age, gender gender)
        {
            Name = name;
            LastName = lastName;
            Age = age;
            _gender = gender;
        }

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public Person() : this("Dab", "Bergnight", 23, gender.Male) { }

        //TODO: Несоответствие XML-комментария сигнатуре метода 
        /// <summary>
        /// Проверка формата имени и фамилии
        /// </summary>
        /// <param name="Name">Имя или фамилия для проверки</param>
        /// <returns>True если корректная строка</returns>
        private bool CheckNames(string name)
        {
            //TODO:
            if (name == string.Empty && name == null)
                throw new Exception("Исключение! Задана пустая строка");
           //TODO:
            return 0 == 0;
        }

        /// <summary>
        /// Приведение необходимых символов имени/фамилии к верхнему регистру
        /// </summary>
        /// <param name="name">Фамилия или имя для преобразования</param>
        /// <returns>Имя/фамилия с корректным регистром</returns>
        private string ToNormalCase(string name)
        {
            name = name.Substring(0, 1).ToUpper() +
                    name.Substring(1).ToLower();
            int pos = name.IndexOf("-");
            //TODO:
            if (pos != -1)
                name = name.Substring(0, pos) +
                    name.Substring(pos + 1, 1).ToUpper() +
                    name.Substring(pos + 2).ToLower();
            return name;
        }

        //TODO: naming
        /// <summary>
        /// Вывод информации о человеке
        /// </summary>
        public string GetPersonInfo => $"{Name} {LastName}; Age: {Age}; Gender: {_gender}";

        /// <summary>
        /// Генерирует случайного человека
        /// </summary>
        /// <returns>Случайный человек</returns>
        public static Person GetRandomPerson()
        {
            //TODO: RSDN
            string[] MaleNames = 
            {
                "David", "James", "Charles", "Donald",
                "William", "Lawrence", "Cody",
                "John", "Kenneth", "Scott", "Samuel",
                "Craig", "Willie", "Gregory", "Robert", "George"
            };

            string[] FemaleNames = 
            {
                "Frances", "Shannon", "Patricia", "Barbara",
                "Hazel", "Roberta", "Gloria",
                "Teresa", "Donna", "Violet", "Anna",
                "Tracy", "Lois", "Carolyn", "Kimberly", "Virginia"
            };

            string[] AllSurnames = 
            {
                "Spencer", "Parker", "Butler", "Nelson",
                "Daniels", "Miller", "Riley",
                "Nelson", "Roberts", "Watts", "Lestrange",
                "King", "Patrick", "Jenkins",
                "Fisher", "Luna", "Collins", "Sanders",
                "Wilson", "Gonzales", "Tran", "Morgan",
            };
            Random random = new Random();
            gender gender = (gender)random.Next(0, 2);
            string name;
            switch (gender)
            {
                case gender.Male:
                    name = MaleNames[random.Next(MaleNames.Length)];
                    break;
                case gender.Female:
                    name = FemaleNames[random.Next(FemaleNames.Length)];
                    break;
                default:
                    return new Person("John", "Potter", 0, gender.Male);
            }
            string lastName = AllSurnames[random.Next(AllSurnames.Length)];
            int age = random.Next(0, Person._ageMax);
            return new Person(name, lastName, age, gender);
        }

        /// <summary>
        /// Клонирование экземпляра
        /// </summary>
        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
