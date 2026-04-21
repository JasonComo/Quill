using System.Net;
using QuillApp.IRepositories;
using QuillApp.IServices;
using QuillApp.Models;
using QuillApp.Models.Enums;

namespace QuillApp.Services;

public class MockupService : IMockupService
{
    private readonly IMockupRepository _mockupRepository;
    private readonly IStoryRepository _storyRepository;
    private readonly IAiMockupGenerator _aiMockupGenerator;
    private readonly ILogger<MockupService> _logger;

    public MockupService(
        IMockupRepository mockupRepository,
        IStoryRepository storyRepository,
        IAiMockupGenerator aiMockupGenerator,
        ILogger<MockupService> logger)
    {
        _mockupRepository = mockupRepository;
        _storyRepository = storyRepository;
        _aiMockupGenerator = aiMockupGenerator;
        _logger = logger;
    }

    public async Task<Mockup> GenerateMockupAsync(int storyId, string? generationPrompt, int currentUserId)
    {
        if (storyId < 1)
            throw new ArgumentOutOfRangeException(nameof(storyId));

        if (currentUserId < 1)
            throw new ArgumentOutOfRangeException(nameof(currentUserId));

        generationPrompt = generationPrompt?.Trim();

        var story = await _storyRepository.GetStoryAsync(storyId, currentUserId);
        if (story is null)
            throw new InvalidOperationException("Story not found.");

        var generation = await GenerateHtmlDocumentWithFallbackAsync(story, generationPrompt);

        var mockup = new Mockup
        {
            StoryId = storyId,
            HtmlDocument = generation.HtmlDocument,
            GenerationPrompt = generationPrompt,
            Status = generation.Status,
            ErrorMessage = generation.ErrorMessage,
            CreatedAtUtc = DateTime.UtcNow
        };

        return await _mockupRepository.CreateMockupAsync(mockup, currentUserId);
    }

    private async Task<GeneratedMockupHtml> GenerateHtmlDocumentWithFallbackAsync(Story story, string? generationPrompt)
    {
        try
        {
            var htmlDocument = await _aiMockupGenerator.GenerateHtmlMockupAsync(story, generationPrompt);
            return new GeneratedMockupHtml(htmlDocument, MockupStatus.Ready, null);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "AI mockup generation failed. Falling back to placeholder HTML.");
            return new GeneratedMockupHtml(
                BuildPlaceholderHtmlDocument(story, generationPrompt),
                MockupStatus.Failed,
                ex.Message);
        }
    }

    public async Task<Mockup?> GetMockupAsync(int mockupId, int currentUserId)
    {
        if (mockupId < 1)
            throw new ArgumentOutOfRangeException(nameof(mockupId));

        if (currentUserId < 1)
            throw new ArgumentOutOfRangeException(nameof(currentUserId));

        return await _mockupRepository.GetMockupAsync(mockupId, currentUserId);
    }

    public async Task<List<Mockup>> GetMockupsByStoryIdAsync(int storyId, int currentUserId)
    {
        if (storyId < 1)
            throw new ArgumentOutOfRangeException(nameof(storyId));

        if (currentUserId < 1)
            throw new ArgumentOutOfRangeException(nameof(currentUserId));

        return await _mockupRepository.GetMockupsByStoryIdAsync(storyId, currentUserId);
    }

    public async Task DeleteMockupAsync(int mockupId, int currentUserId)
    {
        if (mockupId < 1)
            throw new ArgumentOutOfRangeException(nameof(mockupId));

        if (currentUserId < 1)
            throw new ArgumentOutOfRangeException(nameof(currentUserId));

        await _mockupRepository.DeleteMockupAsync(mockupId, currentUserId);
    }

    private static string BuildPlaceholderHtmlDocument(Story story, string? generationPrompt)
    {
        var title = WebUtility.HtmlEncode(story.Title);
        var description = WebUtility.HtmlEncode(story.Description);
        var criteria = WebUtility.HtmlEncode(story.Criteria);
        var prompt = WebUtility.HtmlEncode(
            string.IsNullOrWhiteSpace(generationPrompt)
                ? "No extra generation notes were provided."
                : generationPrompt);

        return $$"""
               <!DOCTYPE html>
               <html lang="en">
               <head>
                   <meta charset="utf-8" />
                   <meta name="viewport" content="width=device-width, initial-scale=1" />
                   <title>{{title}} Mockup</title>
                   <style>
                       :root {
                           color-scheme: light;
                           --ink: #10241b;
                           --muted: #637369;
                           --green: #0b4a32;
                           --gold: #b78922;
                           --paper: #ffffff;
                           --canvas: #f3f5ef;
                           --line: #d8dfd9;
                       }

                       * { box-sizing: border-box; }

                       body {
                           margin: 0;
                           min-height: 100vh;
                           background: linear-gradient(180deg, rgba(11, 74, 50, .09), transparent 260px), var(--canvas);
                           color: var(--ink);
                           font-family: Aptos, "Segoe UI", Arial, sans-serif;
                       }

                       main {
                           width: min(960px, calc(100% - 32px));
                           margin: 0 auto;
                           padding: 48px 0;
                       }

                       .hero, .panel {
                           border: 1px solid var(--line);
                           border-radius: 10px;
                           background: var(--paper);
                           box-shadow: 0 18px 42px rgba(16, 36, 27, .10);
                       }

                       .hero {
                           padding: 34px;
                           border-top: 6px solid var(--green);
                       }

                       .kicker {
                           color: var(--gold);
                           font-size: .78rem;
                           font-weight: 800;
                           letter-spacing: .08em;
                           text-transform: uppercase;
                       }

                       h1 {
                           margin: 8px 0 12px;
                           font-size: clamp(2rem, 5vw, 3.25rem);
                           line-height: 1.08;
                       }

                       p {
                           line-height: 1.6;
                           color: var(--muted);
                       }

                       .grid {
                           display: grid;
                           grid-template-columns: repeat(auto-fit, minmax(240px, 1fr));
                           gap: 16px;
                           margin-top: 18px;
                       }

                       .panel {
                           padding: 22px;
                       }

                       button {
                           border: 1px solid var(--green);
                           border-radius: 6px;
                           background: var(--green);
                           color: #fff;
                           cursor: pointer;
                           font: inherit;
                           font-weight: 700;
                           padding: 12px 16px;
                       }

                       .result {
                           display: none;
                           margin-top: 16px;
                           border-left: 4px solid var(--gold);
                           background: #fff8e8;
                           padding: 12px 14px;
                       }
                   </style>
               </head>
               <body>
                   <main>
                       <section class="hero">
                           <div class="kicker">Generated feature mockup</div>
                           <h1>{{title}}</h1>
                           <p>{{description}}</p>
                           <button id="simulate">Simulate interaction</button>
                           <div id="result" class="result">Interaction simulated. Replace this placeholder with AI-generated UI behavior.</div>
                       </section>

                       <section class="grid">
                           <article class="panel">
                               <div class="kicker">Acceptance criteria</div>
                               <p>{{criteria}}</p>
                           </article>
                           <article class="panel">
                               <div class="kicker">Generation notes</div>
                               <p>{{prompt}}</p>
                           </article>
                       </section>
                   </main>

                   <script>
                       document.getElementById('simulate').addEventListener('click', function () {
                           document.getElementById('result').style.display = 'block';
                       });
                   </script>
               </body>
               </html>
               """;
    }

    private sealed record GeneratedMockupHtml(
        string HtmlDocument,
        MockupStatus Status,
        string? ErrorMessage);
}
