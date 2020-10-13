using System.Threading.Tasks;
using MediatR;

namespace Equinor.ProCoSys.PO.WebApi.Misc
{
    public interface IProjectChecker
    {
        Task EnsureValidProjectAsync<TRequest>(TRequest request) where TRequest: IBaseRequest;
    }
}
