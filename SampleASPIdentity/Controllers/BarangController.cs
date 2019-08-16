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
        public BarangController(IBarang barang)
        {
            _barang = barang;
        }

        // GET: api/Barang
        [HttpGet]
        public IEnumerable<Barang> Get()
        {
            return _barang.GetAll();
        }

        // GET: api/Barang/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpGet("GetUsername")]
        public string GetUsername()
        {
            return $"Username: {User.Identity.Name}";
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
