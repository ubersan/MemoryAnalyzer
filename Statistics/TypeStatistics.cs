using System.Collections.Generic;

namespace MemoryAnalyzer
{
    public class TypeStatistics<T> where T : Node
    {
        private readonly List<T> _nodes;
        private long _sizeSum;

        public TypeStatistics()
        {
            _nodes = new List<T>();
        }

        public void Add(T node)
        {
            if (node == null)
            {
                return;
            }

            _nodes.Add(node);

            UpdateBiggestNode(node);
            UpdateSmallestNode(node);
            UpdateAverageSize(node);
        }

        private void UpdateBiggestNode(T node)
        {
            if (BiggestNode == null || node.Size > BiggestNode.Size)
            {
                BiggestNode = node;
            }
        }

        private void UpdateSmallestNode(T node)
        {
            if (SmallestNode == null || node.Size < SmallestNode.Size)
            {
                SmallestNode = node;
            }
        }

        private void UpdateAverageSize(T node)
        {
            _sizeSum += node.Size;
            AverageSize = _sizeSum / _nodes.Count;
        }

        public T BiggestNode { get; private set; }
        public T SmallestNode { get; private set; }
        public long AverageSize { get; private set; }
    }
}