using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
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

        private readonly CompositeDisposable _disposable = new();
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
            _model = model;

            UrlText = new ReactiveProperty<string>();
            LogText = _model.ObserveProperty(m => m.LogText).ToReactiveProperty().AddTo(_disposable);
            _ = LogText.Subscribe(_ => service.ScrollToEndLog());

            DownloadCommand = new DelegateCommand(StartDownload);
            ClosedCommand = new DelegateCommand(Close);
        }

        public void StartDownload()
        {
            _ = _model.Download(UrlText.Value);
        }

        public void Close()
        {
            _disposable.Dispose();
        }
    }
}
