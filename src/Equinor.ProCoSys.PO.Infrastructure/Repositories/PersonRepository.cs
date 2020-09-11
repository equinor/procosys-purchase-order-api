using System;
using System.Threading.Tasks;
using Equinor.ProCoSys.PO.Domain.AggregateModels.PersonAggregate;
using Microsoft.EntityFrameworkCore;

namespace Equinor.ProCoSys.PO.Infrastructure.Repositories
{
    public class PersonRepository : RepositoryBase<Person>, IPersonRepository
    {
        public PersonRepository(POContext context)
            : base(context, context.Persons) { }

        public Task<Person> GetByOidAsync(Guid oid)
            => DefaultQuery.SingleOrDefaultAsync(p => p.Oid == oid);
    }
}
