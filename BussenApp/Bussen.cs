using System;

namespace BussenApp {
    public class Bussen {
        private bool _isFull = false;
        private int _maxLimit;

        private int _passengerCount;

        //// Private variables
        private Person[] _passengerList;

        //// Constructor
        public Bussen(int maxLimit) {
            this._maxLimit = maxLimit;
            _passengerList = new Person[maxLimit];
        }

        //// Public functions
        public void RegisterPassenger() {
            if (!_isFull) {
                int age = 0;
                string gender = null;
                string occupation = null;
                string name = null;

                Console.WriteLine("\nEnter the information below");
                Console.Write("- Age: ");
                while (age == 0)
                    if (int.TryParse(Console.ReadLine(), out int tempAge) && tempAge > 0 && tempAge < 100)
                        age = tempAge;
                    else
                        Console.WriteLine("(!) Error: Invalid age, try again.\n");

                Console.WriteLine("- Gender: ");
                Console.WriteLine("     [1] Male");
                Console.WriteLine("     [2] Female");
                while (gender == null)
                    if (int.TryParse(Console.ReadLine(), out int tempGender) && tempGender <= 2 && tempGender >= 1) {
                        gender = tempGender switch {
                            1 => "Male",
                            2 => "Female"
                        };
                    }
                    else {
                        Console.WriteLine("(!) Error: Invalid gender, try again.\n");
                    }

                Console.Write("- Name: ");
                while (name == null) {
                    string input = Console.ReadLine();
                    if (!int.TryParse(input, out int useless))
                        name = input;
                    else
                        Console.WriteLine("(!) Error: Invalid characters, try again.\n");
                }

                Console.Write("- Occupation: ");
                while (occupation == null) {
                    string input = Console.ReadLine();
                    if (!int.TryParse(input, out int useless))
                        occupation = input;
                    else
                        Console.WriteLine("(!) Error: Invalid characters, try again.\n");
                }

                Person newPassenger = new Person(age, gender, name, occupation);

                for (int i = 0; i < _passengerList.Length; i++) {
                    if (_passengerList[i].GetGender() == null) {
                        _passengerList[i] = newPassenger;
                        _passengerCount++;
                        break;
                    }
                    else {
                        _isFull = _passengerCount == _passengerList.Length ? true : false;
                    }
                }

                Console.WriteLine("\nPassenger successfully registered!");
                Console.WriteLine("... Press any key to return!");
                Console.ReadKey();
            }
            else {
                Console.WriteLine("\n(!) Error: Bus is full!");
                Console.ReadKey();
            }
        }

        public void PrintAverageAge() {
            Console.Write("Average age on the bus: ");

            int ageCount = 0;
            int tempCount = 0;
            foreach (Person passenger in _passengerList)
                if (passenger.GetAge() != 0) {
                    ageCount += passenger.GetAge();
                    tempCount++;
                }

            ageCount = ageCount / tempCount;

            Console.Write(ageCount);
            Console.ReadKey();
        }

        public void PrintTotalAge() {
            int totalAge = 0;

            foreach (Person passenger in _passengerList)
                if (passenger.GetAge() != 0)
                    totalAge += passenger.GetAge();

            Console.WriteLine(totalAge);
            Console.ReadKey();
        }

        public void PrintMaxAge() {
            Person oldestPerson = _passengerList[0];
            
            foreach (Person passenger in _passengerList) {
                if (passenger.GetAge() > oldestPerson.GetAge()) {
                    oldestPerson = passenger;
                }
            }
            
            Console.WriteLine("Oldest person is {0} years old (Name: {1})", 
                oldestPerson.GetAge(), oldestPerson.GetName());
            Console.ReadKey();
        }

        public void FindAge() {
            string filter = "none";

            do {
                string[] input = Console.ReadLine().Split(' ');
                if (input.Length == 2) {
                    if (int.TryParse(input[0], out int filterOne) && filterOne != 0) {
                        if (int.TryParse(input[1], out int filterTwo) && filterTwo != 0) {
                            Console.WriteLine("{0}, {1}", filterOne, filterTwo);
                            filter = "twoNumberFilter";
                        }
                        else {
                            Console.WriteLine("(!) Error: Invalid Input, try again!");
                            break;   
                        }
                    }
                    else {
                        Console.WriteLine("(!) Error: Invalid Input, try again!");
                        break;
                    }
                }
                else if (input.Length == 1) {
                    if (int.TryParse(input[0], out int filterAge)) {
                        Console.WriteLine(filterAge);
                        filter = "oneNumberFilter";
                    }
                    else
                        break;
                }
                
            } while (filter == "none");
            Console.ReadKey();
        }
        
        public void PrintPassengerList() {
            int passengerCount = 0; // Count to keep track of the passengers onboard
            for (int i = 0; i < _passengerList.Length; i++)
                if (_passengerList[i].GetGender() != null) // Loop through and check if the index is null, if not print to console
                {
                    Console.WriteLine("[{4}] Name: {0, -10}| Age: {1, -10}| Gender: {2, -10}| Occupation: {3}",
                        _passengerList[i].GetName(), _passengerList[i].GetAge(), _passengerList[i].GetGender(),
                        _passengerList[i].GetOccupation(), (i + 1).ToString());
                    passengerCount++;
                }

            if (passengerCount <= 0)
                Console.WriteLine("\n(!) Error: Bus is empty!");
            Console.WriteLine("... Press any key to return!");
            Console.ReadKey();
        }

        public void PrintGenders() {
            int mCounter = 0;
            int fCounter = 0;

            foreach (Person passenger in _passengerList)
                if (passenger.GetGender() != null)
                    if (passenger.GetGender() == "Male")
                        mCounter++;
                    else if (passenger.GetGender() == "Female")
                        fCounter++;

            Console.WriteLine("Male(s): {0}  |  Female(s): {1}", mCounter, fCounter);
            Console.ReadKey();
        }

        public void SortPassengerList() {
            var sortedList = new Person[_passengerList.Length];
            int index = 0;
            foreach (Person passenger in _passengerList) {
                sortedList[index] = passenger;
                index++;
            }

            for (int i = 0; i < sortedList.Length; i++)
            for (int j = 1; j < sortedList.Length - 1; j++)
                if (sortedList[i].GetGender() != null && sortedList[j].GetGender() != null)
                    if (sortedList[j].GetAge() < sortedList[j - 1].GetAge()) {
                        Person exPerson = sortedList[j - 1];
                        sortedList[j - 1] = sortedList[j];
                        sortedList[j] = exPerson;
                    }

            foreach (Person passenger in sortedList)
                if (passenger.GetGender() != null)
                    Console.Write("{0}  ", passenger.GetAge());

            Console.ReadKey();
        }

        public void PokePassenger() {
        }

        public void PassengerLeave() {
        }

        public void GeneratePassengers() {
            // Get list of people from json
            // Loop through _passengerList.Length times
            //     Call RegisterPassenger()
            //         Randomly pick out people
            //         Insert name, occupation and gender from json
            //         Randomize age
        }
    }
}