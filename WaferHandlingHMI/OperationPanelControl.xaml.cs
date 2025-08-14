using System.Windows;
using System.Windows.Controls;
using WaferHandlingHMI.ViewModels;

namespace WaferHandlingHMI
{
    /// <summary>
    /// Interaction logic for the UserControl containing the Start and Stop Buttons of the handling system.
    /// </summary>
    public partial class OperationPanelControl : UserControl
    {
        // The button needs to start the operation with the robot view model, so it's needed as a dependency property
        public static readonly DependencyProperty RobotViewModelProperty =
            DependencyProperty.Register(
                nameof(RobotViewModel),
                typeof(RobotViewModel),
                typeof(OperationPanelControl),
                new PropertyMetadata(null));

        public RobotViewModel RobotViewModel
        {
            get => (RobotViewModel)GetValue(RobotViewModelProperty);
            set => SetValue(RobotViewModelProperty, value);
        }

        public OperationPanelControl()
        {
            InitializeComponent();
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            // Start the transfer with the robot view model
            RobotViewModel?.StartTransfer();
            StopButton.IsEnabled = true;
            StartButton.IsEnabled = false;
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            StopButton.IsEnabled = false;
            StartButton.IsEnabled = true;
        }
    }
}
