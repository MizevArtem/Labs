﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary2
{
    //TODO: XML | +
    /// <summary>
    /// Абстрактный класс человека.
    /// </summary>
    public abstract class PersonBase
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
        /// Максимально допустимый возраст 
        /// </summary>
        public const int AgeMax = 100;

        /// <summary>
        /// Минимально допустимый возраст 
        /// </summary>
        public const int AgeMin = 0;

        /// <summary>
        /// Брачный возраст
        /// </summary>
        public const int MarriageableAge = 16;

        /// <summary>
        /// Метод для работы с именем 
        /// </summary>
        public string Name
        {
            get => _name;
            set
            {
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
                if (CheckNames(value))
                {
                    _lastName = value;
                }

            }
        }

        /// <summary>
        /// Метод для работы с возрастом 
        /// </summary>
        public abstract int Age { get; set; }

        /// <summary>
        /// Метод для работы с полом 
        /// </summary>
        public PossibleGender Gender { get; set; }

        //TODO: | +
        /// <summary>
        /// Констукрутор класса
        /// </summary>
        /// <param name="name">Имя человека</param>
        /// <param name="lastName">Фамилия человека</param>
        /// <param name="age">Возраст человека</param>
        /// <param name="gender">Пол человека</param>
        protected PersonBase(string name, string lastName, int age, PossibleGender gender)
        {
            Name = name;
            LastName = lastName;
            Age = age;
            Gender = gender;
        }

        //TODO: | +
        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        protected PersonBase() : this("Dab", "Bergnight", 23, PossibleGender.Male) { }

        /// <summary>
        /// Проверка формата имени и фамилии
        /// </summary>
        /// <param name="name">Имя или фамилия для проверки</param>
        /// <returns>True если корректная строка</returns>
        private bool CheckNames(string name)
        {
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
        public abstract string PersonInfo();
    }
}
