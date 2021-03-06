using Generic_POS_System.Mdoels;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Generic_POS_System.Repository
{
    public interface IAccountRepository
    {
        Task<IdentityResult> CreateUserAsync(SignUpCustomerModel userModel);

        Task<SignInResult> PasswordSignInAsync(LoginModel logInModel);

        Task<IdentityResult> AddUserAsync(SignUpUserModel userModel2);

        Task SignOutAsync();

        /*Task<List<RolesModel>> GetRolesUserAsync();*/
    }
}