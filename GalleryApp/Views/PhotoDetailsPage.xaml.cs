using GalleryApp.Models;
using GalleryApp.ViewModels;

namespace GalleryApp.Views;

public partial class PhotoDetailsPage : ContentPage
{
    public PhotoDetailsPage(PhotoDetailsViewModel viewModel)
	{
		InitializeComponent();

        BindingContext = viewModel;
    }
}