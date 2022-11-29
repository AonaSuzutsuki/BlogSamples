using System.Collections;
using SortableComparerListView.ViewModels;

namespace SortableComparerListView.Comparer
{
    public class FileSizeDescComparer : IComparer
    {
        public int Compare(object? x, object? y)
        {
            if (x is not FileListInfo logFileItemX || y is not FileListInfo logFileItemY)
                return 0;

            return logFileItemX.FileSize.CompareTo(logFileItemY.FileSize) * -1;
        }
    }
}
