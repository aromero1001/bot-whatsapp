using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.AI;
using NurseBot.API.Models;

namespace NurseBot.API.Services
{
    public class NurseChatService : IChatService
    {
        private readonly IChatClient? _chatClient;
        private readonly Dictionary<string, decimal> _serviceCosts = new()
        {
            { "curas", 25.0m },
            { "inyectables", 15.0m },
            { "signos vitales", 10.0m },
            { "post-operatorio", 50.0m }
        };

        public NurseChatService(IChatClient? chatClient = null)
        {
            _chatClient = chatClient;
        }

        public async Task<ChatResponse> ProcessMessageAsync(ChatRequest request)
        {
            string message = request.Message.ToLower();

            // 1. Triage & Keyword Identification
            string? detectedService = null;
            if (message.Contains("curas")) detectedService = "curas";
            else if (message.Contains("inyectables")) detectedService = "inyectables";
            else if (message.Contains("signos") || message.Contains("vitales")) detectedService = "signos vitales";
            else if (message.Contains("post-operatorio")) detectedService = "post-operatorio";

            if (detectedService != null)
            {
                var cost = _serviceCosts[detectedService];
                var availability = SimulateAvailability();
                
                string response = $"He detectado que necesitas asistencia con {detectedService}. " +
                                  $"El costo estimado es de ${cost}. " +
                                  $"{availability}";

                return new ChatResponse(response, detectedService, cost);
            }

            // 2. AI Integration (or simulation)
            if (_chatClient != null)
            {
                var aiResponse = await _chatClient.CompleteAsync(new List<ChatMessage> 
                { 
                    new ChatMessage(ChatRole.User, request.Message) 
                });
                return new ChatResponse(aiResponse.Message.Text ?? "No pude procesar tu solicitud.");
            }

            // Fallback Simulation
            return new ChatResponse("Hola, soy NurseBot. Puedo ayudarte con curas, inyectables, toma de signos vitales o cuidados post-operatorios. ¿En qué puedo ayudarte hoy?");
        }

        private string SimulateAvailability()
        {
            bool isAvailable = new Random().Next(0, 2) == 0;
            return isAvailable 
                ? "Tenemos enfermeros disponibles para este servicio en tu zona." 
                : "Lo siento, no tenemos disponibilidad inmediata, pero podemos agendar para más tarde.";
        }
    }
}
