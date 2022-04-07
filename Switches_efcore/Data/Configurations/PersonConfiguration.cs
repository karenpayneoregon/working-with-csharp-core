﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Person = SwitchExpressions_efcore.Models.Person;

#nullable disable

namespace SwitchExpressions_efcore.Data.Configurations
{
    public class PersonConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> entity)
        {
            entity.Property(e => e.Discriminator)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(e => e.EnrollmentDate).HasColumnType("datetime");

            entity.Property(e => e.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(e => e.HireDate).HasColumnType("datetime");

            entity.Property(e => e.LastName)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}
