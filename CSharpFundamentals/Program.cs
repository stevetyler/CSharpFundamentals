using System;
using CSharpFundamentals.Math;

namespace CSharpFundamentals

{
    class Program
    {
        static void Main(string[] args)
        {
            MaxNumber();
        }

        static void MaxNumber()
        {
            Console.WriteLine("Enter a series of numbers followed by a comma");
            var strings = Console.ReadLine().Split(',');
            // Console.WriteLine("numbers" + numbers);

            int[] numbers = {};

            try {
                // int[] numbers = Array.ConvertAll(strings, s => int.Parse(s));
                numbers = Array.ConvertAll(strings, int.Parse); // using a method group instead of a lambda expression

                Console.WriteLine("strings" + strings + " numbers " + numbers);

                var maxNumber = 0;

                foreach (var number in numbers)
                {
                    // Console.WriteLine("number is " + number);
                    if (number > maxNumber)
                    {
                        maxNumber = number;
                    }
                }
                Console.WriteLine("The maximum number is " + maxNumber);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        static void NumberGuesser()
        {
            var random = new Random();
            var randomNum = random.Next(1, 10);

            for (var i = 4; i > 0; i--)
            {
                Console.Write("Guess a number between 1 and 10: ");
                var guess = Console.ReadLine();
                var triesLeft = i - 1;

                if (guess == randomNum.ToString())
                {
                    Console.WriteLine("Correct! I was thinking of the number " + randomNum);
                    break;
                }
                else if (triesLeft != 0)
                {
                    Console.WriteLine("Incorrect! " + "You have " + triesLeft + " tries left. Please try again");
                }
                else {
                    Console.WriteLine("Incorrect! The number I was thinking of is " + randomNum);
                }
            }
        }
        
        static void PasswordGenerator()
        {
            var random = new Random();
            const int passwordLength = 10;
            var buffer = new char[passwordLength];

            for (var i = 0; i < passwordLength; i++)
            {
                buffer[i] = (char)('a' + random.Next(0, 26));
            }

            var password = new string(buffer);

            Console.WriteLine(password);
        }
    }
}

