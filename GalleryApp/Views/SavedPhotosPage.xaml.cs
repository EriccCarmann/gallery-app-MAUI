using GalleryApp.Services.Implementation;
using GalleryApp.ViewModels;

namespace GalleryApp.Views;

public partial class SavedPhotosPage : ContentPage
{
    public SavedPhotosPage(SavedPhotosViewModel viewModel)
    {
        InitializeComponent();

        BindingContext = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is SavedPhotosViewModel viewModel)
        {
            await viewModel.LoadPhotosCommand.ExecuteAsync(null);
        }
    }
}