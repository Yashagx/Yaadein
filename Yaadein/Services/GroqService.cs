using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Yaadein.Services
{
    /// <summary>
    /// Service for AI-powered memory recall using Groq API
    /// </summary>
    public class GroqService
    {
        private readonly string apiKey = Environment.GetEnvironmentVariable("GROQ_API_KEY");
        private readonly string apiUrl = "https://api.groq.com/openai/v1/chat/completions";
        private readonly HttpClient httpClient;

        public GroqService()
        {
            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");
            httpClient.Timeout = TimeSpan.FromSeconds(30);
        }

        /// <summary>
        /// Generate a personalized memory recall message for a person
        /// </summary>
        public async Task<string> RecallPersonInfoAsync(string name, string relationship, string details)
        {
            try
            {
                var requestBody = new
                {
                    model = "llama-3.3-70b-versatile",
                    messages = new[]
                    {
                        new
                        {
                            role = "system",
                            content = "You are a compassionate memory assistant helping an Alzheimer's patient remember people in their life. Provide warm, gentle reminders about the person in 2-3 sentences. Focus on positive memories and important details. Keep it simple and reassuring."
                        },
                        new
                        {
                            role = "user",
                            content = $"Help me remember: {name} is my {relationship}. Important details: {details}"
                        }
                    },
                    temperature = 0.7,
                    max_tokens = 150
                };

                string jsonContent = JsonConvert.SerializeObject(requestBody);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync(apiUrl, content);
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<ChatCompletionResponse>(responseBody);

                return result.Choices[0].Message.Content;
            }
            catch (HttpRequestException)
            {
                return $"💭 {name} is your {relationship}. They are someone special in your life who cares about you deeply.";
            }
            catch (TaskCanceledException)
            {
                return $"💭 {name} is your {relationship}. They are someone special in your life who cares about you deeply.";
            }
            catch (Exception)
            {
                return $"💭 {name} is your {relationship}. They are someone special in your life who cares about you deeply.";
            }
        }

        /// <summary>
        /// Generate suggestions for daily routines
        /// </summary>
        public async Task<string> SuggestRoutineAsync(string routineType, string userPreferences)
        {
            try
            {
                var requestBody = new
                {
                    model = "llama-3.3-70b-versatile",
                    messages = new[]
                    {
                        new
                        {
                            role = "system",
                            content = "You are a helpful assistant for Alzheimer's patients. Suggest simple, easy-to-follow routine steps. Keep instructions clear, brief, and encouraging."
                        },
                        new
                        {
                            role = "user",
                            content = $"Suggest a {routineType} routine for someone with memory challenges. Preferences: {userPreferences}. Provide 4-6 simple steps."
                        }
                    },
                    temperature = 0.7,
                    max_tokens = 200
                };

                string jsonContent = JsonConvert.SerializeObject(requestBody);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync(apiUrl, content);
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<ChatCompletionResponse>(responseBody);

                return result.Choices[0].Message.Content;
            }
            catch (HttpRequestException)
            {
                return "Unable to generate suggestions at this time. Please try again later.";
            }
            catch (TaskCanceledException)
            {
                return "Request timed out. Please check your internet connection and try again.";
            }
            catch (Exception)
            {
                return "Unable to generate suggestions at this time. Please try again later.";
            }
        }

        /// <summary>
        /// Clean up resources
        /// </summary>
        public void Dispose()
        {
            httpClient?.Dispose();
        }

        // -----------------------------------------------------------
        // Strongly-typed response models (replaces 'dynamic')
        // -----------------------------------------------------------

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
}