namespace GameLogicService.Business.Exceptions
{
    [Serializable]
    public class InvalidChoiceException : Exception
    {
        public InvalidChoiceException(string message) : base(message)
        {
        }
    }
}
