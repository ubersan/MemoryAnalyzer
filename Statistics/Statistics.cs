using System.Collections.Generic;
using System.ComponentModel;

namespace MemoryAnalyzer
{
    public class Statistics : INotifyPropertyChanged
    {
        public TypeStatistics<FileNode> FileStatistics { get; }
        public TypeStatistics<DirectoryNode> DirectoryStatistics { get; }
        public TypeStatistics<DriveNode> DriveStatistics { get; }

        public Statistics()
        {
            FileStatistics = new TypeStatistics<FileNode>();
            DirectoryStatistics = new TypeStatistics<DirectoryNode>();
            DriveStatistics = new TypeStatistics<DriveNode>();
        }

        public void Process(FileNode fileNode)
        {
            FileStatistics.Add(fileNode);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FileStatistics)));
        }

        public void Process(DirectoryNode directoryNode)
        {
            DirectoryStatistics.Add(directoryNode);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DirectoryStatistics)));
        }

        public void Process(DriveNode driveNode)
        {
            DriveStatistics.Add(driveNode);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DriveStatistics)));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}