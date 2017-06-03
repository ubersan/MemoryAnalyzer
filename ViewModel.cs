using System.Collections.ObjectModel;
using System.Threading;

namespace MemoryAnalyzer
{
    public class ViewModel : ViewModelBase
    {
        private ObservableCollection<Node> _root;

        public ViewModel()
        {
            _root = new ObservableCollection<Node>();
            var uiContext = SynchronizationContext.Current;

            FileSystemWalker = new FileSystemWalker(_root, uiContext);
            FileSystemWalker.Start();
        }

        public ObservableCollection<Node> Root
        {
            get { return _root; }
            set
            {
                if (_root == value) return;

                _root = value;
                RaisePropertyChanged();
            }
        }

        public FileSystemWalker FileSystemWalker { get; }
    }
}