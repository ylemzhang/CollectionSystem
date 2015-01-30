namespace DAL
{
    using System;
    using System.Data;
    using System.Data.SqlClient;

    public class PatchDAL
    {
        public static void DeletePatch(string CompanyID, string IDs)
        {
            DataHelper.DeleteByIDs("Patch", IDs);
            string sql = "delete companyCase_{0} where PatchID in ({1})";
            DataHelper.ExecuteQuerys(string.Format(sql, CompanyID, IDs));
        }

        public static DataSet GetPatchList()
        {
            return DataHelper.GetAll("Patch");
        }

        public static DataSet GetPatchList(string where)
        {
            return DataHelper.GetList("Patch", where);
        }

        public static DataSet GetPatchList(string field, string where)
        {
            return DataHelper.GetList("Patch", field, where);
        }

        public static int GetPatchTotalItems(string where)
        {
            string sql = "select count(*) from Patch ";
            if (where != "")
            {
                sql = sql + " where " + where;
            }
            return (int) DataHelper.ExecuteScalar(sql);
        }

        public static int InsertPatch(string PatchName, DateTime ExpireDate, DateTime ImportTime, int CompanyID)
        {
            SqlConnection conn = new SqlConnection(DataHelper.conn);
            string updateSql = "insert   Patch (PatchName,ExpireDate,ImportTime,CompanyID)  values (@PatchName,@ExpireDate,@ImportTime,@CompanyID)";
            SqlCommand cmd = new SqlCommand(updateSql, conn);
            SqlParameter sp = new SqlParameter("@PatchName", SqlDbType.NVarChar) {
                Value = PatchName
            };
            SqlParameter sp1 = new SqlParameter("@ExpireDate", SqlDbType.DateTime) {
                Value = ExpireDate
            };
            SqlParameter sp2 = new SqlParameter("@ImportTime", SqlDbType.DateTime) {
                Value = ImportTime
            };
            SqlParameter sp3 = new SqlParameter("@CompanyID", SqlDbType.Int) {
                Value = CompanyID
            };
            cmd.Parameters.Add(sp);
            cmd.Parameters.Add(sp1);
            cmd.Parameters.Add(sp2);
            cmd.Parameters.Add(sp3);
            int id = 0;
            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                string sql = "select max(id) from Patch";
                cmd = new SqlCommand(sql, conn);
                id = (int) cmd.ExecuteScalar();
            }
            finally
            {
                conn.Close();
                conn = null;
            }
            return id;
        }

        public static void UpdatePatch(string PatchName, DateTime ExpireDate, int ID)
        {
            SqlConnection conn = new SqlConnection(DataHelper.conn);
            string updateSql = "UPDATE   Patch SET  PatchName=@PatchName ,ExpireDate =@ExpireDate WHERE ID=@ID";
            SqlCommand cmd = new SqlCommand(updateSql, conn);
            SqlParameter sp = new SqlParameter("@ID", SqlDbType.Int) {
                Value = ID
            };
            SqlParameter sp1 = new SqlParameter("@ExpireDate", SqlDbType.DateTime) {
                Value = ExpireDate
            };
            SqlParameter sp2 = new SqlParameter("@PatchName", SqlDbType.NVarChar) {
                Value = PatchName
            };
            cmd.Parameters.Add(sp);
            cmd.Parameters.Add(sp1);
            cmd.Parameters.Add(sp2);
            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            finally
            {
                conn.Close();
            }
        }
    }
}

