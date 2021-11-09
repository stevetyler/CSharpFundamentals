using System;

namespace CSharpFundamentals

{
    partial class Program
    {
        public class Customer
        {
            public int Id { get; set; }
            public string Name { get; set; }

            public void Promote()
            {
                var rating = CalculateRating(excludeOrders: true);
                if (rating == 0)
                {
                    Console.WriteLine("Promote to level 1");
                }
                else
                {
                    Console.WriteLine("Promote to level 2");
                }
            }

            private int CalculateRating(bool excludeOrders)
            {
                return 0;
            }
        }
    }
}

