﻿using System;
using System.Collections.Generic;
using System.Text;
using BO;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

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
            throw new NotImplementedException();
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
                SqlCommand cmd = new SqlCommand(strSql, conn);
                cmd.Parameters.AddWithValue("@Id", Guid.NewGuid().ToString());
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
                    throw new Exception($"Kesalahan: {sqlEx.Number}  Message: {sqlEx.Message}");
                }
                finally
                {
                    cmd.Dispose();
                    conn.Close();
                }
            }
        }

        public void Update(PraCIF obj)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PraCIF> GetByName(string name)
        {
            List<PraCIF> lstPraCIF = new List<PraCIF>();
            using (SqlConnection conn = new SqlConnection(GetConnString()))
            {
                string strSql = @"
                    SELECT ID,Comp_ID,CIF_No,CIF_Name,CIF_Address,NoHP
                    FROM PraCIF
                    WHERE CIF_Name like @CIF_Name
                    ORDER BY CIF_Name asc";
                SqlCommand cmd = new SqlCommand(strSql, conn);
                cmd.Parameters.AddWithValue("@CIF_Name", $"%{name}%");

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
    }
}
