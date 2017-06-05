using System.ComponentModel;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace MemoryAnalyzer
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            DataContext = new ViewModel();
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void MainWindow_OnClosing(object sender, CancelEventArgs e)
        {
            ((ViewModel) DataContext).FileSystemWalker.Stop();
        }
    }
}