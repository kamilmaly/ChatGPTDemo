using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using OpenAI_API.Completions;
using OpenAI_API;
using Microsoft.Extensions.Options;

namespace ChatGPTDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly OpenAISettings _openAISettings;

        public HomeController(IOptions<OpenAISettings> openAISettings)
        {
            _openAISettings = openAISettings.Value;
        }

        [HttpPost]
        public async Task<IActionResult> GetActionResult(string SearchText)
        {
            string answer = string.Empty;

            var openai = new OpenAIAPI(_openAISettings.APIKey);
            CompletionRequest completionRequest = new CompletionRequest();
            completionRequest.Prompt = SearchText;
            completionRequest.Model = OpenAI_API.Models.Model.DavinciText;
            completionRequest.MaxTokens = 200;

            var result = openai.Completions.CreateCompletionAsync(completionRequest);
            foreach (var item in result.Result.Completions)
            {
                answer = item.Text;
            }

            return Ok(answer);
        }
    }
}
