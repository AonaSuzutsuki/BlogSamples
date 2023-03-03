using System.Collections.ObjectModel;
using Prism.Mvvm;

namespace ComboTest.Models;

public class MainWindowModel : BindableBase
{
    private ObservableCollection<MyComboItem> _items;
    private MyComboItem? _validSelectedItem;
    private MyComboItem? _validOneWaySelectedItem;

    public ObservableCollection<MyComboItem> Items
    {
        get => _items;
        set => SetProperty(ref _items, value);
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
        _items.Clear();
        _items.Add(new MyComboItem { Name = "Item1" });
        _items.Add(new MyComboItem { Name = "Item2", IsSelected = true });
        _items.Add(new MyComboItem { Name = "Item3" });
        _items.Add(new MyComboItem { Name = "Item4" });
        _items.Add(new MyComboItem { Name = "Item5" });

        ValidSelectedItem = _items[1];
        ValidOneWaySelectedItem = _items[1];
    }

    public void Change()
    {
        _items.Clear();
        _items.Add(new MyComboItem { Name = "Item1" });
        _items.Add(new MyComboItem { Name = "Item2", IsSelected = true });
        _items.Add(new MyComboItem { Name = "Item3" });
        _items.Add(new MyComboItem { Name = "Item4" });
        _items.Add(new MyComboItem { Name = "Item5" });

        ValidSelectedItem = _items[1];
        ValidOneWaySelectedItem = _items[1];
    }

    public void SelectValidItem()
    {
        ValidSelectedItem = _items[1];
        ValidOneWaySelectedItem = _items[1];
    }
}