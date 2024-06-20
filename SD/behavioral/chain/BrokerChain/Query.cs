namespace BrokerChain;

public class Query(string creatureName, Query.Argument whatToQuery, int value)
{
   public enum Argument
   {
      Attack,
      Defense
   }

   public string CreatureName { get; } = creatureName;

   public int Value { get; set; } = value;

   public Argument WhatToQuery { get; } = whatToQuery;
}