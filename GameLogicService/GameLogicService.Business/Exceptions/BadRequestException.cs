using FluentValidation.Results;

namespace GameLogicService.Business.Exceptions
{
    [Serializable]
    public class BadRequestException : ApplicationException
    {
        public IList<ValidationFailure> Errors { get; }
        public BadRequestException(IList<ValidationFailure> errors)
        {
            Errors = errors;
        }
    }
}
