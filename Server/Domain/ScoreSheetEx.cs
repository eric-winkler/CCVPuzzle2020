using PuzzlePortal.Shared;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace PuzzlePortal.Server.Domain
{
    internal static class ScoreSheetEx
    {
        public static string ComputeSignature(this ScoreSheet scoresheet, string key)
        {
            var document = JsonSerializer.Serialize(new ScoreSheet(scoresheet));
            using var hmac = new HMACSHA384(Encoding.UTF8.GetBytes(key));
            return Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(document)));
        }
    }
}
