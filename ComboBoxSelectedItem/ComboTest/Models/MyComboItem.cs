using Prism.Mvvm;

namespace ComboTest.Models;

public class MyComboItem : BindableBase
{
    private bool _isSelected;

    public string Name { get; set; } = string.Empty;

    public bool IsSelected
    {
        get => _isSelected;
        set => SetProperty(ref _isSelected, value);
    }
}