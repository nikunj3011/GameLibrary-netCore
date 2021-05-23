using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace GameLibrary.Services
{
    public interface IMailService
    { 
        Task SendEmailAsync(MailRequest mailRequest);
    }
}