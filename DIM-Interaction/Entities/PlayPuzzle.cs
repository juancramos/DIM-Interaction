using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Windows.Storage;
using Windows.UI.Xaml.Media.Imaging;
using Windows.Storage.Streams;
using DIM_Interaction.Cross;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Input;
using Windows.Foundation;
using Windows.UI;
using System.Linq;

namespace DIM_Interaction.Entities
{
    public class PlayPuzzle
    {
        public int PuzzleSize { get; private set; }
        public int TotalPuzzleSize { get; private set; }
        public string PuzzleName { get; private set; }
        public StorageFolder Folder { get; private set; }
        private List<BitmapImage> PuzzleImages;
        private Grid WhitePuzzlePiece;
        private List<Grid> PuzzlePieces;
        public Grid PuzzleBox { get; private set; }

        public PlayPuzzle(int pPuzzleSize, StorageFolder pFolder, string pPuzzleName)
        {
            PuzzleSize = pPuzzleSize;
            Folder = pFolder;
            PuzzleName = pPuzzleName;
            TotalPuzzleSize = (PuzzleSize * PuzzleSize);
            PuzzleImages = new List<BitmapImage>();
            WhitePuzzlePiece = new Grid();
            PuzzlePieces = new List<Grid>();
            PuzzleBox = new Grid();
        }

        public async Task GeneratePuzzle()
        {
            await GetAllImages();
            CreatePuzzleBoxHolder();
            LoadPuzzleBoxHolder();
        }

        public async Task GetAllImages()
        {
            try
            {
                StorageFolder pictureFolder2 = await Folder.GetFolderAsync(PuzzleSize.ToString());
                for (int i = 1; i <= TotalPuzzleSize; i++)
                {
                    StorageFile img = await pictureFolder2.GetFileAsync($"{i.ToString()}{ImageTypes.Png}");
                    BitmapImage image = new BitmapImage();
                    using (IRandomAccessStream fileStream = await img.OpenAsync(FileAccessMode.Read))
                    {
                        image.SetSource(fileStream);
                    }
                    PuzzleImages.Add(image);
                }
                if (PuzzleImages.Count != TotalPuzzleSize)
                {
                    throw new Exception("PUzzle can not be generated.");
                }
            }
            catch
            {
                throw;
            }
        }

        private void CreatePuzzleBoxHolder()
        {
            PuzzleBox = new Grid();
            PuzzleBox.HorizontalAlignment = HorizontalAlignment.Stretch;
            PuzzleBox.VerticalAlignment = VerticalAlignment.Stretch;
            for (int Row = 0; Row < PuzzleSize; Row++)
            {
                PuzzleBox.RowDefinitions.Add(new RowDefinition
                {
                    Height = new GridLength((500 / PuzzleSize), GridUnitType.Star)
                });
            }
            for (int Column = 0; Column < PuzzleSize; Column++)
            {
                PuzzleBox.ColumnDefinitions.Add(new ColumnDefinition
                {
                    Width = new GridLength((500 / PuzzleSize), GridUnitType.Star)
                });
            }
        }

        private void LoadPuzzleBoxHolder()
        {
            List<int> listNumbers = new List<int>();
            Random rnd = new Random();
            listNumbers.AddRange(Enumerable.Range(1, TotalPuzzleSize));
            foreach (int item in listNumbers.OrderBy(item => rnd.Next()))
            {
                PuzzlePieces.Add(GeneratePuzzlePiece(item));
            }
            int CurrentPuzzlePiece = 0;
            for (int Row = 0; Row < PuzzleSize; Row++)
            {
                for (int Column = 0; Column < PuzzleSize; Column++)
                {
                    PuzzleBox.Children.Add(PuzzlePieces[CurrentPuzzlePiece]);
                    Grid.SetRow(PuzzlePieces[CurrentPuzzlePiece], Row);
                    Grid.SetColumn(PuzzlePieces[CurrentPuzzlePiece], Column);
                    CurrentPuzzlePiece++;
                }
            }
        }

        private Grid GeneratePuzzlePiece(int PuzzlePieceCount)
        {
            if (PuzzlePieceCount == TotalPuzzleSize)
            {
                WhitePuzzlePiece.HorizontalAlignment = HorizontalAlignment.Stretch;
                WhitePuzzlePiece.VerticalAlignment = VerticalAlignment.Stretch;
                WhitePuzzlePiece.Background = new SolidColorBrush(Colors.Black);
                WhitePuzzlePiece.Tag = PuzzlePieceCount;
                return WhitePuzzlePiece;
            }
            Grid PuzzlePiece = new Grid();
            PuzzlePiece.HorizontalAlignment = HorizontalAlignment.Stretch;
            PuzzlePiece.VerticalAlignment = VerticalAlignment.Stretch;
            PuzzlePiece.Tag = PuzzlePieceCount;
            PuzzlePiece.BorderThickness = new Thickness(2);
            ImageBrush brush = new ImageBrush();
            brush.ImageSource = PuzzleImages[PuzzlePieceCount];
            PuzzlePiece.Background = brush;
            PuzzlePiece.ManipulationMode = ManipulationModes.All;
            PuzzlePiece.RenderTransform = new TransformGroup();
            PuzzlePiece.ManipulationDelta += touchImg_ManipulationDelta;

            return PuzzlePiece;
        }



        private void touchImg_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            FrameworkElement source = (FrameworkElement)e.OriginalSource;
            Point center = source.TransformToVisual(source).TransformPoint(e.Position);
            TransformGroup ct = source.RenderTransform as TransformGroup;

            Grid parent = VisualTreeHelper.GetParent(source) as Grid;

            TranslateTransform translation = new TranslateTransform();
            translation.X = e.Delta.Translation.X - parent.CenterPoint.X;
            translation.Y = e.Delta.Translation.Y - parent.CenterPoint.Y;
            ct.Children.Add(translation);
        }
    }
}
