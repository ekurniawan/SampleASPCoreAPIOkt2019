using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SampleASPIdentity.Data;
using SampleASPIdentity.Models;

namespace SampleASPIdentity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUser _user;
        public UserController(IUser user)
        {
            _user = user;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody]User user)
        {
            try
            {
                await _user.Register(user);
                return Ok("Proses Registrasi Berhasil");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("createrole")]
        public async Task<IActionResult> CreateRole([FromBody]string roleName)
        {
            await _user.CreateRole(roleName);
            return Ok($"Proses Pembuatan Role {roleName} berhasil");
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody]User userParam)
        {
            var user = await _user.Authenticate(userParam.Username, userParam.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(user);
        }

        [HttpPost("addusertorole")]
        public async Task<IActionResult> AddUserToRole([FromBody] UserRole usrRole)
        {
            await _user.AddUserToRole(usrRole.username, usrRole.rolename);
            return Ok();
        }

    }
}