using GameStatsService.Business.Handlers;

namespace GameStatsService.Tests.Integration
{
    public class AddGameResultValidationHandlerTests
    {
        private readonly AddGameResultHandler.RequestValidator _validator;

        public AddGameResultValidationHandlerTests()
        {
            _validator = new AddGameResultHandler.RequestValidator();
        }


    }
}
