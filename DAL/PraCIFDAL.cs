using System;
using System.Collections.Generic;
using System.Text;
using BO;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using Dapper;

namespace DAL
{
    public class PraCIFDAL : IPraCIF
    {
        private IConfiguration _config;
        public PraCIFDAL(IConfiguration config)
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
                string strSql = @"delete from PraCIF where ID=@ID";
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

        public IEnumerable<PraCIF> GetAll()
        {
            List<PraCIF> lstPraCIF = new List<PraCIF>();
            using (SqlConnection conn = new SqlConnection(GetConnString()))
            {
                string strSql = @"
                    SELECT ID,Comp_ID,CIF_No,CIF_Name,CIF_Address,NoHP
                    FROM PraCIF
                    ORDER BY CIF_Name asc";
                SqlCommand cmd = new SqlCommand(strSql, conn);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        PraCIF objPraCIF = new PraCIF
                        {
                            ID = dr["ID"].ToString(),
                            Comp_ID = dr["Comp_ID"].ToString(),
                            CIF_No = dr["CIF_No"].ToString(),
                            CIF_Name = dr["CIF_Name"].ToString(),
                            CIF_Address = dr["CIF_Address"].ToString(),
                            NoHP = dr["NoHP"].ToString()
                        };
                        lstPraCIF.Add(objPraCIF);
                    }
                }
                dr.Close();
                cmd.Dispose();
                conn.Close();

                return lstPraCIF;
            }
        }

        public PraCIF GetById(string id)
        {
            throw new NotImplementedException();
        }

        public void Insert(PraCIF obj)
        {
            using (SqlConnection conn = new SqlConnection(GetConnString()))
            {
                string strSql = @"
                    INSERT INTO PraCIF(Id,Comp_ID,CIF_No,CIF_Name,CIF_Address,NoHP) 
                    VALUES(@Id,@Comp_ID,@CIF_No,@CIF_Name,@CIF_Address,@NoHP)";

                var param = new PraCIF
                {
                    ID=Guid.NewGuid().ToString(),
                    Comp_ID = obj.Comp_ID,
                    CIF_No = obj.CIF_No,
                    CIF_Name=obj.CIF_Name,
                    CIF_Address = obj.CIF_Address,
                    NoHP = obj.NoHP
                };

                try
                {
                    conn.Execute(strSql);
                }
                catch (SqlException sqlEx)
                {
                    throw new Exception($"Kesalahan: {sqlEx.Number}  Message: {sqlEx.Message}");
                }
            }
        }

        public void Update(PraCIF obj)
        {
            using(SqlConnection conn = new SqlConnection(GetConnString()))
            {
                string strSql = @"update PraCIF set Comp_ID=@Comp_ID,CIF_No=@CIF_No,
                    CIF_Name=@CIF_Name,CIF_Address=@CIF_Address,NoHP=@NoHP 
                    where ID=@ID";
                SqlCommand cmd = new SqlCommand(strSql, conn);
                cmd.Parameters.AddWithValue("@ID",obj.ID);
                cmd.Parameters.AddWithValue("@CIF_No", obj.CIF_No);
                cmd.Parameters.AddWithValue("@Comp_ID", obj.Comp_ID);
                cmd.Parameters.AddWithValue("@CIF_Name", obj.CIF_Name);
                cmd.Parameters.AddWithValue("@CIF_Address", obj.CIF_Address);
                cmd.Parameters.AddWithValue("@NoHP", obj.NoHP);
                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (SqlException sqlEx)
                {
                    throw new Exception($"Kesalahan: {sqlEx.Number} - {sqlEx.Message}");
                }
                finally
                {
                    cmd.Dispose();
                    conn.Close();
                }
            }
        }

        public IEnumerable<PraCIF> GetByName(string name)
        {
            using (SqlConnection conn = new SqlConnection(GetConnString()))
            {
                string strSql = @"
                    SELECT ID,Comp_ID,CIF_No,CIF_Name,CIF_Address,NoHP
                    FROM PraCIF
                    WHERE CIF_Name like @CIF_Name
                    ORDER BY CIF_Name asc";

                var param = new { CIF_Name = $"%{name}%" };
                return conn.Query<PraCIF>(strSql, param);
            }
        }

        public IEnumerable<ViewCountryCity> GetAllCityWithCountry()
        {
            using(SqlConnection conn = new SqlConnection(GetConnString()))
            {
                string strSql = @"select * from ViewCountryCity";
                return conn.Query<ViewCountryCity>(strSql);
            }
        }

        public IEnumerable<SampleCity> GetCityWithCountry()
        {
            using (SqlConnection conn = new SqlConnection(GetConnString()))
            {
                var strSql = @"SELECT * FROM dbo.SampleCity LEFT JOIN
                dbo.SampleCountry ON dbo.SampleCity.CountyID = dbo.SampleCountry.CountyID";

                var results = conn.Query<SampleCity, SampleCountry, SampleCity>(strSql,
                    (SampleCity, SampleCountry) =>
                    {
                        SampleCity.SampleCountry = SampleCountry;
                        return SampleCity;
                    },splitOn: "CountyID");
                return results;
            }
        }
    }
}
