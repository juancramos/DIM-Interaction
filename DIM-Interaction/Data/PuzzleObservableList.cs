using DIM_Interaction.Entities;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace DIM_Interaction.Data
{
    public class PuzzleObservableList : ObservableCollection<Puzzle>, INotifyPropertyChanged
    {
        private static PuzzleObservableList instance;

        private PuzzleObservableList() { }

        public static PuzzleObservableList Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new PuzzleObservableList();
                }
                return instance;
            }
        }

    }
}
