using MarsRovers.Resources;
using System;
using System.Linq;

namespace MarsRovers
{
    class Program
    {
        public static int PlateauX { get; set; }
        public static int PlateauY { get; set; }
        public static int RoverX { get; set; }
        public static int RoverY { get; set; }
        public static string RoverD { get; set; }
        public static string RoverCommands { get; set; }

        private static readonly string[] Directions = { "N", "W", "S", "E" };

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Mars. ");

            // Init pleateau with given parameters.
            do
            {
                Console.WriteLine("Please input limit information with space \"X Y\": ");

            } while (!GetPlateauCoordinates());

            ConsoleKeyInfo continueKey;
            do
            {
                // Get Mars Rover positions
                do
                {
                    Console.WriteLine("Please input rover position with space \"X Y D\" (X and Y is coordinates and D is direction - N W S E): ");

                } while (!GetRoverPosition());
                
                // Add Rover Commands

                Console.WriteLine("Do you want to add a new rover or cancel Y/N (Y: Add New, N: Cancel)");
                continueKey = Console.ReadKey();

            } while (continueKey.Key != ConsoleKey.N);
        }

        private static bool GetRoverPosition()
        {
            var roverPosition = Console.ReadLine();
            bool _roverD = false;

            if (string.IsNullOrEmpty(roverPosition))
            {
                Console.WriteLine(ErrorMessages.EmptyData);
                return false;
            }
            else
            {
                var splitAndConvert = roverPosition.Split(' ');
                if (splitAndConvert.Length != 3)
                {
                    Console.WriteLine("Given Parameters is wrong insert rover position with space \"X Y D\"!");
                    return false;
                }
                else
                {
                    _ = int.TryParse(splitAndConvert[0], out int xInt);
                    if (xInt > 0 && xInt <= PlateauX)
                        RoverX = xInt;
                    else if(xInt > PlateauX)
                    {
                        Console.WriteLine("X coordinate should not be bigger than plateau. Max plateau X is {0}!", PlateauX);
                        return false;
                    }
                    else
                    {
                        Console.WriteLine("X coordinate should be bigger than 0!");
                        return false;
                    }

                    _ = int.TryParse(splitAndConvert[1], out int yInt);
                    if (yInt > 0 && yInt <= PlateauY)
                        RoverY = yInt;
                    else if (yInt > PlateauY)
                    {
                        Console.WriteLine("Y coordinate should not be bigger than plateau. Max plateau Y is {0}!", PlateauY);
                        return false;
                    }
                    else
                    {
                        Console.WriteLine("Y coordinate should be bigger than 0!");
                        return false;
                    }

                    if (Directions.Contains(splitAndConvert[2]))
                    {
                        RoverD = splitAndConvert[1];
                        _roverD = true;
                    }
                    else
                    {
                        Console.WriteLine("Direction should be N, S, W, E!");
                        return false;
                    }
                }
            }

            return RoverX > 0 && RoverY > 0 && _roverD;
        }

        private static bool GetPlateauCoordinates()
        {
            var plateauLimits = Console.ReadLine();
            if (string.IsNullOrEmpty(plateauLimits))
            {
                Console.WriteLine(ErrorMessages.EmptyData);
                return false;
            }
            else
            {
                var splitAndConvert = plateauLimits.Split(' ');
                if (splitAndConvert.Length != 2)
                {
                    Console.WriteLine("Given Parameters is wrong insert limit information with space \"X Y\"!");
                    return false;
                }
                else
                {
                    _ = int.TryParse(splitAndConvert[0], out int xInt);
                    if (xInt > 0)
                        PlateauX = xInt;
                    else
                    {
                        Console.WriteLine("X coordinate should be bigger than 0!");
                        return false;
                    }

                    _ = int.TryParse(splitAndConvert[1], out int yInt);
                    if (yInt > 0)
                        PlateauY = yInt;
                    else
                    {
                        Console.WriteLine("Y coordinate should be bigger than 0!");
                        return false;
                    }
                }
            }

            return PlateauX > 0 && PlateauY > 0;
        }
    }
}
