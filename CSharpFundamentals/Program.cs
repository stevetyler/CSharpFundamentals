using System;
using System.Collections.Generic;

namespace CSharpFundamentals

{
    class Program
    {
        static void Main(string[] args)
        {
           
        }


        static void GetDate()
        {
            var dateTime = new DateTime(2021, 11, 30);
            var now = DateTime.Now;

            Console.WriteLine(now.ToLongDateString());
            Console.WriteLine(now.ToString("dddd MMMM yyyy"));
        }

        static void List3Smallest()
        {
            // supply comma separated list of numbers
            // if list is empty or includes less than 5, display "invalid list" and ask to retry
            // otherwise display 3 smallest numbers

            string[] elements;

            while (true)
            {
                Console.WriteLine("Enter a series of numbers followed by a comma");
                var input = Console.ReadLine();

                if (!String.IsNullOrWhiteSpace(input))
                {
                    elements = input.Split(',');
                    if (elements.Length >= 5)
                        break;

                }
                Console.WriteLine("Invalid list!");
            }

            var numbers = new List<int>();

            foreach (var number in elements)
            {
                numbers.Add(Int32.Parse(number));
            }

            var smallest = new List<int>(); // 3,7,2,8,9
            while (smallest.Count < 3)
            {
                var min = numbers[0]; // assume first element is smallest
                foreach (var number in numbers)
                {
                    if (number < min)
                    {
                        min = number;
                    }
                    smallest.Add(number);
                }

            }

        }

        static void ListMethods()
        {
            // Arrays have a fixed size and length cannot be changed, whereas Lists can be
            var numbers = new List<int>() { 1, 2, 3, 4 };
            numbers.Add(1);
            numbers.AddRange(new int[3] { 5, 6, 7 });

            foreach (var number in numbers)
                Console.WriteLine(number);

            Console.WriteLine();
            Console.WriteLine("Index of 1: " + numbers.IndexOf(1));
            Console.WriteLine("Last Index of 1: " + numbers.LastIndexOf(1));

            Console.WriteLine("Count: " + numbers.Count);

            for (var i = 0; i < numbers.Count; i++)
            {
                if (numbers[i] == 1)
                    numbers.Remove(numbers[i]);
            }
            foreach (var number in numbers)
                Console.WriteLine(number);
        }

        static void ArrayMethods()
        {
            var numbers = new[] { 12, 2, 33, 8, 3 };

            // Length
            Console.WriteLine("Length:" + numbers.Length);

            // IndexOf
            var index = Array.IndexOf(numbers, 3);
            Console.WriteLine("Index of 3:" + index);

            // Clear
            Array.Clear(numbers, 0, 2);
            Console.WriteLine("Effect of Clear()");

            foreach (var n in numbers)
            {
                Console.WriteLine(n);
            }

            // Copy
            int[] another = new int[3];
            Array.Copy(numbers, another, 3);

            Console.WriteLine("Effect of Copy()");
            foreach (var n in another)
            {
                Console.WriteLine(n);
            }

            // Sort
            Array.Sort(numbers);
            foreach (var n in numbers)
            {
                Console.Write(n + " ");
            }
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

