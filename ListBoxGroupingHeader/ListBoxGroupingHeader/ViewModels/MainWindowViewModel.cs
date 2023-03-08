using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Commands;

namespace ListBoxGroupingHeader.ViewModels
{
    public class MainWindowViewModel
    {
        public ObservableCollection<GroupingListBoxItem> GroupingItems { get; set; }

        public ICommand SelectionChangedCommand { get; set; }

        public MainWindowViewModel()
        {
            GroupingItems = new()
            {
                new GroupingListBoxItem
                {
                    CategoryName = "カテゴリ1",
                    IsHeader = true
                },
                new GroupingListBoxItem
                {
                    Name = "アイテム1"
                },
                new GroupingListBoxItem
                {
                    Name = "アイテム2"
                },
                new GroupingListBoxItem
                {
                    CategoryName = "カテゴリ2",
                    IsHeader = true
                },
                new GroupingListBoxItem
                {
                    Name = "アイテム3"
                },
                new GroupingListBoxItem
                {
                    CategoryName = "カテゴリ3",
                    IsHeader = true
                },
                new GroupingListBoxItem
                {
                    Name = "アイテム4"
                },
                new GroupingListBoxItem
                {
                    Name = "アイテム5"
                }
            };

            SelectionChangedCommand = new DelegateCommand<GroupingListBoxItem?>(SelectionChanged);
        }

        public void SelectionChanged(GroupingListBoxItem? item)
        {
            if (item == null)
                return;

            Debug.WriteLine(item.IsHeader ? item.CategoryName : item.Name);
        }
    }
}
