using System;

namespace Cares.ExceptionHandling
{
    /// <summary>
    /// Cares Exception
    /// </summary>
    public sealed class CaresException : ApplicationException
    {
        /// <summary>
        /// Initializes a new instance of Cares Exception
        /// </summary>
        public CaresException(string message): base(message)
        {            
        }
        /// <summary>
        /// Initializes a new instance of Cares Exception
        /// </summary>
        public CaresException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
