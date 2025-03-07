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
            try
            {
                await Init();
                await database.InsertAsync(photo);
            }
            catch (SQLiteException ex) when (ex.Result == SQLite3.Result.Constraint)
            {
                Console.WriteLine("Duplicate photo skipped");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Insert failed: {ex.Message}");
                throw;
            }
        }

        public async Task<List<Photo>> GetAllAsync()
        {
            await Init();
            return await database.Table<Photo>().ToListAsync();
        }

        public async Task<Photo> GetByUrlAsync(string urlSmall)
        {
            try
            {
                await Init();
                return await database.Table<Photo>().Where(i => i.UrlSmall == urlSmall).FirstOrDefaultAsync();
            }
            catch (SQLiteException ex) when (ex.Result == SQLite3.Result.NotFound)
            {
                Console.WriteLine("Photo not found");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Get photo failed: {ex.Message}");
                throw;
            }
        }

        public async Task DeleteAsync(Photo photo)
        {
            try
            {
                await Init();
                await database.DeleteAsync(photo);
            }
            catch (SQLiteException ex) when (ex.Result == SQLite3.Result.NotFound)
            {
                Console.WriteLine("Photo not found");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Get photo failed: {ex.Message}");
                throw;
            }
        }
    }
}