using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SampleASPIdentity.Models;

namespace SampleASPIdentity.Data
{
    public class StreamDAL : IStreams
    {
        private ApplicationDbContext _context;
        public StreamDAL(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task Delete(string id)
        {
            try
            {
                var result = await GetById(id);
                _context.streams.Remove(result);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error {ex.Message}");
            }
        }

        public async Task<IEnumerable<stream>> GetAll()
        {
            return await _context.streams.OrderBy(s => s.title).ToListAsync();
        }

        public async Task<stream> GetById(string id)
        {
            var result = await _context.streams.Where(s => s.streamid.ToString() == id).SingleOrDefaultAsync();
            return result;
        }

        public async Task Insert(stream obj)
        {
            try
            {
                _context.Add(obj);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Task Update(stream obj)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateWithID(int id, stream obj)
        {
            var result = await GetById(id.ToString());
            if (result!=null)
            {
                result.description = obj.description;
                result.title = obj.title;
                await _context.SaveChangesAsync();
            }else
            {
                throw new Exception("Data Tidak Ditemukan !");
            }
        }
    }
}
