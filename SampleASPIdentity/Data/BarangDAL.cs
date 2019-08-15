using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public void Delete(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Barang> GetAll()
        {
            /*var results = from b in _db.Barang
                          orderby b.NamaBarang ascending
                          select b;*/
            var results = _db.Barang.OrderBy(b => b.NamaBarang);
            return results;
        }

        public Barang GetById(string id)
        {
            throw new NotImplementedException();
        }

        public void Insert(Barang obj)
        {
            try
            {
                _db.Add(obj);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Update(Barang obj)
        {
            throw new NotImplementedException();
        }
    }
}
