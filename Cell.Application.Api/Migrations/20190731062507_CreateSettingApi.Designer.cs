﻿// <auto-generated />
using System;
using Cell.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Cell.Application.Api.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20190731062507_CreateSettingApi")]
    partial class CreateSettingApi
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Cell.Model.Entities.SecurityGroupEntity.SecurityGroup", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID");

                    b.Property<string>("Code")
                        .HasColumnName("CODE");

                    b.Property<DateTimeOffset>("Created")
                        .HasColumnName("CREATED");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnName("CREATED_BY");

                    b.Property<string>("Data")
                        .HasColumnName("DATA")
                        .HasColumnType("xml");

                    b.Property<string>("Description")
                        .HasColumnName("DESCRIPTION");

                    b.Property<int>("IndexLeft")
                        .HasColumnName("INDEX_LEFT");

                    b.Property<int>("IndexRight")
                        .HasColumnName("INDEX_RIGHT");

                    b.Property<int>("IsLeaf")
                        .HasColumnName("IS_LEAF");

                    b.Property<DateTimeOffset>("Modified")
                        .HasColumnName("MODIFIED");

                    b.Property<Guid>("ModifiedBy")
                        .HasColumnName("MODIFIED_BY");

                    b.Property<string>("Name")
                        .HasColumnName("NAME");

                    b.Property<int>("NodeLevel")
                        .HasColumnName("NODE_LEVEL");

                    b.Property<Guid?>("Parent")
                        .HasColumnName("PARENT");

                    b.Property<string>("PathCode")
                        .HasColumnName("PATH_CODE")
                        .HasMaxLength(1000);

                    b.Property<string>("PathId")
                        .HasColumnName("PATH_ID")
                        .HasMaxLength(1000);

                    b.Property<string>("Settings")
                        .HasColumnName("SETTINGS");

                    b.Property<Guid>("Status")
                        .HasColumnName("STATUS");

                    b.Property<int>("Version")
                        .HasColumnName("VERSION");

                    b.HasKey("Id");

                    b.ToTable("T_SECURITY_GROUP");
                });

            modelBuilder.Entity("Cell.Model.Entities.SecurityPermissionEntity.SecurityPermission", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID");

                    b.Property<Guid>("AuthorizedId")
                        .HasColumnName("AUTHORIZED_ID");

                    b.Property<string>("AuthorizedType")
                        .HasColumnName("AUTHORIZED_TYPE")
                        .HasMaxLength(200);

                    b.Property<string>("Code")
                        .HasColumnName("CODE");

                    b.Property<DateTimeOffset>("Created")
                        .HasColumnName("CREATED");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnName("CREATED_BY");

                    b.Property<string>("Data")
                        .HasColumnName("DATA")
                        .HasColumnType("xml");

                    b.Property<string>("Description")
                        .HasColumnName("DESCRIPTION");

                    b.Property<DateTimeOffset>("Modified")
                        .HasColumnName("MODIFIED");

                    b.Property<Guid>("ModifiedBy")
                        .HasColumnName("MODIFIED_BY");

                    b.Property<string>("Name")
                        .HasColumnName("NAME");

                    b.Property<Guid>("ObjectId")
                        .HasColumnName("OBJECT_ID");

                    b.Property<string>("ObjectName")
                        .HasColumnName("OBJECT_NAME")
                        .HasMaxLength(200);

                    b.Property<string>("Settings")
                        .HasColumnName("SETTINGS");

                    b.Property<string>("TableName")
                        .HasColumnName("TABLE_NAME")
                        .HasMaxLength(200);

                    b.Property<int>("Version")
                        .HasColumnName("VERSION");

                    b.HasKey("Id");

                    b.ToTable("T_SECURITY_PERMISSION");
                });

            modelBuilder.Entity("Cell.Model.Entities.SecuritySessionEntity.SecuritySession", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID");

                    b.Property<string>("Code")
                        .HasColumnName("CODE");

                    b.Property<DateTimeOffset>("Created")
                        .HasColumnName("CREATED");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnName("CREATED_BY");

                    b.Property<string>("Data")
                        .HasColumnName("DATA")
                        .HasColumnType("xml");

                    b.Property<string>("Description")
                        .HasColumnName("DESCRIPTION");

                    b.Property<DateTimeOffset>("ExpiredTime")
                        .HasColumnName("EXPIRED_TIME");

                    b.Property<DateTimeOffset>("Modified")
                        .HasColumnName("MODIFIED");

                    b.Property<Guid>("ModifiedBy")
                        .HasColumnName("MODIFIED_BY");

                    b.Property<string>("Name")
                        .HasColumnName("NAME");

                    b.Property<string>("Settings")
                        .HasColumnName("SETTINGS");

                    b.Property<DateTimeOffset>("SigninTime")
                        .HasColumnName("SIGNIN_TIME");

                    b.Property<string>("UserAccount")
                        .HasColumnName("USER_ACCOUNT")
                        .HasMaxLength(200);

                    b.Property<Guid>("UserId")
                        .HasColumnName("USER_ID");

                    b.Property<int>("Version")
                        .HasColumnName("VERSION");

                    b.HasKey("Id");

                    b.ToTable("T_SECURITY_SESSION");
                });

            modelBuilder.Entity("Cell.Model.Entities.SecurityUserEntity.SecurityUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID");

                    b.Property<string>("Account")
                        .HasColumnName("ACCOUNT")
                        .HasMaxLength(200);

                    b.Property<string>("Code")
                        .HasColumnName("CODE");

                    b.Property<DateTimeOffset>("Created")
                        .HasColumnName("CREATED");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnName("CREATED_BY");

                    b.Property<string>("Data")
                        .HasColumnName("DATA")
                        .HasColumnType("xml");

                    b.Property<Guid>("DefaultDepartment")
                        .HasColumnName("DEFAULT_DEPARTMENT");

                    b.Property<Guid>("DefaultRole")
                        .HasColumnName("DEFAULT_ROLE");

                    b.Property<string>("Description")
                        .HasColumnName("DESCRIPTION");

                    b.Property<string>("Email")
                        .HasColumnName("EMAIL")
                        .HasMaxLength(200);

                    b.Property<string>("EncryptedPassword")
                        .HasColumnName("ENCRYPTED_PASSWORD")
                        .HasMaxLength(1000);

                    b.Property<DateTimeOffset>("Modified")
                        .HasColumnName("MODIFIED");

                    b.Property<Guid>("ModifiedBy")
                        .HasColumnName("MODIFIED_BY");

                    b.Property<string>("Name")
                        .HasColumnName("NAME");

                    b.Property<string>("Phone")
                        .HasColumnName("PHONE")
                        .HasMaxLength(50);

                    b.Property<string>("Settings")
                        .HasColumnName("SETTINGS");

                    b.Property<Guid>("Status")
                        .HasColumnName("STATUS");

                    b.Property<int>("Version")
                        .HasColumnName("VERSION");

                    b.HasKey("Id");

                    b.ToTable("T_SECURITY_USER");
                });

            modelBuilder.Entity("Cell.Model.Entities.SettingActionEntity.SettingAction", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID");

                    b.Property<string>("Code")
                        .HasColumnName("CODE");

                    b.Property<string>("ContainerType")
                        .HasColumnName("CONTAINER_TYPE")
                        .HasMaxLength(50);

                    b.Property<DateTimeOffset>("Created")
                        .HasColumnName("CREATED");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnName("CREATED_BY");

                    b.Property<string>("Data")
                        .HasColumnName("DATA")
                        .HasColumnType("xml");

                    b.Property<string>("Description")
                        .HasColumnName("DESCRIPTION");

                    b.Property<DateTimeOffset>("Modified")
                        .HasColumnName("MODIFIED");

                    b.Property<Guid>("ModifiedBy")
                        .HasColumnName("MODIFIED_BY");

                    b.Property<string>("Name")
                        .HasColumnName("NAME");

                    b.Property<string>("Settings")
                        .HasColumnName("SETTINGS");

                    b.Property<Guid>("TableId")
                        .HasColumnName("TABLE_ID");

                    b.Property<string>("TableName")
                        .HasColumnName("TABLE_NAME")
                        .HasMaxLength(200);

                    b.Property<int>("Version")
                        .HasColumnName("VERSION");

                    b.HasKey("Id");

                    b.ToTable("T_SETTING_ACTION");
                });

            modelBuilder.Entity("Cell.Model.Entities.SettingActionInstanceEntity.SettingActionInstance", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID");

                    b.Property<Guid>("ActionId")
                        .HasColumnName("ACTION_ID");

                    b.Property<string>("Code")
                        .HasColumnName("CODE");

                    b.Property<string>("ContainerType")
                        .HasColumnName("CONTAINER_TYPE")
                        .HasMaxLength(50);

                    b.Property<DateTimeOffset>("Created")
                        .HasColumnName("CREATED");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnName("CREATED_BY");

                    b.Property<string>("Data")
                        .HasColumnName("DATA")
                        .HasColumnType("xml");

                    b.Property<string>("Description")
                        .HasColumnName("DESCRIPTION");

                    b.Property<DateTimeOffset>("Modified")
                        .HasColumnName("MODIFIED");

                    b.Property<Guid>("ModifiedBy")
                        .HasColumnName("MODIFIED_BY");

                    b.Property<string>("Name")
                        .HasColumnName("NAME");

                    b.Property<int>("OrdinalPosition")
                        .HasColumnName("ORDINAL_POSITION");

                    b.Property<Guid>("Parent")
                        .HasColumnName("PARENT");

                    b.Property<string>("ParentText")
                        .HasColumnName("PARENT_TEXT");

                    b.Property<string>("Settings")
                        .HasColumnName("SETTINGS");

                    b.Property<Guid>("TableId")
                        .HasColumnName("TABLE_ID");

                    b.Property<string>("TableName")
                        .HasColumnName("TABLE_NAME")
                        .HasMaxLength(200);

                    b.Property<int>("Version")
                        .HasColumnName("VERSION");

                    b.HasKey("Id");

                    b.ToTable("T_SETTING_ACTION_INSTANCE");
                });

            modelBuilder.Entity("Cell.Model.Entities.SettingAdvancedEntity.SettingAdvanced", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID");

                    b.Property<string>("Code")
                        .HasColumnName("CODE");

                    b.Property<DateTimeOffset>("Created")
                        .HasColumnName("CREATED");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnName("CREATED_BY");

                    b.Property<string>("Data")
                        .HasColumnName("DATA")
                        .HasColumnType("xml");

                    b.Property<string>("Description")
                        .HasColumnName("DESCRIPTION");

                    b.Property<int>("IndexLeft")
                        .HasColumnName("INDEX_LEFT");

                    b.Property<int>("IndexRight")
                        .HasColumnName("INDEX_RIGHT");

                    b.Property<int>("IsLeaf")
                        .HasColumnName("IS_LEAF");

                    b.Property<DateTimeOffset>("Modified")
                        .HasColumnName("MODIFIED");

                    b.Property<Guid>("ModifiedBy")
                        .HasColumnName("MODIFIED_BY");

                    b.Property<string>("Name")
                        .HasColumnName("NAME");

                    b.Property<int>("NodeLevel")
                        .HasColumnName("NODE_LEVEL");

                    b.Property<Guid?>("Parent")
                        .HasColumnName("PARENT");

                    b.Property<string>("PathCode")
                        .HasColumnName("PATH_CODE")
                        .HasMaxLength(1000);

                    b.Property<string>("PathId")
                        .HasColumnName("PATH_ID")
                        .HasMaxLength(1000);

                    b.Property<string>("SettingValue")
                        .HasColumnName("SETTING_VALUE");

                    b.Property<int>("Version")
                        .HasColumnName("VERSION");

                    b.HasKey("Id");

                    b.ToTable("T_SETTING_ADVANCED");
                });

            modelBuilder.Entity("Cell.Model.Entities.SettingApiEntity.SettingApi", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID");

                    b.Property<string>("Code")
                        .HasColumnName("CODE");

                    b.Property<DateTimeOffset>("Created")
                        .HasColumnName("CREATED");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnName("CREATED_BY");

                    b.Property<string>("Data")
                        .HasColumnName("DATA")
                        .HasColumnType("xml");

                    b.Property<string>("Description")
                        .HasColumnName("DESCRIPTION");

                    b.Property<string>("Library")
                        .HasColumnName("LIBRARY");

                    b.Property<string>("Method")
                        .HasColumnName("METHOD");

                    b.Property<DateTimeOffset>("Modified")
                        .HasColumnName("MODIFIED");

                    b.Property<Guid>("ModifiedBy")
                        .HasColumnName("MODIFIED_BY");

                    b.Property<string>("Name")
                        .HasColumnName("NAME");

                    b.Property<string>("Settings")
                        .HasColumnName("SETTINGS");

                    b.Property<Guid>("TableId")
                        .HasColumnName("TABLE_ID");

                    b.Property<string>("TableName")
                        .HasColumnName("TABLE_NAME");

                    b.Property<int>("Version")
                        .HasColumnName("VERSION");

                    b.HasKey("Id");

                    b.ToTable("T_SETTING_API");
                });

            modelBuilder.Entity("Cell.Model.Entities.SettingFeatureEntity.SettingFeature", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID");

                    b.Property<string>("Code")
                        .HasColumnName("CODE");

                    b.Property<DateTimeOffset>("Created")
                        .HasColumnName("CREATED");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnName("CREATED_BY");

                    b.Property<string>("Data")
                        .HasColumnName("DATA")
                        .HasColumnType("xml");

                    b.Property<string>("Description")
                        .HasColumnName("DESCRIPTION");

                    b.Property<string>("Icon")
                        .HasColumnName("ICON")
                        .HasMaxLength(50);

                    b.Property<int>("IndexLeft")
                        .HasColumnName("INDEX_LEFT");

                    b.Property<int>("IndexRight")
                        .HasColumnName("INDEX_RIGHT");

                    b.Property<int>("IsLeaf")
                        .HasColumnName("IS_LEAF");

                    b.Property<DateTimeOffset>("Modified")
                        .HasColumnName("MODIFIED");

                    b.Property<Guid>("ModifiedBy")
                        .HasColumnName("MODIFIED_BY");

                    b.Property<string>("Name")
                        .HasColumnName("NAME");

                    b.Property<int>("NodeLevel")
                        .HasColumnName("NODE_LEVEL");

                    b.Property<Guid?>("Parent")
                        .HasColumnName("PARENT");

                    b.Property<string>("PathCode")
                        .HasColumnName("PATH_CODE")
                        .HasMaxLength(1000);

                    b.Property<string>("PathId")
                        .HasColumnName("PATH_ID")
                        .HasMaxLength(1000);

                    b.Property<string>("Settings")
                        .HasColumnName("SETTINGS");

                    b.Property<int>("Version")
                        .HasColumnName("VERSION");

                    b.HasKey("Id");

                    b.ToTable("T_SETTING_FEATURE");
                });

            modelBuilder.Entity("Cell.Model.Entities.SettingFieldEntity.SettingField", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID");

                    b.Property<bool>("AllowFilter")
                        .HasColumnName("ALLOW_FILTER");

                    b.Property<bool>("AllowSummary")
                        .HasColumnName("ALLOW_SUMMARY");

                    b.Property<string>("Caption")
                        .HasColumnName("CAPTION")
                        .HasMaxLength(200);

                    b.Property<string>("Code")
                        .HasColumnName("CODE");

                    b.Property<DateTimeOffset>("Created")
                        .HasColumnName("CREATED");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnName("CREATED_BY");

                    b.Property<string>("Data")
                        .HasColumnName("DATA")
                        .HasColumnType("xml");

                    b.Property<string>("DataType")
                        .HasColumnName("DATA_TYPE")
                        .HasMaxLength(50);

                    b.Property<string>("Description")
                        .HasColumnName("DESCRIPTION");

                    b.Property<DateTimeOffset>("Modified")
                        .HasColumnName("MODIFIED");

                    b.Property<Guid>("ModifiedBy")
                        .HasColumnName("MODIFIED_BY");

                    b.Property<string>("Name")
                        .HasColumnName("NAME");

                    b.Property<int>("OrdinalPosition")
                        .HasColumnName("ORDINAL_POSITION");

                    b.Property<string>("PlaceHolder")
                        .HasColumnName("PLACE_HOLDER")
                        .HasMaxLength(200);

                    b.Property<string>("Settings")
                        .HasColumnName("SETTINGS");

                    b.Property<string>("StorageType")
                        .HasColumnName("STORAGE_TYPE")
                        .HasMaxLength(50);

                    b.Property<Guid>("TableId")
                        .HasColumnName("TABLE_ID");

                    b.Property<string>("TableName")
                        .HasColumnName("TABLE_NAME")
                        .HasMaxLength(200);

                    b.Property<int>("Version")
                        .HasColumnName("VERSION");

                    b.HasKey("Id");

                    b.ToTable("T_SETTING_FIELD");
                });

            modelBuilder.Entity("Cell.Model.Entities.SettingFieldInstanceEntity.SettingFieldInstance", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID");

                    b.Property<string>("Caption")
                        .HasColumnName("CAPTION")
                        .HasMaxLength(200);

                    b.Property<string>("Code")
                        .HasColumnName("CODE");

                    b.Property<string>("ContainerType")
                        .HasColumnName("CONTAINER_TYPE")
                        .HasMaxLength(50);

                    b.Property<DateTimeOffset>("Created")
                        .HasColumnName("CREATED");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnName("CREATED_BY");

                    b.Property<string>("Data")
                        .HasColumnName("DATA")
                        .HasColumnType("xml");

                    b.Property<string>("DataType")
                        .HasColumnName("DATA_TYPE")
                        .HasMaxLength(50);

                    b.Property<string>("Description")
                        .HasColumnName("DESCRIPTION");

                    b.Property<Guid>("FieldId")
                        .HasColumnName("FIELD_ID");

                    b.Property<DateTimeOffset>("Modified")
                        .HasColumnName("MODIFIED");

                    b.Property<Guid>("ModifiedBy")
                        .HasColumnName("MODIFIED_BY");

                    b.Property<string>("Name")
                        .HasColumnName("NAME");

                    b.Property<int>("OrdinalPosition")
                        .HasColumnName("ORDINAL_POSITION");

                    b.Property<Guid>("Parent")
                        .HasColumnName("PARENT");

                    b.Property<string>("ParentText")
                        .HasColumnName("PARENT_TEXT");

                    b.Property<string>("Settings")
                        .HasColumnName("SETTINGS");

                    b.Property<string>("StorageType")
                        .HasColumnName("STORAGE_TYPE")
                        .HasMaxLength(50);

                    b.Property<int>("Version")
                        .HasColumnName("VERSION");

                    b.HasKey("Id");

                    b.ToTable("T_SETTING_FIELD_INSTANCE");
                });

            modelBuilder.Entity("Cell.Model.Entities.SettingFilterEntity.SettingFilter", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID");

                    b.Property<string>("Code")
                        .HasColumnName("CODE");

                    b.Property<DateTimeOffset>("Created")
                        .HasColumnName("CREATED");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnName("CREATED_BY");

                    b.Property<string>("Data")
                        .HasColumnName("DATA")
                        .HasColumnType("xml");

                    b.Property<string>("Description")
                        .HasColumnName("DESCRIPTION");

                    b.Property<DateTimeOffset>("Modified")
                        .HasColumnName("MODIFIED");

                    b.Property<Guid>("ModifiedBy")
                        .HasColumnName("MODIFIED_BY");

                    b.Property<string>("Name")
                        .HasColumnName("NAME");

                    b.Property<string>("Settings")
                        .HasColumnName("SETTINGS");

                    b.Property<Guid>("TableId")
                        .HasColumnName("TABLE_ID");

                    b.Property<string>("TableIdText")
                        .HasColumnName("TABLE_ID_TEXT");

                    b.Property<string>("TableName")
                        .HasColumnName("TABLE_NAME")
                        .HasMaxLength(200);

                    b.Property<int>("Version")
                        .HasColumnName("VERSION");

                    b.HasKey("Id");

                    b.ToTable("T_SETTING_FILTER");
                });

            modelBuilder.Entity("Cell.Model.Entities.SettingFormEntity.SettingForm", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID");

                    b.Property<string>("Code")
                        .HasColumnName("CODE");

                    b.Property<DateTimeOffset>("Created")
                        .HasColumnName("CREATED");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnName("CREATED_BY");

                    b.Property<string>("Data")
                        .HasColumnName("DATA")
                        .HasColumnType("xml");

                    b.Property<string>("Description")
                        .HasColumnName("DESCRIPTION");

                    b.Property<Guid>("LayoutId")
                        .HasColumnName("LAYOUT_ID");

                    b.Property<DateTimeOffset>("Modified")
                        .HasColumnName("MODIFIED");

                    b.Property<Guid>("ModifiedBy")
                        .HasColumnName("MODIFIED_BY");

                    b.Property<string>("Name")
                        .HasColumnName("NAME");

                    b.Property<string>("Settings")
                        .HasColumnName("SETTINGS");

                    b.Property<Guid>("TableId")
                        .HasColumnName("TABLE_ID");

                    b.Property<string>("TableName")
                        .HasColumnName("TABLE_NAME")
                        .HasMaxLength(200);

                    b.Property<int>("Version")
                        .HasColumnName("VERSION");

                    b.HasKey("Id");

                    b.ToTable("T_SETTING_FORM");
                });

            modelBuilder.Entity("Cell.Model.Entities.SettingReportEntity.SettingReport", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID");

                    b.Property<string>("Code")
                        .HasColumnName("CODE");

                    b.Property<DateTimeOffset>("Created")
                        .HasColumnName("CREATED");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnName("CREATED_BY");

                    b.Property<string>("Data")
                        .HasColumnName("DATA")
                        .HasColumnType("xml");

                    b.Property<string>("Description")
                        .HasColumnName("DESCRIPTION");

                    b.Property<DateTimeOffset>("Modified")
                        .HasColumnName("MODIFIED");

                    b.Property<Guid>("ModifiedBy")
                        .HasColumnName("MODIFIED_BY");

                    b.Property<string>("Name")
                        .HasColumnName("NAME");

                    b.Property<string>("Settings")
                        .HasColumnName("SETTINGS");

                    b.Property<Guid>("TableId")
                        .HasColumnName("TABLE_ID");

                    b.Property<string>("TableIdText")
                        .HasColumnName("TABLE_ID_TEXT");

                    b.Property<string>("TableName")
                        .HasColumnName("TABLE_NAME")
                        .HasMaxLength(200);

                    b.Property<int>("Version")
                        .HasColumnName("VERSION");

                    b.HasKey("Id");

                    b.ToTable("T_SETTING_REPORT");
                });

            modelBuilder.Entity("Cell.Model.Entities.SettingTableEntity.SettingTable", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID");

                    b.Property<string>("BasedTable")
                        .HasColumnName("BASED_TABLE")
                        .HasMaxLength(200);

                    b.Property<string>("Code")
                        .HasColumnName("CODE");

                    b.Property<DateTimeOffset>("Created")
                        .HasColumnName("CREATED");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnName("CREATED_BY");

                    b.Property<string>("Data")
                        .HasColumnName("DATA")
                        .HasColumnType("xml");

                    b.Property<string>("Description")
                        .HasColumnName("DESCRIPTION");

                    b.Property<DateTimeOffset>("Modified")
                        .HasColumnName("MODIFIED");

                    b.Property<Guid>("ModifiedBy")
                        .HasColumnName("MODIFIED_BY");

                    b.Property<string>("Name")
                        .HasColumnName("NAME");

                    b.Property<string>("Settings")
                        .HasColumnName("SETTINGS");

                    b.Property<int>("Version")
                        .HasColumnName("VERSION");

                    b.HasKey("Id");

                    b.ToTable("T_SETTING_TABLE");
                });

            modelBuilder.Entity("Cell.Model.Entities.SettingViewEntity.SettingView", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID");

                    b.Property<string>("Code")
                        .HasColumnName("CODE");

                    b.Property<DateTimeOffset>("Created")
                        .HasColumnName("CREATED");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnName("CREATED_BY");

                    b.Property<string>("Data")
                        .HasColumnName("DATA")
                        .HasColumnType("xml");

                    b.Property<string>("Description")
                        .HasColumnName("DESCRIPTION");

                    b.Property<DateTimeOffset>("Modified")
                        .HasColumnName("MODIFIED");

                    b.Property<Guid>("ModifiedBy")
                        .HasColumnName("MODIFIED_BY");

                    b.Property<string>("Name")
                        .HasColumnName("NAME");

                    b.Property<string>("Settings")
                        .HasColumnName("SETTINGS");

                    b.Property<Guid>("TableId")
                        .HasColumnName("TABLE_ID");

                    b.Property<string>("TableName")
                        .HasColumnName("TABLE_NAME")
                        .HasMaxLength(200);

                    b.Property<int>("Version")
                        .HasColumnName("VERSION");

                    b.HasKey("Id");

                    b.ToTable("T_SETTING_VIEW");
                });
#pragma warning restore 612, 618
        }
    }
}
