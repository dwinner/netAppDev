namespace HowToCSharp.ch09.GenericClass
{
   // Объект, который нужно добавить в индекс
   class Part
   {
      private readonly string _partId;
      private readonly string _name;
      private readonly double _weight;

      public string PartId { get { return _partId; } }

      public Part(string partId, string name, double weight)
      {
         _partId = partId;
         _name = name;
         _weight = weight;
      }

      public override string ToString()
      {
         return string.Format("Part: {0}, Name: {1}, Weight: {2}", _partId, _name, _weight);
      }
   }
}