using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using StockTracker.Models.FinnHub;


namespace StockTracker.Utilities
{
    public class NewsArticleConverter : JsonConverter<NewsArticle>
    {
        public override NewsArticle? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            using (JsonDocument doc = JsonDocument.ParseValue(ref reader))
            {
                var root = doc.RootElement;

                return new NewsArticle
                {
                    Category = root.GetProperty("category").GetString(),
                    Datetime = DateTimeConverter.UnixTimeStampToDateTime(root.GetProperty("datetime").GetInt32()),
                    Headline = root.GetProperty("headline").GetString(),
                    Id = root.GetProperty("id").GetInt32(),
                    Image = root.GetProperty("image").GetString(),
                    Related = root.GetProperty("related").GetString(),
                    Source = root.GetProperty("source").GetString(),
                    Summary = root.GetProperty("summary").GetString(),
                    Url = root.GetProperty("url").GetString()
                };
            }
        }

        public override void Write(Utf8JsonWriter writer, NewsArticle value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            writer.WriteString("category", value.Category);
            writer.WriteNumber("dateTime", DateTimeConverter.DateTimeToUnixTimeStamp(value.Datetime));
            writer.WriteString("headline", value.Headline);
            writer.WriteNumber("id", value.Id);
            writer.WriteString("image", value.Image);
            writer.WriteString("related", value.Related);
            writer.WriteString("source", value.Source);
            writer.WriteString("summary", value.Summary);
            writer.WriteString("url", value.Url);
            writer.WriteEndObject();
        }

    }
}