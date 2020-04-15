namespace BussenApp {
    public struct Person {
        public Person(int age, string gender, string name, string occupation) {
            this._age = age;
            this._gender = gender;
            this._name = name;
            this._occupation = occupation;
        }

        private readonly int _age;
        private readonly string _gender;
        private readonly string _name;
        private readonly string _occupation;

        public int GetAge() { return _age; }
        public string GetGender() { return _gender; }
        public string GetName() { return _name; }
        public string GetOccupation() { return _occupation; }
    }
}