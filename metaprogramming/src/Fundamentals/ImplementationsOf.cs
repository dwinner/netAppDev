using System.Collections;

namespace Fundamentals;

/// <summary>
///    Represents an implementation of <see cref="IImplementationsOf{T}" />.
/// </summary>
/// <typeparam name="T">Base type to discover for - must be an abstract class or an interface.</typeparam>
public class ImplementationsOf<T> : IImplementationsOf<T>
   where T : class
{
   private readonly IEnumerable<Type> _types;

   /// <summary>
   ///    Initializes a new instance of the <see cref="ImplementationsOf{T}" /> class.
   /// </summary>
   /// <param name="types"><see cref="ITypes" /> to use for finding types.</param>
   public ImplementationsOf(ITypes types) => _types = types.FindMultiple<T>();

   /// <inheritdoc />
   public IEnumerator<Type> GetEnumerator() => _types.GetEnumerator();

   /// <inheritdoc />
   IEnumerator IEnumerable.GetEnumerator() => _types.GetEnumerator();
}