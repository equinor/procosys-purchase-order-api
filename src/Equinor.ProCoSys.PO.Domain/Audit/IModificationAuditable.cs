﻿using System;
using Equinor.ProCoSys.PO.Domain.AggregateModels.PersonAggregate;

namespace Equinor.ProCoSys.PO.Domain.Audit
{
    /// <summary>
    /// Interface to get and set creation data on an entity.
    /// The methods are used by the context and should NOT be used by anyone else.
    /// </summary>
    public interface IModificationAuditable
    {
        DateTime? ModifiedAtUtc { get; }
        int? ModifiedById { get; }

        /// <summary>
        /// Method to set modification data on an entity.
        /// This is used by the context and should NOT be used by anyone else.
        /// </summary>
        /// <param name="modifiedAtUtc">Modification date and time</param>
        /// <param name="modifiedBy">The user who modified the entity</param>
        void SetModified(Person modifiedBy);
    }
}
