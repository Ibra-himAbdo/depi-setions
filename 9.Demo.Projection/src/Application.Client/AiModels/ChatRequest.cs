using System.Text.Json.Serialization;

namespace Application.Client;

public class ChatRequest
{
    public string? Model { get; set; }

    public List<Message>? Messages { get; set; }

    [JsonPropertyName("max_completion_tokens")]
    public int MaxTokens { get; set; } = 1024;

    public int Temperature { get; set; } = 1;
    public bool Stream { get; set; } = false;
}