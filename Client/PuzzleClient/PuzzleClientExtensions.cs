using Cloudcrate.AspNetCore.Blazor.Browser.Storage;
using Microsoft.AspNetCore.Components;
using PuzzlePortal.Shared;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace PuzzlePortal.Client.PuzzleClient
{
    public static class PuzzleClientExtensions
    {
        public static void AddAuth(this HttpRequestMessage message, LocalStorage storage)
        {
            var scoreSheet = storage.GetItem<ScoreSheetModel>("scoreSheet");
            if (scoreSheet == null)
            {
                Console.WriteLine("No active scoresheet!");
                return;
            }

            Console.WriteLine($"Loaded scoresheet for {scoreSheet.Name}");
            message.Headers.Add("ScoreSheet", System.Text.Json.JsonSerializer.Serialize(scoreSheet));
        }

        public static async Task SaveAuth(this HttpResponseMessage message, LocalStorage storage)
        {
            var scoreSheet = await message.ReadJsonResponse<ScoreSheetModel>();
            storage.SetItem("scoreSheet", scoreSheet);
        }

        public static bool EnsureAuthorized(this HttpResponseMessage response, NavigationManager navigationManager)
        {
            if(response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                navigationManager.NavigateTo("https://www.youtube.com/watch?v=dQw4w9WgXcQ");
                return false;
            }

            return true;
        }

        public static async Task<T> ReadJsonResponse<T>(this HttpResponseMessage response)
        {
            var contentString = await response.Content.ReadAsStringAsync();
            try
            {
                return JsonSerializer.Deserialize<T>(contentString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            catch(JsonException)
            {
                Console.Write("Couldn't deserialize:" + Environment.NewLine + contentString);
                throw;
            }
        }
    }
}
