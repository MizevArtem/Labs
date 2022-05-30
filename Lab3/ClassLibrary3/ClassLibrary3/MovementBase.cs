using System;

namespace ClassLibrary3
{
    /// <summary>
    /// Базовый клас для всех видов движения
    /// </summary>
    public abstract class MovementBase
    {
        /// <summary>
        /// Начальное положение объекта 
        /// </summary>
        public double StartPosition { get; set; }

        /// <summary>
        /// Максимальная скорость положение объекта 
        /// </summary>
        public const int MaxSpeed = 299792458;

        /// <summary>
        /// Констукрутор класса
        /// </summary>
        /// <param name="position">Начальная координата объекта</param>
        protected MovementBase(double position)
        {
            StartPosition = position;
        }

        /// <summary>
        /// Констукрутор по умолчанию
        /// </summary>
        protected MovementBase() : this(0) { }

        /// <summary>
        /// Метод расчета положения тела
        /// </summary>
        /// <param name="time">Момент времени</param>
        public abstract double PositionCalculation(double time);

        /// <summary>
        /// Функция проверки параметра на допустимость значения
        /// </summary>
        /// <param name="param">Наименование параметра</param>
        /// <param name="value">Значние параметра</param>
        /// <param name="minValue">Наименьшее допустимое значение параметра</param>
        /// <param name="maxValue">Наибольшее допустимое значение параметра</param>
        public void CheckArgumnet(string param, double value,
            double minValue = double.MinValue, double maxValue = double.MaxValue)
        {
            if (value < minValue)
            {
                throw new ArgumentException($"Параметр \"{param}\" не должен " +
                                                    $"быть меньше {minValue}");
            }
            if (value > maxValue)
            {
                throw new ArgumentException($"Параметр \"{param}\" не должен " +
                                                    $"быть больше {maxValue}");
            }
        }
    }
}
