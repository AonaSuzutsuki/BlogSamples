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
            switch (args.Type)
            {
                case MoveableTreeViewBehavior.InsertType.Before:
                    Debug.WriteLine("Before");
                    break;
                case MoveableTreeViewBehavior.InsertType.After:
                    Debug.WriteLine("After");
                    break;
                case MoveableTreeViewBehavior.InsertType.Children:
                    Debug.WriteLine("Children");
                    break;
            }
        }
    }
}
