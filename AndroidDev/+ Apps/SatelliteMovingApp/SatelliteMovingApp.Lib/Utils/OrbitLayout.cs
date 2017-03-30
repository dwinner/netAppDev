using System;
using System.Collections.Generic;
using Android.Content;
using Android.Runtime;
using Android.Util;
using Android.Views;
using SatelliteMovingApp.Lib.Model;
using static System.Math;

namespace SatelliteMovingApp.Lib.Utils
{
   /// <summary>
   ///    Менеджер расположения узлов View, упорядоченных по орбите включающего контейнера, начиная с центра
   /// </summary>
   public sealed class OrbitLayout : ViewGroup
   {
      public OrbitLayout(SortedDictionary<Satellite, View> aSatelliteMap, Context aContext)
         : this(aContext)
      {
         SetupSatelliteMap(aSatelliteMap);
      }

      public OrbitLayout(IntPtr javaReference, JniHandleOwnership transfer)
         : base(javaReference, transfer)
      {
      }

      public OrbitLayout(Context context)
         : base(context)
      {
      }

      public OrbitLayout(Context context, IAttributeSet attrs)
         : base(context, attrs)
      {
      }

      public OrbitLayout(Context context, IAttributeSet attrs, int defStyleAttr)
         : base(context, attrs, defStyleAttr)
      {
      }

      public OrbitLayout(Context context, IAttributeSet attrs, int defStyleAttr, int defStyleRes)
         : base(context, attrs, defStyleAttr, defStyleRes)
      {
      }

      public SortedDictionary<Satellite, View> SatelliteMap { get; set; }

      public List<Satellite> SatelliteList { get; set; }

      /// <summary>
      ///    Установка карты соответствия параметров спутника с его View
      /// </summary>
      /// <param name="aSatelliteMap">Карта соответствия параметров спутника с его View</param>
      private void SetupSatelliteMap(SortedDictionary<Satellite, View> aSatelliteMap)
      {
         SatelliteMap = aSatelliteMap;
         var layoutParams = new LayoutParams(LayoutParams.WrapContent, LayoutParams.WrapContent);
         foreach (var view in SatelliteMap.Values)
            AddView(view, layoutParams);

         SatelliteList = new List<Satellite>(SatelliteMap.Keys);
      }

      protected override void OnLayout(bool changed, int left, int top, int right, int bottom)
      {
         var availableWidth = right - left;
         var availableHeight = bottom - top;
         var max = MeasureChildrenSizes(availableWidth, availableHeight);
         availableWidth -= max[0];
         availableHeight -= max[1];
         var centerX = availableWidth / 2;
         var centerY = availableHeight / 2;

         for (var childIndex = 0; childIndex < ChildCount; childIndex++)
         {
            var child = GetChildAt(childIndex);
            int x, y;
            if (childIndex == 0)
            {
               x = left + centerX;
               y = top + centerY;
            }
            else
            {
               var distance = SatelliteList[childIndex].Distance;
               var angle = SatelliteList[childIndex].Angle;
               x = (int) (left + centerX - distance * Cos(angle));
               y = (int) (top + centerY - distance * Sin(angle));
            }

            child.Layout(x, y, x + child.MeasuredWidth, y + child.MeasuredHeight);
         }
      }

      /// <summary>
      ///    Вычисление максимальных значений ширины и высоты дочерних узлов View во ViewGroup
      /// </summary>
      /// <param name="sw">Ширина контейнера</param>
      /// <param name="sh">Высота контейнера</param>
      /// <returns>Массив максимальных значений: [0] =&gt; ширина, [1] =&gt; высота</returns>
      private int[] MeasureChildrenSizes(int sw, int sh)
      {
         var maxWidth = 0;
         var maxHeight = 0;

         for (var childIndex = 0; childIndex < ChildCount; childIndex++)
         {
            var child = GetChildAt(childIndex);
            MeasureChild(child, sw, sh);
            maxWidth = Max(maxWidth, child.MeasuredWidth);
            maxHeight = Max(maxHeight, child.MeasuredHeight);
         }

         return new[] {maxWidth, maxHeight};
      }
   }
}