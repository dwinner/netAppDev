namespace FirstsStepsRUI.Models
{
   public class Menu
   {
      public Menu(MenuOption option)
      {
         Option = option;
      }

      public MenuOption Option { get; private set; }

      public override string ToString()
      {
         string name;
         switch (Option)
         {
            default:
               name = Option.ToString();
               break;
         }

         return name;
      }
   }
}