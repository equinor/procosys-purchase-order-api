namespace Equinor.ProCoSys.PO.WebApi.Misc
{
    public interface IBearerTokenSetter
    {
        void SetBearerToken(string token, bool isUserToken = true);
    }
}
