using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System.Net;
using ContactsFunctions.Services;

namespace ContactsFunctions.Functions
{
    public class GetContactsFunction
    {
        private readonly IContactService _contactService;

        public GetContactsFunction(IContactService contactService)
        {
            _contactService = contactService;
        }

        [Function("GetContacts")]
        public async Task<HttpResponseData> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "contacts")] HttpRequestData req,
            FunctionContext context)
        {
            var logger = context.GetLogger("GetContacts");
            logger.LogInformation("GetContacts function triggered.");

            var contacts = await _contactService.GetContactsAsync();

            var response = req.CreateResponse(HttpStatusCode.OK);
            await response.WriteAsJsonAsync(contacts);
            return response;
        }
    }
}
