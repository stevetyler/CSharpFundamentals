using System;
namespace CSharpFundamentals
{
    public class Conditionals
    {
        public static void EchoName()
        {
            while (true)
            {
                Console.Write("Enter a name: ");
                var input = Console.ReadLine();

                if (!String.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("@Echo: " + input);
                    continue;
                }
                break;
            }
        }

        public static void IsNumberValid()
        {
            Console.WriteLine("Please enter a number");
            string input1 = Console.ReadLine();

            var number1 = Int32.Parse(input1);

            try
            {
                if (number1 > 0 && number1 < 11)
                {
                    Console.WriteLine("Number is valid");
                }
                else
                {
                    Console.WriteLine("Number is invalid");
                }
            }
            catch (FormatException)
            {
                Console.WriteLine($"Unable to parse '{input1}'");
            }
        }

        public static void CompareNumbers()
        {
            Console.WriteLine("Please enter the first of 2 numbers");
            var input1 = Console.ReadLine();
            var number1 = Int32.Parse(input1);

            Console.WriteLine("Please enter the second number");
            var input2 = Console.ReadLine();
            var number2 = Int32.Parse(input2);

            try
            {
                if (number1 > number2)
                {
                    Console.WriteLine($"{number1} is the larger number");
                }
                else if (number2 > number1)
                {
                    Console.WriteLine($"{number2} is the larger number");
                }
                else
                    Console.WriteLine("Both numbers are equal");
            }
            catch (FormatException)
            {
                Console.WriteLine($"Unable to parse '{input1}' and '{input2}'");
            }
        }

        public static void SpeedCamera()
        {
            // if value is more, calculate the number of points, 1 per 5miles over. If more than 12 points => disqualified
            Console.WriteLine("Please enter the speed limit");
            var input1 = Console.ReadLine();
            var speedLimit = Int32.Parse(input1);

            Console.WriteLine("Please enter your speed");
            var input2 = Console.ReadLine();
            var mySpeed = Int32.Parse(input2);
            var speedDifference = mySpeed - speedLimit;

            try
            {
                if (speedDifference < 0)
                {
                    Console.WriteLine("You are under the speed limit");
                }
                else if (speedDifference == 0)
                {
                    Console.WriteLine("You are at the speed limit");
                }
                else if (speedDifference > 0)
                {
                    int points = speedDifference / 5;

                    var message = $"You are over the speed limit and {points} points will be added to you licence.";

                    if (points < 12)
                        Console.WriteLine(message);
                    else
                        Console.WriteLine(message + " You have been disqualified");
                }
            }
            catch (FormatException)
            {
                Console.WriteLine($"Unable to parse '{input1}' and '{input2}'");
            }
        }
    }
}
