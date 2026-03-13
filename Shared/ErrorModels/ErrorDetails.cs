using System.Text.Json;
using System.Text.Json.Serialization;

namespace Shared.ErrorModels;
public class ErrorDetails
{
    public int StatusCode { get; set; }
    public string ErrorMessage { get; set; } = string.Empty;

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public IEnumerable<string>? Errors { get; set; }
    public override string ToString()
        => JsonSerializer.Serialize(this);
}