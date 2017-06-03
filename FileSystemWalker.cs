using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading;

namespace MemoryAnalyzer
{
    public class FileSystemWalker
    {
        private readonly Thread _walkerThread;

        private readonly Action<Node, Node> _addNodeFunc;
        private readonly Stack<Node> _queuedNodes;
        private readonly Action<DriveNode> _addDriveFunc;

        public FileSystemWalker(ObservableCollection<Node> root, SynchronizationContext uiContext)
        {
            var uiContext1 = uiContext;
            _walkerThread = new Thread(WalkMemory);

            _addNodeFunc = (parent, child) => uiContext1.Send(state => parent.Children.Add(child), null);
            _addDriveFunc = (drive) => uiContext1.Send(state => root.Add(drive), null);

            _queuedNodes = new Stack<Node>();
        }

        private void WalkMemory()
        {
            Directory.GetLogicalDrives()
                .Select(drive => new DirectoryInfo(drive))
                .Where(directoryInfo => directoryInfo.Exists)
                .Select(DriveNode.Create)
                .ForEach(driveNode => driveNode.Enqueue(_addDriveFunc, _queuedNodes));

            while (_queuedNodes.Count > 0)
            {
                var node = _queuedNodes.Pop();
                node.CompletionState.SetIsRunning();

                if (node.IsDirectory())
                {
                    var hasDirectories = node.GetDirectories()
                        .Select(directoryInfo => DirectoryNode.Create(directoryInfo, node))
                        .ForEach(directoryNode => directoryNode.Enqueue(node, _addNodeFunc, _queuedNodes));

                    var hasFiles = node.GetFiles()
                        .Select(fileInfo => FileNode.Create(fileInfo, node))
                        .ForEach(fileNode => fileNode.Enqueue(node, _addNodeFunc, _queuedNodes));

                    if (!hasFiles && !hasDirectories)
                    {
                        node.CompletionState.SetIsCompleted();
                        node.NotifyParentAboutCompletion();
                    }
                }
                else
                {
                    node.CompletionState.SetIsCompleted();
                    node.NotifyParentAboutCompletion();
                }

                node.CompletionState.SetTotalActiveChildCounter();
            }
        }

        public void Start()
        {
            _walkerThread.Start();
        }

        public void Stop()
        {
            _walkerThread.Abort();
            if (!_walkerThread.Join(TimeSpan.FromSeconds(5)))
            {
                throw new ThreadStateException("Aborting Walkerthread timed out");
            }
        }
    }
}