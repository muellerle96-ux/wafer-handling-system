using System.Windows;
using WaferHandlingHMI.ViewModels;

namespace WaferHandlingHMI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly LoadPortViewModel _loadPort1ViewModel;
        private readonly LoadPortViewModel _loadPort2ViewModel;

        //private readonly RobotArmViewModel _robotArm;

        public MainWindow()
        {
            InitializeComponent();

            _loadPort1ViewModel = new LoadPortViewModel(initialStateOccupied: true);
            _loadPort2ViewModel = new LoadPortViewModel();

            LP1Control.DataContext = _loadPort1ViewModel;
            LP2Control.DataContext = _loadPort2ViewModel;

            //_robotArm = new RobotArmControl();

            _loadPort1ViewModel.SlotStateChanged += (slotNumber, isOccupied) =>
            {
                //Handle Slot state changes for logging the transfers in a list view
            };

            _loadPort2ViewModel.SlotStateChanged += (slotNumber, isOccupied) =>
            {
                //Handle Slot state changes for logging the transfers in a list view
            };
        }
    }
}
