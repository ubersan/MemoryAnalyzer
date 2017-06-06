using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

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

        // TODO: Can this be somewhere else?
        private void TreeView_OnSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var tv = (TreeView) e.OriginalSource;
            ((ViewModel) DataContext).SelectedNode = ((Node) e.NewValue);
        }
    }
}