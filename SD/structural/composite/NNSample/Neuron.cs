using System.Collections;

namespace NNSample;

public class Neuron : IEnumerable<Neuron>
{
   public List<Neuron> In { get; } = [];

   public List<Neuron> Out { get; } = [];

   public IEnumerator<Neuron> GetEnumerator()
   {
      yield return this;
   }

   IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}