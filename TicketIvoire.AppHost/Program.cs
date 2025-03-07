IDistributedApplicationBuilder builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.TicketIvoire_Administration_Api>("ticketivoire-administration-api");

await builder.Build().RunAsync();
