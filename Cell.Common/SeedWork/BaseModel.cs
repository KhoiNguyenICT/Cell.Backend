﻿using System;

namespace Cell.Common.SeedWork
{
    public class BaseModel
    {
        public Guid Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTimeOffset Created { get; set; }

        public Guid CreatedBy { get; set; }

        public DateTimeOffset Modified { get; set; }

        public Guid ModifiedBy { get; set; }

        public int Version { get; set; }
    }
}