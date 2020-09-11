﻿using Equinor.ProCoSys.PO.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Equinor.ProCoSys.PO.Infrastructure.EntityConfigurations.Extensions
{
    public static class PlantConfigurationExtensions
    {
        public static void ConfigurePlant<TEntity>(this EntityTypeBuilder<TEntity> builder) where TEntity : PlantEntityBase =>
            builder.Property(x => x.Plant)
                .HasMaxLength(PlantEntityBase.PlantLengthMax)
                .IsRequired();
    }
}
