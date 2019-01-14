using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace DIM_Interaction
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private readonly TransformGroup ct = new TransformGroup();

        public MainPage()
        {
            this.InitializeComponent();

            this.typeImg.PointerPressed += touchImg_PointerPressed;
            this.typeImg.PointerReleased += touchImg_PointerReleased;
            this.typeImg.PointerExited += touchImg_PointerExited;
            this.typeImg.PointerEntered += touchImg_PointerEntered;
            this.typeImg.Tapped += touchImg_PointerTap;
            this.typeImg.DoubleTapped += touchImg_PointerDoubleTapped;
            this.typeImg.Holding += touchImg_PointerHolding;
            this.typeImg.ManipulationDelta += touchImg_ManipulationDelta;
        }

        private void touchImg_PointerHolding(object sender, HoldingRoutedEventArgs e)
        {
            this.itemList.Items.Clear();
        }

        private void touchImg_PointerDoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            this.typeImg.Opacity = 1;
        }

        private void touchImg_PointerTap(object sender, TappedRoutedEventArgs e)
        {
            writeOnList(e.GetPosition(this.typeImg).X, e.GetPosition(this.typeImg).Y);
        }

        private void touchImg_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            FrameworkElement source = (FrameworkElement)e.OriginalSource;
            Point center = this.TransformToVisual(source).TransformPoint(e.Position);

            RotateTransform rotation = new RotateTransform();
            rotation.CenterX = center.X;
            rotation.CenterY = center.Y;
            rotation.Angle = e.Delta.Rotation;
            this.ct.Children.Add(rotation);

            ScaleTransform scaling = new ScaleTransform();
            scaling.CenterX = center.X;
            scaling.CenterY = center.Y;
            scaling.ScaleX = e.Delta.Scale;
            scaling.ScaleY = e.Delta.Scale;
            this.ct.Children.Add(scaling);

            TranslateTransform translation = new TranslateTransform();
            translation.X = e.Delta.Translation.X;
            translation.Y = e.Delta.Translation.Y;
            this.ct.Children.Add(translation);

            source.RenderTransform = ct;
        }
        
        private void touchImg_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            this.itemList.Items.Add(new ListViewItem { Content = new TextBlock { Text = $"{e.Pointer.PointerId} has entered" } });
        }

        private void touchImg_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            this.itemList.Items.Add(new ListViewItem { Content = new TextBlock { Text = $"{e.Pointer.PointerId} has exit" } });
        }

        private void touchImg_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            this.itemList.Items.Add(new ListViewItem { Content = new TextBlock { Text = $"{e.Pointer.PointerId} has released" } });
        }

        private void touchImg_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            writeOnList(e.GetCurrentPoint(this.typeImg).Position.X, e.GetCurrentPoint(this.typeImg).Position.Y);
        }

        private void writeOnList(double x, double y)
        {
            this.itemList.Items.Add(new ListViewItem { Content = new TextBlock { Text = $"Current point X = {x}, Y = {y}" } });
            this.typeImg.Opacity -= 0.1;
        }
    }
}
