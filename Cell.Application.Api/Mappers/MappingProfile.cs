using System.Collections.Generic;
using AutoMapper;
using Cell.Application.Api.Commands;
using Cell.Domain.Aggregates.SettingFieldAggregate;
using Cell.Domain.Aggregates.SettingTableAggregate;
using Newtonsoft.Json;

namespace Cell.Application.Api.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<SettingTableCommand, SettingTable>()
                .ForMember(d => d.Settings, s => s.MapFrom(x => JsonConvert.SerializeObject(x.Settings)));
            CreateMap<SettingTable, SettingTableCommand>()
                .ForMember(d => d.Settings,
                    s => s.MapFrom(x =>
                        JsonConvert.DeserializeObject<List<SettingTableSettingsConfigurationCommand>>(x.Settings)));

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
        }
    }
}