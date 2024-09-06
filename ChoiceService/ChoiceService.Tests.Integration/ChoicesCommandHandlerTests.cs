using ChoiceService.Business.Contracts;
using ChoiceService.Business.Handlers;
using ChoiceService.Business.Models;
using ChoiceService.Business.Requests;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Shouldly;

namespace ChoiceService.Tests.Integration
{
    public class ChoicesCommandHandlerTests
    {
        private readonly IMediator _mediator;
        private readonly ServiceProvider _serviceProvider;

        public ChoicesCommandHandlerTests()
        {
            var services = new ServiceCollection();

            services.AddMediatR(typeof(ChoicesHandler.CommandHandler).Assembly);

            var mockChoiceProvider = new Mock<IChoiceProvider>();
            mockChoiceProvider.Setup(x => x.GetAllChoices())
                .Returns(new List<Choice>
                {
                    new Choice { Id = 1, Name = "Rock" },
                    new Choice { Id = 2, Name = "Paper" },
                    new Choice { Id = 3, Name = "Scissors" }
                });

            services.AddSingleton<IChoiceProvider>(mockChoiceProvider.Object);

            _serviceProvider = services.BuildServiceProvider();
            _mediator = _serviceProvider.GetRequiredService<IMediator>();
        }

        [Fact]
        public async Task GetChoices_ShouldReturnChoicesResponse()
        {
            // Arrange
            var request = new GetChoicesRequest();

            // Act
            var response = await _mediator.Send(request);

            // Assert
            response.ShouldNotBeNull();
            response.Choices.Count.ShouldBe(3);
            response.Choices[0].Name.ShouldBe("Rock");
            response.Choices[1].Name.ShouldBe("Paper");
            response.Choices[2].Name.ShouldBe("Scissors");
        }

        public void Dispose()
        {
            _serviceProvider?.Dispose();
        }
    }
}