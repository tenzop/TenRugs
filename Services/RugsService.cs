using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using TenRugs.Models;
using WikiDataProvider.Data.Extensions;
using WikiDataProvider.Data.Interfaces;

namespace TenRugs.Services
{
    public class RugsService
    {
        public List<Rug> SelectAll()
        {
            List<Rug> rug = new List<Rug>();
            DataProvider.ExecuteCmd(
                GetConnection,
                "Rugs_SelectAll",
                inputParamMapper: null,
                map: delegate (IDataReader reader, short set)
                {
                    Rug r = MapRug(reader);
                    rug.Add(r);
                });
            return rug;
        }

        public int Insert(Rug r)
        {
            int i = 0;
            DataProvider.ExecuteNonQuery(
                GetConnection,
                "Rugs_Insert",
                inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@RugName", r.RugName);
                    paramCollection.AddWithValue("@Price", r.Price);
                    paramCollection.AddWithValue("@Size", r.Size);
                    paramCollection.AddWithValue("@Material", r.Material);
                    paramCollection.AddWithValue("@Color", r.Color);
                    paramCollection.AddWithValue("@Url", r.Url);
                 
                    SqlParameter parm = new SqlParameter("@Id", SqlDbType.Int);
                    parm.Direction = ParameterDirection.Output;
                    paramCollection.Add(parm);
                },
                returnParameters: delegate (SqlParameterCollection paramCollection)
                {
                    int.TryParse(paramCollection["@Id"].Value.ToString(), out i);
                });
            return i;
        }

        public void Update(Rug r)
        {
            DataProvider.ExecuteNonQuery(
                GetConnection,
                "Rugs_Update",
                inputParamMapper: delegate (SqlParameterCollection paramCollection) {
                    paramCollection.AddWithValue("@Id", r.Id);
                    paramCollection.AddWithValue("@RugName", r.RugName);
                    paramCollection.AddWithValue("@Price", r.Price);
                    paramCollection.AddWithValue("@Size", r.Size);
                    paramCollection.AddWithValue("@Material", r.Material);
                    paramCollection.AddWithValue("@Color", r.Color);
                    paramCollection.AddWithValue("@Url", r.Url);

                },
                returnParameters: null);
        }

        public void Delete(int id)
        {
            DataProvider.ExecuteNonQuery(
                GetConnection,
                "Rugs_Delete",
                inputParamMapper: delegate (SqlParameterCollection paramCollection) {
                    paramCollection.AddWithValue("@Id", id);
                },
                returnParameters: null);
        }

        private Rug MapRug(IDataReader reader)
        {
            Rug r = new Rug();
            int startingIndex = 0;
            r.Id = reader.GetSafeInt32(startingIndex++);
            r.RugName = reader.GetSafeString(startingIndex++);
            r.Price = reader.GetSafeDecimal(startingIndex++);
            r.Size = reader.GetSafeString(startingIndex++);
            r.Material = reader.GetSafeString(startingIndex++);
            r.Color = reader.GetSafeString(startingIndex++);
            r.Url = reader.GetSafeString(startingIndex++);

            return r;
        }

        // Alternatively, create a BaseService class
        // add this method to the base class
        protected static IDao DataProvider
        {
            get { return WikiDataProvider.Data.DataProvider.Instance; }
        }

        // Alternatively, create a BaseService class
        // add this method to the base class
        protected static SqlConnection GetConnection()
        {
            return new System.Data.SqlClient.SqlConnection(
                System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
        }
    }
}