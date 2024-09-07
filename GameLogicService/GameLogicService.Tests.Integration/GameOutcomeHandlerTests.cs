using GameLogicService.Business.Requests;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Shared.Enums;
using Shouldly;
using static GameLogicService.Business.Handlers.PlayerChoiceHandler;

namespace GameLogicService.Tests.Integration
{
    public class GameOutcomeHandlerTests
    {
        private readonly IMediator _mediator;

        public GameOutcomeHandlerTests()
        {
            var services = new ServiceCollection();
            services.AddMediatR(typeof(CommandHandler).Assembly);

            var serviceProvider = services.BuildServiceProvider();
            _mediator = serviceProvider.GetRequiredService<IMediator>();
        }

        [Fact]
        public async Task Handle_ShouldReturnCorrectGameOutcomes()
        {
            // Arrange
            var request = new GameOutcomeRequest();

            // Act
            var response = await _mediator.Send(request);

            // Assert
            response.ShouldNotBeNull();
            response.GameOutcomes.ShouldNotBeEmpty();

            var enumValues = Enum.GetValues(typeof(GameOutcomeEnum)).Cast<GameOutcomeEnum>().ToList();
            foreach (var enumValue in enumValues)
            {
                response.GameOutcomes.ShouldContain(go => go.Id == (int)enumValue && go.Name == enumValue.ToString());
            }
        }
    }
}