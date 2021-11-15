using System;
using System.IO;
using System.Collections.Generic;
using System.Collections;


namespace CSharpFundamentals

{
    partial class Program
    {

        static void Main(string[] args)
        {
            DateTime woCompleteDate = new DateTime(2021, 8, 30);
            DateTime cp12Date = new DateTime(2021, 11, 3);
            TimeSpan timeSpan = cp12Date - woCompleteDate;
            DateTime interval = DateTime.MinValue + timeSpan;
            int month = interval.Month - 1;

            Console.WriteLine("complete date {0}", woCompleteDate);
            Console.WriteLine("complete date {0}", cp12Date);
            Console.WriteLine("timespan {0}", timeSpan);
            Console.WriteLine("interval {0}", interval);
            Console.WriteLine("month {0}", month);

        }

        public void UpdateCP12AnniversaryDate()
        {
            Entity Incident = this.OrgService.Retrieve(
                this.Record.LogicalName,
                this.Record.Id,
                new ColumnSet(WorkOrderIncident.CompletedDate, WorkOrderIncident.StateCode, WorkOrderIncident.WorkOrder, WorkOrderIncident.incidentType)
            );
            EntityReference incidentType = Incident.GetAttributeValue<EntityReference>(WorkOrderIncident.incidentType);

            Entity defaultWorkOrderType = this.OrgService.Retrieve(
                incidentType.LogicalName,
                incidentType.Id,
                new ColumnSet(incidentType.defaultWorkorderType)
            );

            var isWorkOrderComplete = this.Record.GetAttributeValue<OptionSetValue>(WorkOrderIncident.StatusCode).Value.Equals((int)WorkOrderIncident.StatusCodeOptions.Completed);

            var isLandLord = defaultWorkOrderType.GetAttributeValue<EntityReference>(IncidentType.DefaultWorkOrderType).Id == WorkOrderTypeAttributes.WorkorderTypeGUID.LandLord;

            if (isWorkOrderComplete && isLandLord)
            {
                var workOrderId = Incident.GetAttributeValue<EntityReference>(WorkOrderIncident.WorkOrder);
                Entity workOrder = this.OrgService.Retrieve(
                    workOrderId.LogicalName,
                    workOrderId.Id,
                    new ColumnSet(WorkOrder.ServiceAccount, WorkOrder.WorkOrderType)
                );

                if (workOrder.Contains(WorkOrder.ServiceAccount))
                {
                    var accountId = workOrder.GetAttributeValue<EntityReference>(WorkOrder.ServiceAccount);
                    Entity account = this.OrgService.Retrieve(
                        accountId.LogicalName,
                        accountId.Id,
                        new ColumnSet(Account.CP12Date, Account.CP12AnniversaryDate));

                    var cp12Date = Convert.ToDateTime(account.GetAttributeValue<DateTime>(Account.CP12Date));
                    var woCompleteDate = DateTime.Today;
                    var defaultDate = Convert.ToDateTime("01-01-0001 00:00:00");
                    var hasCp12Date = cp12Date != defaultDate && cp12Date != null;
                    var hasWoCompleteDate = woCompleteDate != defaultDate && woCompleteDate != null;
                    var hasNoDatesSet = woCompleteDate == defaultDate && cp12Date == defaultDate;
                    var hasWoCompleteDateAndCp12Date = hasCp12Date && hasWoCompleteDate;
                    var hasWoCompleteDateBeforeOrEqualCp12Date = hasWoCompleteDateAndCp12Date && (woCompleteDate.Date <= cp12Date.Date);
                    var hasWoCompleteDateAfterCp12Date = !hasWoCompleteDateBeforeOrEqualCp12Date;

                    TimeSpan timeDifference = cp12Date - woCompleteDate;
                    DateTime mnth = DateTime.MinValue + timeDifference;
                    int month = mnth.Month - 1;
                    int day = mnth.Day - 1;
                    var hasTimeDiffGreatherThan2Months = month >= 2 && day > 0 || month > 2;

                    if (hasNoDatesSet)
                    {
                        return;
                    }
                    else
                    {
                        if (
                            !hasCp12Date ||
                            hasWoCompleteDateAfterCp12Date ||
                            (hasWoCompleteDateBeforeOrEqualCp12Date && hasTimeDiffGreatherThan2Months)
                        )
                        {
                            account[Account.CP12AnniversaryDate] = woCompleteDate.AddMonths(12);
                        }
                        else
                        {
                            account[Account.CP12AnniversaryDate] = cp12Date.AddMonths(12);
                        }

                        this.OrgService.Update(account);
                    }
                }
            }
        }


        // Composition
        public class Installer
        {
            private readonly Logger _logger;

            public Installer(Logger logger)
            {
                _logger = logger;
            }

            public void Install()
            {
                _logger.Log("We are installing the application");
            }
        }

        public class Logger
        {
            public void Log(string message)
            {
                Console.WriteLine(message);
            }
        }

        public class DbMigrator
        {
            /* initialise in constructor
                var dbMigrator = new DbMigrator(new Logger());

                var logger = new Logger();
                var installer = new Installer(logger);

                dbMigrator.Migrate();
             */

            private readonly Logger _logger;

            public DbMigrator(Logger logger)
            {
                _logger = logger;
            }

            public void Migrate()
            {
                _logger.Log("We are migrating");
            }
        }



        //// Inheritance
        //public class Text : PresentationObject
        //{
        //    public int FontSize { get; set; }
        //    public string FontName { get; set; }

        //    public void AddHyperLink(string url)
        //    {
        //        Console.WriteLine("We added a link to " + url);
        //    }
        //}

        //public class PresentationObject
        //{
        //    public int Width { get; set; }
        //    public int Height { get; set; }

        //    public void Copy()
        //    {
        //        Console.WriteLine("Object copied to clipboard");
        //    }

        //    public void Duplicate()
        //    {
        //        Console.WriteLine("Object was duplicated");
        //    }

        //}

        public class Post
        {
            /* Design a class called Post.
            This class models a StackOverflow post.It should have properties for title, description and the date/time it was created.
            We should be able to up-vote or down-vote a post.
            We should also be able to see the current vote value.
            In the main method, create a post, up-vote and down-vote it a few times and then display the the current vote value.
            In this exercise, you will learn that a StackOverflow post should provide methods for up-voting and down-voting.
            You should not give the ability to set the Vote property from the outside, because otherwise, you may accidentally change the votes of a class to 0 or to a random number.
            And this is how we create bugs in our programs. The class should always protect its state and hide its implementation detail.
            Educational tip: The aim of this exercise is to help you understand that classes should encapsulate data AND behaviour around that data.
            Many developers (even those with years of experience) tend to create classes that are purely data containers, and other classes that are purely behaviour (methods) providers.This is not object-oriented programming.
            This is procedural programming. Such programs are very fragile. Making a change breaks many parts of the code. */

            public string Title;
            public string Description;
            public DateTime CreatedDate;
            private int _voteCount;

            public Post(string title)
            {
                this.Title = title;
                this.CreatedDate = DateTime.Now;
                this._voteCount = 0;
            }

            public void UpVote()
            {
                this._voteCount += 1;
            }

            public void DownVote()
            {
                this._voteCount -= 1;
            }

            public void GetVotes()
            {
                Console.WriteLine("The post has {0} votes", this._voteCount);
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
            Console.WriteLine(Convert.ToDateTime("01-01-0001 00:00:00")); // 1/1/0001 12:00:00 AM
            Console.WriteLine(DateTime.MinValue); // 1/1/0001 12:00:00 AM
            Console.WriteLine(new DateTime()); // 1/1/0001 12:00:00 AM

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
            // implicitly converted to base class - not typesafe
            var list = new ArrayList();
            list.Add(1);
            list.Add("Steve");
            //list.Add(new Text());


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

