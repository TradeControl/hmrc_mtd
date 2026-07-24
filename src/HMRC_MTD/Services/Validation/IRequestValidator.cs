namespace TradeControl.Tax.UK.Services.Validation;

public interface IRequestValidator
{
    ValidationResult Validate(Dictionary<string, object?> parameters);
}
