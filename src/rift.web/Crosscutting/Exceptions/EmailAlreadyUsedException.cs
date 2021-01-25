using rift.Web.Crosscutting.Constants;

namespace rift.Web.Crosscutting.Exceptions
{
    public class EmailAlreadyUsedException : BadRequestAlertException
    {
        public EmailAlreadyUsedException() : base(ErrorConstants.EmailAlreadyUsedType, "Email is already in use!",
            "userManagement", "emailexists")
        {
        }
    }
}
