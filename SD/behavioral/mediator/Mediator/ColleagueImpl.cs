namespace Mediator
{
   public abstract class ColleagueImpl : IColleague
   {
      protected ColleagueImpl(IMediator mediator) => Mediator = mediator;

      public IMediator Mediator { get; set; }
   }
}