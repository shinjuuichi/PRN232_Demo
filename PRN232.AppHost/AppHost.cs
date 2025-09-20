var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.Slot2API>("slot2api");

builder.AddProject<Projects.WebMVC>("webmvc");

builder.AddProject<Projects.ApiGateway>("apigateway");

builder.Build().Run();
