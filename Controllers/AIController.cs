using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpenAI.Chat;

namespace SIADAL.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class AIController : ControllerBase
    {
        [HttpPost("chat")]
        public async Task<IActionResult> Chat([FromBody] ChatRequest request)
        {
            var apiKey = Environment.GetEnvironmentVariable("OPENAI_API_KEY");
            if (string.IsNullOrEmpty(apiKey))
                return StatusCode(500, "OpenAI API key not configured.");

            var client = new ChatClient("gpt-4o-mini", apiKey);

            var messages = request.Messages.Select<ChatMessageDto, ChatMessage>(m => m.Role switch
            {
                "system" => ChatMessage.CreateSystemMessage(m.Content),
                "assistant" => ChatMessage.CreateAssistantMessage(m.Content),
                _ => ChatMessage.CreateUserMessage(m.Content)
            }).ToList();

            var completion = await client.CompleteChatAsync(messages);
            var responseText = completion.Value.Content[0].Text;

            return Ok(new { reply = responseText });
        }
    }

    public class ChatRequest
    {
        public List<ChatMessageDto> Messages { get; set; } = [];
    }

    public class ChatMessageDto
    {
        public string Role { get; set; } = "user";
        public string Content { get; set; } = string.Empty;
    }
}