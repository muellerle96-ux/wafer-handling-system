using System.ComponentModel;
using System.Windows.Media;

namespace WaferHandlingHMI.ViewModels
{
    public class SlotViewModel : INotifyPropertyChanged
    {
        private Brush _fillColor;
        public Brush FillColor
        {
            get => _fillColor;
            set
            {
                _fillColor = value;
                OnPropertyChanged(nameof(FillColor));
            }
        }

        public int SlotNumber { get; }

        public SlotViewModel(int slotNumber, bool isOccupied = false)
        {
            SlotNumber = slotNumber;
            FillColor = isOccupied ? Brushes.Gray : Brushes.Transparent;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
