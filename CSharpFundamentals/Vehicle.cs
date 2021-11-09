using System;

namespace CSharpFundamentals

{
    partial class Program
    {
        public class Vehicle
        {
            private readonly string _registrationNumber;

            public Vehicle(string registrationNumber)
            {
                _registrationNumber = registrationNumber;

                Console.WriteLine("Vehicle is being initialised with number {0}", registrationNumber);
            }
        }
    }
}

