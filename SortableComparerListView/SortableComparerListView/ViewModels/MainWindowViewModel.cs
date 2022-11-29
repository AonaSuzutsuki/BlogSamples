using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortableComparerListView.ViewModels
{
    public class FileListInfo
    {
        public string Name { get; set; } = string.Empty;
        public long FileSize { get; set; } = 0L;

        public string FileSizeText
        {
            get
            {
                if (FileSize < 1024)
                    return $"{FileSize} Bytes";
                else if (FileSize < 1024 * 1024)
                    return $"{Math.Floor((double)FileSize / 1024)} KB";
                else if (FileSize < 1024 * 1024 * 1024)
                    return $"{Math.Floor((double)FileSize / (1024 * 1024))} MB";
                else
                    return $"{Math.Floor((double)FileSize / (1024 * 1024 * 1024))} GB";
            }
        }
    }

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
