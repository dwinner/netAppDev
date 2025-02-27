﻿using System.Collections.Generic;

namespace DataLib;

public record Team
{
   public Team(string name, params int[] years)
   {
      Name = name;
      Years = years is not null ? new List<int>(years) : new List<int>();
   }

   public string Name { get; }

   public IEnumerable<int> Years { get; }
}
