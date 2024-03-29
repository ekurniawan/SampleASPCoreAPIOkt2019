﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SampleASPIdentity.Helpers;
using SampleASPIdentity.Models;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace SampleASPIdentity.Data
{
    public class UserDAL : IUser
    {
        private AppSettings _appSettings;
        private UserManager<IdentityUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        private IConfiguration _config;

        public UserDAL(IOptions<AppSettings> appSettings,
            UserManager<IdentityUser> userManager, 
            RoleManager<IdentityRole> roleManager,
            IConfiguration config)
        {
            _appSettings = appSettings.Value;
            _userManager = userManager;
            _roleManager = roleManager;
            _config = config;
        }


        private string GetConnStr()
        {
            return _config.GetConnectionString("KurniaConnection");
        }

        public async Task<bool> CekApiAuth(string username, string cname, string aname)
        {
            using(SqlConnection conn = new SqlConnection(GetConnStr()))
            {
                var role = await GetRolesFromUser(username);
                var strSql = @"select Id from RoleDesc2 
                where role=@role and CName=@CName and AName=@AName";
                var param = new { role = role, CName = cname, AName = aname };
                var result = await conn.QuerySingleOrDefaultAsync<RoleDesc2>(strSql, param);
                if (result == null)
                    return false;
                else
                    return true;
            }
        }

        public async Task AddUserToRole(string username, string role)
        {
            var user = await _userManager.FindByNameAsync(username);
            try
            {
                await _userManager.AddToRoleAsync(user, role);

            }
            catch (Exception ex)
            {
                throw new Exception($"Error {ex.Message}");
            }
        }

        public async Task<User> Authenticate(string username, string password)
        {
            var userFind = await _userManager.CheckPasswordAsync(await _userManager.FindByNameAsync(username), password);

            // return null if user not found
            if (!userFind)
                return null;

            var user = new User
            {
                Username = username
            };

            //var roles = await _userManager.GetRolesAsync(new IdentityUser { UserName = username });

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Username)
                }),
                Expires = DateTime.UtcNow.AddHours(6),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            // remove password before returning
            user.Password = null;

            return user;
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

        public async Task<List<string>> GetRolesFromUser(string username)
        {
            List<string> listRoles = new List<string>();
            var user = await _userManager.FindByEmailAsync(username);
            var roles = await _userManager.GetRolesAsync(user);
            foreach(var role in roles)
            {
                listRoles.Add(role);
            }
            return listRoles;
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
