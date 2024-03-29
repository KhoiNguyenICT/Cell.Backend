﻿using System;
using System.Collections.Generic;

namespace Cell.Application.Api.Commands
{
    public class SettingFeatureCommand : Command
    {
        public string Icon { get; set; }

        public int IndexLeft { get; set; }

        public int IndexRight { get; set; }

        public int IsLeaf { get; set; }

        public int NodeLevel { get; set; }

        public Guid Parent { get; set; }

        public string PathCode { get; set; }

        public string PathId { get; set; }

        public SettingFeatureSettings Settings { get; set; }

        public List<SettingFeatureCommand> Children { get; set; }
    }

    public class SettingFeatureSettings
    {
        public string MenuType { get; set; }
        public List<SettingFeatureSettingInitParam> InitParams { get; set; }
        public ContainerSettings ContainerSettings { get; set; }
    }

    public class ContainerSettings
    {
        public string Table { get; set; }
        public string ComponentType { get; set; }
        public ComponentItem ComponentItem { get; set; }
    }

    public class ComponentItem
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<SettingFeatureSettingInitParam> InitParams { get; set; }
    }

    public class SettingFeatureSettingInitParam
    {
        public string Name { get; set; }
        public string Param { get; set; }
        public string Source { get; set; }
        public string DataType { get; set; }
        public string DefaultValue { get; set; }
    }
}