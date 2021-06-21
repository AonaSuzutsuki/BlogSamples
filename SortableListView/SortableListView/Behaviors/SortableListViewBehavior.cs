using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Microsoft.Xaml.Behaviors;

namespace SortableListView.Behaviors
{
    public class HeaderSortEventArgs : EventArgs
    {
        public string HeaderName { get; }
        public Action<string> SetContentSuffixAction { get; }

        public HeaderSortEventArgs(string headerName, Action<string> setContentSuffixAction)
        {
            HeaderName = headerName;
            SetContentSuffixAction = setContentSuffixAction;
        }
    }

    public class SortableListViewBehavior : Behavior<ListView>
    {

        #region Fields

        private readonly Dictionary<GridViewColumnHeader, string> _headers =
            new Dictionary<GridViewColumnHeader, string>();

        #endregion

        protected override void OnAttached()
        {
            base.OnAttached();

            AssociatedObject.Loaded += AssociatedObjectOnLoaded;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();

            if (!(AssociatedObject.View is GridView gridView))
                return;

            AssociatedObject.Loaded -= AssociatedObjectOnLoaded;
            foreach (var gridViewColumn in gridView.Columns)
            {
                var header = (GridViewColumnHeader) gridViewColumn.Header;
                header.Click -= HeaderOnClick;
            }
        }

        private void HeaderOnClick(object sender, RoutedEventArgs e)
        {
            if (!(e.OriginalSource is GridViewColumnHeader header))
                return;

            foreach (var h in _headers)
            {
                h.Key.Content = h.Value;
            }

            var content = header.Content.ToString();

            GridViewColumnHeaderSort(AssociatedObject, new HeaderSortEventArgs(header.Tag.ToString(),
                arg => header.Content = $"{content} {arg}"));
        }

        private void AssociatedObjectOnLoaded(object sender, RoutedEventArgs e)
        {
            if (!(AssociatedObject.View is GridView gridView))
                return;

            foreach (var gridViewColumn in gridView.Columns)
            {
                if (!(gridViewColumn.Header is GridViewColumnHeader header) || _headers.ContainsKey(header))
                    continue;

                _headers.Add(header, header.Content.ToString());
                header.Click += (_, args) => HeaderOnClick(AssociatedObject, args);
            }

            var firstHeader = _headers.Keys.First();
            var content = firstHeader.Content.ToString();

            GridViewColumnHeaderSort(AssociatedObject, new HeaderSortEventArgs("Id",
                arg => firstHeader.Content = $"{content} {arg}"));
        }


        public void GridViewColumnHeaderSort(ListView listView, HeaderSortEventArgs eventArgs)
        {
            var header = eventArgs.HeaderName;
            var collectionView = CollectionViewSource.GetDefaultView(listView.ItemsSource);

            var sortDescription = collectionView.SortDescriptions.FirstOrDefault();
            collectionView.SortDescriptions.Clear();
            if (sortDescription.PropertyName == header)
            {
                if (sortDescription.Direction == ListSortDirection.Ascending)
                {
                    collectionView.SortDescriptions.Add(new SortDescription(header, ListSortDirection.Descending));
                    eventArgs.SetContentSuffixAction?.Invoke("↓");
                }
                else
                {
                    collectionView.SortDescriptions.Add(new SortDescription(header, ListSortDirection.Ascending));
                    eventArgs.SetContentSuffixAction?.Invoke("↑");
                }
            }
            else
            {
                collectionView.SortDescriptions.Add(new SortDescription(header, ListSortDirection.Ascending));
                eventArgs.SetContentSuffixAction?.Invoke("↑");
            }
        }
    }
}
