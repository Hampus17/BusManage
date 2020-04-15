using System;

namespace BussenApp {
    internal static class Program {
        
        private static bool _isRunning = false; // Declare a variable to keep track if the program is running or not, default it to false
        private static readonly Bussen bussen = new Bussen(25); // Create a new instance of the Bussen class, which can only be read from 

        // All the logic that keeps the program running goes here
        private static void Run() {
            _isRunning = true; // On runtime set the variable to true
            do // Run the program at least once
            {
                Console.Clear();
                Console.WriteLine("=| Bus Management System |=");
                Console.WriteLine("===========================\n");
                HandleChoice();
            } while (_isRunning);

            _isRunning =
                false; // When the program exits out of the handleChoice loop (program ends) set the variable to false
            Console.WriteLine("Exiting application...");
            Console.ReadKey();
        }

        private static void HandleChoice() {
            bool isChoosing = true;

            // for (int i = 0; i < 20; i++)
            //    Console.Write((char)178);

            Console.WriteLine("\n- Choose an option:");
            Console.WriteLine("     [1]  Register a new passenger");
            Console.WriteLine("     [2]  Print a list of the passengers");
            Console.WriteLine("     [3]  Print the average age");
            Console.WriteLine("     [4]  Print the total age ");
            Console.WriteLine("     [5]  Print max age");
            Console.WriteLine("     [6]  Find age ");
            Console.WriteLine("     [7]  Print genders ");
            Console.WriteLine("     [8]  Sort bus ");
            Console.WriteLine("     [9]  Poke passenger ");
            Console.WriteLine("     [10] Passenger getting off ");
            Console.WriteLine("     [0]  Exit");

            do {
                Console.Write("Choose a number: ");
                if (int.TryParse(Console.ReadLine(), out int choice)) {
                    if (choice >= 0 && choice <= 11 ) { // Because of this we really don't need a default in the switch statement
                        switch (choice) {
                            case 1:
                                bussen.RegisterPassenger();
                                break;
                            case 2:
                                bussen.PrintPassengerList();
                                break;
                            case 3:
                                bussen.PrintAverageAge();
                                break;
                            case 4:
                                bussen.PrintTotalAge();
                                break;
                            case 5:
                                bussen.PrintMaxAge();
                                break;
                            case 6:
                                bussen.FindAge();
                                break;
                            case 7:
                                bussen.PrintGenders();
                                break;
                            case 8:
                                bussen.SortPassengerList();
                                break;
                            case 9:
                                bussen.PokePassenger();
                                break;
                            case 10:
                                bussen.PassengerLeave();
                                break;
                            case 11: 
                                bussen.GeneratePassengers();
                                break;
                            case 0:
                                _isRunning = false;
                                break;
                        }

                        Console.WriteLine("");
                        isChoosing = false;
                    }
                    else
                        Console.WriteLine("(!) Error: Invalid option, try again.\n");
                }
                else
                    Console.WriteLine("(!) Error: Invalid input, try again.\n");
            } while (isChoosing);
        }

        private static void Main(string[] args) {
            Run(); // Run the program
        }
    }
}