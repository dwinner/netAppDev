namespace CardShuffle
{
   internal class Card
   {
      public CardSuit Suit { private get; set; }
      public int Value { private get; set; }

      public int Ordinal
      {
         get
         {
            //return a value from 0 - 51
            return ((int) Suit*13) + (Value - 1);
         }
      }

      public override string ToString()
      {
         string str;
         switch (Value)
         {
            case 1:
               str = "A";
               break;
            case 11:
               str = "J";
               break;
            case 12:
               str = "Q";
               break;
            case 13:
               str = "K";
               break;
            default:
               str = Value.ToString();
               break;
         }
         switch (Suit)
         {
            case CardSuit.Clubs:
               str += "C";
               break;
            case CardSuit.Diamonds:
               str += "D";
               break;
            case CardSuit.Hearts:
               str += "H";
               break;
            case CardSuit.Spades:
               str += "S";
               break;
         }
         return str;
      }
   };
}