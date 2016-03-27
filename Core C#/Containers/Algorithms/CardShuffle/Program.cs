using System;

namespace CardShuffle
{
   internal static class Program
   {
      private const int NumCards = 52;

      private static readonly Random Rand = new Random();

      private static void Main(string[] args)
      {
         var numTests = 100000000;
         if (args.Length > 0)
         {
            if (!int.TryParse(args[0], out numTests))
            {
               Console.WriteLine("Usage: CardShuffle [numTests]\nnumTests is optional. Default value is 100,000,000");
               return;
            }
         }
         CheckShuffleAlgorithm(numTests);
      }

      private static Card[] CreateNewDeck()
      {
         var cards = new Card[NumCards];
         var index = 0;
         for (var s = CardSuit.Clubs; s <= CardSuit.Spades; s++)
         {
            for (var value = 1; value <= 13; ++value)
            {
               cards[index++] = new Card {Suit = s, Value = value};
            }
         }
         return cards;
      }

      private static void PrintDeck(Card[] cards)
      {
         for (var i = 0; i < cards.Length; ++i)
         {
            if (i > 0)
            {
               Console.Write(",");
            }
            Console.Write(cards[i].ToString());
         }
      }

      private static void ShuffleDeck(Card[] cards)
      {
         var n = cards.Length;

         while (n > 1)
         {
            var k = Rand.Next(n);
            --n;
            var temp = cards[n];
            cards[n] = cards[k];
            cards[k] = temp;
         }
      }

      private static void BadShuffle(Card[] cards)
      {
         for (var i = cards.Length - 1; i > 0; i--)
         {
            var n = Rand.Next(i + 1);
            var temp = cards[i];
            cards[i] = cards[n];
            cards[n] = temp;
         }
      }

      private static void CheckShuffleAlgorithm(int numIterations)
      {
         //count how many times each card appears in each position
         //each row represents the positions for each card
         var cardCount = Generate2DArray(0);
         for (var i = 0; i < numIterations; ++i)
         {
            var cards = CreateNewDeck();
            //BadShuffle(cards);
            ShuffleDeck(cards);
            RecordStats(cards, cardCount);
         }

         for (var i = 0; i < NumCards; ++i)
         {
            ulong rowTotal = 0;

            for (var j = 0; j < NumCards; ++j)
            {
               rowTotal += (ulong) cardCount[i, j];
            }

            for (var j = 0; j < NumCards; ++j)
            {
               var percent = cardCount[i, j]*100.0/rowTotal;
               Console.Write("{0:N3}% ", percent);
            }
            Console.WriteLine();
         }
      }

      private static T[,] Generate2DArray<T>(T defaultValue)
      {
         var cardCount = new T[NumCards, NumCards];
         for (var i = 0; i < NumCards; ++i)
         {
            for (var j = 0; j < NumCards; ++j)
            {
               cardCount[i, j] = defaultValue;
            }
         }
         return cardCount;
      }

      private static void RecordStats(Card[] cards, int[,] cardCount)
      {
         for (var i = 0; i < cards.Length; ++i)
         {
            ++cardCount[cards[i].Ordinal, i];
         }
      }
   }
}