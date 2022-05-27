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


    }
}
