﻿using System.Windows.Input;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using StarWarsSample.Core.Resources;

namespace StarWarsSample.Core.ViewModels
{
   public class StatusViewModel : BaseViewModel
   {
      public StatusViewModel(IMvxNavigationService navigation)
      {
         CloseCommand = new MvxAsyncCommand(async () => await navigation.Close(this).ConfigureAwait(true));
      }

      // MvvmCross Lifecycle

      // MVVM Properties
      public PlotModel PlotModel => GeneratePlotModel();

      // MVVM Commands
      public ICommand CloseCommand { get; }

      // Private methods
      private static PlotModel GeneratePlotModel()
      {
         var model = new PlotModel
         {
            PlotAreaBorderColor = OxyColors.LightGray,
            LegendTextColor = OxyColors.LightGray,
            LegendTitleColor = OxyColors.LightGray,
            TextColor = OxyColors.White
         };
         model.Axes.Add(new LinearAxis
         {
            Position = AxisPosition.Bottom,
            Title = Strings.Time,
            TitleColor = OxyColors.White,
            AxislineColor = OxyColors.LightGray,
            TicklineColor = OxyColors.LightGray
         });
         model.Axes.Add(new LinearAxis
         {
            Position = AxisPosition.Left,
            Maximum = 10,
            Minimum = 0,
            Title = Strings.Targets,
            TitleColor = OxyColors.White,
            AxislineColor = OxyColors.LightGray,
            TicklineColor = OxyColors.LightGray
         });

         var series1 = new LineSeries
         {
            MarkerType = MarkerType.Circle,
            MarkerSize = 4,
            MarkerStroke = OxyColors.Red,
            Color = OxyColors.Red
         };

         series1.Points.Add(new DataPoint(0.0, 6.0));
         series1.Points.Add(new DataPoint(1.4, 2.1));
         series1.Points.Add(new DataPoint(2.0, 4.2));
         series1.Points.Add(new DataPoint(3.3, 2.3));
         series1.Points.Add(new DataPoint(4.7, 7.4));
         series1.Points.Add(new DataPoint(6.0, 6.2));
         series1.Points.Add(new DataPoint(8.9, 8.9));

         model.Series.Add(series1);

         return model;
      }
   }
}