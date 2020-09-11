namespace Equinor.ProCoSys.PO.Command.Validators
{
    public interface IRowVersionValidator
    {
        bool IsValid(string rowVersion);
    }
}
