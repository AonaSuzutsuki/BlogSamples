using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace TreeViewItemDragMove.ViewModels
{
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
