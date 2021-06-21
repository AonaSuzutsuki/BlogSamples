using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace SortableListView.ViewModels
{
    public class MainWindowViewModel
    {
        public ICollection<ListViewValue> Accounts { get; set; }

        public MainWindowViewModel()
        {
            var random = new Random(Environment.TickCount);
            Accounts = new ObservableCollection<ListViewValue>
            {
                new ListViewValue { Id = 1, Value1 = "e", Value2 = random.Next().ToString() },
                new ListViewValue { Id = 2, Value1 = "d", Value2 = "" },
                new ListViewValue { Id = 3, Value1 = "i", Value2 = random.Next().ToString() },
                new ListViewValue { Id = 4, Value1 = "a", Value2 = random.Next().ToString() },
                new ListViewValue { Id = 5, Value1 = "h", Value2 = random.Next().ToString() },
                new ListViewValue { Id = 6, Value1 = "c", Value2 = random.Next().ToString() },
                new ListViewValue { Id = 7, Value1 = "b", Value2 = random.Next().ToString() },
                new ListViewValue { Id = 10, Value1 = "f", Value2 = random.Next().ToString() },
                new ListViewValue { Id = 15, Value1 = "j", Value2 = "" },
                new ListViewValue { Id = 101, Value1 = "g", Value2 = random.Next().ToString() }
            };
        }
    }
}
