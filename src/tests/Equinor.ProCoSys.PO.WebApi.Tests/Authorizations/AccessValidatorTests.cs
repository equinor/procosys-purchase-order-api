using Equinor.ProCoSys.PO.Domain;
using Equinor.ProCoSys.PO.WebApi.Authorizations;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Equinor.ProCoSys.PO.WebApi.Tests.Authorizations
{
    [TestClass]
    public class AccessValidatorTests
    {
        private AccessValidator _dut;
        private Mock<IContentRestrictionsChecker> _contentRestrictionsCheckerMock;
        private Mock<IProjectAccessChecker> _projectAccessCheckerMock;
        private Mock<ILogger<AccessValidator>> _loggerMock;
        private Mock<ICurrentUserProvider> _currentUserProviderMock;
        private const int PurchaseOrderIdWithAccessToProject = 1;
        private const int PurchaseOrderIdWithoutAccessToProject = 2;
        private const string ProjectWithAccess = "TestProjectWithAccess";
        private const string ProjectWithoutAccess = "TestProjectWithoutAccess";
        private const string RestrictedToContent = "ResponsbleA";

        [TestInitialize]
        public void Setup()
        {
            _currentUserProviderMock = new Mock<ICurrentUserProvider>();

            _projectAccessCheckerMock = new Mock<IProjectAccessChecker>();
            _contentRestrictionsCheckerMock = new Mock<IContentRestrictionsChecker>();

            _projectAccessCheckerMock.Setup(p => p.HasCurrentUserAccessToProject(ProjectWithoutAccess)).Returns(false);
            _projectAccessCheckerMock.Setup(p => p.HasCurrentUserAccessToProject(ProjectWithAccess)).Returns(true);

            _contentRestrictionsCheckerMock.Setup(c => c.HasCurrentUserExplicitNoRestrictions()).Returns(true);
            _contentRestrictionsCheckerMock.Setup(c => c.HasCurrentUserExplicitAccessToContent(RestrictedToContent)).Returns(false);

            _loggerMock = new Mock<ILogger<AccessValidator>>();

            _dut = new AccessValidator(
                _currentUserProviderMock.Object,
                _projectAccessCheckerMock.Object,
                _contentRestrictionsCheckerMock.Object,
                _loggerMock.Object);
        }

        // todo Add tests for each Query / Command with security checks. See preservation solution
    }
}
