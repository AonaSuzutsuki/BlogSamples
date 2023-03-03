using System.Collections.ObjectModel;
using Prism.Mvvm;

namespace ComboTest.Models;

public class MainWindowModel : BindableBase
{
    private ObservableCollection<MyComboItem> _items;
    private MyComboItem? _invalidTwoWaySelectedItem;
    private MyComboItem? _validSelectedItem;
    private MyComboItem? _validOneWaySelectedItem;

    public ObservableCollection<MyComboItem> Items
    {
        get => _items;
        set => SetProperty(ref _items, value);
    }

    public MyComboItem? InvalidTwoWaySelectedItem
    {
        get => _invalidTwoWaySelectedItem;
        set => SetProperty(ref _invalidTwoWaySelectedItem, value);
    }

    public MyComboItem? ValidSelectedItem
    {
        get => _validSelectedItem;
        set => SetProperty(ref _validSelectedItem, value);
    }

    public MyComboItem? ValidOneWaySelectedItem
    {
        get => _validOneWaySelectedItem;
        set => SetProperty(ref _validOneWaySelectedItem, value);
    }

    public MainWindowModel()
    {
        _items = new ObservableCollection<MyComboItem>();
    }

    public void Init()
    {
        Items.Add(new MyComboItem { Name = "Item1" });
        Items.Add(new MyComboItem { Name = "Item2", IsSelected = true });
        Items.Add(new MyComboItem { Name = "Item3" });
        Items.Add(new MyComboItem { Name = "Item4" });
        Items.Add(new MyComboItem { Name = "Item5" });

        InvalidTwoWaySelectedItem = Items[1];
        ValidOneWaySelectedItem = Items[1];
    }

    public void Change()
    {
        Items.Clear();
        Init();
    }

    public void SelectValidItem()
    {
        ValidSelectedItem = Items[1];
    }
}