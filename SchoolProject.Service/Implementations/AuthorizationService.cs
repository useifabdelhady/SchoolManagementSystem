﻿using Microsoft.AspNetCore.Identity;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Infrustucture.Data;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Service.Implementations
{
    public class AuthorizationService : IAuthorizationService
    {
        #region Fields
        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<User> _userManager;
        private readonly ApplicationDBContext _dbContext;
        #endregion

        #region Constructors
        public AuthorizationService(RoleManager<Role> roleManager,
                                    UserManager<User> userManager,
                                    ApplicationDBContext dbContext)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _dbContext = dbContext;
        }


        #endregion

        #region handle Functions
        public async Task<string> AddRoleAsync(string roleName)
        {
            var identityRole = new Role();
            identityRole.Name = roleName;
            var result = await _roleManager.CreateAsync(identityRole);
            if (result.Succeeded)
                return "Success";
            return "Failed";
        }

        public async Task<bool> IsRoleExistByName(string roleName)
        {
            //var role=await _roleManager.FindByNameAsync(roleName);
            //if(role == null) return false;
            //return true;
            return await _roleManager.RoleExistsAsync(roleName);
        }
        #endregion
    }
}
