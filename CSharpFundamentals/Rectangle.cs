using System;


namespace CSharpFundamentals

{
    public class Rectangle : Shape
    {
        public override void Draw()
        {
            Console.WriteLine("Draw a rectangle");

            // base.Draw(); call parent if necessary
        }
    }
}

