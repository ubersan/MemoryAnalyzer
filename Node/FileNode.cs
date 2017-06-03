using System;
using System.Collections.Generic;
using System.IO;

namespace MemoryAnalyzer
{
    public class FileNode : Node
    {
        public static FileNode Create(FileInfo file, Node node)
        {
            return new FileNode
            {
                Name = file.Name,
                FileInfo = new DirectoryInfo(file.FullName),
                CompletionState = new CompletionStatus(),
                Children = null,
                Parent = node,
                Size = file.Length
            };
        }

        public void Enqueue(Node parent, Action<Node, Node> addChildFunc, Stack<Node> queuedNodes)
        {
            addChildFunc(parent, this);
            queuedNodes.Push(this);
            parent.CompletionState.IncreaseActiveChildCounter();
        }
    }
}