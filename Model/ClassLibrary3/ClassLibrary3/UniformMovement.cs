using System;

namespace ClassLibrary3
{
    /// <summary>
    /// Класс равномерного движения
    /// </summary>
    public class UniformMovement : MovementBase
    {
        /// <summary>
        /// Скорость
        /// </summary>
        private double _speed;

        /// <summary>
        /// Метод для задания/получения скорости
        /// </summary>
        public double Speed
        {
            get { return _speed; }
            set
            {
                CheckArgumnet("Скорость", value, -MaxSpeed, MaxSpeed);
                _speed = value;
            }
        }
        

        /// <summary>
        /// Конструктор равномерного движения
        /// </summary>
        /// <param name="speed">Скорость объекта</param>
        /// <param name="startPosition">Начальная координата объекта</param>
        public UniformMovement(double speed, double startPosition)
            : base(startPosition)
        {
            Speed = speed;
        }

        /// <summary>
        /// Конструктор равномерного движения с нулевой начальной координатой
        /// </summary>
        /// <param name="speed">Скорость объекта</param>
        public UniformMovement(double speed) : base(0)
        {
            Speed = speed;
        }

        /// <summary>
        /// Констукрутор по умолчанию
        /// </summary>
        public UniformMovement() : this(0, 0) { }

        public override string TypeMovement => "Равномерное движение";

        /// <summary>
        /// Вычисления координаты объекты в заданный момент времени
        /// </summary>
        protected override double PositionCalculation()
        {
            return StartPosition + Speed * Time;
        }
    }
}
