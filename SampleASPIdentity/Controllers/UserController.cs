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
            await _user.Register(user);
            return Ok("Proses Registrasi Berhasil");
        }

    }
}