﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Media;
using Prism.Mvvm;

namespace TreeViewItemDragMove.ViewModels
{
    public class TreeViewItemInfo : BindableBase
    {
        private string _name;
        private Brush _background = Brushes.Transparent;
        private bool _isExpanded;
        private bool _isSelected;
        private Visibility _beforeSeparatorVisibility = Visibility.Hidden;
        private Visibility _afterSeparatorVisibility = Visibility.Hidden;

        public TreeViewItemInfo Parent { get; set; }

        public ObservableCollection<TreeViewItemInfo> Children { get; set; } = new ObservableCollection<TreeViewItemInfo>();

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

        public void SetParentToChildren(TreeViewItemInfo parent = null)
        {
            Parent = parent;

            if (Children == null)
                return;
            foreach (var child in Children)
            {
                child.SetParentToChildren(this);
            }
        }

        public void InsertBeforeChildren(TreeViewItemInfo from, TreeViewItemInfo to)
        {
            var index = Children.IndexOf(to);
            if (index < 0)
                return;

            Children.Insert(index, from);
        }

        public void InsertAfterChildren(TreeViewItemInfo from, TreeViewItemInfo to)
        {
            var index = Children.IndexOf(to);
            if (index < 0)
                return;

            Children.Insert(index + 1, from);
        }

        public void AddChildren(TreeViewItemInfo info)
        {
            Children.Add(info);
        }

        public void RemoveChildren(TreeViewItemInfo info)
        {
            Children.Remove(info);
        }

        public bool ContainsParent(TreeViewItemInfo info)
        {
            if (Parent == null)
                return false;
            return Parent == info || Parent.ContainsParent(info);
        }
    }
    public class MainWindowViewModels
    {
        public ObservableCollection<TreeViewItemInfo> SampleItems { get; set; }

        public MainWindowViewModels()
        {
            SampleItems = new ObservableCollection<TreeViewItemInfo>(new []
            {
                new TreeViewItemInfo { Name = "Item1"},
                new TreeViewItemInfo
                {
                    Name = "Item2",
                    Children = new ObservableCollection<TreeViewItemInfo>
                    {
                        new TreeViewItemInfo { Name = "SubItem1"},
                        new TreeViewItemInfo
                        {
                            Name = "SubItem2",
                            Children = new ObservableCollection<TreeViewItemInfo>
                            {
                                new TreeViewItemInfo { Name = "SubSubItem1" },
                                new TreeViewItemInfo { Name = "SubSubItem2" },
                            }
                        },
                        new TreeViewItemInfo { Name = "SubItem3"}
                    }
                },
                new TreeViewItemInfo { Name = "Item3"},
                new TreeViewItemInfo { Name = "Item4"},
                new TreeViewItemInfo
                {
                    Name = "Item5",
                    Children = new ObservableCollection<TreeViewItemInfo>
                    {
                        new TreeViewItemInfo { Name = "SubItem1"},
                        new TreeViewItemInfo { Name = "SubItem2"},
                        new TreeViewItemInfo { Name = "SubItem3"}
                    }
                }
            });

            var dummy = new TreeViewItemInfo { Children = SampleItems };
            foreach (var item in dummy.Children)
            {
                item.SetParentToChildren(dummy);
            }
        }
    }
}
