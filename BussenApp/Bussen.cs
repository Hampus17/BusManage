using System;

namespace BussenApp {
    public class Bussen {
        private bool _isFull = false;
        private int _maxLimit;

        private int _passengerCount; // Keep track of passengers on board

        //// Private variables
        private Person[] _passengerList; // Easy access point to __passengerList

        //// Constructor
        public Bussen(int maxLimit) {
            this._maxLimit = maxLimit;
            _passengerList = new Person[maxLimit]; // Copies a new array with the type Person into _passengerList
        }
        
        //// Private functions
        private bool PositionIsEmpty(int index) { return _passengerList[index].GetGender() == null; } // Function to ease the use of checking if seat is empty or not
        
        //// Public functions
        public void RegisterPassenger() { // Here goes all the logic for adding passengers to the _passengerList array
            if (!_isFull) {
                int age = 0;
                string gender = null;
                string occupation = null;
                string name = null;

                // Assign a number to the variable age 
                Console.WriteLine("\nEnter the information below");
                Console.Write("- Age: ");
                while (age == 0)
                    if (int.TryParse(Console.ReadLine(), out int tempAge) && tempAge > 0 && tempAge < 100)
                        age = tempAge;
                    else
                        Console.WriteLine("(!) Error: Invalid age, try again.\n");

                // Assign a string to the variable gender
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

                // Assign a string to the variable name
                Console.Write("- Name: ");
                while (name == null) {
                    string input = Console.ReadLine();
                    if (!int.TryParse(input, out int useless))
                        name = input;
                    else
                        Console.WriteLine("(!) Error: Invalid characters, try again.\n");
                }

                // Assign a string to the variable occupation
                Console.Write("- Occupation: ");
                while (occupation == null) {
                    string input = Console.ReadLine();
                    if (!int.TryParse(input, out int useless))
                        occupation = input;
                    else
                        Console.WriteLine("(!) Error: Invalid characters, try again.\n");
                }

                // Create a new instance of Person and put it into the _passengerList array with the above variables
                Person newPassenger = new Person(age, gender, name, occupation);

                for (int i = 0; i < _passengerList.Length; i++) {
                    if (PositionIsEmpty(i)) { // If the position is empty (null) add newPassenger and break out of the for loop
                        _passengerList[i] = newPassenger;
                        _passengerCount++;
                        break;
                    }
                    else 
                        _isFull = _passengerCount == _passengerList.Length; // Check if it's full and set the _isFull var
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
            int filterOne = 0, filterTwo = 0, filterAge = 0;

            Console.Write("Enter two numbers to find age between those numbers, or enter one number to find that specific age (e.g. 23 55): ");
            
            do {
                string[] input = Console.ReadLine().Split(' ');
                if (input.Length == 2) 
                    if (int.TryParse(input[0], out filterOne) && filterOne != 0) 
                        if (int.TryParse(input[1], out filterTwo) && filterTwo != 0)
                            filter = "twoNumberFilter";
                        else 
                            Console.WriteLine("(!) Error: Invalid Input, try again!\n");
                    else 
                        Console.WriteLine("(!) Error: Invalid Input, try again!\n");
                else if (input.Length == 1) 
                    if (int.TryParse(input[0], out filterAge)) 
                        filter = "oneNumberFilter";
                    else 
                        Console.WriteLine("(!) Error: Invalid input, try again!\n");
                else 
                    Console.WriteLine("(!) Error: Invalid input, try again!\n");

            } while (filter == "none");

            
            if (filter == "oneNumberFilter") {
                Console.WriteLine("People that are {0} years old: ", filterAge);
                for (int i = 0; i < _passengerList.Length; i++) {
                    if (!PositionIsEmpty(i) && _passengerList[i].GetAge() == filterAge) {
                        Console.Write("{0} | ", _passengerList[i].GetName());
                    }
                }    
            } 
            else if (filter == "twoNumberFilter") {
                int minAge = Math.Min(filterOne, filterTwo);
                int maxAge = Math.Max(filterOne, filterTwo);
                
                for (int i = 0; i < _passengerList.Length; i++) {
                    if (!PositionIsEmpty(i) && _passengerList[i].GetAge() >= minAge && _passengerList[i].GetAge() <= maxAge) {
                        Console.Write("{0} | ", _passengerList[i].GetName());
                    }
                }    
            }
            Console.ReadKey();
        }
        
        public void PrintPassengerList() {
            int passengerCount = 0; // Count to keep track of the passengers onboard
            for (int i = 0; i < _passengerList.Length; i++)
                if (!PositionIsEmpty(i)) // Loop through and check if the index is null, if not print to console
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

            for (int i = 0; i < _passengerList.Length; i++)
                if (!PositionIsEmpty(i))
                    if (_passengerList[i].GetGender() == "Male") {
                        mCounter++;
                        Console.Write("Seat {0}: Male  -", Array.IndexOf(_passengerList, _passengerList[i]) + 1);
                    }
                    else if (_passengerList[i].GetGender() == "Female") {
                        fCounter++;
                        Console.Write("Seat {0} Female  -", Array.IndexOf(_passengerList, _passengerList[i]) + 1);
                    }

            Console.WriteLine("\n\nMale(s): {0}  |  Female(s): {1}", mCounter, fCounter);
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
                if (!(sortedList[i].Equals(null)) && !(sortedList[j].Equals(null)))
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

        public void PassengerLeave() {
            Random random = new Random();
            int passengerIndex = random.Next(0, _passengerCount);
            if (_passengerCount > 0 && !_passengerList.Equals(null)) {
                Console.WriteLine("{0} got off the bus.", _passengerList[passengerIndex].GetName());
                Array.Clear(_passengerList, passengerIndex, 1);
                _passengerCount--;
            }
            else {
                Console.WriteLine("(!) Error: Bus is empty!");
            }
            Console.ReadKey();
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