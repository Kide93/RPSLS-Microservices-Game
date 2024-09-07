using FluentValidation.TestHelper;
using GameLogicService.Business.Requests;
using Shared.Enums;
using static GameLogicService.Business.Handlers.PlayerChoiceHandler;

namespace GameLogicService.Tests.Integration
{
    public class PlayerChoiceRequestValidatorTests
    {
        private readonly PlayerChoiceRequestValidator _validator;

        public PlayerChoiceRequestValidatorTests()
        {
            _validator = new PlayerChoiceRequestValidator();
        }

        [Fact]
        public void Should_PassValidation_WhenRequestIsValid()
        {
            // Arrange
            var request = new PlayerChoiceRequest
            {
                UserId = "validUserId",
                Choice = ChoiceEnum.Rock
            };

            // Act
            var result = _validator.TestValidate(request);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void Should_FailValidation_WhenUserIdIsNullOrEmpty()
        {
            // Arrange
            var requestWithNullUserId = new PlayerChoiceRequest
            {
                UserId = null,
                Choice = ChoiceEnum.Rock
            };

            var requestWithEmptyUserId = new PlayerChoiceRequest
            {
                UserId = string.Empty,
                Choice = ChoiceEnum.Rock
            };

            // Act & Assert
            _validator.TestValidate(requestWithNullUserId)
                .ShouldHaveValidationErrorFor(x => x.UserId)
                .WithErrorMessage("UserId cannot be null or empty.");

            _validator.TestValidate(requestWithEmptyUserId)
                .ShouldHaveValidationErrorFor(x => x.UserId)
                .WithErrorMessage("UserId cannot be null or empty.");
        }

        [Fact]
        public void Should_FailValidation_WhenChoiceIsInvalid()
        {
            // Arrange
            var request = new PlayerChoiceRequest
            {
                UserId = "validUserId",
                Choice = (ChoiceEnum)999
            };

            // Act & Assert
            _validator.TestValidate(request)
                .ShouldHaveValidationErrorFor(x => x.Choice)
                .WithErrorMessage("Invalid choice. Please select a valid option.");
        }
    }
}
