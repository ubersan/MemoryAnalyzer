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
            while (true)
            {
                var par = node.Parent;
                if (par == null)
                {// size into xaml as text block
                    break;
                }
                par.CompletionState.DecreaseActiveChildCounter();
                par.Size += node.Size;
                if (par.CompletionState.ActiveChildCounter == 0)
                {
                    par.CompletionState.SetIsCompleted();
                    node = par;
                    continue;
                }
                break;
            }
        }
    }
}