using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BO;
using DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SampleWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PraCIFController : ControllerBase
    {
        private IPraCIF _praCIF;
        public PraCIFController(IPraCIF praCIF)
        {
            _praCIF = praCIF;
        }

        // GET: api/PraCIF
        [HttpGet]
        public IEnumerable<PraCIF> Get()
        {
            return _praCIF.GetAll();
        }

        // GET: api/PraCIF/GetByName
        [HttpGet("GetByName/{name}")]
        public IEnumerable<PraCIF> GetByName(string name)
        {
            return _praCIF.GetByName(name);
        }

        // GET: api/PraCIF/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/PraCIF
        [HttpPost]
        public IActionResult Post([FromBody] PraCIF praCif)
        {
            try
            {
                _praCIF.Insert(praCif);
                return Ok("Berhasil Tambah Data PraCIF");
            }
            catch (Exception ex)
            {
                return BadRequest($"Kesalahan: {ex.Message}");
            }
        }

        // PUT: api/PraCIF/5
        [HttpPut]
        public IActionResult Put([FromBody] PraCIF praCIF)
        {
            try
            {
                _praCIF.Update(praCIF);
                return Ok("Berhasil update data PraCIF");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            try
            {
                _praCIF.Delete(id);
                return Ok($"Data id:{id} berhasil dihapus");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
