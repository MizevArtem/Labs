using System;
using System.Collections.Generic;
using ClassLibrary3;

namespace ConsoleApp3
{
    internal class Program
    {
        static void Main(string[] args)
        {

            List<MovementBase> Movements = new List<MovementBase>()
            {
               new UniformMovement(15, 25),
               new UniformMovement(15),
               new UniformlyAcceleratedMotion(5, 2, 150),
               new UniformlyAcceleratedMotion(12, -2),
               new OscillatoryMotion(12, 50, 25)
            };
            for (int t = 0; t < 150; t+=5)
            {
                Console.WriteLine($"Момент времени t={t}");
                int i = 0;
                foreach (var Movement in Movements)
                {
                    i++;
                    Console.WriteLine($"{i}) " + Movement.PositionCalculation(t));
                }
            }
            Console.ReadLine();
        }
    }
}
