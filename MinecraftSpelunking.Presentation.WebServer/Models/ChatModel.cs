using MinecraftPinger.Library.Models;
using System.Text.Json.Serialization;

namespace MinecraftSpelunking.Presentation.WebServer.Models
{
    public class ChatModel
    {
        public static readonly ChatModel Empty = new ChatModel();

        [JsonPropertyName("text")]
        public string Text { get; set; } = string.Empty;

        [JsonPropertyName("bold")]
        public bool Bold { get; set; }

        [JsonPropertyName("italic")]
        public bool Italic { get; set; }

        [JsonPropertyName("underline")]
        public bool Underline { get; set; }

        [JsonPropertyName("strikethrough")]
        public bool Strikethrough { get; set; }

        [JsonPropertyName("obfuscated")]
        public bool Obfuscated { get; set; }

        [JsonPropertyName("color")]
        public string Color { get; set; } = string.Empty;

        [JsonPropertyName("extra")]
        public List<Chat> Extra { get; set; } = [];
    }
}
