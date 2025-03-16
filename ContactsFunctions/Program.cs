using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using ContactsFunctions.Data;
using ContactsFunctions.Repositories;
using ContactsFunctions.Services;
using Microsoft.Extensions.DependencyInjection;

Environment.SetEnvironmentVariable("AzureFunctionsWorkerDisableCLI", "true");

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices(services =>
    {
        var connectionString = Environment.GetEnvironmentVariable("DefaultConnection");
        services.AddDbContext<ContactsDbContext>(options =>
            options.UseSqlServer(connectionString));

        services.AddScoped<IContactRepository, ContactRepository>();
        services.AddScoped<IContactService, ContactService>();
    })
    .Build();

host.Run();
