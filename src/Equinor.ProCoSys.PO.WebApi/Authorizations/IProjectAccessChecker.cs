namespace Equinor.ProCoSys.PO.WebApi.Authorizations
{
    public interface IProjectAccessChecker
    {
        bool HasCurrentUserAccessToProject(string projectName);
    }
}
