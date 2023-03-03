using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using ComboTest.Models;
using Prism.Commands;
using Prism.Mvvm;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;

namespace ComboTest.ViewModels;

public class MainWindowViewModel : BindableBase
{
    private readonly MainWindowModel _model;

    public ReadOnlyObservableCollection<MyComboItem> InvalidComboItems { get; set; }
    public ReadOnlyObservableCollection<MyComboItem> ValidComboItems { get; set; }
    public ReadOnlyObservableCollection<MyComboItem> ValidOneWayComboItems { get; set; }
    
    public ReactiveProperty<MyComboItem?> ValidSelectedItem { get; set; }
    public ReactiveProperty<MyComboItem?> ValidOneWaySelectedItem { get; set; }

    public ICommand ChangeCommand { get; set; }

    public MainWindowViewModel(MainWindowModel model)
    {
        _model = model;

        ValidComboItems = model.Items.ToReadOnlyReactiveCollection();
        ValidComboItems.ObserveAddChanged().Subscribe(x =>
        {
            if (x.IsSelected)
                _model.SelectValidItem();
        });
        ValidOneWayComboItems = model.Items.ToReadOnlyReactiveCollection();
        InvalidComboItems = model.Items.ToReadOnlyReactiveCollection();
        ValidSelectedItem = model.ToReactivePropertyAsSynchronized(m => m.ValidSelectedItem);
        ValidOneWaySelectedItem = model.ObserveProperty(m => m.ValidOneWaySelectedItem).ToReactiveProperty();


        ChangeCommand = new DelegateCommand(Change);
    }

    public void Change()
    {
        _model.Change();
    }
}