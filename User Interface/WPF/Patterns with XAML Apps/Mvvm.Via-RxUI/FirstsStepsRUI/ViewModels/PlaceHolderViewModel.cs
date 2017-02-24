using ReactiveUI;
using System.Reactive;

namespace FirstsStepsRUI.ViewModels
{
   public class PlaceHolderViewModel : ReactiveObject, IRoutableViewModel
   {
      public IScreen HostScreen { get; private set; }

      public string UrlPathSegment
      {
         get { return "Placeholder"; }
      }

      public ReactiveCommand<Unit> ChangeView { get; private set; }

      public PlaceHolderViewModel(IScreen screen)
      {
         HostScreen = screen;
         ChangeView = ReactiveCommand.CreateAsyncObservable(_ => HostScreen.Router.NavigateBack.ExecuteAsync());
      }
   }
}
