using Microsoft.Extensions.Logging;
using FluentValidation.Results;

namespace Repair.Infrastructure.Exceptions;

public class BaseException : Exception
{
    public EventId EventId { get; init; }
    public ValidationResult? ValidationResult { get; init; } = null;

    public BaseException(EventId eventId) : base(default)
    {
        EventId = eventId;
    }

    public BaseException(EventId eventId, string message) : base(message)
    {
        EventId = eventId;
    }

    public BaseException(EventId eventId, string message, string propertyName) : this(eventId, GetValidationResult(message, propertyName))
    {
    }

    public BaseException(EventId eventId, ValidationResult valResult) : base(GetMessage(valResult))
    {
        EventId = eventId;
        ValidationResult = valResult;

        if (ValidationResult != null && !ValidationResult.IsValid)
        {
            int cnt = 1;
            foreach (var val in ValidationResult.Errors)
            {
                Data.Add($"Message_{cnt}", val.ErrorMessage);
                Data.Add($"ErrorCode_{cnt}", val.ErrorCode);
                Data.Add($"PropertyName_{cnt}", val.PropertyName);
                Data.Add($"AttemptedValue_{cnt}", val.AttemptedValue);
                cnt++;
            }
        }
    }

    private static ValidationResult GetValidationResult(string message, string propertyName)
    {
        ValidationResult vr = new(new[] { new ValidationFailure(propertyName, message) });
        return vr;
    }

    private static string GetMessage(ValidationResult valResult)
    {
        if (valResult == null)
        {
            return string.Empty;
        }

        var messages = valResult.Errors
            .Select(x => $"{x.ErrorMessage} ({x.PropertyName})")
            .ToList();

        var msg = string.Join(", ", messages);
        return msg;
    }
}

