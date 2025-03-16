using GalleryApp.Services.Abstract;

namespace GalleryApp.Services.Implementation
{
    public class DialogService : IDialogService
    {
        public void DisplayAlertMessage(string message)
        {
            Application.Current.MainPage.DisplayAlert("Error", $"{message}", "OK");
        }
    }
}
