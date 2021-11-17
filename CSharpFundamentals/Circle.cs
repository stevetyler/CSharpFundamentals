using System;


namespace CSharpFundamentals

{
    public class Circle : Shape
    {
        public override void Draw()
        {
            Console.WriteLine("Draw a circle");

            // base.Draw(); call parent if necessary
        }
    }
}

