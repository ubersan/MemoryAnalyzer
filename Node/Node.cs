using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;

namespace MemoryAnalyzer
{
    public class Node
    {
        public IEnumerable<DirectoryInfo> GetDirectories()
        {
            try
            {
                return FileInfo.GetDirectories();
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("Catched exception on folder {0}", FileInfo.FullName);
                return new DirectoryInfo[] {};
            }
        }

        public IEnumerable<FileInfo> GetFiles()
        {
            try
            {
                return FileInfo.GetFiles();
            }
            catch (Exception)
            {
                Console.WriteLine("Catched exception on folder {0}", FileInfo.FullName);
                return new FileInfo[] {};
            }
        }

        public string Name { get; set; }
        public DirectoryInfo FileInfo { get; protected set; }
        public ObservableCollection<Node> Children { get; set; }
        public CompletionStatus CompletionState { get; set; }
        public Node Parent { get; protected set; }
        // TODO: Size not updating after treeitem opening. Probably missing a PropertyChanged
        public long Size { get; set; }
    }
}