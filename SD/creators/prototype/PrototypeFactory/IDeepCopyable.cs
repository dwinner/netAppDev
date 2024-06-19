namespace PrototypeFactory;

internal interface IDeepCopyable<out T>
{
   T DeepCopy();
}