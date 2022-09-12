using System.Security.Claims;

namespace Security.Accessors.UserAccessor
{
    public interface IUserAccessor
    {
        public ClaimsPrincipal User { get; }
    }
}