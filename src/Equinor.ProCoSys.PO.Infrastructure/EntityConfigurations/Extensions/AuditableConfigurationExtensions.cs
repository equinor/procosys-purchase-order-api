﻿using Equinor.ProCoSys.PO.Domain.AggregateModels.PersonAggregate;
using Equinor.ProCoSys.PO.Domain.Audit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Equinor.ProCoSys.PO.Infrastructure.EntityConfigurations.Extensions
{
    public static class AuditableConfigurationExtensions
    {
        public static void ConfigureCreationAudit<TEntity>(this EntityTypeBuilder<TEntity> builder) where TEntity : class, ICreationAuditable
        {
            builder
                .Property(x => x.CreatedAtUtc)
                .HasConversion(POContext.DateTimeKindConverter);

            builder
                .HasOne<Person>()
                .WithMany()
                .HasForeignKey(x => x.CreatedById)
                .OnDelete(DeleteBehavior.NoAction);
        }

        public static void ConfigureModificationAudit<TEntity>(this EntityTypeBuilder<TEntity> builder) where TEntity : class, IModificationAuditable
        {
            builder
                .Property(x => x.ModifiedAtUtc)
                .HasConversion(POContext.DateTimeKindConverter);

            builder
                .HasOne<Person>()
                .WithMany()
                .HasForeignKey(x => x.ModifiedById)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
