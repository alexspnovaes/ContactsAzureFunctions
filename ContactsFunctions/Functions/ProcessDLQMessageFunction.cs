using Azure.Messaging.ServiceBus;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using ContactsFunctions.Services;
using ContactsFunctions.Models;

namespace ContactsFunctions.Functions
{
    public class ProcessDLQMessageFunction
    {
        private readonly ILogger<ProcessDLQMessageFunction> _logger;
        private readonly IContactService _contactService;

        public ProcessDLQMessageFunction(ILogger<ProcessDLQMessageFunction> logger, IContactService contactService)
        {
            _logger = logger;
            _contactService = contactService;
        }

        [Function(nameof(ProcessDLQMessageFunction))]
        public async Task Run(
            [ServiceBusTrigger("contacts-queue/$DeadLetterQueue", Connection = "ServiceBusConnection")]
            ServiceBusReceivedMessage message,
            ServiceBusMessageActions messageActions)
        {
            _logger.LogInformation("DLQ Message ID: {MessageId}", message.MessageId);
            _logger.LogInformation("DLQ Message Body: {MessageBody}", message.Body.ToString());
            _logger.LogInformation("DLQ Message Content-Type: {ContentType}", message.ContentType);

            try
            {
                var contact = System.Text.Json.JsonSerializer.Deserialize<Contact>(message.Body);
                if (contact != null)
                {
                    await _contactService.CreateContactAsync(contact);
                    _logger.LogInformation("Mensagem DLQ processada com sucesso.");
                }
                else
                {
                    _logger.LogWarning("Falha ao desserializar a mensagem DLQ.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro durante o processamento da mensagem DLQ.");
            }

            await messageActions.CompleteMessageAsync(message);
        }
    }
}