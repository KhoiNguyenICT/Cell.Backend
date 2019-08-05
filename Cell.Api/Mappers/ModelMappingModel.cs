using AutoMapper;
using Cell.Model.Models.SecurityGroup;
using Cell.Model.Models.SecurityUser;
using Cell.Model.Models.SettingAction;
using Cell.Model.Models.SettingActionInstance;
using Cell.Model.Models.SettingAdvanced;
using Cell.Model.Models.SettingApi;
using Cell.Model.Models.SettingFeature;
using Cell.Model.Models.SettingField;
using Cell.Model.Models.SettingFieldInstance;
using Cell.Model.Models.SettingForm;
using Cell.Model.Models.SettingReport;
using Cell.Model.Models.SettingTable;
using Cell.Model.Models.SettingView;

namespace Cell.Application.Api.Mappers
{
    public class ModelMappingModel : Profile
    {
        public ModelMappingModel()
        {
            CreateMap<SecurityGroupCreateModel, SecurityGroupModel>();
            CreateMap<SecurityGroupUpdateModel, SecurityGroupModel>();

            CreateMap<SecurityUserCreateModel, SecurityUserModel>();
            CreateMap<SecurityUserUpdateModel, SecurityUserModel>();

            CreateMap<SettingActionCreateModel, SettingActionModel>();
            CreateMap<SettingActionUpdateModel, SettingActionModel>();

            CreateMap<SettingActionInstanceCreateModel, SettingActionInstanceModel>();
            CreateMap<SettingActionInstanceUpdateModel, SettingActionInstanceModel>();

            CreateMap<SettingAdvancedCreateModel, SettingAdvancedModel>();
            CreateMap<SettingAdvancedUpdateModel, SettingAdvancedModel>();

            CreateMap<SettingFeatureCreateModel, SettingFeatureModel>();
            CreateMap<SettingFeatureUpdateModel, SettingFeatureModel>();

            CreateMap<SettingFieldCreateModel, SettingFieldModel>();
            CreateMap<SettingFieldUpdateModel, SettingFieldModel>();

            CreateMap<SettingFieldInstanceCreateModel, SettingFieldInstanceModel>();
            CreateMap<SettingFieldInstanceUpdateModel, SettingFieldInstanceModel>();

            CreateMap<SettingFormCreateModel, SettingFormModel>();
            CreateMap<SettingFormUpdateModel, SettingFormModel>();

            CreateMap<SettingReportCreateModel, SettingReportModel>();
            CreateMap<SettingReportUpdateModel, SettingReportModel>();

            CreateMap<SettingTableCreateModel, SettingTableModel>();
            CreateMap<SettingTableUpdateModel, SettingTableModel>();

            CreateMap<SettingViewCreateModel, SettingViewModel>();
            CreateMap<SettingViewUpdateModel, SettingViewModel>();

            CreateMap<SettingApiCreateModel, SettingApiModel>();
            CreateMap<SettingApiUpdateModel, SettingApiUpdateModel>();
        }
    }
}