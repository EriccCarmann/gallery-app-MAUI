using SQLite;
using System.ComponentModel;

namespace GalleryApp.Models
{
    public class Photo : INotifyPropertyChanged
    {
        [PrimaryKey, AutoIncrement]
        public int PhotoId { get; set; }
        public string UrlSmall { get; set; }
        public string UrlRegular { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        private bool _isFavorite;
        public bool IsFavorite
        {
            get => _isFavorite;
            set
            {
                if (_isFavorite != value)
                {
                    _isFavorite = value;
                    OnPropertyChanged(nameof(IsFavorite));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}