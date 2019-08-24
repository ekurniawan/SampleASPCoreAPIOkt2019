using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SampleASPIdentity.Data;
using SampleASPIdentity.Models;

namespace SampleASPIdentity.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BarangController : ControllerBase
    {
        private IBarang _barang;
        private IUser _user;

        public BarangController(IBarang barang,IUser user)
        {
            _barang = barang;
            _user = user;
        }

        // GET: api/Barang
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var cek = await _user.CekApiAuth(User.Identity.Name,
                ControllerContext.ActionDescriptor.ControllerName,
                ControllerContext.ActionDescriptor.ActionName);
            if (cek)
                return Ok(await _barang.GetAll());
            else
                return Unauthorized();
        }

        // GET: api/Barang/5
        [HttpGet("{id}")]
        public async Task<Barang> Get(int id)
        {
            var result = await _barang.GetById(id.ToString());
            return result;
        }

        [HttpGet("GetUsername")]
        public async Task<IActionResult> GetUsername()
        {
            var cek = await _user.CekApiAuth(User.Identity.Name,
                ControllerContext.ActionDescriptor.ControllerName,
                ControllerContext.ActionDescriptor.ActionName);
            if (cek)
                return Ok($"Username: {User.Identity.Name}");
            else
                return Unauthorized();

        }

        // POST: api/Barang
        [HttpPost]
        public IActionResult Post([FromBody] Barang barang)
        {
            try
            {
                _barang.Insert(barang);
                return Ok("Data berhasil ditambah !");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/Barang/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
