using System;

namespace NurseBot.API.Models
{
    public record ChatRequest(string Message, Guid UserId);
    public record ChatResponse(string Response, string ServiceType = "General", decimal? EstimatedCost = null);
}
