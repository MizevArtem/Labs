using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary2
{
    /// <summary>
    /// Класс на основе класса Random
    /// </summary>
    public class MyRandom : Random
    {
        /// <summary>
        /// Коструктор без параметров
        /// </summary>
        public MyRandom() : base() { }

        /// <summary>
        /// Конструктор с параметром
        /// </summary>
        /// /// <param name="Seed">Число, используемое для вычисления
        /// начального значения последовательности псевдослучайных чисел.</param>
        public MyRandom(int Seed) : base(Seed) { }

        /// <summary>
        /// Получение следующего псведослучайного значения
        /// </summary>
        /// <param name="lowerBound">Нижняя границй</param>
        /// <param name="upperBound">Верхняяя граница</param>
        /// <returns>Случайное значение (bool) типа</returns> 
        public bool Next(bool lowerBound, bool upperBound)
        {
            if (lowerBound & upperBound)
            {
                return true;
            }
            else if (!lowerBound & !upperBound)
            {
                return false;
            }

            Random rnd = new Random(System.DateTime.Now.Millisecond);
            int randomInt = rnd.Next(0, 2);

            if (randomInt == 0)
            {
                return false;
            }
            else
            {
                return true;
            }

        }
    }
}
