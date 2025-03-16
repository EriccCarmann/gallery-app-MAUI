using GalleryApp.ViewModels;

namespace GalleryApp.Views;

public partial class NewPhotosPage : ContentPage
{
    public NewPhotosPage(NewPhotosViewModel viewModel)
	{
		InitializeComponent();
        
        BindingContext = viewModel;
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is NewPhotosViewModel viewModel)
        {
            await viewModel.LoadPhotosCommand.ExecuteAsync(null);
        }
    }
}