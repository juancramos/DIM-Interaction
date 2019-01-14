using System.Collections.Generic;
using System.Linq;
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
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void touchImg_PointerHolding(object sender, HoldingRoutedEventArgs e)
        {
            FrameworkElement source = (FrameworkElement)e.OriginalSource;
            IEnumerable<object> it = this.itemList.Items.Where(x => ((ListViewItem)x).Tag.Equals(source.Name));
            foreach (object item in it)
            {
                this.itemList.Items.Remove(item);
            }
        }

        private void touchImg_PointerDoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            FrameworkElement source = (FrameworkElement)e.OriginalSource;
            source.Opacity = 1;
        }

        private void touchImg_PointerTap(object sender, TappedRoutedEventArgs e)
        {
            FrameworkElement source = (FrameworkElement)e.OriginalSource;
            Point pSorce = e.GetPosition(source);
            writeOnList(pSorce.X, pSorce.Y, source);
        }

        private void touchImg_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            FrameworkElement source = (FrameworkElement)e.OriginalSource;
            Point center = this.TransformToVisual(source).TransformPoint(e.Position);
            TransformGroup ct = source.RenderTransform as TransformGroup;

            RotateTransform rotation = new RotateTransform();
            rotation.CenterX = center.X;
            rotation.CenterY = center.Y;
            rotation.Angle = e.Delta.Rotation;
            ct.Children.Add(rotation);

            ScaleTransform scaling = new ScaleTransform();
            scaling.CenterX = center.X;
            scaling.CenterY = center.Y;
            scaling.ScaleX = e.Delta.Scale;
            scaling.ScaleY = e.Delta.Scale;
            ct.Children.Add(scaling);

            TranslateTransform translation = new TranslateTransform();
            translation.X = e.Delta.Translation.X;
            translation.Y = e.Delta.Translation.Y;
            ct.Children.Add(translation);
        }
        
        private void touchImg_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            FrameworkElement source = (FrameworkElement)e.OriginalSource;
            this.itemList.Items.Add(new ListViewItem { Tag = source.Name, Content = new TextBlock { Text = $"{source.Name}: {e.Pointer.PointerId} has entered" } });
        }

        private void touchImg_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            FrameworkElement source = (FrameworkElement)e.OriginalSource;
            if (string.IsNullOrWhiteSpace(source.Name)) return;
            this.itemList.Items.Add(new ListViewItem { Tag = source.Name, Content = new TextBlock { Text = $"{source.Name}: {e.Pointer.PointerId} has exit" } });
        }

        private void touchImg_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            FrameworkElement source = (FrameworkElement)e.OriginalSource;
            this.itemList.Items.Add(new ListViewItem { Tag = source.Name, Content = new TextBlock { Text = $"{source.Name}: {e.Pointer.PointerId} has released" } });
        }

        private void touchImg_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            FrameworkElement source = (FrameworkElement)e.OriginalSource;
            Point pSorce = e.GetCurrentPoint(source).Position;
            writeOnList(pSorce.X, pSorce.Y, source);
        }

        private void writeOnList(double x, double y, FrameworkElement source)
        {
            this.itemList.Items.Add(new ListViewItem { Tag = source.Name, Content = new TextBlock { Text = $"{source.Name}: Current point X = {x}, Y = {y}" } });
            source.Opacity -= 0.1;
        }
    }
}
