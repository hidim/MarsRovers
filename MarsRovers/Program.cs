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

        private static readonly string[] Directions = { "N", "E", "S", "W" };
        private static readonly string[] Commands = { "L", "R", "M" };


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

                // Get Rover Commands
                do
                {
                    Console.WriteLine("Please input rover commands without space (L is turns rover to 90 degrees left and R turns rover to 90 degrees right, M is for move forward without changeing direction): ");

                } while (!GetRoverCommands());

                MoveRoverWithCommands();

                if (RoverX <= PlateauX && RoverY <= PlateauY)
                {
                    Console.WriteLine("Rover's position after commands: ");
                    Console.WriteLine("{0} {1} {2}", RoverX, RoverY, RoverD);
                }
                else
                {
                    Console.WriteLine("Rover is outside of the plateau!");
                }

                Console.WriteLine("Do you want to add a new rover or cancel Y/N (Y: Add New, N: Cancel)");
                continueKey = Console.ReadKey();
                if(continueKey.Key != ConsoleKey.N)
                {
                    RoverD = string.Empty;
                    RoverX = 0;
                    RoverY = 0;
                    RoverCommands = string.Empty;
                }

            } while (continueKey.Key != ConsoleKey.N);
        }

        private static void MoveRoverWithCommands()
        {
            foreach (var command in RoverCommands.ToArray())
            {
                switch (command)
                {
                    case 'L':
                        RoverD = RoverD == "N" ? "W" : (RoverD == "W" ? "S" : (RoverD == "S" ? "E" : "N"));
                        break;
                    case 'R':
                        RoverD = RoverD == "N" ? "E" : (RoverD == "E" ? "S" : (RoverD == "S" ? "W" : "N"));
                        break;
                    case 'M':
                        if (RoverD == "N")
                            RoverY++;
                        if (RoverD == "E")
                            RoverX++;
                        if (RoverD == "S")
                            RoverY--;
                        if (RoverD == "W")
                            RoverX--;
                        break;
                }
            }
        }

        private static bool GetRoverCommands()
        {
            var roverCommands = Console.ReadLine();
            bool _roverD = true;

            if (string.IsNullOrEmpty(roverCommands))
            {
                Console.WriteLine(ErrorMessages.EmptyData);
                return false;
            }
            else
            {
                var splitAndConvert = roverCommands.ToArray();
                foreach (var charComm in splitAndConvert)
                {
                    if (!Commands.Contains(charComm.ToString().ToUpper()))
                    {
                        _roverD = false;
                    }
                }
            }

            if (!_roverD)
            {
                Console.WriteLine("Given Parameters is wrong insert rover rommands with only using \"L R M\" without space!");
            }
            else
            {
                RoverCommands = roverCommands;
            }
            return _roverD;
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
                    else if (xInt > PlateauX)
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

                    if (Directions.Contains(splitAndConvert[2].ToUpper()))
                    {
                        RoverD = splitAndConvert[2];
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
