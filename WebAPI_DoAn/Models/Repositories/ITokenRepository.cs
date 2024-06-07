using Microsoft.AspNetCore.Identity;

namespace WebAPI_DoAn.Models.Repositories
{
    public interface ITokenRepository
    {
        string CreateJWTToken(IdentityUser user, List<string> roles);
    }
}
