using System.Collections.Concurrent;

namespace Fundamentals;

/// <summary>
///    Represents an implementation of <see cref="IContractToImplementorsMap" />.
/// </summary>
public class ContractToImplementorsMap : IContractToImplementorsMap
{
   private readonly ConcurrentDictionary<Type, Type> _allTypes = new();
   private readonly ConcurrentDictionary<Type, ConcurrentBag<Type>> _contractsAndImplementors = new();

   /// <inheritdoc />
   public IDictionary<Type, IEnumerable<Type>> ContractsAndImplementors =>
      _contractsAndImplementors.ToDictionary(_ => _.Key, _ => _.Value.AsEnumerable());

   /// <inheritdoc />
   public IEnumerable<Type> All => _allTypes.Keys;

   /// <inheritdoc />
   public void Feed(IEnumerable<Type> types)
   {
      MapTypes(types);
      AddTypesToAllTypes(types);
   }

   /// <inheritdoc />
   public IEnumerable<Type> GetImplementorsFor<T>() => GetImplementorsFor(typeof(T));

   /// <inheritdoc />
   public IEnumerable<Type> GetImplementorsFor(Type contract) => GetImplementingTypesFor(contract);

   private void AddTypesToAllTypes(IEnumerable<Type> types)
   {
      foreach (var type in types)
      {
         _allTypes[type] = type;
      }
   }

   private void MapTypes(IEnumerable<Type> types)
   {
      var implementors = types.Where(IsImplementation);
      Parallel.ForEach(implementors, implementor =>
      {
         foreach (var contract in implementor.AllBaseAndImplementingTypes())
         {
            var implementingTypes = GetImplementingTypesFor(contract);
            if (!implementingTypes.Contains(implementor))
            {
               implementingTypes.Add(implementor);
            }
         }
      });
   }

   private bool IsImplementation(Type type) => type is { IsInterface: false, IsAbstract: false };

   private ConcurrentBag<Type> GetImplementingTypesFor(Type contract)
   {
      return _contractsAndImplementors.GetOrAdd(contract, _ => new ConcurrentBag<Type>());
   }
}