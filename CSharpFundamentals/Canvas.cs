using System;
using System.IO;
using System.Collections.Generic;
using System.Collections;

namespace CSharpFundamentals

{
    partial class Canvas
    {
       
        public void DrawShapes(List<Shape> shapes)
        {
            foreach (var shape in shapes)
            {
                shape.Draw();
            }
        }
    }
}

