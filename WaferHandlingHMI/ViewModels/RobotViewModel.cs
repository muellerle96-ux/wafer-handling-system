using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Media;

namespace WaferHandlingHMI.ViewModels
{
    public class RobotViewModel : INotifyPropertyChanged
    {
        private bool _facingLeft;
        public bool FacingLeft 
        { 
            get => _facingLeft; 
            set => _facingLeft = value;
        }

        public ObservableCollection<ArmPartViewModel> RobotParts { get; } = new ObservableCollection<ArmPartViewModel>();

        public RobotViewModel()
        {
            RobotParts = new ObservableCollection<ArmPartViewModel>();
            RobotParts.Add(new ArmPartViewModel(ArmParts.Hand, initiallyActive: false));
            RobotParts.Add(new ArmPartViewModel(ArmParts.Arm, initiallyActive: true));
            RobotParts.Add(new ArmPartViewModel(ArmParts.Joint, initiallyActive: true));
            _facingLeft = true;
        }


        public void UpdateArmPartState(ArmParts part, bool isActive)
        {
            if (part == ArmParts.Joint)
                return;

            ArmPartViewModel? armPart = RobotParts.Where(p => p.ArmParts == part).FirstOrDefault();
            if (armPart is not null)
            {
                armPart.FillColor = isActive ? Brushes.Gray : Brushes.Transparent;
                ArmPartStateChanged?.Invoke(armPart, isActive);
            }
        }
        
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        // Event to notify slot state changes
        public event Action<ArmPartViewModel, bool> ArmPartStateChanged;


    }
}
