namespace Shared.ErrorModels;
public class ValidationErrorResponse
{
    public int StatusCode { get; set; }
    public string ErrorMessage { get; set; } = string.Empty;
    public IEnumerable<ValidationError> Errors { get; set; } = [];
}
public class ValidationError
{
    public string Field { get; set; } = string.Empty;
    public IEnumerable<string> Errors { get; set; } = [];
}