using SQLite;

namespace IMDbTop250Scraper.Services
{
    public class DatabaseService
    {
        private readonly SQLiteConnection _db;

        public DatabaseService()
        {
            _db = new SQLiteConnection("movies.db");

            _db.CreateTable<Movie>();
        }

        public void SaveMovies(List<Movie> movies)
        {
            _db.DeleteAll<Movie>();

            _db.InsertAll(movies);
        }

        public List<Movie> LoadMovies()
        {
            return _db.Table<Movie>().ToList();
        }

        public void ClearMovies()
        {
            _db.DeleteAll<Movie>();
        }
    }
}