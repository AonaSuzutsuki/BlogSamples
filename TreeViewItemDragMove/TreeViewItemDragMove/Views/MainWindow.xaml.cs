using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TreeViewItemDragMove.Extensions;
using TreeViewItemDragMove.ViewModels;

namespace TreeViewItemDragMove
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DataContext = new MainWindowViewModels();

            SampleTreeView.AllowDrop = true;
            SampleTreeView.PreviewMouseLeftButtonDown += SampleTreeViewOnPreviewMouseLeftButtonDown;
            SampleTreeView.PreviewMouseLeftButtonUp += SampleTreeViewOnPreviewMouseLeftButtonUp;
            SampleTreeView.PreviewMouseMove += SampleTreeViewOnPreviewMouseMove;
            SampleTreeView.Drop += SampleTreeViewOnDrop;
            SampleTreeView.DragOver += SampleTreeViewOnDragOver;
        }

        public enum InsertType
        {
            After,
            Before,
            Children
        }

        private readonly HashSet<TreeViewItemInfo> _changedBlocks = new HashSet<TreeViewItemInfo>();
        private InsertType _insertType;
        private Point? _startPos;

        private void SampleTreeViewOnDragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(TreeViewItemInfo)))
            {
                e.Effects = DragDropEffects.Move;

                if (!(sender is ItemsControl itemsControl))
                    return;

                var scrollViewer = itemsControl.Descendants<ScrollViewer>().FirstOrDefault();
                DragScroll(scrollViewer, itemsControl, e);

                var sourceItem = (TreeViewItemInfo)e.Data.GetData(typeof(TreeViewItemInfo));
                var targetElement = HitTest<FrameworkElement>(itemsControl, e.GetPosition);

                var parentGrid = targetElement?.GetParent<Grid>();
                if (parentGrid == null || !(targetElement.DataContext is TreeViewItemInfo targetElementInfo))
                    return;

                ResetSeparator(_changedBlocks);

                var targetParent = targetElementInfo.Parent;
                if (targetElementInfo == sourceItem || targetParent == null)
                    return;

                const int boundary = 10;
                var pos = e.GetPosition(parentGrid);
                if (pos.Y > 0 && pos.Y < boundary)
                {
                    _insertType = InsertType.Before;
                    targetElementInfo.BeforeSeparatorVisibility = Visibility.Visible;
                }
                else if (targetParent.Children.Last() == targetElementInfo
                         && pos.Y < parentGrid.ActualHeight && pos.Y > parentGrid.ActualHeight - boundary)
                {
                    _insertType = InsertType.After;
                    targetElementInfo.AfterSeparatorVisibility = Visibility.Visible;
                }
                else
                {
                    _insertType = InsertType.Children;
                    targetElementInfo.Background = Brushes.Black;
                }

                if (!_changedBlocks.Contains(targetElementInfo))
                    _changedBlocks.Add(targetElementInfo);
            }
        }

        private void SampleTreeViewOnDrop(object sender, DragEventArgs e)
        {
            ResetSeparator(_changedBlocks);

            if (!(sender is ItemsControl itemsControl))
                return;

            var sourceItem = (TreeViewItemInfo)e.Data.GetData(typeof(TreeViewItemInfo));
            var targetItem = HitTest<FrameworkElement>(itemsControl, e.GetPosition)?.DataContext as TreeViewItemInfo;

            if (targetItem == null || sourceItem == null || sourceItem == targetItem)
                return;

            var targetItemParent = targetItem.Parent;
            var sourceItemParent = sourceItem.Parent;
            if (_insertType == InsertType.Before)
            {
                RemoveCurrentItem(sourceItemParent, sourceItem);

                targetItemParent.InsertBeforeChildren(sourceItem, targetItem);
                sourceItem.Parent = targetItemParent;
            }
            else if (_insertType == InsertType.After)
            {
                RemoveCurrentItem(sourceItemParent, sourceItem);

                targetItemParent.InsertAfterChildren(sourceItem, targetItem);
                sourceItem.Parent = targetItemParent;
            }
            else
            {
                RemoveCurrentItem(sourceItemParent, sourceItem);

                targetItem.AddChildren(sourceItem);
                targetItem.IsExpanded = true;
                sourceItem.Parent = targetItem;
            }
        }

        private static void RemoveCurrentItem(TreeViewItemInfo sourceItemParent, TreeViewItemInfo sourceItem)
        {
            sourceItemParent.RemoveChildren(sourceItem);
        }

        private void SampleTreeViewOnPreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (!(sender is TreeView lb) || lb.SelectedItem == null)
                return;
            if (_startPos == null)
                return;

            var curPos = lb.PointToScreen(e.GetPosition(lb));
            var diff = curPos - (Point)_startPos;
            if (!IsDragStartable(diff))
                return;

            DragDrop.DoDragDrop(lb, lb.SelectedItem, DragDropEffects.Move);

            _startPos = null;
        }

        private void SampleTreeViewOnPreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _startPos = null;
        }

        private void SampleTreeViewOnPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!(sender is ItemsControl itemsControl))
                return;

            var pos = e.GetPosition(itemsControl);
            var hit = HitTest<FrameworkElement>(itemsControl, e.GetPosition);
            if (hit.DataContext is TreeViewItemInfo)
                _startPos = itemsControl.PointToScreen(pos);
            else
                _startPos = null;
        }

        private static void ResetSeparator(IEnumerable<TreeViewItemInfo> enumerable)
        {
            foreach (var pair in enumerable)
            {
                ResetSeparator(pair);
            }
        }

        private static void ResetSeparator(TreeViewItemInfo info)
        {
            info.Background = Brushes.Transparent;
            info.BeforeSeparatorVisibility = Visibility.Hidden;
            info.AfterSeparatorVisibility = Visibility.Hidden;
        }

        private static void DragScroll(ScrollViewer scrollViewer, ItemsControl itemsControl, DragEventArgs e)
        {
            const double tolerance = 10d;
            const double offset = 3d;
            var verticalPos = e.GetPosition(itemsControl).Y;
            if (verticalPos < tolerance) // Top of visible list?
                scrollViewer?.ScrollToVerticalOffset(scrollViewer.VerticalOffset - offset); //Scroll up.
            else if (verticalPos > itemsControl.ActualHeight - tolerance) //Bottom of visible list?
                scrollViewer?.ScrollToVerticalOffset(scrollViewer.VerticalOffset + offset); //Scroll down.
        }

        private static T HitTest<T>(UIElement itemsControl, Func<IInputElement, Point> getPosition) where T : class
        {
            var pt = getPosition(itemsControl);
            var result = VisualTreeHelper.HitTest(itemsControl, pt);
            if (result.VisualHit is T ret)
                return ret;
            return null;
        }

        private static bool IsDragStartable(Vector delta)
        {
            return (SystemParameters.MinimumHorizontalDragDistance < Math.Abs(delta.X)) ||
                   (SystemParameters.MinimumVerticalDragDistance < Math.Abs(delta.Y));
        }
    }
}
