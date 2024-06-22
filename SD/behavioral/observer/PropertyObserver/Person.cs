namespace PropertyObserver;

internal class Person : PropertyNotificationSupport
{
   private readonly Func<bool> _canVote;
   private int _age;

   public Person()
   {
      _canVote = Property(nameof(CanVote), () => Age >= 16);
   }

   public int Age
   {
      get => _age;
      set
      {
         if (value == _age)
         {
            return;
         }

         _age = value;
         OnPropertyChanged();
      }
   }

   public bool CanVote => _canVote();
}