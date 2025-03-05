using SQLite;

namespace GalleryApp.Models
{
    public class Photo
    {
        [PrimaryKey, AutoIncrement]
        public int PhotoId { get; set; }
        public string UrlSmall { get; set; }
        public string UrlFull { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public bool IsFavorite { get; set; } = false;
    }
}