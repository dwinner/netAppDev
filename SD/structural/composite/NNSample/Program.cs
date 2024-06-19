namespace NNSample;

internal static class Program
{
   private static void Main()
   {
      var neuron1 = new Neuron();
      var neuron2 = new Neuron();
      var layer1 = new NeuronLayer(3);
      var layer2 = new NeuronLayer(4);

      neuron1.ConnectTo(neuron2);
      neuron1.ConnectTo(layer1);
      layer2.ConnectTo(neuron1);
      layer1.ConnectTo(layer2);
   }
}