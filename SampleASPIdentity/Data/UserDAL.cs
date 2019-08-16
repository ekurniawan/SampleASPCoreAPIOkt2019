using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using SampleASPIdentity.Models;

namespace SampleASPIdentity.Data
{
    public class UserDAL : IUser
    {
        private UserManager<IdentityUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;

        public UserDAL(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public Task AddUserToRole(string username, string role)
        {
            throw new NotImplementedException();
        }

        public Task<User> Authenticate(string username, string password)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CheckUserInRole(string username, string rolename)
        {
            throw new NotImplementedException();
        }

        public async Task CreateRole(string roleName)
        {
            IdentityResult roleResult;
            var roleExist = await _roleManager.RoleExistsAsync(roleName);
            if (!roleExist)
                roleResult = await _roleManager.CreateAsync(new IdentityRole(roleName));
            else
                throw new Exception($"Role {roleName} sudah ada");
        }

        public IEnumerable<User> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task Register(User usr)
        {
            var user = new IdentityUser { UserName = usr.Username, Email = usr.Username };
            var result = await _userManager.CreateAsync(user, usr.Password);
            
            if (!result.Succeeded)
                throw new Exception("Gagal menambah Pengguna ");
        }

        
    }
}
