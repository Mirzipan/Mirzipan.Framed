using System;

namespace Mirzipan.Framed.Exceptions
{
    public class SceneNotLoadedException: Exception
    {
        public SceneNotLoadedException(string message) : base(message)
        {
        }
    }
}