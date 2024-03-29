﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore;
using NorthWind2022CoreLibrary.Data;
using NorthWind2022CoreLibrary.Models;
using System;

namespace NorthWind2022CoreLibrary.Data.Configurations
{
    public partial class EmployeeTerritoriesConfiguration : IEntityTypeConfiguration<EmployeeTerritories>
    {
        public void Configure(EntityTypeBuilder<EmployeeTerritories> entity)
        {
            entity.HasKey(e => new { e.EmployeeId, e.TerritoryId })
                .IsClustered(false);

            entity.HasIndex(e => e.TerritoryId, "IX_EmployeeTerritories_TerritoryID");

            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");

            entity.Property(e => e.TerritoryId)
                .HasMaxLength(20)
                .HasColumnName("TerritoryID");

            entity.HasOne(d => d.Employee)
                .WithMany(p => p.EmployeeTerritories)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EmployeeTerritories_Employees");

            entity.HasOne(d => d.Territory)
                .WithMany(p => p.EmployeeTerritories)
                .HasForeignKey(d => d.TerritoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EmployeeTerritories_Territories");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<EmployeeTerritories> entity);
    }
}
