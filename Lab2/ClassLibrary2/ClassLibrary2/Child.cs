using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary2
{
    /// <summary>
    /// Класс ребенка.
    /// Включает в себя информацию о имене, фамилии, 
    /// учебном заведении и ссылки на родителей и др.
    /// </summary>
    public class Child : PersonBase
    {
        /// <summary>
        /// Возраст
        /// </summary>
        private int _age;

        /// <summary>
        /// Метод по работе с возрастом
        /// </summary>
        public override int Age
        {
            get => _age;
            set
            {
                if (value < AgeMin || value > AgeMax)
                {
                    throw new ArgumentException
                        ($"Некоректный возраст. " +
                        $"Задайте возраст от {AgeMin}" +
                                       $" до {AgeMax}");
                }
                _age = value;
            }
        }

        /// <summary>
        /// Мать
        /// </summary>
        private Adult _mother;

        /// <summary>
        /// Мать
        /// </summary>
        public Adult Mother
        {
            get => _mother;
            set
            {
                GenderCheck(value, PossibleGender.Female);
                _mother = value;
            }
        }

        /// <summary>
        /// Отец
        /// </summary>
        private Adult _father;

        /// <summary>
        /// Отец
        /// </summary>
        public Adult Father
        {
            get => _father;
            set
            { 
                GenderCheck(value, PossibleGender.Male);
                _father = value;
            }
            
        }

        /// <summary>
        /// Название учебного учреждения
        /// </summary>
        public string ChildrenInstitution { get; set; }

        /// <summary>
        /// Конструктор ребенка
        /// </summary>
        /// <param name="name">Имя ребенка</param>
        /// <param name="surname">Фамилия ребенка</param>
        /// <param name="age">Возраст ребенка</param>
        /// <param name="gender">Пол ребенка</param>
        /// <param name="mother">Мать ребенка</param>
        /// <param name="father">Отец ребенка</param>
        /// <param name="childrenInstitution">Имя образовательного учреждения</param>
        public Child(string name, string surname, int age, PossibleGender gender,
            Adult mother, Adult father, string childrenInstitution)
            : base(name, surname, age, gender)
        {
            Mother = mother;
            Father = father;
            ChildrenInstitution = childrenInstitution;
        }

        /// <summary>
        /// Проверка пол родителя
        /// </summary>
        /// <param name="parent">Ссылка на родителя</param>
        /// <param name="gender">Ожидаемый пол родителя</param>
        public void GenderCheck(Adult parent, PossibleGender gender)
        {
            if (!(parent is null) && parent.Gender != gender)
            {
                throw new ArgumentException
                                  ("Родитель имеет неверный гендер");
            }
        }

        /// <summary>
        /// Генерирует случайного ребенка случайного пола
        /// </summary>
        /// <returns>Случайный человек</returns>
        public static Child GetRandomPerson(Random rnd)
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
            //Random rnd = new Random();
            PossibleGender gender = (PossibleGender)rnd.Next(1, 3);
            string name;
            switch (gender)
            {
                case PossibleGender.Male:
                    name = maleNames[rnd.Next(maleNames.Length)];
                    break;
                case PossibleGender.Female:
                    name = femaleNames[rnd.Next(femaleNames.Length)];
                    break;
                default:
                    return new Child("John", "Potter", 22,
                        PossibleGender.Male, null, null,
                        "666");
            }
            string lastName = allSurnames[rnd.Next(allSurnames.Length)];
            int age = rnd.Next(0, PersonBase.AgeMax);
            Adult mother = null;
            Adult father = null;
            string childrenInstitution = "Institution №" + rnd.Next(1, 10);

            return new Child(name, lastName, age,
                gender, mother, father, childrenInstitution);
        }

        /// <summary>
        /// Получение информации о ребенке
        /// </summary>
        /// <returns>Строка с информацией о ребенке</returns>
        public override string PersonInfo()
        {
            string mother = Mother is null ? $"The mother is missing " 
                            : $"Mother: {Mother.Name} {Mother.LastName}";
            string father = Father is null ? $"The father is missing " 
                            : $"Father: {Father.Name} {Father.LastName}";
            return $"{Name} {LastName}; Age: {Age}; Gender: {Gender};" +
                $" {mother}; {father}; Children Institution: {ChildrenInstitution}";
        }

        /// <summary>
        /// Получение родителей ременка
        /// </summary>
        /// <returns>Ссылка на отца и мать</returns>
        public (Adult, Adult) GetParents()
        {
            return (Father, Mother);
        }
    }
}
