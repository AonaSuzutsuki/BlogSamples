using SortableComparerListView.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortableComparerListView.Views.Comparer
{
    public class FileSizeAscComparer : IComparer
    {
        public int Compare(object? x, object? y)
        {
            if (x is not FileListInfo logFileItemX || y is not FileListInfo logFileItemY)
                return 0;

            return logFileItemX.FileSize.CompareTo(logFileItemY.FileSize);
        }
    }
}
