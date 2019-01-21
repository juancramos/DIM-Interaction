using DIM_Interaction.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
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
        private Grid gPuzzleGrid { get; set; }

        public PlayPuzzleView()
        {
            this.InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            Puzzle = e.Parameter as PlayPuzzle;
            await Puzzle.GeneratePuzzle();

            gPuzzleGrid = Puzzle.PuzzleBox;
            gPuzzleGrid.Name = "GeneratedPuzzleGrid";
            Grid.SetColumn(gPuzzleGrid, 0);
            PuzzleGrid.Children.Add(gPuzzleGrid);

            base.OnNavigatedTo(e);
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }
    }
}
