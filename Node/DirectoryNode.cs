using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;

namespace MemoryAnalyzer
{
    public class DirectoryNode : Node
    {
        public static DirectoryNode Create(DirectoryInfo directoryInfo, Node parent)
        {
            return new DirectoryNode
            {
                Name = directoryInfo.Name,
                FileInfo = directoryInfo,
                CompletionState = new CompletionStatus(),
                Children = new ObservableCollection<Node>(),
                Parent = parent,
                Size = 0
            };
        }

        public void Enqueue(Node parent, Action<Node, Node> addChildFunc, Stack<Node> queuedNodes)
        {
            parent.CompletionState.IncreaseActiveChildCounter();
            addChildFunc(parent, this);
            queuedNodes.Push(this);
        }
    }
}