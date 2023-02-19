using System;

namespace Mirzipan.Framed.Exceptions
{
    public class CoreNotLoadedException: Exception
    {
        public CoreNotLoadedException(string message) : base(message)
        {
        }
    }
}