using System;
using System.ComponentModel;
using System.Windows.Media;

namespace WaferHandlingHMI.ViewModels
{
    public enum ArmParts
    {
        Hand = 1, 
        Arm = 2, 
        Joint = 3
    }

    public class ArmPartViewModel : INotifyPropertyChanged
    {
        private ArmParts _armParts;
        public ArmParts ArmParts { get => _armParts; }

        private Brush _fillColor;
        public Brush FillColor
        {
            get => _fillColor;
            set
            {
                if (_armParts != ArmParts.Joint)
                {
                    _fillColor = value;
                    OnPropertyChanged(nameof(FillColor));
                }
            }
        }


        public ArmPartViewModel(ArmParts armParts, bool initiallyActive = false)
        {
            _armParts = armParts;
            FillColor = initiallyActive ? Brushes.Gray : Brushes.Transparent;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
