using DIM_Interaction.Entities;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace DIM_Interaction.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PlayPuzzleView : Page
    {
        private PlayPuzzle Puzzle { get; set; }

        public PlayPuzzleView()
        {
            this.InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            Puzzle = e.Parameter as PlayPuzzle;
            await Puzzle.GeneratePuzzle();
            
            Grid.SetColumn(Puzzle.PuzzleBox, 0);
            Grid.SetColumn(Puzzle.PuzzleImage, 1);
            PuzzleGrid.Children.Add(Puzzle.PuzzleBox);
            PuzzleGrid.Children.Add(Puzzle.PuzzleImage);

            base.OnNavigatedTo(e);
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(NewPuzzle));
        }
    }
}
