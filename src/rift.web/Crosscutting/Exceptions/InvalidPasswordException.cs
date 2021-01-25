using rift.Web.Crosscutting.Constants;

namespace rift.Web.Crosscutting.Exceptions
{
    public class InvalidPasswordException : BaseException
    {
        public InvalidPasswordException() : base(ErrorConstants.InvalidPasswordType, "Incorrect Password")
        {
            //Status = StatusCodes.Status400BadRequest
        }
    }
}
