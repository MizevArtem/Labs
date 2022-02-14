using System;
using System.Text.RegularExpressions;

namespace ClassLibrary1
{
    /// <summary>
    /// Класс Person 
    /// Включает в себя информацию о имени, фамилии, возрасте и поле
    /// </summary>
    public class Person : ICloneable
    {
        /// <summary>
        /// Имя
        /// </summary>
        private string _name;

        /// <summary>
        /// Afvbkbz
        /// </summary>
        private string _lastName;

        /// <summary>
        /// Возраст 
        /// </summary>
        private int _age;

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

        /// <summary>
        /// Проверка формата имени и фамилии
        /// </summary>
        /// <param name="Name">Имя или фамилия для проверки</param>
        /// <returns>True если корректная строка</returns>
        private bool CheckNames(string Name)
        {
            
            if (Name == string.Empty && Name == null)
                throw new Exception("Исключение! Задана пустая строка");
            return 0 == 0;
        }

        /// <summary>
        /// Приведение необходимых символов имени/фамилии к верхнему регистру
        /// </summary>
        /// <param name="Name">Фамилия или имя для преобразования</param>
        /// <returns>Имя/фамилия с корректным регистром</returns>
        private string ToNormalCase(string Name)
        {
            Name = Name.Substring(0, 1).ToUpper() +
                    Name.Substring(1).ToLower();
            int pos = Name.IndexOf("-");
            if (pos != -1)
                Name = Name.Substring(0, pos) +
                    Name.Substring(pos + 1, 1).ToUpper() +
                    Name.Substring(pos + 2).ToLower();
            return Name;
        }

        /// <summary>
        /// Вывод информации о человеке
        /// </summary>
        public string GetPersonInfo
        {
            get
            {
                return $"{_name} {_lastName}; Age: {_age}; Gender: {_gender}";
            }
        }

        /// <summary>
        /// Генерирует случайного человека
        /// </summary>
        /// <returns>Случайный человек</returns>
        static public Person GetRandomPerson()
        {
            string[] MaleNames = new string[]
            {
                "David", "James", "Charles", "Donald",
                "William", "Lawrence", "Cody",
                "John", "Kenneth", "Scott", "Samuel",
                "Craig", "Willie", "Gregory", "Robert", "George"
            };

            string[] FemaleNames = new string[]
            {
                "Frances", "Shannon", "Patricia", "Barbara",
                "Hazel", "Roberta", "Gloria",
                "Teresa", "Donna", "Violet", "Anna",
                "Tracy", "Lois", "Carolyn", "Kimberly", "Virginia"
            };

            string[] AllSurnames = new string[]
            {
                "Spencer", "Parker", "Butler", "Nelson",
                "Daniels", "Miller", "Riley",
                "Nelson", "Roberts", "Watts", "Lestrange",
                "King", "Patrick", "Jenkins",
                "Fisher", "Luna", "Collins", "Sanders",
                "Wilson", "Gonzales", "Tran", "Morgan",
            };
            Random random = new Random();
            gender Gender = (gender)random.Next(0, 2);
            string Name;
            switch (Gender)
            {
                case gender.Male:
                    Name = MaleNames[random.Next(MaleNames.Length)];
                    break;
                case gender.Female:
                    Name = FemaleNames[random.Next(FemaleNames.Length)];
                    break;
                default:
                    return new Person("John", "Potter", 0, gender.Male);
            }
            string LastName = AllSurnames[random.Next(AllSurnames.Length)];
            int age = random.Next(0, Person._ageMax);
            return new Person(Name, LastName, age, Gender);
        }

        /// <summary>
        /// Клонирование экземпляра
        /// </summary>
        public object Clone()
        {
            return MemberwiseClone();
        }


    }

    /// <summary>
    /// Класс листа людей 
    /// Включает в себя информацию о людях (имена, фамилии, явки, пароли)
    /// </summary>
    public class PersonList
    {
        /// <summary>
        /// Массив людей
        /// </summary>
        private Person[] _persons = new Person[0];

        /// <summary>
        /// Добавление человека в список
        /// </summary>
        /// <param name="person">Экземпляр класса Person</param>
        public void AddPerson(Person person)
        {
            Array.Resize(ref _persons, _persons.Length + 1);
            _persons[_persons.Length - 1] = (Person)person.Clone();
        }

        /// <summary>
        /// Удаление содержимого списка
        /// </summary>
        public void DeleteAllPeople()
        {
            Array.Resize(ref _persons, 0);
        }

        /// <summary>
        /// Удаление человека по индексу в массиве
        /// </summary>
        /// <<param name="index">Индекс Person-ы в массиве
        public void DeleteByIndex(int index)
        {
            if (index < 0 || index > _persons.Length - 1)
            {
                throw new Exception("Index does not exists");
            }
            var BufferArray = _persons;
            int pos = 0;
            _persons = new Person[_persons.Length - 1];
            for (int i = 0; i < BufferArray.Length; i++)
            {
                if (i != index)
                {
                    _persons[pos] = BufferArray[i];
                    pos++;
                }
            }
        }

        /// <summary>
        /// Удаление человека из списка по имени и фамилии
        /// </summary>
        /// <param name="name">Имя удаялемого человека</param>
        /// <param name="LastName">Фамилия удаляемого человека</param>
        public void DeletePersonByName(string name, string LastName)
        {
            Person[] BufferArray = new Person[0];
            for (int i = 0; i < _persons.Length; i++)
            {
                if ((_persons[i].Name != name) && (_persons[i].LastName != LastName))
                {
                    Array.Resize(ref BufferArray, BufferArray.Length + 1);
                    BufferArray[BufferArray.Length - 1] = _persons[i];
                }
            }
            _persons = BufferArray;
        }

        /// <summary>
        /// Количество людей в списке
        /// </summary>
        public int CountOfPersons => _persons.Length;

        /// <summary>
        /// Получение элемента по индексу
        /// </summary>
        /// <param name="index">Индекс элемента</param>
        /// <returns>Возвращение элементы по индексу</returns> 
        public Person GetByIndex(int index)
        {
            if (index >= 0 && index < _persons.Length)
            {
                return _persons[index];
            }
            else
            {
                throw new Exception("Index does not exists");
            }
        }


    }

}
