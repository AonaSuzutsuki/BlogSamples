using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortableComparerListView.ViewModels
{
    public class MainWindowViewModel
    {
        public ICollection<FileListInfo> FileList { get; set; }

        public MainWindowViewModel()
        {
            FileList = new ObservableCollection<FileListInfo>()
            {
                new()
                {
                    Name = "File3.dat",
                    FileSize = 20801456
                },
                new()
                {
                    Name = "File4.dat",
                    FileSize = 41235
                },
                new ()
                {
                    Name = "File6.dat",
                    FileSize = 15098214000
                },
                new()
                {
                    Name = "File2.dat",
                    FileSize = 644
                },
                new()
                {
                    Name = "File5.dat",
                    FileSize = 1125
                },
                new()
                {
                    Name = "File1.dat",
                    FileSize = 25456
                },
            };
        }
    }
}
