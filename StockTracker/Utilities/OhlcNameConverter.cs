using System.Text.Json;
using System.Text.Json.Serialization;
using StockTracker.Models.FinnHub;

namespace StockTracker.Utilities;

public class OhlcNameConverter : JsonConverter<Ohlc>
{
    public override Ohlc Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using (JsonDocument doc = JsonDocument.ParseValue(ref reader))
        {
            var root = doc.RootElement;
            return new Ohlc
            {
                Close = root.GetProperty("c").GetDouble(),
                Change = root.GetProperty("d").GetDouble(),
                ChangePercentage = root.GetProperty("dp").GetDouble(),
                High = root.GetProperty("h").GetDouble(),
                Low = root.GetProperty("l").GetDouble(),
                Open = root.GetProperty("o").GetDouble(),
                PreviousClose = root.GetProperty("pc").GetDouble(),
                Time = DateTimeConverter.UnixTimeStampToDateTime(root.GetProperty("t").GetInt32())
            };
        }
    }

    public override void Write(Utf8JsonWriter writer, Ohlc value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        writer.WriteNumber("Close", value.Close);
        writer.WriteNumber("Change", value.Change);
        writer.WriteNumber("ChangePercentage", value.ChangePercentage);
        writer.WriteNumber("High", value.High);
        writer.WriteNumber("Low", value.Low);
        writer.WriteNumber("Open", value.Open);
        writer.WriteNumber("PreviousClose", value.PreviousClose);
        writer.WriteNumber("Time", DateTimeConverter.DateTimeToUnixTimeStamp(value.Time));
        writer.WriteEndObject();
    }
}