namespace StockTracker.Utilities;

public class DateTimeConverter
{
    public static DateTime UnixTimeStampToDateTime(long unix)
    {
        return DateTimeOffset.FromUnixTimeSeconds(unix).DateTime;
    }

    public static long DateTimeToUnixTimeStamp(DateTime dateTime)
    {
        var dateTimeOffset = new DateTimeOffset(dateTime);
        return dateTimeOffset.ToUnixTimeSeconds();
    }
}