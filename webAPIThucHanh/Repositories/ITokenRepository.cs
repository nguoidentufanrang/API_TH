using Microsoft.AspNetCore.Identity;

namespace webAPIThucHanh.Repositories
{
	public interface ITokenRepository
	{
		string CreateJWTToken(IdentityUser user, List<string> roles);
	}
}
