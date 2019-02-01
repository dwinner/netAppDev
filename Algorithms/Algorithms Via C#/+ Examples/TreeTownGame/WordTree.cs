using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TreeTownGame
{
   /// <summary>
   ///    Класс, в котором построены все деревья городов
   /// </summary>
   public class WordTree
   {
      #region Вспомогательный класс для построения узлов дерева

      private class TownNode : Node<string>
      {
         private TownNode(string aTown, ISet<string> restVariants, Node<string> parentTown)
            : base(aTown, restVariants, parentTown)
         {
         }

         public TownNode(string aTown, ISet<string> restVariants)
            : base(aTown, restVariants)
         {
         }

         public override void BuidTownTree()
         {
            if (Variants.Count <= 0) return;

            foreach (string variant in Variants)
            {
               if (char.ToLower(Town[Town.Length - 1]) != char.ToLower(variant[0]) || Town.Equals(variant))
                  continue;

               var restVariants = new HashSet<string>(Variants);
               restVariants.Remove(Town);
               var townNode = new TownNode(variant, restVariants, this) { Depth = Depth + 1 };
               Matches.Add(townNode);
               townNode.BuidTownTree();
            }
         }
      }

      #endregion

      private readonly List<LinkedList<string>> _maxChains = new List<LinkedList<string>>();
      // Список максимальных цепочек городов

      private readonly List<Node<string>> _maxLeafs = new List<Node<string>>();
      // Список узлов, для которых строятся максимальные цепочки      

      private readonly ISet<Node<string>> _rootTownSet = new HashSet<Node<string>>(); // Набор корневых узлов

      private readonly LinkedList<string> _tempChain = new LinkedList<string>();
      // Временный связный список для сохранения между вызовами      

      private readonly ISet<string> _uniqueChains = new HashSet<string>(); // Уникальное множество цепочек
      private uint _maxDepth; // Максимальное значение вложенности узла      

      /// <summary>
      ///    Конструктор для построения цепочек из городов
      /// </summary>
      /// <param name="towns">Массив городов</param>
      public WordTree(string[] towns)
      {
         Towns = towns;

         Parallel.ForEach(Towns, town => // Строим корневые узлы
         {
            var townSet = new HashSet<string>(Towns);
            Node<string> rootTownNode = new TownNode(town, townSet);
            lock (_rootTownSet)
            {
               _rootTownSet.Add(rootTownNode);
            }

            rootTownNode.BuidTownTree();
         });

         foreach (var rootNode in _rootTownSet) // Находим максимальное значение вложенности         
            FindMaximumDepth(rootNode);

         foreach (var rootNode in _rootTownSet) // Находим узлы с максимальными цепочками         
            FindMaximumNodes(rootNode);

         foreach (var maxNode in _maxLeafs) // Строим максимальные цепочки городов
         {
            _tempChain.Clear();
            GenerateMaxChains(maxNode);
            _tempChain.AddLast(maxNode.Town);
            _maxChains.Add(new LinkedList<string>(_tempChain));
         }

         foreach (var chainList in _maxChains) // Оставляем только уникальные         
         {
            var strBuilder = new StringBuilder();
            foreach (string town in chainList)
               strBuilder.Append(town).Append(" ");
            _uniqueChains.Add(strBuilder.ToString());
         }
      }

      /// <summary>
      ///    Массив городов
      /// </summary>
      private string[] Towns { get; set; }

      public IEnumerable<string> UniqueChains
      {
         get { return _uniqueChains; }
      }

      /// <summary>
      ///    Нахождение максимального значения вложенности узла
      /// </summary>
      /// <param name="rootNode">Корневой узел</param>
      private void FindMaximumDepth(Node<string> rootNode)
      {
         foreach (var matchNode in rootNode.Matches)
         {
            if (matchNode.Depth > _maxDepth)
               _maxDepth = matchNode.Depth;

            FindMaximumDepth(matchNode);
         }
      }

      /// <summary>
      ///    Нахождение узлов для максимальных цепочек
      /// </summary>
      /// <param name="rootNode">Корневой узел</param>
      private void FindMaximumNodes(Node<string> rootNode)
      {
         foreach (var matchNode in rootNode.Matches)
         {
            if (matchNode.Depth == _maxDepth)
               _maxLeafs.Add(matchNode);

            FindMaximumNodes(matchNode);
         }
      }

      /// <summary>
      ///    Генерирование максимальных цепочек из городов
      /// </summary>
      /// <param name="townNode">Узел города</param>
      private void GenerateMaxChains(Node<string> townNode)
      {
         if (townNode.Depth > 1 && townNode.Parent != null)
            GenerateMaxChains(townNode.Parent);

         if (townNode.Parent != null)
            _tempChain.AddLast(townNode.Parent.Town);
      }
   }
}