using AutoMapper;
using Cell.Application.Api.Commands;
using Cell.Domain.Aggregates.SettingActionAggregate;
using Cell.Domain.Aggregates.SettingFieldAggregate;
using Cell.Domain.Aggregates.SettingFormAggregate;
using Cell.Domain.Aggregates.SettingTableAggregate;
using Newtonsoft.Json;
using System.Collections.Generic;

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
        }
    }
}