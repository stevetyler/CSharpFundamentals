using System;

namespace CSharpFundamentals

{
    partial class Program
    {
        public class StopWatch
        {
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
                        Console.WriteLine("StopWatch stopped at {0} ", stopWatch.Stop());

                    if (input == "3")
                        Environment.Exit(1);
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
    }
}

