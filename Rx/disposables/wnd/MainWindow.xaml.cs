using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Disposables;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using DisposableCreate.Annotations;

namespace DisposableCreate;

public partial class MainWindow : INotifyPropertyChanged
{
   private bool _isBusy;
   private IEnumerable<string> _newsItems;

   public MainWindow()
   {
      InitializeComponent();
      DataContext = this;
   }

   public bool IsBusy
   {
      get => _isBusy;
      set
      {
         if (value == _isBusy)
         {
            return;
         }

         _isBusy = value;
         OnPropertyChanged();
      }
   }

   public IEnumerable<string> NewsItems
   {
      get => _newsItems;
      set
      {
         if (Equals(value, _newsItems))
         {
            return;
         }

         _newsItems = value;
         OnPropertyChanged();
      }
   }

   public event PropertyChangedEventHandler PropertyChanged;

   private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
   {
      RefreshNewsAsync();
   }

   private async void RefreshNewsAsync()
   {
      IsBusy = true;
      NewsItems = Enumerable.Empty<string>();
      using (Disposable.Create(() => IsBusy = false))
      {
         NewsItems = await DownloadNewsItemsAsync().ConfigureAwait(false);
      }
   }

   private async void RefreshNews2Async()
   {
      NewsItems = Enumerable.Empty<string>();
      using (StartBusy())
      {
         NewsItems = await DownloadNewsItemsAsync().ConfigureAwait(false);
      }
   }

   public IDisposable StartBusy()
   {
      IsBusy = true;
      return new ContextDisposable(
         SynchronizationContext.Current,
         Disposable.Create(() => IsBusy = false));
   }

   private async Task<IEnumerable<string>> DownloadNewsItemsAsync()
   {
      await Task.Delay(2000).ConfigureAwait(false);
      return Enumerable.Range(1, 10).Select(i => "News Items " + i);
   }

   [NotifyPropertyChangedInvocator]
   protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
   {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
   }
}