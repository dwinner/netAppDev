using System;

namespace PooledObjects
{
   internal interface IPoolableObject : IDisposable
   {
      int Size { get; }

      void Reset();

      void SetPoolManager(PoolManager poolManager);
   }
}