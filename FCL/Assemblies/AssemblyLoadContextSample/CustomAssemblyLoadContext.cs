﻿using System.Reflection;
using System.Runtime.Loader;

namespace AssemblyLoadContextSample
{
   public class CustomAssemblyLoadContext : AssemblyLoadContext
   {
      public CustomAssemblyLoadContext()
         :base(isCollectible:true)
      {
      }

      protected override Assembly Load(AssemblyName assemblyName) => null;
   }
}