using System.Collections.ObjectModel;
using System;
using System.ComponentModel;
using System.Windows.Media;

namespace WaferHandlingHMI.ViewModels
{
    public class LoadPortViewModel : INotifyPropertyChanged
    {
        private static readonly int _cassetteSize = 25;

        private readonly bool _initialStateOccupied;

        public ObservableCollection<SlotViewModel> Slots { get; } = new ObservableCollection<SlotViewModel>();

        public LoadPortViewModel(bool initialStateOccupied = false)
        {
            _initialStateOccupied = initialStateOccupied;
            for (int i = 0; i < _cassetteSize; i++) 
            {
                Slots.Add(new SlotViewModel(i, initialStateOccupied));
            }
        }

        public void UpdateSlotState(int slotNumber, bool isOccupied)
        {
            if (slotNumber >= 0 && slotNumber < 25)
            {
                var slot = Slots[slotNumber];
                slot.FillColor = isOccupied ? Brushes.Gray : Brushes.Transparent;
                SlotStateChanged?.Invoke(slotNumber, isOccupied);
            }
        }

        // Event to notify slot state changes
        public event Action<int, bool> SlotStateChanged;

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
