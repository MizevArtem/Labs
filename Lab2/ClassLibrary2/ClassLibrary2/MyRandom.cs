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

        //TODO: RSDN | +
        /// <summary>
        /// Конструктор с параметром
        /// </summary>
        /// /// <param name="seed">Число, используемое для вычисления
        /// начального значения последовательности псевдослучайных чисел.</param>
        public MyRandom(int seed) : base(seed) { }

        /// <summary>
        /// Получение следующего псведослучайного значения
        /// </summary>
        /// <param name="lowerBound">Нижняя границй</param>
        /// <param name="upperBound">Верхняяя граница</param>
        /// <returns>Случайное значение (bool) типа</returns> 
        public bool Next(bool lowerBound, bool upperBound)
        {
            //TODO: | +
            if (lowerBound & upperBound)
            {
                return true;
            }
            else if (!(lowerBound | upperBound))
            {
                return false;
            }
            return new Random().Next(0, 2) != 0;
        }
    }
}
