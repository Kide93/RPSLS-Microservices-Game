namespace ChoiceService.Exceptions
{
    public class DeserializationException : Exception
    {
        public DeserializationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public DeserializationException(string message)
            : base(message)
        {
        }
    }
}
