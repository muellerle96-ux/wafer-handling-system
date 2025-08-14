using System.Windows;
using System.Windows.Controls;

namespace WaferHandlingHMI
{
    /// <summary>
    /// Interaction logic for a Load Port
    /// </summary>
    public partial class LoadPortControl : UserControl
    {
        public LoadPortControl()
        {
            InitializeComponent();
        }

        // Dependency Property for the Label of the LoadPort
        public static readonly DependencyProperty LoadPortNameProperty =
            DependencyProperty.Register(
                nameof(LoadPortName),
                typeof(string),
                typeof(LoadPortControl),
                new PropertyMetadata("LoadPortName"));

        public string LoadPortName
        {
            get => (string)GetValue(LoadPortNameProperty);
            set => SetValue(LoadPortNameProperty, value);
        }
    }
}
