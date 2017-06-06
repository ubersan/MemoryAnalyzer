using System;
using System.IO;

namespace MemoryAnalyzer
{
    public static class NodeExenstions
    {
        public static bool IsDirectory(this Node node)
        {
            return node.FileInfo.Attributes.HasFlag(FileAttributes.Directory);
        }

        // TODO: Refactoring. doesn't need to be an extension method anymore (probably)
        public static void NotifyParentAboutCompletion(this Node node)
        {
            node.CompletionState.SetHasCompleted();

            // TODO: Refactoring needed? -> go up twice in worst case
            // Go up the tree and feed the node statistics to the parents
            // in the upper loop, the 'node' var is updated, but it should
            // stay the same
            // TODO: This seems to be a big performance issue!
            // Possible solution: Only update parent and have a simple merge.
            var parent = node.Parent;
            while (parent != null)
            {
                parent.AddNodeToStatistics(node);
                parent = parent.Parent;
            }

            while (true)
            {
                parent = node.Parent;
                if (parent == null)
                {
                    break;
                }

                parent.CompletionState.DecreaseActiveChildCounter();
                parent.AddToSize(node.Size);
                if (parent.CompletionState.ActiveChildCounter == 0)
                {
                    parent.CompletionState.SetHasCompleted();
                    node = parent;
                    continue;
                }
                break;
            }
        }
    }
}