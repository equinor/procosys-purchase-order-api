using System;
using System.Threading.Tasks;
using Equinor.ProCoSys.PO.Command;
using Equinor.ProCoSys.PO.Domain;
using Equinor.ProCoSys.PO.Query;
using Equinor.ProCoSys.PO.WebApi.Misc;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Equinor.ProCoSys.PO.WebApi.Authorizations
{
    public class AccessValidator : IAccessValidator
    {
        private readonly ICurrentUserProvider _currentUserProvider;
        private readonly IProjectAccessChecker _projectAccessChecker;
        private readonly IContentRestrictionsChecker _contentRestrictionsChecker;
        private readonly ILogger<AccessValidator> _logger;

        public AccessValidator(
            ICurrentUserProvider currentUserProvider, 
            IProjectAccessChecker projectAccessChecker,
            IContentRestrictionsChecker contentRestrictionsChecker,
            ILogger<AccessValidator> logger)
        {
            _currentUserProvider = currentUserProvider;
            _projectAccessChecker = projectAccessChecker;
            _contentRestrictionsChecker = contentRestrictionsChecker;
            _logger = logger;
        }

        public async Task<bool> ValidateAsync<TRequest>(TRequest request) where TRequest : IBaseRequest
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var userOid = _currentUserProvider.GetCurrentUserOid();
            if (request is IProjectRequest projectRequest && !_projectAccessChecker.HasCurrentUserAccessToProject(projectRequest.ProjectName))
            {
                _logger.LogWarning($"Current user {userOid} don't have access to project {projectRequest.ProjectName}");
                return false;
            }


            if (request is IPurchaseOrderCommandRequest poCommandRequest)
            {
                var projectName = ""; // todo await _poHelper.GetProjectNameAsync(poCommandRequest.PurchaseOrderId);
                var accessToProject = _projectAccessChecker.HasCurrentUserAccessToProject(projectName);

                if (!accessToProject)
                {
                    _logger.LogWarning($"Current user {userOid} don't have access to project {projectName}");
                }

                var accessToContent = await HasCurrentUserAccessToContentAsync(poCommandRequest);
                if (!accessToContent)
                {
                    _logger.LogWarning($"Current user {userOid} don't have access to content {poCommandRequest.PurchaseOrderId}");
                }
                return accessToProject && accessToContent;
            }

            if (request is IPurchaseOrderQueryRequest poQueryRequest)
            {
                var projectName = ""; // todo await _poHelper.GetProjectNameAsync(poQueryRequest.PurchaseOrderId);
                if (!_projectAccessChecker.HasCurrentUserAccessToProject(projectName))
                {
                    _logger.LogWarning($"Current user {userOid} don't have access to project {projectName}");
                    return false;
                }
            }

            return true;
        }

        private async Task<bool> HasCurrentUserAccessToContentAsync(IPurchaseOrderCommandRequest poCommandRequest)
        {
            if (_contentRestrictionsChecker.HasCurrentUserExplicitNoRestrictions())
            {
                return true;
            }

            var responsibleCode = ""; // todo await _poHelper.GetResponsibleCodeAsync(poCommandRequest.PurchaseOrderId);
            return _contentRestrictionsChecker.HasCurrentUserExplicitAccessToContent(responsibleCode);
        }
    }
}
