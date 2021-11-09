using System;

namespace CSharpFundamentals

{
    partial class Program
    {
        public class Car : Vehicle
        {
            public Car(string registrationNumber)
                : base(registrationNumber)
            {
                Console.WriteLine("Car is being initialised with number {0}", registrationNumber);
            }
        }
    }
}

