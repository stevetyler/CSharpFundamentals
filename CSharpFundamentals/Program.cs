using System;
using System.IO;
using System.Collections.Generic;

namespace CSharpFundamentals

{
    class Program
    {

        static void Main(string[] args)
        {
            StopWatch.Init();
        }

        public class StopWatch
        {
            /* Design a class called Stopwatch. 
             * It should provide two methods: Start and Stop. We call the start method first, and the stop method next. 
             * Then we ask the stopwatch about the duration between start and stop. 
             * Duration should be a value in TimeSpan. Display the duration on the console.
             * We should also be able to use a stopwatch multiple times. So we may start and stop it and then start and stop it again. 
             * Make sure the duration value each time is calculated properly.
             * We should not be able to start a stopwatch twice in a row (because that may overwrite the initial start time). 
             * So the class should throw an InvalidOperationException if its started twice. 
             */

            private DateTime _startTime;
            private DateTime _stopTime;
            private bool _isRunning = false;

            public static void Init()
            {
                var stopWatch = new StopWatch();

                Console.WriteLine("Welcome to StopWatch");
                Console.WriteLine("Start - 1 | Stop - 2| Exit - 3");

                while (true)
                {
                    var input = Console.ReadLine();

                    if (input == "1")
                    {
                        stopWatch.Start();
                        Console.WriteLine("StopWatch has started!");
                    }
                    if (input == "2")
                    {
                        Console.WriteLine("StopWatch stopped at {0} ", stopWatch.Stop());
                    }
                    if (input == "3")
                    {
                        Environment.Exit(1);
                    }
                }
            }

            public void Start()
            {
                if (_isRunning)
                    throw new InvalidOperationException("StopWatch is already running");
                else
                {
                    _startTime = DateTime.Now;
                    _isRunning = true;
                }
            }
            
            public TimeSpan Stop()
            { 
                if (!_isRunning)
                    throw new InvalidOperationException("StopWatch has not been started");
                else
                    _stopTime = DateTime.Now;
                    _isRunning = false;

                return _stopTime - _startTime;
            }
        }

        public class HttpCookie
        {
            private readonly Dictionary<string, string> _dictionary; // could use = new Dictionary()
            public DateTime Expiry { get; set; }

            public HttpCookie()
            {
                _dictionary = new Dictionary<string, string>();
            }

            public string this[string key] // indexer
            {
                get { return _dictionary[key]; }
                set { _dictionary[key] = value; }
            }
        }

        public class Customer
        {
            public int Id;
            public string Name;
            public readonly List<Order> Orders = new List<Order>(); // allows only 1 list and cannot be overwritten

            public Customer(int id)
            {
                this.Id = id;
            }

            public Customer(int id, string name)
                : this(id)
            {
                this.Name = name;
            }

            public void Promote()
            {
                //Orders = new List<Order>();
                //....
            }
        }

        static void ParseNums()
        {
            try
            {
                var num = int.Parse("abc");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Conversion failed");
            }

            int number;
            var result = int.TryParse("abc", out number);
            if (result)
                Console.WriteLine(number);
            else
                Console.WriteLine("Conversion failed");
        }

        static void UseParams()
        {
            var calculator = new Calculator();
            Console.WriteLine(calculator.Add(1, 2, 3, 4, 5));
            Console.WriteLine(calculator.Add(new int[] { 1, 2, 3, 4, 5 }));
        }

        public class Calculator
        {
            public int Add(params int[] numbers)
            {
                var sum = 0;
                foreach (var number in numbers)
                {
                    sum += number;
                }
                return sum;
            }
        }

        static void UsePoints()
        {
            try
            {
                var point = new Point(10, 20);
                point.Move(new Point(40, 60));
                Console.WriteLine("Point is as at {0}, {1}", point.X, point.Y);

                point.Move(100, 200);
                Console.WriteLine("Point is as at {0}, {1}", point.X, point.Y);
            }
            catch (Exception)
            {
                Console.WriteLine("An unxpected error occured");
            }
        }

        public class Point
        {
            public int X;
            public int Y;

            public Point(int x, int y)
            {
                this.X = x;
                this.Y = y;
            }

            public void Move(int x, int y)
            {
                this.X = x;
                this.Y = y;
            }

            public void Move(Point newLocation)
            {
                if (newLocation != null)
                    Move(newLocation.X, newLocation.Y);
                else
                    throw new ArgumentNullException("newLocation");
            }
        }

        public class Order
        {

        }

        public class CustomerA
        {
            public int Id;
            public string Name;
            public List<Order> Orders;

            // lots of constructors - use object initializer instead
            public CustomerA()
            {
                Orders = new List<Order>();
            }

            public CustomerA(int id)
                : this()
            {
                this.Id = id;
            }

            public CustomerA(int id, string name)
                : this(id)
            {
                this.Id = id;
                this.Name = name;
            }
        }

        static void FileMethods()
        {
            var path = @"c:\somefile.txt";
            File.Copy("c:\\temp\\myfile.jpg", "d:\\temp\\myfile.jpg", true);
            File.Copy(@"c:\temp\myfile.jpg", @"d:\temp\myfile.jpg", true); // using verbatim string to remove escape
            File.Delete(path);
            if (File.Exists(path))
            {
                // do something
            }

            var content = File.ReadAllText(path);
            File.ReadAllText(path);

            // File - static method, can be slow due to security checking
            // FileInfo - instance method
            var fileInfo = new FileInfo(path);

            Directory.CreateDirectory(@"c:\temp\folder1");

            Path.GetExtension(path);
        }

        static string SummariseText(string text, int maxLength = 20)
        {
            if (text.Length < maxLength)
                return (text);

            var words = text.Split(' ');
            var totalChars = 0;
            var summaryWords = new List<string>();

            foreach (var word in words)
            {
                summaryWords.Add(word);
                totalChars += word.Length + 1; // add space as well
                if (totalChars > maxLength)
                    break;
            }

            return String.Join(" ", summaryWords) + "...";
        }

        static void StringMethods()
        {
            var fullName = "Steve Tyler  ";
            Console.WriteLine("Trim: '{0}'", fullName.Trim());
            Console.WriteLine("ToUpper: '{0}'", fullName.ToUpper());
            var index = fullName.IndexOf(' ');
            var firstName = fullName.Substring(0, index);
            var lastName = fullName.Substring(index + 1);

            Console.WriteLine("FirstName: " + firstName);
            Console.WriteLine("LastName: " + lastName);

            var names = fullName.Split(' ');
            Console.WriteLine("FirstName: " + names[0]);
            Console.WriteLine("FirstName: " + names[1]);

            Console.WriteLine(fullName.Replace("Steve", "Stephen"));

            if (String.IsNullOrWhiteSpace(" "))
                Console.WriteLine("Invalid");

            float price = 29.95f;
            price.ToString("C");

            string s = "1234";

            int i = int.Parse(s); // throws exception if s is null or undefined
            int j = Convert.ToInt32(s); // returns 0 for null or undefined

            int k = 1234;

            string t = k.ToString("C"); // "$1,234.00" D, Decimal, e/E Exponential
            string u = k.ToString("C0"); // "$1,234"
        }

        static void DateMethods()
        {
            var timeSpan = new TimeSpan(1, 2, 3);
            //var timeSpan1 = new TimeSpan(1, 0, 0);
            //var timeSpan2 = TimeSpan.FromHours(1);


            var start = DateTime.Now;
            var end = DateTime.Now.AddMinutes(2);
            var duration = end - start;
            Console.WriteLine("Duration: " + duration);

            // Properties
            Console.WriteLine("Minutes: " + timeSpan.Minutes);
            Console.WriteLine("Total minutes: " + timeSpan.TotalMinutes);

            // Add
            Console.WriteLine("Add example: " + timeSpan.Add(TimeSpan.FromMinutes(8)));
            Console.WriteLine("Subtract example: " + timeSpan.Subtract(TimeSpan.FromMinutes(2)));

            // ToString
            Console.WriteLine("ToString" + timeSpan.ToString());

            // Parse
            Console.WriteLine("Parse: " + TimeSpan.Parse("01:02:03"));
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

