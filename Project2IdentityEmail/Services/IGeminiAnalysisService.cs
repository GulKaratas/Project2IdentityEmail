using Project2IdentityEmail.Entities;
using Project2IdentityEmail.ViewModels;

namespace Project2IdentityEmail.Services
{
    public interface IGeminiAnalysisService
    {
        Task<MessageAnalysisViewModel> AnalyzeMessagesAsync(
            List<Message> messages);
    }
}