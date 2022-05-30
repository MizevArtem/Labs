using System;

namespace ClassLibrary3
{
    //TODO: XML | +
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

        /// <summary>
        /// Вычисления координаты объекты в заданный момент времени
        /// </summary>
        /// /// <param name="time">Момент времени</param>
        public override double PositionCalculation(double time)
        {
            if (Acceleration == 0)
            {
                return base.PositionCalculation(2);
            }
            //Если скорость света не была достигнута
            if (Math.Abs(Speed + Acceleration * time) <= MaxSpeed)
            {
                return StartPosition + Speed * time +
                    Acceleration * Math.Pow(time, 2) / 2;
            }
            //Время через которое будет достигнута скорость света
            double timeOfMaxSpeed = (MaxSpeed - Math.Abs(Speed)) / Acceleration;
            //Расстояние пройденное к моменту достижения скороcти света
            double positionByMaxSpeed = StartPosition + Speed * timeOfMaxSpeed +
                    Acceleration * Math.Pow(timeOfMaxSpeed, 2) / 2;
            return positionByMaxSpeed + MaxSpeed * (time - timeOfMaxSpeed);
        }


    }
}
