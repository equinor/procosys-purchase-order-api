using System;

namespace Equinor.ProCoSys.PO.MainApi.Exceptions
{
    public class InvalidResultException : Exception
    {
        public InvalidResultException(string message) : base(message)
        {
        }
    }
}
