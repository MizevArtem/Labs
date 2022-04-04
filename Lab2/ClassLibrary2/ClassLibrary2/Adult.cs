using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary2
{
    public class Adult : PersonBase
    {
        /// <summary>
        /// Возраст
        /// </summary>
        private int _age;

        /// <summary>
        /// Брачный возраст
        /// </summary>
        public const int MarriageableAge = 16;

        /// <summary>
        /// Метод для работы с возрастом 
        /// </summary>
        public override int Age
        {
            get => _age;

            set
            {
                if (value < MarriageableAge || value > AgeMax)
                {
                    throw new Exception(
                        $"Некоректный возраст. " +
                        $"Задайте возраст от {MarriageableAge}" +
                                       $" до {AgeMax}");
                }
                _age = value;
            }
        }

        /// <summary>
        /// Статус человека в семейном плане
        /// </summary>
        public MaritalStatus MaritalStatus { get; set; }

        /// <summary>
        /// Серия паспорта
        /// </summary>
        private int _passportSeries;

        /// <summary>
        /// Минимальная серия
        /// </summary>
        private const int _minSeries = 1000;

        /// <summary>
        /// Максимальная серия
        /// </summary>
        private const int _maxSeries = 9999;

        /// <summary>
        /// Номер паспорта
        /// </summary>
        private int _passportNumber;

        /// <summary>
        /// Минимальный номер
        /// </summary>
        private const int _minNumber = 100000;

        /// <summary>
        /// Максимальный номер
        /// </summary>
        private const int _maxNumber = 999999;

        /// <summary>
        /// Метод для работы с серией паспорта
        /// </summary>
        public int PassportSeries
        {
            get => _passportSeries;
            set
            {
                if (value <= _minSeries || value > _maxSeries)
                {
                    throw new ArgumentException
                        ("Некоректная серия паспорта, " +
                        $"задайте серию от {_minSeries} " +
                                      $"до {_maxSeries}.");
                }
                _passportSeries = value;
            }
        }

        /// <summary>
        /// Метод для работы с серией паспорта
        /// </summary>
        public int PassportNumber
        {
            get => _passportNumber;
            set
            {
                if (value <= _minNumber || value > _maxNumber)
                {
                    throw new ArgumentException
                        ("Некоректный номер паспорта, " +
                        $"задайте номер от {_minNumber} " +
                                      $"до {_maxNumber}.");
                }
                _passportNumber = value;
            }
        }

        /// <summary>
        /// Партнер человека
        /// </summary>
        private Adult _partner = null;

        /// <summary>
        /// Метод для работы с партнером человека
        /// </summary>
        public Adult Partner
        {
            get => _partner;
            set
            {
                if (!(value is null))
                {
                    if (Gender == value.Gender)
                    {
                        throw new ArgumentException
                            ("Пока что однополые браки запрещены." +
                            "Выберите персону противоположного пола!");
                    }
                    if (MaritalStatus != MaritalStatus.Married)
                    {
                        throw new ArgumentException
                            ("Не соответствие семейного статуса" +
                            " и наличия партнера");
                    }
                }
                else
                {
                    if (MaritalStatus == MaritalStatus.Married)
                    {
                        throw new ArgumentException
                            ("Не соответствие семейного статуса" +
                            " и наличия партнера");
                    }
                }
                _partner = value;
            }
        }

        /// <summary>
        /// Место работы
        /// </summary>
        public string Work { get; set; }

        /// <summary>
        /// Конструктор человека
        /// </summary>
        /// <param name="name">Имя человека</param>
        /// <param name="lastName">Фамилия человека</param>
        /// <param name="age">Возраст человека</param>
        /// <param name="gender">Пол человека</param>
        /// <param name="passportSeries">Серия паспорта</param>
        /// <param name="passportNumber">Номер паспорта</param>
        /// <param name="maritalStatus">Семейный статус человека</param>
        /// <param name="work">Место работы</param>
        /// <param name="person">Ссылка на партнера</param>
        public Adult(string name, string lastName, int age,
            PossibleGender gender, MaritalStatus maritalStatus,
            int passportSeries, int passportNumber,
            string work = null, Adult person = null)
            : base(name, lastName, age, gender)
        {
            PassportSeries = passportSeries;
            PassportNumber = passportNumber;
            MaritalStatus = maritalStatus;
            Work = work;
            Partner = person;
        }

        /// <summary>
        /// Генерирует случайного человека случайного пола
        /// </summary>
        /// <returns>Случайный человек</returns>
        public static Adult GetRandomPerson(Random rnd)
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
                    return new Adult("John", "Potter", 22,
                        PossibleGender.Male, MaritalStatus.Single,
                        1234, 123456);
            }
            string lastName = allSurnames[rnd.Next(allSurnames.Length)];
            int age = rnd.Next(MarriageableAge, PersonBase.AgeMax);
            int passportSeries = rnd.Next(_minSeries, _maxSeries);
            int passportNumber = rnd.Next(_minNumber, _maxNumber);
            Adult partner = null;
            string work = "Work №" + rnd.Next(1, 10);

            return new Adult(name, lastName, age, gender,
                MaritalStatus.Single, passportSeries,
                passportNumber, work, partner);
        }

        /// <summary>
        /// Генерирует случайного человека заданного пола
        /// </summary>
        /// <param name="gender">Пол человека</param>
        /// <returns>Полуслучайный человек</returns>
        public static Adult GetRandomPerson(PossibleGender gender)
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

            Random random = new Random(System.DateTime.Now.Millisecond);
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
                    return new Adult("John", "Potter", 22,
                        PossibleGender.Male, MaritalStatus.Single,
                        1234, 123456);
            }
            string lastName = allSurnames[random.Next(allSurnames.Length)];
            int age = random.Next(MarriageableAge, PersonBase.AgeMax);
            int passportSeries = random.Next(_minSeries, _maxSeries);
            int passportNumber = random.Next(_minNumber, _maxNumber);
            string work = "Work №" + random.Next(1, 10);

            return new Adult(name, lastName, age,
                (PossibleGender)gender, MaritalStatus.Single,
                passportSeries, passportNumber, work, null);
        }

        /// <summary>
        /// Получение информации о человеке
        /// </summary>
        /// <returns>Строка с информацией о человеке</returns>
        public override string PersonInfo()
        {
            string maritalStatus;
            if (MaritalStatus == MaritalStatus.Married)
            {
                maritalStatus = $"Partner: {Partner.Name} {Partner.LastName}";
            }
            else
            {
                maritalStatus = MaritalStatus.ToString();
            }
            string work;
            if (Work is null)
            {
                work = "Unemployed";
            }
            else
            {
                work = $"Work: {Work}";
            }
            return $"{Name} {LastName}; Age: {Age}; Gender: {Gender};" +
                $" {maritalStatus}; Passport series: {PassportSeries};" +
                $" Passport number: {PassportNumber}; {work}";
        }

        /// <summary>
        /// Получение партнера
        /// </summary>
        /// <returns>Ссылка на партнера</returns>
        public Adult GetPartner()
        {
            return Partner;
        }
        
    }
}
