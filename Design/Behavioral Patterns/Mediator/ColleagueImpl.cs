namespace Mediator
{
   public abstract class ColleagueImpl : IColleague
   {
      public IMediator Mediator { get; set; }

      protected ColleagueImpl(IMediator mediator)
      {
         Mediator = mediator;
      }
   }
}