using System.Collections.Generic;
using AutoMapper;
using Cell.Model.Entities.SecurityGroupEntity;
using Cell.Model.Entities.SecurityPermissionEntity;
using Cell.Model.Entities.SecuritySessionEntity;
using Cell.Model.Entities.SecurityUserEntity;
using Cell.Model.Entities.SettingActionEntity;
using Cell.Model.Entities.SettingActionInstanceEntity;
using Cell.Model.Entities.SettingAdvancedEntity;
using Cell.Model.Entities.SettingApiEntity;
using Cell.Model.Entities.SettingFeatureEntity;
using Cell.Model.Entities.SettingFieldEntity;
using Cell.Model.Entities.SettingFieldInstanceEntity;
using Cell.Model.Entities.SettingFilterEntity;
using Cell.Model.Entities.SettingFormEntity;
using Cell.Model.Entities.SettingReportEntity;
using Cell.Model.Entities.SettingTableEntity;
using Cell.Model.Entities.SettingViewEntity;
using Cell.Model.Models.SecurityGroup;
using Cell.Model.Models.SecurityPermission;
using Cell.Model.Models.SecuritySession;
using Cell.Model.Models.SecurityUser;
using Cell.Model.Models.SettingAction;
using Cell.Model.Models.SettingActionInstance;
using Cell.Model.Models.SettingAdvanced;
using Cell.Model.Models.SettingApi;
using Cell.Model.Models.SettingFeature;
using Cell.Model.Models.SettingField;
using Cell.Model.Models.SettingFieldInstance;
using Cell.Model.Models.SettingFilter;
using Cell.Model.Models.SettingForm;
using Cell.Model.Models.SettingReport;
using Cell.Model.Models.SettingTable;
using Cell.Model.Models.SettingView;
using Newtonsoft.Json;

namespace Cell.Application.Api.Mappers
{
    public class EntityMappingModel : Profile
    {
        public EntityMappingModel()
        {
            CreateMap<SecurityGroup, SecurityGroupModel>()
                .ForMember(d => d.Settings,
                    s => s.MapFrom(x => JsonConvert.DeserializeObject<SecurityGroupSettingsModel>(x.Settings)));
            CreateMap<SecurityPermission, SecurityPermissionModel>()
                .ForMember(d => d.Settings, s => s.MapFrom(x => JsonConvert.DeserializeObject<SecurityPermissionSettingsModel>(x.Settings)));
            CreateMap<SecuritySession, SecuritySessionModel>()
                .ForMember(d => d.Settings,
                    s => s.MapFrom(x => JsonConvert.DeserializeObject<SecuritySessionSettingsModel>(x.Settings)));
            CreateMap<SecurityUser, SecurityUserModel>()
                .ForMember(d => d.Settings,
                    s => s.MapFrom(x => JsonConvert.DeserializeObject<SecurityUserSettingsModel>(x.Settings)));
            CreateMap<SettingAction, SettingActionModel>()
                .ForMember(d => d.Settings, s => s.MapFrom(x => JsonConvert.DeserializeObject(x.Settings)));
            CreateMap<SettingActionInstance, SettingActionInstanceModel>()
                .ForMember(d => d.Settings, s => s.MapFrom(x => x.Settings));
            CreateMap<SettingAdvanced, SettingAdvancedModel>();
            CreateMap<SettingFeature, SettingFeatureModel>()
                .ForMember(d => d.Settings, s => s.MapFrom(x => JsonConvert.DeserializeObject<SettingFeatureSettingsModel>(x.Settings)));
            CreateMap<SettingField, SettingFieldModel>()
                .ForMember(d => d.Settings,
                    s => s.MapFrom(x => JsonConvert.DeserializeObject<SettingFieldSettingsModel>(x.Settings)));
            CreateMap<SettingFieldInstance, SettingFieldInstanceModel>()
                .ForMember(d => d.Settings,
                    s => s.MapFrom(x => JsonConvert.DeserializeObject<SettingFieldInstanceSettingsModel>(x.Settings)));
            CreateMap<SettingFilter, SettingFilterModel>();
            CreateMap<SettingForm, SettingFormModel>()
                .ForMember(d => d.Settings,
                    s => s.MapFrom(x => JsonConvert.DeserializeObject<SettingFormSettingsModel>(x.Settings)));
            CreateMap<SettingReport, SettingReportModel>()
                .ForMember(d => d.Settings,
                    s => s.MapFrom(x => JsonConvert.DeserializeObject<SettingReportSettingsModel>(x.Settings)));
            CreateMap<SettingTable, SettingTableModel>()
                .ForMember(d => d.Settings,
                    s => s.MapFrom(x => JsonConvert.DeserializeObject<List<SettingTableSettingsModel>>(x.Settings)));
            CreateMap<SettingView, SettingViewModel>()
                .ForMember(d => d.Settings,
                    s => s.MapFrom(x => JsonConvert.DeserializeObject<SettingViewSettingsModel>(x.Settings)));
            CreateMap<SettingApi, SettingApiModel>()
                .ForMember(d => d.Settings,
                    s => s.MapFrom(x => JsonConvert.DeserializeObject<List<SettingApiSettingsModel>>(x.Settings)));
        }
    }
}