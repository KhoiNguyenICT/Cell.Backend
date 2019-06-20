﻿// <auto-generated />
using System;
using Cell.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Cell.Application.Api.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20190619022400_AddParentTextForSettingFieldInstance")]
    partial class AddParentTextForSettingFieldInstance
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Cell.Domain.Aggregates.SettingActionAggregate.SettingAction", b =>
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

            modelBuilder.Entity("Cell.Domain.Aggregates.SettingActionInstanceAggregate.SettingActionInstance", b =>
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

            modelBuilder.Entity("Cell.Domain.Aggregates.SettingFeatureAggregate.SettingFeature", b =>
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

                    b.Property<Guid>("Parent")
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

            modelBuilder.Entity("Cell.Domain.Aggregates.SettingFieldAggregate.SettingField", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID");

                    b.Property<int>("AllowFilter")
                        .HasColumnName("ALLOW_FILTER");

                    b.Property<int>("AllowSummary")
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

            modelBuilder.Entity("Cell.Domain.Aggregates.SettingFieldInstanceAggregate.SettingFieldInstance", b =>
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

            modelBuilder.Entity("Cell.Domain.Aggregates.SettingFormAggregate.SettingForm", b =>
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

            modelBuilder.Entity("Cell.Domain.Aggregates.SettingTableAggregate.SettingTable", b =>
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

            modelBuilder.Entity("Cell.Domain.Aggregates.SettingViewAggregate.SettingView", b =>
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
