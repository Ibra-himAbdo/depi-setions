using System.Text.Json.Serialization;

namespace Demo.API.Models;

public class AiChatRequest
{
    public string? Model { get; set; }
    public float Temperature { get; set; }
    public int MaxCompletionTokens { get; set; }
    public float TopP { get; set; }
    public bool Stream { get; set; }
    public ReasoningEffort ReasoningEffort { get; set; } = ReasoningEffort.Low;
    public List<AiChatMessage> Messages { get; set; } = [];
}

public enum ReasoningEffort
{
    Low,
    Meduim,
    High,
}

public class AiChatMessage
{
    public string? Content { get; set; }
    public MessageRole Role { get; set; }
}

public enum MessageRole
{
    System,
    User,
    Assistant
}