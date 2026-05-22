using System.Globalization;

namespace IMDbTop250Scraper.Helpers
{
    public static class ParseHelpers
    {
        public static double ParseRating(string text)
        {
            return double.TryParse(
                text.Replace("\n", "").Trim(),
                CultureInfo.InvariantCulture,
                out var rating)
                ? rating
                : 0;
        }

        public static int ParseVotes(string text)
        {
            if(string.IsNullOrWhiteSpace(text))
                return 0;

            text = text
                .Replace("(", "")
                .Replace(")", "")
                .Replace(",", "")
                .Trim();

            if(text.EndsWith("K"))
            {
                if(double.TryParse(
                    text.Replace("K", ""),
                    CultureInfo.InvariantCulture,
                    out var k))
                {
                    return (int)(k * 1000);
                }
            }

            if(text.EndsWith("M"))
            {
                if(double.TryParse(
                    text.Replace("M", ""),
                    CultureInfo.InvariantCulture,
                    out var m))
                {
                    return (int)(m * 1_000_000);
                }
            }

            return int.TryParse(text, out var votes)
                ? votes
                : 0;
        }

        public static int ParseYear(string text)
        {
            return int.TryParse(text, out var year)
                ? year
                : 0;
        }
    }
}