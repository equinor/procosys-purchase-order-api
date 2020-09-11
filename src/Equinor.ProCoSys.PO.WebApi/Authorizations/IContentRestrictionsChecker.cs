namespace Equinor.ProCoSys.PO.WebApi.Authorizations
{
    public interface IContentRestrictionsChecker
    {
        bool HasCurrentUserExplicitNoRestrictions();
        bool HasCurrentUserExplicitAccessToContent(string responsibleCode);
    }
}
