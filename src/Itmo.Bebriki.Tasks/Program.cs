#pragma warning disable CA1506

using Itmo.Bebriki.Tasks.Application.Extensions;
using Itmo.Bebriki.Tasks.Infrastructure.Persistence.Extensions;
using Itmo.Bebriki.Tasks.Presentation.Grpc.Extensions;
using Itmo.Bebriki.Tasks.Presentation.Kafka.Extensions;
using Itmo.Dev.Platform.Common.Extensions;
using Itmo.Dev.Platform.Events;
using Itmo.Dev.Platform.Observability;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddOptions<JsonSerializerSettings>();
builder.Services.AddSingleton(sp => sp.GetRequiredService<IOptions<JsonSerializerSettings>>().Value);

builder.Services.AddPlatform();
builder.AddPlatformObservability();

builder.Services.AddApplication();
builder.Services.AddInfrastructurePersistence();
builder.Services.AddPresentationGrpc();
builder.Services.AddPresentationKafka(builder.Configuration);

builder.Services.AddPlatformEvents(b => b.AddPresentationKafkaHandlers());

builder.Services.AddUtcDateTimeProvider();

WebApplication app = builder.Build();

app.UseRouting();

app.UsePlatformObservability();

app.UsePresentationGrpc();

await app.RunAsync();