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
    public class ModelMappingEntity : Profile
    {
        public ModelMappingEntity()
        {
            CreateMap<SecurityGroupModel, SecurityGroup>();
            CreateMap<SecurityGroupCreateModel, SecurityGroup>();
            CreateMap<SecurityGroupUpdateModel, SecurityGroup>();

            CreateMap<SecurityPermissionModel, SecurityPermission>()
                .ForMember(d => d.Settings, s => s.MapFrom(x => JsonConvert.SerializeObject(x.Settings)));
            CreateMap<SecurityPermissionCreateModel, SecurityPermission>();

            CreateMap<SecuritySessionModel, SecuritySession>()
                .ForMember(d => d.Settings,
                    s => s.MapFrom(x => JsonConvert.SerializeObject(x.Settings)));
            CreateMap<SecuritySessionCreateModel, SecuritySession>()
                .ForMember(d => d.Settings,
                    s => s.MapFrom(x => JsonConvert.SerializeObject(x.Settings)));

            CreateMap<SecurityUserModel, SecurityUser>()
                .ForMember(d => d.Settings,
                    s => s.MapFrom(x => JsonConvert.SerializeObject(x.Settings)));
            CreateMap<SecurityUserCreateModel, SecurityUser>()
                .ForMember(d => d.Settings,
                    s => s.MapFrom(x => JsonConvert.SerializeObject(x.Settings)));
            CreateMap<SecurityUserUpdateModel, SecurityUser>()
                .ForMember(d => d.Settings,
                    s => s.MapFrom(x => JsonConvert.SerializeObject(x.Settings)));

            CreateMap<SettingActionModel, SettingAction>()
                .ForMember(d => d.Settings, s => s.MapFrom(x => JsonConvert.SerializeObject(x.Settings)));
            CreateMap<SettingActionCreateModel, SettingAction>()
                .ForMember(d => d.Settings, s => s.MapFrom(x => JsonConvert.SerializeObject(x.Settings)));
            CreateMap<SettingActionUpdateModel, SettingAction>()
                .ForMember(d => d.Settings, s => s.MapFrom(x => JsonConvert.SerializeObject(x.Settings)));

            CreateMap<SettingActionInstanceModel, SettingActionInstance>()
                .ForMember(d => d.Settings, s => s.MapFrom(x => JsonConvert.SerializeObject(x.Settings)));
            CreateMap<SettingActionInstanceCreateModel, SettingActionInstance>()
                .ForMember(d => d.Settings, s => s.MapFrom(x => JsonConvert.SerializeObject(x.Settings)));
            CreateMap<SettingActionInstanceUpdateModel, SettingActionInstance>()
                .ForMember(d => d.Settings, s => s.MapFrom(x => JsonConvert.SerializeObject(x.Settings)));

            CreateMap<SettingAdvancedModel, SettingAdvanced>();
            CreateMap<SettingAdvancedCreateModel, SettingAdvanced>();
            CreateMap<SettingAdvancedUpdateModel, SettingAdvanced>();

            CreateMap<SettingFeatureModel, SettingFeature>()
                .ForMember(d => d.Settings, s => s.MapFrom(x => JsonConvert.SerializeObject(x.Settings)));
            CreateMap<SettingFeatureCreateModel, SettingFeature>()
                .ForMember(d => d.Settings, s => s.MapFrom(x => JsonConvert.SerializeObject(x.Settings)));
            CreateMap<SettingFeatureUpdateModel, SettingFeature>()
                .ForMember(d => d.Settings, s => s.MapFrom(x => JsonConvert.SerializeObject(x.Settings)));

            CreateMap<SettingFieldModel, SettingField>()
                .ForMember(d => d.Settings,
                    s => s.MapFrom(x => JsonConvert.SerializeObject(x.Settings)))
                .ForMember(d => d.AllowFilter, s => s.MapFrom(x => x.AllowFilter ? 1 : 0))
                .ForMember(d => d.AllowSummary, s => s.MapFrom(x => x.AllowSummary ? 1 : 0));
            CreateMap<SettingFieldCreateModel, SettingField>()
                .ForMember(d => d.Settings,
                    s => s.MapFrom(x => JsonConvert.SerializeObject(x.Settings)));
            CreateMap<SettingFieldUpdateModel, SettingField>()
                .ForMember(d => d.Settings,
                    s => s.MapFrom(x => JsonConvert.SerializeObject(x.Settings)));

            CreateMap<SettingFieldInstanceModel, SettingFieldInstance>()
                .ForMember(d => d.Settings,
                    s => s.MapFrom(x => JsonConvert.SerializeObject(x.Settings)));
            CreateMap<SettingFieldInstanceCreateModel, SettingFieldInstance>()
                .ForMember(d => d.Settings,
                    s => s.MapFrom(x => JsonConvert.SerializeObject(x.Settings)));
            CreateMap<SettingFieldInstanceUpdateModel, SettingFieldInstance>()
                .ForMember(d => d.Settings,
                    s => s.MapFrom(x => JsonConvert.SerializeObject(x.Settings)));

            CreateMap<SettingFilterModel, SettingFilter>();
            CreateMap<SettingFilterCreateModel, SettingFilter>();
            CreateMap<SettingFilterUpdateModel, SettingFilter>();

            CreateMap<SettingFormModel, SettingForm>()
                .ForMember(d => d.Settings,
                    s => s.MapFrom(x => JsonConvert.SerializeObject(x.Settings)));
            CreateMap<SettingFormCreateModel, SettingForm>()
                .ForMember(d => d.Settings,
                    s => s.MapFrom(x => JsonConvert.SerializeObject(x.Settings)));
            CreateMap<SettingFormUpdateModel, SettingForm>()
                .ForMember(d => d.Settings,
                    s => s.MapFrom(x => JsonConvert.SerializeObject(x.Settings)));

            CreateMap<SettingReportModel, SettingReport>()
                .ForMember(d => d.Settings,
                    s => s.MapFrom(x => JsonConvert.SerializeObject(x.Settings)));
            CreateMap<SettingReportCreateModel, SettingReport>()
                .ForMember(d => d.Settings,
                    s => s.MapFrom(x => JsonConvert.SerializeObject(x.Settings)));
            CreateMap<SettingReportUpdateModel, SettingReport>()
                .ForMember(d => d.Settings,
                    s => s.MapFrom(x => JsonConvert.SerializeObject(x.Settings)));

            CreateMap<SettingTableModel, SettingTable>()
                .ForMember(d => d.Settings,
                    s => s.MapFrom(x => JsonConvert.SerializeObject(x.Settings)));
            CreateMap<SettingTableCreateModel, SettingTable>()
                .ForMember(d => d.Settings,
                    s => s.MapFrom(x => JsonConvert.SerializeObject(x.Settings)));
            CreateMap<SettingTableUpdateModel, SettingTable>()
                .ForMember(d => d.Settings,
                    s => s.MapFrom(x => JsonConvert.SerializeObject(x.Settings)));

            CreateMap<SettingViewModel, SettingView>()
                .ForMember(d => d.Settings,
                    s => s.MapFrom(x => JsonConvert.SerializeObject(x.Settings)));
            CreateMap<SettingViewCreateModel, SettingView>()
                .ForMember(d => d.Settings,
                    s => s.MapFrom(x => JsonConvert.SerializeObject(x.Settings)));
            CreateMap<SettingViewUpdateModel, SettingView>()
                .ForMember(d => d.Settings,
                    s => s.MapFrom(x => JsonConvert.SerializeObject(x.Settings)));

            CreateMap<SettingApiModel, SettingApi>()
                .ForMember(d => d.Settings,
                    s => s.MapFrom(x => JsonConvert.SerializeObject(x.Settings)));
            CreateMap<SettingApiCreateModel, SettingApi>()
                .ForMember(d => d.Settings, 
                    s => s.MapFrom(x => JsonConvert.SerializeObject(x.Settings)));
            CreateMap<SettingApiUpdateModel, SettingApi>()
                .ForMember(d => d.Settings, s => s.MapFrom(x => x.Settings));
        }
    }
}