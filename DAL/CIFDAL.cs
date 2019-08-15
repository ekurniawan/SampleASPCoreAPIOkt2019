using System;
using System.Collections.Generic;
using System.Text;
using BO;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using Dapper;

namespace DAL
{
    public class CIFDAL : ICIF
    {
        private IConfiguration _config;
        public CIFDAL(IConfiguration config)
        {
            _config = config;
        }

        private string GetConnString()
        {
            return _config.GetConnectionString("DefaultConnection");
        }

        public void Delete(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CIF> GetAll()
        {
            using (SqlConnection conn = new SqlConnection(GetConnString()))
            {
                string strSql = @"select * from CIF order by CIF_Name asc";
                return conn.Query<CIF>(strSql);
            }
        }

        public CIF GetById(string id)
        {
            throw new NotImplementedException();
        }

        public void Insert(CIF obj)
        {
           
        }

        public void Update(CIF obj)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CIF> GetByName(string name)
        {
            using(SqlConnection conn = new SqlConnection(GetConnString()))
            {
                List<CIF> listCIF = new List<CIF>();
                string strSql = @"select Comp_ID,CIF_No,CIF_Name,CIF_Type from CIF 
                    where CIF_Name like @CIF_Name order by CIF_Name asc";
                SqlCommand cmd = new SqlCommand(strSql, conn);
                cmd.Parameters.AddWithValue("@CIF_Name", $"%{name}%");

                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        CIF objCif = new CIF
                        {
                            Comp_ID = dr["Comp_ID"].ToString(),
                            CIF_No = dr["CIF_No"].ToString(),
                            CIF_Name = dr["CIF_Name"].ToString(),
                            CIF_Type = Convert.ToByte(dr["CIF_Type"])
                        };
                        listCIF.Add(objCif);
                    }
                }
                dr.Close();
                cmd.Dispose();
                conn.Close();

                return listCIF;
            }
        }
    }
}
