using System;
using Windows.UI.Input;
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
        private PointerPoint _anchorPoint;
        private readonly TranslateTransform _transform = new TranslateTransform();

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
            //this.typeImg.PointerMoved += touchImg_PointerMoved;
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
            Image s = sender as Image;
            Canvas.SetLeft(s, Canvas.GetLeft(s) + e.Delta.Translation.X);
            Canvas.SetTop(s, Canvas.GetTop(s) + e.Delta.Translation.Y);
        }

        //private void touchImg_PointerMoved(object sender, PointerRoutedEventArgs e)
        //{
        //    if (_anchorPoint != null)
        //    {
        //        PointerPoint _currentPoint = e.GetCurrentPoint((sender as Image));
        //        _transform.X += _currentPoint.Position.X - _anchorPoint.Position.X;
        //        _transform.Y += (_currentPoint.Position.Y - _anchorPoint.Position.Y);
        //        this.typeImg.RenderTransform = _transform;
        //        _anchorPoint = _currentPoint;
        //    }
        //}

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
            if (_anchorPoint == null)
            {
                _anchorPoint = e.GetCurrentPoint(sender as Image);
            }
            else
            {
                _anchorPoint = null;
                this.typeImg.RenderTransform = null;
            }
            writeOnList(e.GetCurrentPoint(this.typeImg).Position.X, e.GetCurrentPoint(this.typeImg).Position.Y);
        }

        private void writeOnList(double x, double y)
        {
            this.itemList.Items.Add(new ListViewItem { Content = new TextBlock { Text = $"Current point X = {x}, Y = {y}" } });
            this.typeImg.Opacity -= 0.1;
        }
    }
}
