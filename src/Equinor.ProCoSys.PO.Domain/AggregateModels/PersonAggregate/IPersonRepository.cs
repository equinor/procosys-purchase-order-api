using System;
using System.Threading.Tasks;

namespace Equinor.ProCoSys.PO.Domain.AggregateModels.PersonAggregate
{
    public interface IPersonRepository : IRepository<Person>
    {
        Task<Person> GetByOidAsync(Guid oid);
    }
}
