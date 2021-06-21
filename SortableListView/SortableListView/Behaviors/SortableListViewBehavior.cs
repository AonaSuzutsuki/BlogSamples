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
    public class SortableListViewBehavior : Behavior<ListView>
    {

        #region Fields

        private readonly Dictionary<GridViewColumnHeader, string> _headers =
            new Dictionary<GridViewColumnHeader, string>();

        #endregion

        #region Dependency Properties

        public string FirstSort
        {
            get => (string)GetValue(FirstSortProperty);
            set => SetValue(FirstSortProperty, value);
        }

        public static readonly DependencyProperty FirstSortProperty = DependencyProperty.Register(
            "FirstSort",
            typeof(string),
            typeof(SortableListViewBehavior),
            new UIPropertyMetadata(null));

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

            GridViewColumnHeaderSort(AssociatedObject, header.Tag.ToString(),
                arg => header.Content = $"{content} {arg}");
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

            if (string.IsNullOrEmpty(FirstSort))
                return;

            var firstHeader = _headers.Keys.First();
            var content = firstHeader.Content.ToString();
            GridViewColumnHeaderSort(AssociatedObject, FirstSort,
                arg => firstHeader.Content = $"{content} {arg}");
        }


        private static void GridViewColumnHeaderSort(ListView listView, string headerName, Action<string> setContentSuffixAction)
        {
            var collectionView = CollectionViewSource.GetDefaultView(listView.ItemsSource);

            var sortDescription = collectionView.SortDescriptions.FirstOrDefault();
            collectionView.SortDescriptions.Clear();
            if (sortDescription.PropertyName == headerName)
            {
                if (sortDescription.Direction == ListSortDirection.Ascending)
                {
                    collectionView.SortDescriptions.Add(new SortDescription(headerName, ListSortDirection.Descending));
                    setContentSuffixAction?.Invoke("↓");
                }
                else
                {
                    collectionView.SortDescriptions.Add(new SortDescription(headerName, ListSortDirection.Ascending));
                    setContentSuffixAction?.Invoke("↑");
                }
            }
            else
            {
                collectionView.SortDescriptions.Add(new SortDescription(headerName, ListSortDirection.Ascending));
                setContentSuffixAction?.Invoke("↑");
            }
        }
    }
}
