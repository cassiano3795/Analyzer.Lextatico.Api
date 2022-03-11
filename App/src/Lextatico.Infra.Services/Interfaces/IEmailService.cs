using System.Threading.Tasks;
using Lextatico.Infra.Services.Models.EmailService;

namespace Lextatico.Infra.Services.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(EmailRequest emailRequest);
    }
}