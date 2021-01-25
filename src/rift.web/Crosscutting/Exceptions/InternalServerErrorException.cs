using rift.Web.Crosscutting.Constants;

namespace rift.Web.Crosscutting.Exceptions
{
    public class InternalServerErrorException : BaseException
    {
        public InternalServerErrorException(string message) : base(ErrorConstants.DefaultType, message)
        {
        }
    }
}
