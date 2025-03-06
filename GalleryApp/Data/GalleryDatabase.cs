using SQLite;
using GalleryApp.Models;

namespace GalleryApp.Data
{
    public class GalleryDatabase
    {
        private SQLiteAsyncConnection database;

        public async Task Init()
        {
            if (database is not null)
                return;

            database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
            var result = await database.CreateTableAsync<Photo>();
        }

        public async Task CreateAsync(Photo photo)
        {
            await Init();
            await database.InsertAsync(photo);
        }

        public async Task<List<Photo>> GetAllAsync()
        {
            await Init();
            return await database.Table<Photo>().ToListAsync();
        }

        public async Task<Photo> GetByUrlAsync(string urlSmall)
        {
            await Init();
            return await database.Table<Photo>().Where(i => i.UrlSmall == urlSmall).FirstOrDefaultAsync();
        }

        public async Task DeleteAsync(Photo photo)
        {
            await Init();
            await database.DeleteAsync(photo);
        }
    }
}