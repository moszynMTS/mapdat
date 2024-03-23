using AuthorizationServer.Models;
using AuthorizationServer.PolicyCode;
using AuthorizationServer.Services;
using MapDat.AuthorizationServer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static IdentityServer4.IdentityServerConstants;

namespace AuthorizationServer.Controllers
{
    [ApiController]
    [Authorize(LocalApi.PolicyName)]
    public class ApiUsersController : ControllerBase
    {
        [HttpPost("ChangePassword")]
        [HasPermission(Models.Permissions.UserChange)]
        public async Task<ActionResult<bool>> ChangePassword([FromServices] IUserService service, [FromBody] ChangePasswordRequestModel model)
        {
            string password = await service.ResetPasswordAsync(model.UserName);
            model.CurrentPassword = password;

            return Ok(await service.ChangePasswordAsync(model));
        }

        [HttpPost("CreateOrUpdateUser")]
        [HasPermission(Models.Permissions.UserChange)]
        public async Task<ActionResult> CreateOrUpdateUser([FromServices] IUserService service, UserModel user)
        {
            if (string.IsNullOrEmpty(user.UserId))
            {
                await service.CreateUser(user);
            }
            else
            {
                await service.UpdateUser(user);
            }

            return Ok();
        }

        [HttpGet("DeleteUser")]
        [HasPermission(Models.Permissions.UserChange)]
        public async Task<bool> DeleteUser([FromServices] IUserService service, string userId, string email)
        {
            var result = (userId == null, email == null) switch
            {
                (true, false) =>  await service.DeleteUserByEmail(email),
                (false, true) => await service.DeleteUserById(userId),
                _ => throw new ArgumentException("Wymagany parametr userId albo email")
            };

            return result;
        }

        [HttpGet("GetPasswordResetToken")]
        [HasPermission(Models.Permissions.UserChange)]
        public async Task<ActionResult<string>> GetPasswordResetToken([FromServices] IUserService service, string userId)
        {
            var token = await service.GeneratePasswordResetToken(userId);

            return token;
        }

        [HttpGet("GetPermissionsDictionary")]
        public List<PermissionDictionaryElement> GetPermissionsDictionary()
        {
            List<PermissionDictionaryElement> permissionsDict = new List<PermissionDictionaryElement>();
            foreach (Permissions foo in Enum.GetValues(typeof(Permissions)))
            {
                permissionsDict.Add(new PermissionDictionaryElement() { Name = foo.ToString(), Value = (int)foo });
            }
            return permissionsDict;
        }

        [HttpGet("GetUser")]
        [HasPermission(Models.Permissions.UserRead)]
        public async Task<UserModel> GetUser([FromServices] IUserService service, string userId)
        {
            var user = await service.GetUserById(userId);
            return user;
        }

        [HttpPost("GetUsers")]
        [HasPermission(Models.Permissions.UserRead)]
        public async Task<List<ApplicationUser>> GetUsers([FromServices] IUserService service, DataTableFilterBase filter)
        {
            var users = await service.GetAll();
            return users.ToList();
        }

        [HttpPost("GetUsersForDataTables")]
        [HasPermission(Models.Permissions.UserRead)]
        public async Task<DataTablesResponse<ApplicationUser>> GetUsersForDataTables([FromServices] IUserService service, DataTableFilterBase filter)
        {
            var users = await service.GetForDataTables(filter);
            return users;
        }

        [HttpGet("email-unique")]
        [AllowAnonymous]
        public async Task<ActionResult<bool>> PropertyUnique([FromServices] IUserService service, string email)
        {
            var user = await service.GetUserByEmail(email);

            return user == null;
        }

        [HttpPost("ResetPassword")]
        [HasPermission(Models.Permissions.UserChange)]
        public async Task<ActionResult<string>> ResetPassword([FromServices] IUserService service, string userName)
        {
            return Ok(await service.ResetPasswordAsync(userName));
        }

        [HttpPost("ResetPasswordByEmail")]
        [HasPermission(Models.Permissions.UserChange)]
        public async Task<ActionResult<string>> ResetPasswordByEmail([FromServices] IUserService service, ResetPasswordModel model)
        {
            return Ok(await service.ResetPasswordByEmailAsync(model.Email, model.Token, model.NewPassword));
        }

        [HttpPost("SendPasswordResetMail")]
        [AllowAnonymous]
        public async Task<ActionResult> SendPasswordResetMail([FromServices] IUserService service, string userName)
        {
            await service.SendPasswordResetMail(userName);

            return Ok();
        }

        [HttpGet("Test")]
        public async Task<List<ApplicationUser>> Test([FromServices] IUserService service, string userId)
        {
            var users = await service.GetAll();
            return users.ToList();
        }

        [HttpPut("ChangePasswordById")]
        public async Task<ActionResult<bool>> UpdatePassword([FromServices] IUserService service, [FromBody] UpdatePasswordByIdRequestModel model)
        {
            var result = await service.ChangePasswordAsync(model.Id, model.CurrentPassword, model.NewPassword);

            if (result.Succeeded)
            {
                return Ok();
            }

            return BadRequest(result.Errors);
        }

        [HttpPost("UpdateUser")]
        [HasPermission(Models.Permissions.UserChange)]
        public async Task<ActionResult> UpdateUser([FromServices] IUserService service, UserModel user)
        {
            await service.UpdateUser(user, user.UserId == null);

            return Ok();
        }
        [HttpGet("selectable")]
        public async Task<ActionResult<IEnumerable<UsersSelectableModel>>> getSelectableUsers([FromServices] IUserService service, string word, bool onlyEmployees)
        {
            var result = await service.GetSelectableUsers(word, onlyEmployees);

            return result;
        }
    }
}