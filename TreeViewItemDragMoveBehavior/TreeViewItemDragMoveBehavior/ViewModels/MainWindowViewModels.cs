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
        public ObservableCollection<TreeViewItemInfoBase> SampleItems { get; set; }
        public ICommand DropCommand { get; set; }

        public MainWindowViewModels()
        {
            DropCommand = new DelegateCommand<DropArguments>(Drop);
            SampleItems = new ObservableCollection<TreeViewItemInfoBase>(new []
            {
                new TreeViewItemInfoBase { Name = "Item1"},
                new TreeViewItemInfoBase
                {
                    Name = "Item2",
                    Children = new ObservableCollection<TreeViewItemInfoBase>
                    {
                        new TreeViewItemInfoBase { Name = "SubItem1"},
                        new TreeViewItemInfoBase
                        {
                            Name = "SubItem2",
                            Children = new ObservableCollection<TreeViewItemInfoBase>
                            {
                                new TreeViewItemInfoBase { Name = "SubSubItem1" },
                                new TreeViewItemInfoBase { Name = "SubSubItem2" },
                            }
                        },
                        new TreeViewItemInfoBase { Name = "SubItem3"}
                    }
                },
                new TreeViewItemInfoBase { Name = "Item3"},
                new TreeViewItemInfoBase { Name = "Item4"},
                new TreeViewItemInfoBase
                {
                    Name = "Item5",
                    Children = new ObservableCollection<TreeViewItemInfoBase>
                    {
                        new TreeViewItemInfoBase { Name = "SubItem1"},
                        new TreeViewItemInfoBase { Name = "SubItem2"},
                        new TreeViewItemInfoBase { Name = "SubItem3"}
                    }
                }
            });

            var dummy = new TreeViewItemInfoBase { Children = SampleItems };
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
