using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.AI;
using NurseBot.API.Models;
using NurseBot.API.Services;
using System;

var builder = WebApplication.CreateBuilder(args);

// Port configuration for Railway or other environments
var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
builder.WebHost.UseUrls($"http://*:{port}");

// CORS Configuration
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

// Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// AI Client Configuration (Simulated or real if key exists)
var openAiKey = Environment.GetEnvironmentVariable("OPEN_AI_API_KEY");
var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");

if (!string.IsNullOrEmpty(openAiKey))
{
    // If a key is provided, we could configure a real client here.
    // builder.Services.AddChatClient(new OpenAIClient(openAiKey).AsChatClient("gpt-4o"));
}

if (!string.IsNullOrEmpty(databaseUrl))
{
    // Ready for database integration if needed
    Console.WriteLine($"Database detected: {databaseUrl}");
}

// Dependency Injection
builder.Services.AddScoped<IChatService, NurseChatService>();

var app = builder.Build();

app.UseCors("AllowAll");

// Enable Swagger in all environments for this demo
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "NurseBot API V1");
    c.RoutePrefix = "swagger"; // Swagger UI at /swagger
});

// Endpoints
app.MapGet("/", () => "NurseBot API Online");

app.MapPost("/chat", async (ChatRequest request, IChatService chatService) =>
{
    if (string.IsNullOrEmpty(request.Message))
    {
        return Results.BadRequest("Message cannot be empty.");
    }

    var response = await chatService.ProcessMessageAsync(request);
    return Results.Ok(response);
});

app.Run();
