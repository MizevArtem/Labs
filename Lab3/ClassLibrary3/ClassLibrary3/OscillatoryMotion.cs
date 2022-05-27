using System;

namespace ClassLibrary3
{
    public class OscillatoryMotion : MovementBase
    {
        /// <summary>
        /// Ускорение объекта
        /// </summary>
        public double Amplitude { get; set; }

        /// <summary>
        /// Циклическая частота
        /// </summary>
        public double CyclicFrequency { get; set; }

        /// <summary>
        /// Конструктор колебательного движения
        /// </summary>
        /// <param name="cyclicFrequency">Циклическая частота</param>
        /// <param name="amplitude">Амплитуда колебаний</param>
        /// <param name="startPosition">Начальная координата объекта</param>
        public OscillatoryMotion(double cyclicFrequency, double amplitude,
            double startPosition) : base(startPosition)
        {
            Amplitude = amplitude;
            CyclicFrequency = cyclicFrequency;
        }

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public OscillatoryMotion() : this(0, 0, 0) { }

        /// <summary>
        /// Вычисления координаты объекты в заданный момент времени
        /// </summary>
        /// /// <param name="time">Момент времени</param>
        public override double PositionCalculation(double time)
        {
            return StartPosition + Amplitude * Math.Sin(CyclicFrequency * time);
        }
    }
}
