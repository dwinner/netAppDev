using System.Collections.ObjectModel;

internal class MyCollection<T> : Collection<T>, IEnumerableEx<T>
{
}