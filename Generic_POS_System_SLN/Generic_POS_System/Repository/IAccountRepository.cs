using Generic_POS_System.Mdoels;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Generic_POS_System.Repository
{
    public interface IAccountRepository
    {
        Task<IdentityResult> CreateUserAsync(SignUpUserModel userModel);

        Task<SignInResult> PasswordSignInAsync(LoginModel logInModel);

        Task<IdentityResult> AddUserAsync(SignUpUserModel userModel2);

        Task SignOutAsync();
    }
}