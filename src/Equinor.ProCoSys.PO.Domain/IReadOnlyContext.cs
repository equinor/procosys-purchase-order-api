﻿using System.Linq;

namespace Equinor.ProCoSys.PO.Domain
{
    public interface IReadOnlyContext
    {
        IQueryable<TEntity> QuerySet<TEntity>() where TEntity : class;
    }
}
