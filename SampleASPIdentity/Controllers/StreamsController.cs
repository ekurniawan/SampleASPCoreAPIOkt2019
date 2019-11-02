using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SampleASPIdentity.Data;
using SampleASPIdentity.Models;

namespace SampleASPIdentity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StreamsController : ControllerBase
    {
        private IStreams _stream;
        public StreamsController(IStreams stream)
        {
            _stream = stream;
        }
        // GET: api/Streams
        [Authorize]
        [HttpGet]
        public async Task<IEnumerable<stream>> Get()
        {
            return await _stream.GetAll();
        }

        // GET: api/Streams/5
        [HttpGet("{id}")]
        public async Task<stream> Get(int id)
        {
            return await _stream.GetById(id.ToString());
        }

        // POST: api/Streams
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] stream obj)
        {
            try
            {
                await _stream.Insert(obj);
                return CreatedAtAction("Get", obj);
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}");
            }
        }

        // PUT: api/Streams/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] stream obj)
        {
            try
            {
                await _stream.UpdateWithID(id, obj);
                return Ok(obj);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _stream.Delete(id.ToString());
                return Ok(id);
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}");
            }
        }
    }
}
