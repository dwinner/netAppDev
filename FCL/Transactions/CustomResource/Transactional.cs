using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Transactions;

namespace CustomResource
{
   /// <summary>
   /// Транзакционный ресурс
   /// </summary>
   /// <typeparam name="T">Параметр типа</typeparam>
   public class Transactional<T>
   {
      private T _liveValue;   // Актуальное значение управляемого ресурса
      private ResourceManager<T> _enlistment;   // Временное значение, которое ассоциировано с транзакцией
      private Transaction _enlistmenTransaction;   // Охватывающая транзакция, если она существует

      public Transactional(T value)
      {
         if (Transaction.Current == null)
         {
            _liveValue = value;
         }
         else
         {
            _liveValue = default(T);
            GetEnlistment().Value = value;
         }
      }

      public Transactional()
         : this(default(T)) { }

      public T Value
      {
         get { return GetValue(); }
         set { SetValue(value); }
      }

      internal void Commit(T value, Transaction tx)
      {
         _liveValue = value;
         _enlistmenTransaction = null;
      }

      internal void Rollback(Transaction tx)
      {
         _enlistmenTransaction = null;
      }

      protected virtual T GetValue()
      {
         return Transaction.Current == null ? _liveValue : GetEnlistment().Value;
      }

      protected virtual void SetValue(T value)
      {
         if (Transaction.Current == null)
         {
            _liveValue = value;
         }
         else
         {
            GetEnlistment().Value = value;
         }
      }

      private ResourceManager<T> GetEnlistment()
      {
         var currentTx = Transaction.Current;
         Trace.Assert(currentTx != null, "Must be invoked with ambient currentTx");

         if (_enlistmenTransaction == null)
         {
            _enlistment = new ResourceManager<T>(this, currentTx);
            currentTx.EnlistVolatile(_enlistment, EnlistmentOptions.None);
            _enlistmenTransaction = currentTx;
            return _enlistment;
         }

         if (_enlistmenTransaction == Transaction.Current)
         {
            return _enlistment;
         }

         // Этот класс поддерживает участие только в одной транзакции.
         throw new TransactionException("This class supports enlisting with one currentTx");
      }

      #region Служебный менеджер ресурсов

      internal class ResourceManager<TU> : IEnlistmentNotification
      {
         private readonly Transactional<TU> _parent;
         private readonly Transaction _currentTransaction;

         static ResourceManager()
         {
            Type rmType = typeof(TU);
            Trace.Assert(rmType.IsSerializable, string.Format("Type {0} is not serializable", rmType.Name));
         }

         public ResourceManager(Transactional<TU> parent, Transaction currentTx)
         {
            _parent = parent;
            Value = DeepCopy(parent._liveValue);
            _currentTransaction = currentTx;
         }

         private static TU DeepCopy(TU liveValue)
         {
            using (var memoryStream = new MemoryStream())
            {
               var binaryFormatter = new BinaryFormatter();
               binaryFormatter.Serialize(memoryStream, liveValue);
               memoryStream.Flush();
               memoryStream.Seek(0, SeekOrigin.Begin);

               return (TU)binaryFormatter.Deserialize(memoryStream);
            }
         }

         public TU Value { get; set; }

         public void Prepare(PreparingEnlistment preparingEnlistment)
         {
            preparingEnlistment.Prepared();
         }

         public void Commit(Enlistment enlistment)
         {
            _parent.Commit(Value, _currentTransaction);
            enlistment.Done();
         }

         public void Rollback(Enlistment enlistment)
         {
            _parent.Rollback(_currentTransaction);
            enlistment.Done();
         }

         public void InDoubt(Enlistment enlistment)
         {
            enlistment.Done();
         }
      }

      #endregion

   }
}