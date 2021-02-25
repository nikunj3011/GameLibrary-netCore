using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameLibrary.Services
{
    public class NullMailService : IMailService
    {
        private readonly ILogger<NullMailService> _logger;

        public NullMailService(ILogger<NullMailService> logger)
        {
            _logger = logger;
        }

        public ILogger<NullMailService> Logger { get; }

        public void SendMessage(string to, string subject, string body)
        {
            _logger.LogInformation($"{to },{subject},{body}");
            //log message

        }
    }
}
