namespace BrokerChain;

public class IncreaseDefenseModifier(Game game, Creature creature) : CreatureModifier(game, creature)
{
   protected override void Handle(object sender, Query query)
   {
      if (query.CreatureName == Creature.Name &&
          query.WhatToQuery == Query.Argument.Defense)
      {
         query.Value += 2;
      }
   }
}