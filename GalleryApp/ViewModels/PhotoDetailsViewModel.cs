namespace GalleryApp.ViewModels
{
    public partial class PhotoDetailsViewModel
    {
        void OnSwiped(object sender, SwipedEventArgs e)
        {
            switch (e.Direction)
            {
                case SwipeDirection.Left:
                    Console.WriteLine("Left");
                    break;
                case SwipeDirection.Right:
                    Console.WriteLine("Right");
                    break;
                case SwipeDirection.Down:
                    Console.WriteLine("Down");
                    break;
            }
        }
    }
}
