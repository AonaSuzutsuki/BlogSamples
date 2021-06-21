using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media;
using Prism.Mvvm;

namespace TreeViewItemDragMoveBehavior.ViewModels
{
    public class TreeViewItemInfoBase : BindableBase
    {
        private string _name;
        private Brush _background = Brushes.Transparent;
        private bool _isExpanded;
        private bool _isSelected;
        private Visibility _beforeSeparatorVisibility = Visibility.Hidden;
        private Visibility _afterSeparatorVisibility = Visibility.Hidden;

        public TreeViewItemInfoBase Parent { get; set; }

        public ObservableCollection<TreeViewItemInfoBase> Children { get; set; } = new ObservableCollection<TreeViewItemInfoBase>();

        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        public Brush Background
        {
            get => _background;
            set => SetProperty(ref _background, value);
        }

        public bool IsExpanded
        {
            get => _isExpanded;
            set => SetProperty(ref _isExpanded, value);
        }

        public bool IsSelected
        {
            get => _isSelected;
            set => SetProperty(ref _isSelected, value);
        }

        public Visibility BeforeSeparatorVisibility
        {
            get => _beforeSeparatorVisibility;
            set => SetProperty(ref _beforeSeparatorVisibility, value);
        }

        public Visibility AfterSeparatorVisibility
        {
            get => _afterSeparatorVisibility;
            set => SetProperty(ref _afterSeparatorVisibility, value);
        }

        public void SetParentToChildren(TreeViewItemInfoBase parent = null)
        {
            Parent = parent;

            if (Children == null)
                return;
            foreach (var child in Children)
            {
                child.SetParentToChildren(this);
            }
        }

        public void InsertBeforeChildren(TreeViewItemInfoBase from, TreeViewItemInfoBase to)
        {
            var index = Children.IndexOf(to);
            if (index < 0)
                return;

            Children.Insert(index, from);
        }

        public void InsertAfterChildren(TreeViewItemInfoBase from, TreeViewItemInfoBase to)
        {
            var index = Children.IndexOf(to);
            if (index < 0)
                return;

            Children.Insert(index + 1, from);
        }

        public void AddChildren(TreeViewItemInfoBase infoBase)
        {
            Children.Add(infoBase);
        }

        public void RemoveChildren(TreeViewItemInfoBase infoBase)
        {
            Children.Remove(infoBase);
        }

        public bool ContainsParent(TreeViewItemInfoBase infoBase)
        {
            if (Parent == null)
                return false;
            return Parent == infoBase || Parent.ContainsParent(infoBase);
        }
    }

    public class TreeViewItemInfo : TreeViewItemInfoBase
    {

    }
}