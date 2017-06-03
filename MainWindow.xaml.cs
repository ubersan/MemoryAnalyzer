using System.ComponentModel;
using System.Windows;

namespace MemoryAnalyzer
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            DataContext = new ViewModel();
            InitializeComponent();
        }

        private void MainWindow_OnClosing(object sender, CancelEventArgs e)
        {
            ((ViewModel) DataContext).FileSystemWalker.Stop();
        }
    }
}