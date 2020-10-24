using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Windows.Input;
using Prism.Commands;
using TreeViewItemDragMoveBehavior.Views;

namespace TreeViewItemDragMoveBehavior.ViewModels
{
    public class MainWindowViewModels
    {
        public ObservableCollection<TreeViewItemInfo> SampleItems { get; set; }
        public ICommand DropCommand { get; set; }

        public MainWindowViewModels()
        {
            DropCommand = new DelegateCommand<DropArguments>(Drop);
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

        public void Drop(DropArguments args)
        {
            var targetItem = args.Target;
            var sourceItem = args.Source;
            var targetItemParent = targetItem.Parent;
            switch (args.Type)
            {
                case MoveableTreeViewBehavior.InsertType.Before:
                    targetItemParent.InsertBeforeChildren(sourceItem, targetItem);
                    sourceItem.Parent = targetItemParent;
                    sourceItem.IsSelected = true;
                    break;
                case MoveableTreeViewBehavior.InsertType.After:
                    targetItemParent.InsertAfterChildren(sourceItem, targetItem);
                    sourceItem.Parent = targetItemParent;
                    sourceItem.IsSelected = true;
                    break;
                case MoveableTreeViewBehavior.InsertType.Children:
                    targetItem.AddChildren(sourceItem);
                    targetItem.IsExpanded = true;
                    sourceItem.IsSelected = true;
                    sourceItem.Parent = targetItem;
                    break;
                default:
                    break;
            }
        }
    }
}
