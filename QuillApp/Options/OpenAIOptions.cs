namespace QuillApp.Options;

public class OpenAIOptions
{
    public string ApiKey { get; set; } = string.Empty;
    public string Model { get; set; } = "gpt-4.1-mini";
    public int MaxOutputTokens { get; set; } = 8000;
    public int TimeoutSeconds { get; set; } = 120;
}
