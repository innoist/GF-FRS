
namespace Cares.ExceptionHandling
{
    /// <summary>
    /// Cares Exception Contents
    /// </summary>
    public sealed class CaresExceptionContent
    {
        /// <summary>
        /// Error message
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Cares Exception Type
        /// </summary>
        public string ExceptionType { get { return CaresExceptionTypes.CaresGeneralException; } }

    }
}
