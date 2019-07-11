using AutoMapper;
using Cell.Application.Api.Commands;
using Cell.Domain.Aggregates.SettingActionAggregate;
using Cell.Domain.Aggregates.SettingActionInstanceAggregate;
using Cell.Domain.Aggregates.SettingFeatureAggregate;
using Cell.Domain.Aggregates.SettingFieldAggregate;
using Cell.Domain.Aggregates.SettingFieldInstanceAggregate;
using Cell.Domain.Aggregates.SettingFormAggregate;
using Cell.Domain.Aggregates.SettingTableAggregate;
using Cell.Domain.Aggregates.SettingViewAggregate;
using Newtonsoft.Json;
using System.Collections.Generic;
using Cell.Domain.Aggregates.SecurityGroupAggregate;
using Cell.Domain.Aggregates.SecurityPermissionAggregate;
using Cell.Domain.Aggregates.SecurityUserAggregate;
using Cell.Domain.Aggregates.SettingAdvancedAggregate;

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

            CreateMap<SettingFeature, SettingFeatureCommand>()
                .ForMember(d => d.Settings,
                    s => s.MapFrom(x => JsonConvert.DeserializeObject<SettingFeatureSettings>(x.Settings)));
            CreateMap<SettingFeatureCommand, SettingFeature>()
                .ForMember(d => d.Settings,
                    s => s.MapFrom(x => JsonConvert.SerializeObject(x.Settings)));

            #endregion SettingFeature

            #region SettingFieldInstance

            CreateMap<SettingFieldInstanceCommand, SettingFieldInstance>()
                .ForMember(d => d.Settings, s => s.MapFrom(x => JsonConvert.SerializeObject(x.Settings)));

            CreateMap<SettingFieldInstance, SettingFieldInstanceCommand>()
                .ForMember(d => d.Settings,
                    s => s.MapFrom(x => JsonConvert.DeserializeObject<SettingFieldInstanceSettings>(x.Settings)));

            #endregion SettingFieldInstance

            #region SettingActionInstance

            CreateMap<SettingActionInstance, SettingActionInstanceCommand>();

            CreateMap<SettingActionInstanceCommand, SettingActionInstance>();

            #endregion SettingActionInstance

            #region SettingAdvanced

            CreateMap<SettingAdvanced, SettingAdvancedCommand>();
            CreateMap<SettingAdvancedCommand, SettingAdvanced>();

            #endregion

            #region SecurityGroup

            CreateMap<SecurityGroup, SettingGroupCommand>();
            CreateMap<SettingGroupCommand, SecurityGroup>();

            #endregion

            #region SecurityUser

            CreateMap<SecurityUser, SettingUserCommand>()
                .ForMember(d => d.Settings, s => s.MapFrom(x => JsonConvert.DeserializeObject<SettingUserSettingsCommand>(x.Settings))); ;
            CreateMap<SettingUserCommand, SecurityUser>()
                .ForMember(d => d.Settings, s => s.MapFrom(x => JsonConvert.SerializeObject(x.Settings)));

            #endregion

            #region SecurityPermission

            CreateMap<SecurityPermission, SettingPermissionCommand>()
                .ForMember(d => d.Settings, s => s.MapFrom(x => JsonConvert.DeserializeObject(x.Settings)));

            CreateMap<SettingPermissionCommand, SecurityPermission>()
                .ForMember(d => d.Settings, s => s.MapFrom(x => JsonConvert.SerializeObject(x.Settings)));

            #endregion
        }
    }
}