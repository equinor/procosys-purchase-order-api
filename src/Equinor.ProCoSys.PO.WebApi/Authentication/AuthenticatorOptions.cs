namespace Equinor.ProCoSys.PO.WebApi.Authentication
{
    public class AuthenticatorOptions
    {
        public string Instance { get; set; }

        public string POApiClientId { get; set; }
        public string POApiSecret { get; set; }

        public string MainApiClientId { get; set; }
        public string MainApiSecret { get; set; }
        public string MainApiScope { get; set; }
    }
}
