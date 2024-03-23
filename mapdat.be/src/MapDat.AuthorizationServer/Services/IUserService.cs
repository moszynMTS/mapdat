using AuthorizationServer.Models;
using MapDat.AuthorizationServer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuthorizationServer.Services
{
    public interface IUserService
    {
        void AddRoleToUser(string roleName, string userId);

        Task<bool> ChangePasswordAsync(ChangePasswordRequestModel model);

        Task<IdentityResult> ChangePasswordAsync(string userId, string currentPassword, string newPassword);

        Task<UserModel> CreateUser(UserModel userModel);

        Task<bool> DeleteUserById(string Id);
        Task<bool> DeleteUserByEmail(string Id);

        Task<string> GeneratePasswordResetToken(string userId);

        Task<IEnumerable<ApplicationUser>> GetAll();

        Task<UserDetails> GetById(string Id);

        Task<DataTablesResponse<ApplicationUser>> GetForDataTables(DataTableFilterBase filters);

        Task<UserModel> GetUserByEmail(string email);

        Task<UserModel> GetUserById(string Id);

        Task<string> ResetPasswordAsync(string userName);

        Task<string> ResetPasswordByEmailAsync(string email, string token, string newPassword);

        Task SendPasswordResetMail(string userName, string returnUrl = null);

        void SetDataKey(string userId, string dataKey);

        Task<UserModel> UpdateUser(UserModel userModel, bool byEmail = false);
        Task<ActionResult<IEnumerable<UsersSelectableModel>>> GetSelectableUsers(string word, bool onlyEmployees);
        Task<ActionResult<UserLpModel>> GetUserLp(string Id);
    }
}