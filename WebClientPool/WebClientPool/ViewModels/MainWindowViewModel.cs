using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Prism.Commands;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using WebClientPool.Models;

namespace WebClientPool.ViewModels
{
    public class MainWindowViewModel
    {

        #region Fields

        private readonly MainWindowModel _model;

        #endregion

        #region Properties

        public ReactiveProperty<string> UrlText { get; set; }
        public ReactiveProperty<string> LogText { get; set; }

        #endregion

        #region Event Properties

        public ICommand ClosedCommand { get; set; }
        public ICommand DownloadCommand { get; set; }

        #endregion

        public MainWindowViewModel(IWindowService service, MainWindowModel model)
        {
            UrlText = ReactiveProperty.FromObject(_model, m => m.UrlText);
            LogText = _model.ObserveProperty(m => m.LogText).ToReactiveProperty();
            _model = model;

            DownloadCommand = new DelegateCommand(StartDownload);
            ClosedCommand = new DelegateCommand(Close);
        }

        public void StartDownload()
        {
            _ = _model.Download();
        }

        public void Close()
        {
            _model?.Dispose();
        }
    }
}
