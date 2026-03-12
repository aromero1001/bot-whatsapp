using System.Threading.Tasks;
using NurseBot.API.Models;

namespace NurseBot.API.Services
{
    public interface IChatService
    {
        Task<ChatResponse> ProcessMessageAsync(ChatRequest request);
    }
}
