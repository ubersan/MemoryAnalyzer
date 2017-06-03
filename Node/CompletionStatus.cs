using System.ComponentModel;

namespace MemoryAnalyzer
{
    public class CompletionStatus : INotifyPropertyChanged
    {
        private bool _totalActiveChildCounterWasSet;

        public CompletionStatus()
        {
            ActiveChildCounter = 0;
            TotalActiveChildCounter = 1;

            IsCompleted = false;
            IsRunning = false;
            IsQueued = true;
            UpdateAllCompletionPropertyChanged();
        }

        public void SetIsCompleted()
        {
            IsCompleted = true;
            IsRunning = false;
            IsQueued = false;
            UpdateAllCompletionPropertyChanged();
        }

        public void SetIsRunning()
        {
            IsCompleted = false;
            IsRunning = true;
            IsQueued = false;
            UpdateAllCompletionPropertyChanged();
        }

        public void IncreaseActiveChildCounter()
        {
            ++ActiveChildCounter;
            UpdateAllCounterPropertyChanged();
        }

        public void DecreaseActiveChildCounter()
        {
            --ActiveChildCounter;
            UpdateAllCounterPropertyChanged();
        }

        public void SetTotalActiveChildCounter()
        {
            TotalActiveChildCounter = ActiveChildCounter;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TotalActiveChildCounter)));

            _totalActiveChildCounterWasSet = true;
            UpdateAllCounterPropertyChanged();
        }

        private void UpdateAllCompletionPropertyChanged()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsCompleted)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsRunning)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsQueued)));
        }

        private void UpdateAllCounterPropertyChanged()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ActiveChildCounter)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ProcessedChildrenRatio)));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public bool IsCompleted { get; private set; }
        public bool IsRunning { get; private set; }
        public bool IsQueued { get; private set; }
        public int ActiveChildCounter { get; private set; }
        private int TotalActiveChildCounter { get; set; }

        public int ProcessedChildrenRatio => !_totalActiveChildCounterWasSet || TotalActiveChildCounter == 0
            ? 0
            : (int)(100 * (TotalActiveChildCounter - ActiveChildCounter) / (double)TotalActiveChildCounter);
    }
}