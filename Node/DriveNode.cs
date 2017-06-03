using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;

namespace MemoryAnalyzer
{
    public class DriveNode : Node
    {
        public static DriveNode Create(DirectoryInfo directoryInfo)
        {
            return new DriveNode
            {
                Name = directoryInfo.Name,
                FileInfo = directoryInfo,
                CompletionState = new CompletionStatus(),
                Children = new ObservableCollection<Node>(),
                Parent = null,
                Size = 0
            };
        }

        public void Enqueue(Action<DriveNode> addDriveFunc, Stack<Node> queuedNodes)
        {
            addDriveFunc(this);
            queuedNodes.Push(this);
        }
    }
}