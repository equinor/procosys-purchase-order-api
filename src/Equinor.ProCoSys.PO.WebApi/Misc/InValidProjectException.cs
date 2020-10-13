using System;

namespace Equinor.ProCoSys.PO.WebApi.Misc
{
    public class InValidProjectException : Exception
    {
        public InValidProjectException(string error) : base(error)
        {
        }
    }
}
