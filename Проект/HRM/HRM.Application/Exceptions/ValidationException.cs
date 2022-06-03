namespace HRM.Application.Exceptions
{
    public class ValidationException : Exception
    {
        public ValidationException(string key) : base($"{key} validation failed.") { }
    }
}
