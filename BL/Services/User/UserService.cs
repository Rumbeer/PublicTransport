using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Claims;
using BL.DTOs.UserAccount;
using BL.Utils.AccountPolicy;
using BrockAllen.MembershipReboot;

namespace BL.Services.User
{
    /// <summary>
    /// Provides user account related functionality
    /// </summary>
    public class UserService : AppService, IUserService
    {
        #region Dependencies

        private readonly UserAccountService<DAL.Entities.UserAccount> coreService;

        public UserService(UserAccountService<DAL.Entities.UserAccount> service)
        {
            coreService = service;
        } 

        #endregion

        /// <summary>
        /// Registers user (typically with default claims)
        /// </summary>
        /// <param name="userRegistration">User registration details</param>
        /// <param name="createAdmin">Grant user admin rights</param>
        /// <returns>ID of registered user</returns>
        public Guid RegisterUserAccount(UserRegistrationDTO userRegistration, bool createAdmin = false)
        {
            using (UnitOfWorkProvider.Create())
            {
                var userClaims = new List<Claim>();

                if (createAdmin)
                {
                    userClaims.Add(new Claim(ClaimTypes.Role, Claims.Admin));
                }
                else
                {
                    // for the moment there is just Customer role left
                    userClaims.Add(new Claim(ClaimTypes.Role, Claims.Customer));
                }

                //var account = coreService.CreateAccount(null, userRegistration.Password, userRegistration.Email, null, null);

                var account = coreService.CreateAccount(null, userRegistration.Password, userRegistration.Email, null, null, null);

                AutoMapper.Mapper.Map(userRegistration, account);

                foreach (var claim in userClaims)
                {
                    coreService.AddClaim(account.ID, claim.Type, claim.Value);
                }
           
                coreService.Update(account);

                return account.ID;
            }           
        }

        /// <summary>
        /// Authenticates user with given username and password
        /// </summary>
        /// <param name="loginDto">user login details</param>
        /// <returns>ID of the authenticated user</returns>
        public Guid AuthenticateUser(UserLoginDTO loginDto)
        {
            DAL.Entities.UserAccount account;
            var result = coreService.Authenticate(loginDto.Username, loginDto.Password, out account);
            if (!result)
            {
                Debug.WriteLine($"Failed to authenticate user: {loginDto.Username}");
                return Guid.Empty;
            }
            return account.ID;      
        }
    }
}
