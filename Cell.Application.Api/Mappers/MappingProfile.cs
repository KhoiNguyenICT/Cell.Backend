using AutoMapper;
using Cell.Application.Api.Commands;
using Cell.Domain.Aggregates.SettingActionAggregate;
using Cell.Domain.Aggregates.SettingFeatureAggregate;
using Cell.Domain.Aggregates.SettingFieldAggregate;
using Cell.Domain.Aggregates.SettingFormAggregate;
using Cell.Domain.Aggregates.SettingTableAggregate;
using Cell.Domain.Aggregates.SettingViewAggregate;
using Newtonsoft.Json;
using System.Collections.Generic;
using Cell.Domain.Aggregates.SettingFieldInstanceAggregate;

namespace Cell.Application.Api.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region SettingTable

            CreateMap<SettingTableCommand, SettingTable>()
                .ForMember(d => d.Settings, s => s.MapFrom(x => JsonConvert.SerializeObject(x.Settings)));
            CreateMap<SettingTable, SettingTableCommand>()
                .ForMember(d => d.Settings,
                    s => s.MapFrom(x =>
                        JsonConvert.DeserializeObject<List<SettingTableSettingsConfigurationCommand>>(x.Settings)));

            #endregion SettingTable

            #region SettingField

            CreateMap<SettingFieldCommand, SettingField>()
                .ForMember(d => d.AllowFilter, s => s.MapFrom(x => x.AllowFilter ? 1 : 0))
                .ForMember(d => d.AllowSummary, s => s.MapFrom(x => x.AllowSummary ? 1 : 0))
                .ForMember(d => d.Settings, s => s.MapFrom(x => JsonConvert.SerializeObject(x.Settings)));
            CreateMap<SettingField, SettingFieldCommand>()
                .ForMember(d => d.Settings,
                    s => s.MapFrom(x =>
                        JsonConvert.DeserializeObject<SettingFieldSettingsConfigurationCommand>(x.Settings)))
                .ForMember(d => d.AllowFilter, s => s.MapFrom(x => x.AllowFilter == 1 ? true : false))
                .ForMember(d => d.AllowSummary, s => s.MapFrom(x => x.AllowSummary == 1 ? true : false));

            #endregion SettingField

            #region SettingAction

            CreateMap<SettingAction, SettingActionCommand>()
                .ForMember(d => d.Settings,
                    s => s.MapFrom(
                        x => JsonConvert.DeserializeObject<SettingActionSettingConfigurationCommand>(x.Settings)));
            CreateMap<SettingActionCommand, SettingAction>()
                .ForMember(d => d.Settings, s => s.MapFrom(x => JsonConvert.SerializeObject(x.Settings)));

            #endregion SettingAction

            #region SettingForm

            CreateMap<SettingForm, SettingFormCommand>()
                .ForMember(d => d.Settings,
                    s => s.MapFrom(x => JsonConvert.DeserializeObject<SettingConfigurationCommand>(x.Settings)));
            CreateMap<SettingFormCommand, SettingForm>()
                .ForMember(d => d.Settings, s => s.MapFrom(x => JsonConvert.SerializeObject(x.Settings)));

            #endregion SettingForm

            #region SettingView

            CreateMap<SettingView, SettingViewCommand>()
                .ForMember(d => d.Settings, s => s.MapFrom(x => JsonConvert.DeserializeObject<SettingViewSettingsCommand>(x.Settings)));

            CreateMap<SettingViewCommand, SettingView>()
                .ForMember(d => d.Settings, s => s.MapFrom(x => JsonConvert.SerializeObject(x.Settings)));

            #endregion SettingView

            #region SettingFeature

            CreateMap<SettingFeature, SettingFeatureCommand>();
            CreateMap<SettingFeatureCommand, SettingFeature>();

            #endregion SettingFeature

            #region SettingFieldInstance

            CreateMap<SettingFieldInstanceCommand, SettingFieldInstance>()
                .ForMember(d => d.Settings, s => s.MapFrom(x => x.Settings));

            CreateMap<SettingFieldInstance, SettingFieldInstanceCommand>()
                .ForMember(d => d.Settings,
                    s => s.MapFrom(x => JsonConvert.DeserializeObject<SettingFieldInstanceSettings>(x.Settings)));

            #endregion
        }
    }
}