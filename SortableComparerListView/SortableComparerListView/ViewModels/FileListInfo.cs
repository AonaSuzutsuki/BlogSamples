using System;

namespace SortableComparerListView.ViewModels;

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