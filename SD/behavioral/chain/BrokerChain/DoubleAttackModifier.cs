namespace BrokerChain;

public class DoubleAttackModifier(Game game, Creature creature) : CreatureModifier(game, creature)
{
   protected override void Handle(object sender, Query query)
   {
      if (query.CreatureName == Creature.Name
          && query.WhatToQuery == Query.Argument.Attack)
      {
         query.Value *= 2;
      }
   }
}