using Microsoft.Extensions.Logging;

namespace GameLibrary.Services
{
    public interface IMailService
    {
        ILogger<NullMailService> Logger { get; }

        void SendMessage(string to, string subject, string body);
    }
}