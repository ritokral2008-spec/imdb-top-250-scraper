using SQLite;

namespace IMDbTop250Scraper
{
    [Table("Movies")]
    public class Movie
    {
        [PrimaryKey]
        public int Position { get; set; }

        public string Title { get; set; }

        public string Rating { get; set; }

        public string VoteCount { get; set; }

        public string Year { get; set; }

        public string Poster { get; set; }

        public string Link { get; set; }
    }
}
