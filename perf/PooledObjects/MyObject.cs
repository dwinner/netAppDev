namespace PooledObjects
{
   internal class MyObject : IPoolableObject
   {
      private PoolManager _poolManager;
      public byte[] Data { get; set; }
      public int UsableLength { get; set; }

      public int Size => Data?.Length ?? 0;

      void IPoolableObject.Reset()
      {
         UsableLength = 0;
      }

      void IPoolableObject.SetPoolManager(PoolManager aPoolManager)
      {
         _poolManager = aPoolManager;
      }

      public void Dispose()
      {
         _poolManager.ReturnObject(this);
      }
   }
}