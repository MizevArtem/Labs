using System;
using System.Collections.Generic;
using ClassLibrary3;

namespace ConsoleApp3
{
    //TODO: XML | + 
    /// <summary>
    /// Class program
    /// </summary>
    internal class Program
    { 
        //TODO: XML | +
        /// <summary>
        /// Точка входа приложения C#
        /// </summary>
        /// <param name="args">Аргументы командной строки</param>
        static void Main(string[] args)
        {
            //TODO: добаввить строки для корректного вывода | +
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            Console.InputEncoding = System.Text.Encoding.Unicode;

            //TODO: RSDN | +
            List<MovementBase> movements = new List<MovementBase>()
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
                int i = 0;
                foreach (var Movement in movements)
                {
                    i++;
                    Console.WriteLine($"{i}) " + Movement.PositionCalculation(t));
                }
            }
            Console.ReadLine();
        }
    }
}
