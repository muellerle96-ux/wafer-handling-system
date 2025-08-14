using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Media;

namespace WaferHandlingHMI.ViewModels
{
    public class RobotViewModel : INotifyPropertyChanged
    {
        private bool _facingLeft;
        public bool FacingLeft
        {
            get => _facingLeft;
            set
            {
                _facingLeft = value;
            }
        }

        public ObservableCollection<ArmPartViewModel> RobotParts { get; } = new ObservableCollection<ArmPartViewModel>();

        private readonly LoadPortViewModel _lp1ViewModel;
        private readonly LoadPortViewModel _lp2ViewModel;

        public RobotViewModel(LoadPortViewModel lp1ViewModel, LoadPortViewModel lp2ViewModel)
        {
            _lp1ViewModel = lp1ViewModel;
            _lp2ViewModel = lp2ViewModel;

            RobotParts.Add(new ArmPartViewModel(ArmParts.Hand, initiallyActive: false));
            RobotParts.Add(new ArmPartViewModel(ArmParts.Arm, initiallyActive: true));
            RobotParts.Add(new ArmPartViewModel(ArmParts.Joint, initiallyActive: true));
            RobotParts.Add(new ArmPartViewModel(ArmParts.Arm, initiallyActive: false));
            RobotParts.Add(new ArmPartViewModel(ArmParts.Hand, initiallyActive: false));
            _facingLeft = true;
        }

        public async void StartTransfer()
        {
            for (int slot = 0; slot < 25; slot++)
            {
                // Picks up the wafer when the current slot is occupied
                if (_lp1ViewModel.Slots[slot].FillColor == Brushes.Gray) 
                {
                    // The wafer is picked up
                    UpdateArmPartState(ArmParts.Hand, true, 0);
                    _lp1ViewModel.UpdateSlotState(slot, false); 
                    await Task.Delay(500);

                    // Robot arm rotates 180 degrees
                    FacingLeft = !FacingLeft;
                    UpdateArmPartState(ArmParts.Arm, false, 1);
                    UpdateArmPartState(ArmParts.Hand, false, 0);
                    UpdateArmPartState(ArmParts.Arm, true, 3);
                    UpdateArmPartState(ArmParts.Hand, true, 4);
                    await Task.Delay(500); 

                    // The wafer is placed in LP2
                    _lp2ViewModel.UpdateSlotState(slot, true); 
                    UpdateArmPartState(ArmParts.Hand, false, 4);
                    await Task.Delay(500); 

                    // Rotate back towards LP1
                    FacingLeft = !FacingLeft;
                    UpdateArmPartState(ArmParts.Arm, false, 3);
                    UpdateArmPartState(ArmParts.Arm, true, 1);
                    await Task.Delay(500);
                }
            }
            UpdateStatus("Transfer Complete");
        }

        public void UpdateArmPartState(ArmParts part, bool isActive, int position)
        {
            if (part == ArmParts.Joint)
                return;

            ArmPartViewModel? armPart = null;
            for (int i = 0; i < RobotParts.Count; i++)
            {
                if (i == position && RobotParts[i].ArmParts == part)
                    armPart = RobotParts[i];
            }

            if (armPart is not null)
            {
                armPart.FillColor = isActive ? Brushes.Gray : Brushes.Transparent;
                ArmPartStateChanged?.Invoke(armPart, isActive);
            }
        }

        private void UpdateStatus(string status) { }


        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        // Event to notify robot arm state changes
        public event Action<ArmPartViewModel, bool> ArmPartStateChanged;


    }
}
