using System;

namespace ClassLibrary3
{
    /// <summary>
    /// Класс равноускоренного движения
    /// </summary>
    public class UniformlyAcceleratedMotion : UniformMovement
    {
        /// <summary>
        /// Ускорение объекта
        /// </summary>
        public double Acceleration { get; set; }

        /// <summary>
        /// Конструктор равноускоренного движения
        /// </summary>
        /// <param name="acceleration">Ускорение объекта</param>
        /// <param name="initialSpeed">Начальная скорость объекта</param>
        /// <param name="startPosition">Начальная координата объекта</param>
        public UniformlyAcceleratedMotion (double acceleration, 
            double initialSpeed, double startPosition) : base (initialSpeed, startPosition)
        {
            Acceleration = acceleration;
        }

        /// <summary>
        /// Конструктор равноускоренного движения c нулевой начальной координатой
        /// </summary>
        /// <param name="acceleration">Ускорение объекта</param>
        /// <param name="initialSpeed">Начальная скорость объекта</param>
        public UniformlyAcceleratedMotion(double acceleration, double initialSpeed)
            : base(initialSpeed, 0)
        {
            Acceleration = acceleration;
        }

        /// <summary>
        /// Конструктор равноускоренного движения c нулевой 
        /// начальной скоростью и координатой
        /// </summary>
        /// <param name="acceleration">Ускорение объекта</param>
        public UniformlyAcceleratedMotion(double acceleration) : base(0, 0)
        {
            Acceleration = acceleration;
        }

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public UniformlyAcceleratedMotion() : this(0, 0, 0) { }

        public override string TypeMovement => "Равноускоренное движение";

        /// <summary>
        /// Вычисления координаты объекты в заданный момент времени
        /// </summary>
        /// /// <param name="time">Момент времени</param>
        protected override double PositionCalculation()
        {
            if (Acceleration == 0)
            {
                return base.PositionCalculation();
            }
            //Если скорость света не была достигнута
            if (Math.Abs(Speed + Acceleration * Time) <= MaxSpeed)
            {
                return StartPosition + Speed * Time +
                    Acceleration * Math.Pow(Time, 2) / 2;
            }
            //Время через которое будет достигнута скорость света
            double timeOfMaxSpeed = (MaxSpeed - Math.Abs(Speed)) / Acceleration;
            //Расстояние пройденное к моменту достижения скороcти света
            double positionByMaxSpeed = StartPosition + Speed * timeOfMaxSpeed +
                    Acceleration * Math.Pow(timeOfMaxSpeed, 2) / 2;
            return positionByMaxSpeed + MaxSpeed * (Time - timeOfMaxSpeed);
        }


    }
}
