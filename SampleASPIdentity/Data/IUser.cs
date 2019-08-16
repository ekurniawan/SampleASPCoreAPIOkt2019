using SampleASPIdentity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleASPIdentity.Data
{
    public interface IUser
    {
        Task<User> Authenticate(string username, string password);
        IEnumerable<User> GetAll();
        Task Register(User usr);
        Task CreateRole(string roleName);
        Task AddUserToRole(string username, string role);
        Task<bool> CheckUserInRole(string username, string rolename);
    }
}
