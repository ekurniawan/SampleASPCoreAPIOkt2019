using BO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace DAL
{
    public class PraContractDAL : IPraContract
    {
        private IConfiguration _config;
        public PraContractDAL(IConfiguration config)
        {
            _config = config;
        }

        private string GetConnString()
        {
            return _config.GetConnectionString("DefaultConnection");
        }

        public void Delete(string id)
        {
            using(SqlConnection conn = new SqlConnection(GetConnString()))
            {
                string strSql = @"delete from PraContract where ID=@ID";
                SqlCommand cmd = new SqlCommand(strSql, conn);
                cmd.Parameters.AddWithValue("@ID", id);

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (SqlException sqlEx)
                {
                    throw new Exception($"Kesalahan: {sqlEx}");
                }
            }
        }

        public IEnumerable<PraContract> GetAll()
        {
            List<PraContract> lstPraContract = new List<PraContract>();
            using (SqlConnection conn = new SqlConnection(GetConnString()))
            {
                string strSql = @"select ID,Comp_ID,CIF_No,ApplicationNo from PraContract order by ApplicationNo asc";
                SqlCommand cmd = new SqlCommand(strSql, conn);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        PraContract objPraContract = new PraContract
                        {
                            ID = dr["ID"].ToString(),
                            Comp_ID = dr["Comp_ID"].ToString(),
                            CIF_No = dr["CIF_No"].ToString(),
                            ApplicationNo = dr["ApplicationNo"].ToString()
                        };
                        lstPraContract.Add(objPraContract);
                    }
                }
                dr.Close();
                cmd.Dispose();
                conn.Close();

                return lstPraContract;
            }
        }

        public PraContract GetById(string id)
        {
            throw new NotImplementedException();
        }

        public void Insert(PraContract obj)
        {
            using (SqlConnection conn = new SqlConnection(GetConnString()))
            {
                string strSql = @"insert into PraContract(ID,Comp_ID,CIF_No,ApplicationNo) 
                   values(@ID,@Comp_ID,@CIF_No,@ApplicationNo)";
                SqlCommand cmd = new SqlCommand(strSql, conn);
                cmd.Parameters.AddWithValue("@ID", Guid.NewGuid().ToString());
                cmd.Parameters.AddWithValue("@CIF_No", obj.CIF_No);
                cmd.Parameters.AddWithValue("@Comp_ID", obj.Comp_ID);
                cmd.Parameters.AddWithValue("@ApplicationNo", obj.ApplicationNo);

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (SqlException sqlEx)
                {
                    throw new Exception($"Kesalahan: {sqlEx.Number}  Message: {sqlEx.Message}");
                }
                finally
                {
                    cmd.Dispose();
                    conn.Close();
                }
            }
        }

        public void Update(PraContract obj)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PraContract> GetByName(string name)
        {
            throw new NotImplementedException();
        }
    }
}
