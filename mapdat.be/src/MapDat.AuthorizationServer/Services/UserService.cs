using AuthorizationServer.Data;
using AuthorizationServer.Exceptions;
using AuthorizationServer.Models;
using AuthorizationServer.PolicyCode;
using IdentityModel;
using MapDat.AuthorizationServer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AuthorizationServer.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        private readonly CreateAccountOptions _createAccountOptions;
        private readonly IEmailSender _emailSender;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserService(ApplicationDbContext context, CreateAccountOptions createAccountOptions, IEmailSender emailSender, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _createAccountOptions = createAccountOptions;
            _emailSender = emailSender;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async void AddRoleToUser(string roleName, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            await _userManager.AddToRoleAsync(user, roleName);
        }

        public async Task<bool> ChangePasswordAsync(ChangePasswordRequestModel model)
        {
            ApplicationUser user = await _userManager.FindByNameAsync(model.UserName);
            bool succeeded = false;

            if (user != null)
            {
                var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
                succeeded = result.Succeeded;
            }

            return succeeded;
        }

        public async Task<IdentityResult> ChangePasswordAsync(string userId, string currentPassword, string newPassword)
        {
            var user = await _userManager.FindByIdAsync(userId);

            return await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
        }

        public async Task<UserModel> CreateUser(UserModel userModel)
        {
            var existingUser = await _userManager.FindByEmailAsync(userModel.Email);
            var existingUserByName = await _userManager.FindByNameAsync(userModel.UserName);
            if (existingUser != null)
            {
                throw new Exception("User with the same email already exists. Please change email or update existing user with email: " + userModel.Email);
                //userModel.UserId = existingUser.Id;
                //return await UpdateUser(existingUser, userModel);
            }
            if (existingUserByName != null)
            {
                throw new Exception("User with the same full name already exists. Please change full name  or update existing user with full name : " + userModel.UserName);
            }
            var lastLp = await _context.Users.Select(x => x.Lp).MaxAsync();
            var user = new ApplicationUser
            {
                Lp = lastLp+1,
                UserName = userModel.UserName,
                Email = userModel.Email,
            };

            var passwordGenerated = userModel.Password == null;
            if (passwordGenerated)
            {
                userModel.Password = GetRandomPassword(32);
            }

            var result = await _userManager.CreateAsync(user, userModel.Password);
            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }

            if (userModel.Permissions != null)
            {
                var claims = userModel.Permissions.Select(x => x.ToClaim()).ToList();
                result = await _userManager.AddClaimsAsync(user, claims);
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }
            }

            if (!string.IsNullOrEmpty(userModel.DataKey))
            {
                var claim = new Claim(CustomPermissionTypes.DataKey, userModel.DataKey);
                result = await _userManager.AddClaimAsync(user, claim);
            }

            if (!string.IsNullOrEmpty(userModel.FirstName))
            {
                user.Firstname = userModel.FirstName;
                var claim = new Claim(JwtClaimTypes.GivenName, userModel.FirstName);
                result = _userManager.AddClaimAsync(user, claim).Result;
            }

            if (!string.IsNullOrEmpty(userModel.LastName))
            {
                user.Lastname = userModel.LastName;
                var claim = new Claim(JwtClaimTypes.FamilyName, userModel.LastName);
                result = _userManager.AddClaimAsync(user, claim).Result;
            }

            if (userModel.Roles != null)
            {
                result = await _userManager.AddToRolesAsync(user, userModel.Roles);
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }
            }

            if (!string.IsNullOrEmpty(userModel.Email))
            {
                var emailClaim = new Claim(JwtClaimTypes.Email, userModel.Email);
                await _userManager.AddClaimAsync(user, emailClaim);
            }

            if (passwordGenerated)
            {
                await SendPasswordResetMail(user);
            }

            userModel.UserId = user.Id;

            return userModel;
        }

        public async Task<bool> DeleteUserByEmail(string email)
        {
            var user = await _context.Users.FirstAsync(x => x.Email.ToLower() == email.ToLower());

            return await DeleteUser(user);
        }

        public async Task<bool> DeleteUserById(string id)
        {
            var user = await _context.Users.FirstAsync(x => x.Id == id);

            return await DeleteUser(user);
        }

        public async Task<string> GeneratePasswordResetToken(string userId)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(userId);
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            return token;
        }

        public async Task<IEnumerable<ApplicationUser>> GetAll()
        {
            var users = _context.Users.ToList();
            //var bob = _context.Users.Where(x => x.UserName == "bob").First();
            //var roles = _context.UserRoles.Where(x => x.UserId == bob.Id).ToList();

            return users;

            //var x = _userManager.GetUsersInRoleAsync("admin");
            //var y = await _userManager.FindByIdAsync("id");
            //var claims = _userManager.GetClaimsAsync(y);
            //throw new NotImplementedException();
        }

        public async Task<UserDetails> GetById(string Id)
        {
            var user = _context.Users.Where(x => x.Id == Id).First();
            var roles = await _userManager.GetRolesAsync(user);
            var claims = await _userManager.GetClaimsAsync(user);

            var stringClaims = claims.Select(x => x.Value).ToList();

            var result = new UserDetails()
            {
                UserId = user.Id,
                UserName = user.UserName,
                Roles = roles.ToList(),
                Claims = stringClaims.ToList()
            };
            return result;
        }

        public async Task<DataTablesResponse<ApplicationUser>> GetForDataTables(DataTableFilterBase filters)
        {
            DataTablesResponse<ApplicationUser> response = new DataTablesResponse<ApplicationUser>();

            var users = _context.Users.AsQueryable();

            if (filters.filterWords.Any())
            {
                users = users.Where(x => x.Email.Contains(filters.filterWords.First()) || x.Id.Contains(filters.filterWords.First()));
            }

            if (filters.sortDirection == "asc")
            {
                if (filters.sortColumn == "id") users = users.OrderBy(x => x.Id);
                if (filters.sortColumn == "name") users = users.OrderBy(x => x.UserName);
                if (filters.sortColumn == "email") users = users.OrderBy(x => x.Email);
            }
            else
            {
                if (filters.sortColumn == "id") users = users.OrderByDescending(x => x.Id);
                if (filters.sortColumn == "name") users = users.OrderByDescending(x => x.UserName);
                if (filters.sortColumn == "email") users = users.OrderByDescending(x => x.Email);
            }

            var filteredUsers = users.Skip(filters.pageNumber * filters.pageSize).Take(filters.pageSize).ToList();

            response.Data = filteredUsers;
            response.TotalPages = users.Count();

            return response;
        }

        public async Task<UserModel> GetUserByEmail(string email)
        {
            //var user = _context.Users.Where(x => x.Id == Id).First();
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return null;
            }
            var roles = await _userManager.GetRolesAsync(user);
            var claims = await _userManager.GetClaimsAsync(user);

            var permissionClaims = claims.Where(x => x.Type == CustomPermissionTypes.EnumPermission).Select(x => x.ToPermission()).ToList();

            var dataKey = claims.Where(x => x.Type == CustomPermissionTypes.DataKey).FirstOrDefault()?.Value;

            var result = new UserModel()
            {
                UserId = user.Id,
                UserName = user.UserName,
                FirstName = user.Firstname,
                LastName = user.Lastname,
                Email = user.Email,
                Roles = roles.ToList(),
                Permissions = permissionClaims,
                DataKey = dataKey
            };
            return result;
        }

        public async Task<UserModel> GetUserById(string Id)
        {
            var user = _context.Users.Where(x => x.Id == Id).FirstOrDefault();
            if (user == null) return null;
            var roles = await _userManager.GetRolesAsync(user);
            var claims = await _userManager.GetClaimsAsync(user);

            var permissionClaims = claims.Where(x => x.Type == CustomPermissionTypes.EnumPermission).Select(x => x.ToPermission()).ToList();

            var dataKey = claims.Where(x => x.Type == CustomPermissionTypes.DataKey).FirstOrDefault()?.Value;
            var firstName = claims.Where(x => x.Type == JwtClaimTypes.GivenName).FirstOrDefault()?.Value;
            var lastName = claims.Where(x => x.Type == JwtClaimTypes.FamilyName).FirstOrDefault()?.Value;

            var result = new UserModel()
            {
                UserId = user.Id,
                UserName = user.UserName,
                FirstName = user.Firstname,
                LastName = user.Lastname,
                Email = user.Email,
                Roles = roles.ToList(),
                Permissions = permissionClaims,
                DataKey = dataKey
            };
            return result;
        }

        public async Task<string> ResetPasswordAsync(string userName)
        {
            ApplicationUser user = await _userManager.FindByNameAsync(userName);
            string password = null;

            if (user != null)
            {
                password = GetRandomPassword(16);

                await _userManager.RemovePasswordAsync(user);
                await _userManager.AddPasswordAsync(user, password);
            }

            return password;
        }

        public async Task<string> ResetPasswordByEmailAsync(string email, string token, string newPassword)
        {
            var user = await _userManager.FindByEmailAsync(email);
            ConfirmUserEmail(user);

            var result = await _userManager.ResetPasswordAsync(user, token, newPassword);
            return user.Id;
        }

        public async Task SendPasswordResetMail(string userName, string returnUrl = null)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
            {
                throw new UserNotExistException(userName);
            }
            await SendPasswordResetMail(user, returnUrl);
        }

        public async Task SendPasswordResetMail(ApplicationUser user, string returnUrl = null)
        {
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            await _emailSender.SendEmailAsync(user.Email, _createAccountOptions.Title, _createAccountOptions.GetFormatedMessage(user.UserName, token, returnUrl));
        }

        public async void SetDataKey(string userId, string dataKey)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var existingDataKey = (await _userManager.GetClaimsAsync(user)).Where(x => x.Type == CustomPermissionTypes.DataKey).FirstOrDefault();
            if (existingDataKey != null)
            {
                await _userManager.RemoveClaimAsync(user, existingDataKey);
            }
            var dataKeyClaim = new Claim(CustomPermissionTypes.DataKey, dataKey);
            await _userManager.AddClaimAsync(user, dataKeyClaim);
        }

        public async Task<UserModel> UpdateUser(UserModel userModel, bool byEmail = false)
        {
            var user = byEmail ? await _userManager.FindByEmailAsync(userModel.Email) : await _userManager.FindByIdAsync(userModel.UserId);
            return await UpdateUser(user, userModel, !byEmail);
        }

        private void ConfirmUserEmail(ApplicationUser user)
        {
            user.EmailConfirmed = true;
            _context.Users.Update(user);
        }

        private async Task<bool> DeleteUser(ApplicationUser user)
        {
            var result = await _userManager.DeleteAsync(user);

            return result.Succeeded;
        }

        private string GetRandomPassword(int length)
        {
            string allowedLowercaseCharacters = "qwertyuiopasdfghjklzxcvbnm";
            string allowedUppercaseCharacters = "QWERTYUIOPASDFGHJKLZXCVBNM";
            string allowedNonAlphanumericCharacters = "@$!%*?&";
            string allowedDigitCharacters = "1234567890";

            string allowedCharacters = allowedLowercaseCharacters + allowedUppercaseCharacters +
                allowedNonAlphanumericCharacters + allowedDigitCharacters;

            var builder = new StringBuilder();
            var rand = new Random();

            builder.Append(allowedUppercaseCharacters[rand.Next(allowedUppercaseCharacters.Length)]);
            builder.Append(allowedLowercaseCharacters[rand.Next(allowedLowercaseCharacters.Length)]);
            builder.Append(allowedNonAlphanumericCharacters[rand.Next(allowedNonAlphanumericCharacters.Length)]);
            builder.Append(allowedDigitCharacters[rand.Next(allowedDigitCharacters.Length)]);

            for (int i = 4; i < length; i++)
            {
                builder.Append(allowedCharacters[rand.Next(allowedCharacters.Length)]);
            }

            return builder.ToString();
        }

        private async Task<UserModel> UpdateUser(ApplicationUser user, UserModel userModel, bool updateEmail = true)
        {
            var existingUser = await _userManager.FindByEmailAsync(userModel.Email);
            var existingUserByName = await _userManager.FindByNameAsync(userModel.UserName);
            if (existingUser != null && existingUser.Id != userModel.UserId)
            {
                throw new Exception("User with the same email already exists. Please change email or update existing user with email: " + userModel.Email);
                //userModel.UserId = existingUser.Id;
                //return await UpdateUser(existingUser, userModel);
            }
            if (existingUserByName != null && existingUserByName.Id != userModel.UserId)
            {
                throw new Exception("User with the same full name already exists. Please change full name  or update existing user with full name : " + userModel.UserName);
            }

            if (updateEmail && !string.IsNullOrEmpty(userModel.Email))
            {
                user.Email = userModel.Email;
                user.UserName = userModel.UserName;
            }

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }
            //remove old claims
            var existingClaims = (await _userManager.GetClaimsAsync(user)).Where(x => x.Type == CustomPermissionTypes.EnumPermission).ToList();
            result = await _userManager.RemoveClaimsAsync(user, existingClaims);
            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }
            //add new claims
            var claims = userModel.Permissions.Select(x => x.ToClaim()).ToList();
            result = _userManager.AddClaimsAsync(user, claims).Result;
            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }
            //remove old Datakey
            var existingDataKey = (await _userManager.GetClaimsAsync(user)).Where(x => x.Type == CustomPermissionTypes.DataKey).FirstOrDefault();
            if (existingDataKey != null)
            {
                result = await _userManager.RemoveClaimAsync(user, existingDataKey);
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }
            }
            //add new Datakey
            if (!string.IsNullOrEmpty(userModel.DataKey))
            {
                var claim = new Claim(CustomPermissionTypes.DataKey, userModel.DataKey);
                result = _userManager.AddClaimAsync(user, claim).Result;
            }
            //remove old FirstName
            var existingFirstName = (await _userManager.GetClaimsAsync(user)).Where(x => x.Type == JwtClaimTypes.GivenName).FirstOrDefault();
            if (existingFirstName != null)
            {
                result = await _userManager.RemoveClaimAsync(user, existingFirstName);
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }
            }
            //add new FirstName
            if (!string.IsNullOrEmpty(userModel.FirstName))
            {
                user.Firstname = userModel.FirstName;
                var claim = new Claim(JwtClaimTypes.GivenName, userModel.FirstName);
                result = _userManager.AddClaimAsync(user, claim).Result;
            }
            //remove old LastName
            var existingLastName = (await _userManager.GetClaimsAsync(user)).Where(x => x.Type == JwtClaimTypes.FamilyName).FirstOrDefault();
            if (existingLastName != null)
            {
                result = await _userManager.RemoveClaimAsync(user, existingLastName);
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }
            }
            //add new LastName
            if (!string.IsNullOrEmpty(userModel.LastName))
            {
                user.Lastname = userModel.LastName;
                var claim = new Claim(JwtClaimTypes.FamilyName, userModel.LastName);
                result = _userManager.AddClaimAsync(user, claim).Result;
            }
            //remove user from roles
            var existingRoles = await _userManager.GetRolesAsync(user);
            result = await _userManager.RemoveFromRolesAsync(user, existingRoles);
            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }
            //add to roles
            result = await _userManager.AddToRolesAsync(user, userModel.Roles);
            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }
            //remove old email
            var existingEmail = (await _userManager.GetClaimsAsync(user)).Where(x => x.Type == JwtClaimTypes.Email).FirstOrDefault();
            if (existingEmail != null)
            {
                result = await _userManager.RemoveClaimAsync(user, existingEmail);
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }
            }
            //add email claim
            if (!string.IsNullOrEmpty(userModel.Email))
            {
                var emailClaim = new Claim(JwtClaimTypes.Email, userModel.Email);
                await _userManager.AddClaimAsync(user, emailClaim);
            }

            userModel.UserId = user.Id;

            return userModel;
        }
        public async Task<ActionResult<UserLpModel>> GetUserLp(string Id){
            var user = await _userManager.FindByIdAsync(Id);
            UserLpModel result = new UserLpModel()
            {
                Lp = user.Lp
            };
            return result;

        }
        public async Task<ActionResult<IEnumerable<UsersSelectableModel>>> GetSelectableUsers(string filterWords, bool onlyEmployees)
        {
            if (filterWords == null)
                filterWords = "";
            var users = await _context.Users.Where(x=>x.Firstname.Contains(filterWords)||x.Lastname.Contains(filterWords)).ToListAsync();
            var result = new List<UsersSelectableModel>();

            foreach (var user in users)
            {
                var userRoles = await (from ur in _context.UserRoles
                                       join r in _context.Roles on ur.RoleId equals r.Id
                                       where ur.UserId == user.Id
                                       select r).ToListAsync();
                if (onlyEmployees && (userRoles.All(x=>x.Name != "Client") || userRoles.Count() == 0))
                    result.Add(new UsersSelectableModel
                    {
                        Id = Guid.Parse(user.Id),
                        Label = user.Firstname + " " + user.Lastname,
                        FirstName = user.Firstname,
                        LastName = user.Lastname,
                        Email = user.Email,
                    });
                else if (!onlyEmployees && userRoles.Count()>0 && userRoles.All(x => x.Name == "Client"))
                    result.Add(new UsersSelectableModel
                    {
                        Id = Guid.Parse(user.Id),
                        Label = user.Firstname + " " + user.Lastname,
                        FirstName = user.Firstname,
                        LastName = user.Lastname,
                        Email = user.Email,
                    });
            }

            return result;
        }
    }
}