using System;
namespace Exceptions{
    public class OrderException : Exception
    {
        public OrderException() { }
        public OrderException(string message) : base(message) { }
        public OrderException(string message, Exception inner) : base(message, inner) { }
  
    }
    public class OrderItemException: Exception
    {
        public OrderItemException(string message) : base(message) { }
    }

}