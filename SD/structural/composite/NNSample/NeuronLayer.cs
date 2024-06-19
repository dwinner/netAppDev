using System.Collections.ObjectModel;

namespace NNSample;

public class NeuronLayer : Collection<Neuron>
{
   public NeuronLayer(int count)
   {
      while (count-- > 0)
      {
         Add(new Neuron());
      }
   }
}