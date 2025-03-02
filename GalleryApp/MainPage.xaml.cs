using GalleryApp.ViewModels;
using Microsoft.Maui.Storage;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace GalleryApp
{
    public partial class MainPage : ContentPage
    {
        private bool _isLoading = false;

        public MainPage()
        {
            InitializeComponent();
            BindingContext = new PhotoGridViewModel(new UnsplashService());
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            if (BindingContext is PhotoGridViewModel viewModel)
            {
               await viewModel.LoadRandomPhotosAsync(14);
            }
        }
    }
}
