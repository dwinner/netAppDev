using AppDev.Sapper.Model.Models;
using AppDev.Sapper.Model.ViewModels;
using AutoMapper;

namespace AppDev.Sapper.AppLoader.Startup
{
   internal static class AutomapperConfig
   {
      internal static void ConfigureViewModelMapping() => Mapper.Initialize(Config);

      private static void Config(IMapperConfigurationExpression expression)
      {
         expression.CreateMap<MinefieldCell, MinefieldCellViewModel>()
            .ForMember(minefieldCellVm => minefieldCellVm.Row,
               configurationExpr => configurationExpr.MapFrom(minefieldCell => minefieldCell.Row))
            .ForMember(minefieldCellVm => minefieldCellVm.Column,
               configurationExpr => configurationExpr.MapFrom(minefieldCell => minefieldCell.Column))
            .ForMember(minefieldCellVm => minefieldCellVm.HasMine,
               configurationExpr => configurationExpr.MapFrom(minefieldCell => minefieldCell.HasMine))
            .ForMember(minefieldCellVm => minefieldCellVm.State,
               configurationExpr => configurationExpr.MapFrom(minefieldCell => minefieldCell.State))
            .ForMember(minefieldCellVm => minefieldCellVm.NeibourMinesCount,
               configurationExpr => configurationExpr.MapFrom(minefieldCell => minefieldCell.NeibourMinesCount));

         expression.CreateMap<MinefieldCellViewModel, MinefieldCell>()
            .ForMember(minefieldCell => minefieldCell.Row,
               configurationExpr => configurationExpr.MapFrom(minefieldCellVm => minefieldCellVm.Row))
            .ForMember(minefieldCell => minefieldCell.Column,
               configurationExpr => configurationExpr.MapFrom(minefieldCellVm => minefieldCellVm.Column))
            .ForMember(minefieldCell => minefieldCell.HasMine,
               configurationExpr => configurationExpr.MapFrom(minefieldCellVm => minefieldCellVm.HasMine))
            .ForMember(minefieldCell => minefieldCell.State,
               configurationExpr => configurationExpr.MapFrom(minefieldCellVm => minefieldCellVm.State))
            .ForMember(minefieldCell => minefieldCell.NeibourMinesCount,
               configurationExpr => configurationExpr.MapFrom(minefieldCellVm => minefieldCellVm.NeibourMinesCount));
      }
   }
}