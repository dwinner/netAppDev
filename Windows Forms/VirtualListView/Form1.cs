using System;
using System.Collections.Generic;
using System.Windows.Forms;
using VirtualListViewSort.Properties;

namespace VirtualListViewSort
{
   public partial class Form1 : Form
   {
      // Простой кэш элементов списка
      private readonly List<ListViewItem> _listViewItemCache = new List<ListViewItem>();

      // Это нужно для отображения индекса в списке на индекс в кэше
      private int _topIndex = -1;

      public Form1()
      {
         InitializeComponent();

         listView.VirtualMode = true;
         listView.VirtualListSize = (int)numericUpDown1.Value;
         listView.CacheVirtualItems += listView_CacheVirtualItems;
         listView.RetrieveVirtualItem += listView_RetrieveVirtualItem;
      }

      // Вызывается непосредственно перед тем, как ListView выведет новую порцию элементов списка
      private void listView_CacheVirtualItems(object sender, CacheVirtualItemsEventArgs e)
      {
         _topIndex = e.StartIndex;
         // Выяснить, нужны ли еще элементы
         // (Обратите внимание, что мы никогда не делаем список короче;
         //  это признак не самой эффективной реализации кэша)
         var needed = (e.EndIndex - e.StartIndex) + 1;
         if (_listViewItemCache.Capacity < needed)
         {
            var toGrow = needed - _listViewItemCache.Capacity;
            // Привести вместимость в соответствие с характеристиками цели
            _listViewItemCache.Capacity = needed;
            
            // Добавить новые кэшированные элементы
            for (var i = 0; i < toGrow; i++)
            {
               _listViewItemCache.Add(new ListViewItem());
            }
         }
      }

      private void listView_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
      {
         var cacheIndex = e.ItemIndex - _topIndex;
         if (cacheIndex >= 0 && cacheIndex < _listViewItemCache.Count)
         {
            e.Item = _listViewItemCache[cacheIndex];

            // В тексте можно было вывести любые данные;
            // исходя из значения e.ItemIndex, покажем
            // индекс элементов списка и индекс кэша (для простоты)
            e.Item.Text = string.Format("{0}{1}{2}", e.ItemIndex, Resources.Pointer, cacheIndex);
            // Записать произвольный объект в e.Item.Tag
         }
         else
         {
            // Это может иногда случаться, но мы этого не увидим
            e.Item = _listViewItemCache[0];
            e.Item.Text = Resources.Oops;
         }
      }

      // Размер списка изменяется по требованию
      private void numericUpDown1_ValueChanged(object sender, EventArgs e)
      {
         listView.VirtualListSize = (int)numericUpDown1.Value;
      }
   }
}