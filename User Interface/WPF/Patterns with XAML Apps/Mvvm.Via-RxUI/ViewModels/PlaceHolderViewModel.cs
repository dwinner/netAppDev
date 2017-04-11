using System.Reactive;
using ReactiveUI;

namespace FirstsStepsRUI.ViewModels
{
   public class PlaceHolderViewModel : ReactiveObject, IRoutableViewModel
   {
      public PlaceHolderViewModel(IScreen screen)
      {
         HostScreen = screen;
         ChangeView = ReactiveCommand.CreateAsyncObservable(_ => HostScreen.Router.NavigateBack.ExecuteAsync());
      }

      public ReactiveCommand<Unit> ChangeView { get; private set; }
      public IScreen HostScreen { get; private set; }

      public string UrlPathSegment
      {
         get { return "Placeholder"; }
      }
   }
}