namespace CSharpFundamentals

{
    public abstract class Shape
    {
        // abstract class cannot be instantiated

        public int Height { get; set; }
        public int Width { get; set; }
        //public Position Position { get; set; }

        public abstract void Draw(); // has to implement in derived class
    }
}
 
