using System;
using System.Collections.Generic;
using ClassLibrary3;

namespace ConsoleApp3
{
    /// <summary>
    /// Class program
    /// </summary>
    internal class Program
    { 
        /// <summary>
        /// Точка входа приложения C#
        /// </summary>
        /// <param name="args">Аргументы командной строки</param>
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            Console.InputEncoding = System.Text.Encoding.Unicode;
            
            var movements = new List<MovementBase>()
            {
               new UniformMovement(15, 25),
               new UniformMovement(15),
               new UniformlyAcceleratedMotion(5, 2, 150),
               new UniformlyAcceleratedMotion(12, -2),
               new OscillatoryMotion(12, 50, 25)
            };

            for (int t = 0; t < 150; t += 5)
            {
                Console.WriteLine($"Момент времени t={t}");
                //TODO: | +
                for (int i = 0; i < movements.Count; i++)
                {
                    Console.WriteLine($"{i}) " + movements[i].PositionCalculation(t));
                }
            }
            Console.ReadLine();
        }
    }
}
