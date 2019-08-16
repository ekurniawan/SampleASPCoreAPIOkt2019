using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SampleASPIdentity.Models;

namespace SampleASPIdentity.Data
{
    public class BarangDAL : IBarang
    {
        private ApplicationDbContext _db;

        public BarangDAL(ApplicationDbContext db)
        {
            _db = db;
            
        }

        public Task Delete(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Barang>> GetAll()
        {
            /*var results = from b in _db.Barang
                          orderby b.NamaBarang ascending
                          select b;*/
            
            var results = await _db.Barang.OrderBy(b => b.NamaBarang).ToListAsync();
            return results;
        }

        public Task<Barang> GetById(string id)
        {
            throw new NotImplementedException();
        }

        public Task Insert(Barang obj)
        {
            throw new NotImplementedException();
        }

        public Task Update(Barang obj)
        {
            throw new NotImplementedException();
        }
    }
}
