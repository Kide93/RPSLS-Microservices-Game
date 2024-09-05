using FluentValidation;
using GameLogicService.Business.Boundaries;
using GameLogicService.Business.Contracts;
using GameLogicService.Business.Models;
using GameLogicService.Business.Settings;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Shared.DTOs;
using Shared.Enums;
using Shared.Exceptions;
using System.Text.Json;

namespace GameLogicService.Business.Implementations
{
    public class PlayerChoiceHandler
    {
        public class RequestValidator : AbstractValidator<PlayerChoiceRequest>
        {
            public RequestValidator()
            {
                RuleFor(x => x.Choice)
                    .Must(choice => Enum.IsDefined(typeof(ChoiceEnum), choice))
                    .WithMessage("Invalid choice. Please select a valid option.");
            }
        }

        public class CommandHandler : IRequestHandler<PlayerChoiceRequest, GameResultResponse>
        {
            private readonly IChoiceStateFactory _choiceStateFactory;
            private readonly HttpClient _httpClient;
            private readonly ILogger<PlayerChoiceHandler> _logger;
            private readonly string _randomChoiceApiUrl;

            public CommandHandler(
                IChoiceStateFactory choiceStateFactory,
                HttpClient httpClient,
                ILogger<PlayerChoiceHandler> logger,
                IOptions<ExternalApiSettings> options)
            {
                _choiceStateFactory = choiceStateFactory;
                _httpClient = httpClient;
                _logger = logger;
                _randomChoiceApiUrl = options.Value.ChoiceServiceApiUrl;
            }

            public async Task<GameResultResponse> Handle(PlayerChoiceRequest request, CancellationToken cancellationToken)
            {
                // TODO: Handle the response
                var computerChoice = await GetRandomChoiceFromApi();

                var playerState = _choiceStateFactory.GetStateForChoice(request.Choice);
                var computerState = _choiceStateFactory.GetStateForChoice(computerChoice);

                var result = playerState.CalculateResult(computerState);

                return new GameResultResponse(request.Choice, computerChoice, result);
            }

            // TODO: move this from here
            private async Task<ChoiceEnum> GetRandomChoiceFromApi()
            {
                try
                {
                    var response = await _httpClient.GetAsync(_randomChoiceApiUrl);
                    response.EnsureSuccessStatusCode();

                    var content = await response.Content.ReadAsStringAsync();
                    var randomChoiceResponse = JsonSerializer.Deserialize<RandomChoiceResponseDto>(content);

                    // TODO: Validate choice from response

                    return (ChoiceEnum)randomChoiceResponse.Id;
                }
                catch (Exception ex)
                {
                    // TODO: Add Polly
                    _logger.LogError(ex, "Error fetching random choice from the API.");
                    throw new ExternalServiceException("Failed to retrieve random choice from external service.", ex);
                }
            }
        }
    }
}
