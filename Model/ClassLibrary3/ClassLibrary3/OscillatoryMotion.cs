﻿using System;

namespace ClassLibrary3
{
    /// <summary>
    /// Класс колебательного движения
    /// </summary>
    public class OscillatoryMotion : MovementBase
    {
        /// <summary>
        /// Амплитуда колебаний
        /// </summary>
        private double _amplitude;

        /// <summary>
        /// Метод для работы с амлитудой колебаний
        /// </summary>
        
        public double Amplitude 
        {
            get { return _amplitude; }
            set
            {
                CheckArgumnet("Амплитуда", value, 0);
                _amplitude = value;
            }
        }

        /// <summary>
        /// Циклическая частота
        /// </summary>
        private double _cyclicFrequency;

        /// <summary>
        ///Метод для работы с циклической частотой
        /// </summary>
        public double CyclicFrequency
        {
            get { return _cyclicFrequency; }
            set
            {
                CheckArgumnet("Циклическая частота", value, 0);
                _cyclicFrequency = value;
            }
        }

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

        public override string TypeMovement => "Колебательное движение";

        /// <summary>
        /// Вычисления координаты объекты в заданный момент времени
        /// </summary>
        public override double PositionCalculation()
        {
            return StartPosition + Amplitude * Math.Sin(CyclicFrequency * Time);
        }   
    }
}