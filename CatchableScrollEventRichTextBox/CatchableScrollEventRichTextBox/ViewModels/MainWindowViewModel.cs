using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Commands;

namespace CatchableScrollEventRichTextBox.ViewModels
{
    public class MainWindowViewModel
    {
        public ICommand ReachedScrollEndCommand { get; set; }

        public MainWindowViewModel()
        {
            ReachedScrollEndCommand = new DelegateCommand<string>(ReachedScrollEnd);
        }

        public void ReachedScrollEnd(string from)
        {
            Debug.WriteLine($"Reached the end of scroll. {{{from}}}");
        }
    }
}
