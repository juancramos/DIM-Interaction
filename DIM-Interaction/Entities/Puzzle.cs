using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.Storage;
using Windows.UI.Xaml.Media.Imaging;

namespace DIM_Interaction.Entities
{
    public class Puzzle : INotifyPropertyChanged
    {
        public string Name { get; }
        public StorageFolder Folder { get; }
        public BitmapImage Image { get; }
        private bool _IsPuzzleAvailable = false;
        public bool IsPuzzleAvailable
        {
            get { return _IsPuzzleAvailable; }
            set { _IsPuzzleAvailable = value; NotifyPropertyChanged(); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Puzzle(string Name, StorageFolder Folder, BitmapImage Image, bool IsPuzzleAvailable)
        {
            this.Name = Name;
            this.Folder = Folder;
            this.Image = Image;
            _IsPuzzleAvailable = IsPuzzleAvailable;
        }
    }
}
