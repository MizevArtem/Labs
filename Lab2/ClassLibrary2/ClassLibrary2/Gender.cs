using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ClassLibrary2
{
    /// <summary>
    /// Класс Gender 
    /// Включает в себя гендер, и методы для работы с ним
    /// </summary>
    public class Gender
    {
        /// <summary>
        /// Заданный гендер
        /// </summary>
        private PossibleGender m_value;

        /// <summary>
        /// Проверка возможности преобразования числа в вид гендера
        /// </summary>
        /// <param name="input">Число для преобразования в гендер</param>
        /// <param name="output">Переменная для записи результата</param>
        /// <returns>True если преобразование успешно</returns>
        public static bool TryParse(int input, out Gender output)
        {
            switch (input)
            {
                case 1:
                    output = PossibleGender.Male;
                    break;
                case 2:
                    output = PossibleGender.Female;
                    break;
                default:
                    output = PossibleGender.Indefinite;
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Перегрузка оператора неявного преобразования
        /// </summary>
        /// <param name="value">Гендер человека</param>
        /// <returns>Экземпляр класса Gender с заданным гендером</returns>
        public static implicit operator Gender(PossibleGender value)
        {
            return new Gender
            {
                m_value = value
            };
        }

        /// <summary>
        /// Перегрузка оператора явного преобразования
        /// </summary>
        /// <param name="gender">Экземпляр класса Gender</param>
        /// <returns>Гендер человека записанный в объект</returns>
        public static explicit operator PossibleGender(Gender gender)
        {
            return gender.m_value;
        }
    }
}