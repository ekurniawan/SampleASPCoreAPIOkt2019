using System;
using System.Collections.Generic;
using System.Text;
using BO;
using System.Linq;

namespace DAL
{
    public class CustomerDAL : ICustomer
    {
        public void Delete(string id)
        {
            throw new NotImplementedException();
        }

        public static List<Customer> lstCustomer = new List<Customer>
            {
                new Customer{CustomerID=Guid.NewGuid().ToString(),CustomerName="Budi",Address="Jl Rajawali"},
                new Customer{CustomerID=Guid.NewGuid().ToString(),CustomerName="Erick",Address="Jl Merdeka"}
            };
      

        public IEnumerable<Customer> GetAll()
        {
            return lstCustomer;
        }

        public Customer GetById(string id)
        {
            var cust = lstCustomer.Where(l => l.CustomerID == id).SingleOrDefault();
            if (cust != null)
            {
                return cust;
            }
            else
            {
                throw new Exception("Data Tidak Ditemukan !");
            }
        }

        public IEnumerable<Customer> GetByNama(string nama)
        {
            throw new NotImplementedException();
        }

        public void Insert(Customer obj)
        {
            lstCustomer.Add(obj);
        }

        public void Update(Customer obj)
        {
            throw new NotImplementedException();
        }
    }
}
