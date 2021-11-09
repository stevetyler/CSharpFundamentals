using System;
using System.Collections;

namespace CSharpFundamentals

{
    partial class Program
    {
        public class Stack
        {
            /* 
             A Stack is a data structure for storing a list of elements in a LIFO (last in, first out) fashion.
            The Push() method stores the given object on top of the stack. We use the “object” type here so
            we can store any objects inside the stack. Remember the “object” class is the base of all classes
            in the .NET Framework. So any types can be automatically upcast to the object. Make sure to
            take into account the scenario that null is passed to this object. We should not store null
            references in the stack. So if null is passed to this method, you should throw an
            InvalidOperationException. Remember, when coding every method, you should think of all
            possibilities and make sure the method behaves properly in all these edge cases. That’s what
            distinguishes you from an “average” programmer.
            The Pop() method removes the object on top of the stack and returns it. Make sure to take into
            account the scenario that we call the Pop() method on an empty stack. In this case, this method
            should throw an InvalidOperationException. Remember, your classes should always be in a valid
            state and used properly. When they are misused, they should throw exceptions. Again, thinking
            of all these edge cases, separates you from an average programmer. The code written this way
            will be more robust and with less bugs.
            The Clear() method removes all objects from the stack.
            We should be able to use this stack class as follows:
            var stack = new Stack();
            stack.Push(1);
            stack.Push(2);
            stack.Push(3);
            Console.WriteLine(stack.Pop());
            Console.WriteLine(stack.Pop());
            Console.WriteLine(stack.Pop());
            The output of this program will be
            3
            2
            1
            */

            private ArrayList _stack = new ArrayList();


            public void Push(object stackObj)
            {
                if (stackObj != null)
                    _stack.Add(stackObj);
                else
                    throw new ArgumentNullException();
            }

            public object Pop()
            {
                var listLength = _stack.Count;
                object poppedObj;

                if (listLength != 0)
                {
                    poppedObj = _stack[listLength - 1];
                    _stack.RemoveAt(listLength - 1);
                    
                }
                else
                    throw new Exception("Cannot pop from an empty stack");

                return poppedObj;
            }

            public void Clear()
            {
                _stack.Clear();
            }

            public void ListStack()
            {
                for (var i = 0; i < _stack.Count; i++)
                    Console.WriteLine("Element at stack index {0} is {1}", i, _stack[i]);
            }
        }
    }
}

