using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Yaadein.Services
{
    public class GroqService
    {
        private readonly string apiKey;
        private readonly string apiUrl = "https://api.groq.com/openai/v1/chat/completions";
        private readonly HttpClient httpClient;

        public GroqService()
        {
            apiKey = ConfigurationManager.AppSettings["GroqApiKey"] ?? "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";
            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");
            httpClient.Timeout = TimeSpan.FromSeconds(30);
        }

        public async Task<string> RecallPersonInfoAsync(string name, string relationship, string details)
        {
            try
            {
                var requestBody = new
                {
                    model = "mixtral-8x7b-32768",
                    messages = new[]
                    {
                        new { role = "system", content = "You are a warm, caring companion for someone with memory challenges. Speak naturally and kindly, as if you're their trusted friend. Keep responses brief (2-3 sentences) and comforting." },
                        new { role = "user", content = $"Help me remember: {name} is my {relationship}. Details: {details}" }
                    },
                    temperature = 0.8,
                    max_tokens = 150
                };

                string jsonContent = JsonConvert.SerializeObject(requestBody);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync(apiUrl, content);
                string responseBody = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<ChatCompletionResponse>(responseBody);
                return result.Choices[0].Message.Content;
            }
            catch
            {
                return $"💭 {name} is your {relationship}. They care deeply about you and are always there for you.";
            }
        }

        public async Task<EmotionalAnalysis> AnalyzeEmotionAsync(string text)
        {
            try
            {
                var requestBody = new
                {
                    model = "mixtral-8x7b-32768",
                    messages = new[]
                    {
                        new { role = "system", content = "Analyze the emotional tone of text. Respond ONLY with JSON: {\"emotion\":\"happy/sad/anxious/calm/frustrated/content\",\"intensity\":1-10,\"context\":\"brief explanation\"}" },
                        new { role = "user", content = text }
                    },
                    temperature = 0.3,
                    max_tokens = 100
                };

                string jsonContent = JsonConvert.SerializeObject(requestBody);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync(apiUrl, content);
                string responseBody = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<ChatCompletionResponse>(responseBody);

                string cleaned = result.Choices[0].Message.Content.Replace("```json", "").Replace("```", "").Trim();
                return JsonConvert.DeserializeObject<EmotionalAnalysis>(cleaned);
            }
            catch
            {
                return new EmotionalAnalysis { Emotion = "neutral", Intensity = 5, Context = "Unable to analyze" };
            }
        }

        public async Task<string> GenerateEmpatheticResponseAsync(string userMessage, string emotionalState)
        {
            try
            {
                var requestBody = new
                {
                    model = "mixtral-8x7b-32768",
                    messages = new[]
                    {
                        new { role = "system", content = $"You are a compassionate AI companion. The person is feeling {emotionalState}. Respond with warmth and understanding. Be conversational, not robotic. Keep it brief and natural." },
                        new { role = "user", content = userMessage }
                    },
                    temperature = 0.9,
                    max_tokens = 200
                };

                string jsonContent = JsonConvert.SerializeObject(requestBody);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync(apiUrl, content);
                string responseBody = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<ChatCompletionResponse>(responseBody);
                return result.Choices[0].Message.Content;
            }
            catch
            {
                return "I'm here with you. How can I help you feel better right now?";
            }
        }

        public async Task<string> SuggestRoutineAsync(string routineType, string preferences)
        {
            try
            {
                var requestBody = new
                {
                    model = "mixtral-8x7b-32768",
                    messages = new[]
                    {
                        new { role = "system", content = "Suggest simple, gentle routine steps for someone with memory challenges. Be encouraging and clear. Provide 4-6 steps." },
                        new { role = "user", content = $"Create a {routineType} routine. Preferences: {preferences}" }
                    },
                    temperature = 0.7,
                    max_tokens = 300
                };

                string jsonContent = JsonConvert.SerializeObject(requestBody);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync(apiUrl, content);
                string responseBody = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<ChatCompletionResponse>(responseBody);
                return result.Choices[0].Message.Content;
            }
            catch
            {
                return "I'd love to help you create a routine, but I'm having trouble right now. Let's try again in a moment.";
            }
        }

        private class ChatCompletionResponse
        {
            [JsonProperty("choices")]
            public List<Choice> Choices { get; set; }
        }

        private class Choice
        {
            [JsonProperty("message")]
            public Message Message { get; set; }
        }

        private class Message
        {
            [JsonProperty("content")]
            public string Content { get; set; }
        }
    }

    public class EmotionalAnalysis
    {
        [JsonProperty("emotion")]
        public string Emotion { get; set; }

        [JsonProperty("intensity")]
        public int Intensity { get; set; }

        [JsonProperty("context")]
        public string Context { get; set; }
    }
}