using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using SortableComparerListView.ViewModels;

namespace SortableComparerListView.Views.Behaviors
{
    public class SortableFileSizeListViewBehavior : SortableListViewBehavior
    {
        private class FileSizeDescComparer : IComparer
        {
            public int Compare(object? x, object? y)
            {
                if (x is not FileListInfo logFileItemX || y is not FileListInfo logFileItemY)
                    return 0;

                return logFileItemX.FileSize.CompareTo(logFileItemY.FileSize);
            }
        }

        private class FileSizeAscComparer : IComparer
        {
            public int Compare(object? x, object? y)
            {
                if (x is not FileListInfo logFileItemX || y is not FileListInfo logFileItemY)
                    return 0;

                var valueX = logFileItemX.FileSize;
                var valueY = logFileItemY.FileSize;

                if (valueX > valueY) return -1;
                if (valueX < valueY) return 1;
                return 0;
            }
        }

        private readonly IComparer _descFileSizeComparer = new FileSizeDescComparer();
        private readonly IComparer _ascFileSizeComparer = new FileSizeAscComparer();

        protected override void GridViewColumnHeaderSort(ListView listView, OrderType order, string headerName, Action<string> setContentSuffixAction)
        {
            if (headerName == "FileSizeText")
            {
                var collectionView = (ListCollectionView)CollectionViewSource.GetDefaultView(listView.ItemsSource);

                collectionView.SortDescriptions.Clear();

                if (order == OrderType.Asc)
                {
                    collectionView.CustomSort = _ascFileSizeComparer;
                    setContentSuffixAction?.Invoke("↑");
                }
                else
                {
                    collectionView.CustomSort = _descFileSizeComparer;
                    setContentSuffixAction?.Invoke("↓");
                }
                collectionView.Refresh();
            }
            else
            {
                base.GridViewColumnHeaderSort(listView, order, headerName, setContentSuffixAction);
            }
        }

        protected override void GridViewColumnHeaderSort(ListView listView, string headerName, Action<string> setContentSuffixAction)
        {
            if (headerName == "FileSizeText")
            {
                var collectionView = (ListCollectionView)CollectionViewSource.GetDefaultView(listView.ItemsSource);

                collectionView.SortDescriptions.Clear();

                if (collectionView.CustomSort is FileSizeAscComparer)
                {
                    collectionView.CustomSort = _descFileSizeComparer;
                    setContentSuffixAction?.Invoke("↓");
                }
                else
                {
                    collectionView.CustomSort = _ascFileSizeComparer;
                    setContentSuffixAction?.Invoke("↑");
                }
                collectionView.Refresh();
            }
            else
            {
                base.GridViewColumnHeaderSort(listView, headerName, setContentSuffixAction);
            }
        }
    }
}
