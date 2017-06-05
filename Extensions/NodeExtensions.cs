﻿using System.IO;

namespace MemoryAnalyzer
{
    public static class NodeExenstions
    {
        public static bool IsDirectory(this Node node)
        {
            return node.FileInfo.Attributes.HasFlag(FileAttributes.Directory);
        }

        public static void NotifyParentAboutCompletion(this Node node)
        {
            node.CompletionState.SetHasCompleted();

            while (true)
            {
                var par = node.Parent;
                if (par == null)
                {
                    break;
                }

                par.CompletionState.DecreaseActiveChildCounter();
                par.AddToSize(node.Size);
                if (par.CompletionState.ActiveChildCounter == 0)
                {
                    par.CompletionState.SetHasCompleted();
                    node = par;
                    continue;
                }
                break;
            }
        }
    }
}