﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore;
using NorthWind2022CoreLibrary.Data;
using NorthWind2022CoreLibrary.Models;
using System;

namespace NorthWind2022CoreLibrary.Data.Configurations
{
    public partial class SupplierRegionConfiguration : IEntityTypeConfiguration<SupplierRegion>
    {
        public void Configure(EntityTypeBuilder<SupplierRegion> entity)
        {
            entity.HasKey(e => e.RegionId);

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<SupplierRegion> entity);
    }
}
