namespace DAL
{
    using System;
    using System.Data;
    using System.Data.SqlClient;

    public class GroupDAL
    {
        public static void DeleteGroup(string IDs)
        {
            DataHelper.DeleteByIDs("GroupTable", IDs);
        }

        public static DataSet GetGroupList()
        {
            return DataHelper.GetAll("GroupTable");
        }

        public static DataSet GetGroupList(string where)
        {
            return DataHelper.GetList("GroupTable", where);
        }

        public static DataSet GetGroupList(string field, string where)
        {
            return DataHelper.GetList("GroupTable", field, where);
        }

        public static int GetGroupTotalItems(string where)
        {
            string sql = "select count(*) from GroupTable ";
            if (where != "")
            {
                sql = sql + " where " + where;
            }
            return (int) DataHelper.ExecuteScalar(sql);
        }

        public static DataSet GetLeadID(string userID, string CompanyID)
        {
            string where = "id in (select groupID from companyuser where companyID={0} and userID={1})";
            where = string.Format(where, CompanyID, userID);
            return DataHelper.GetList("GroupTable", "LeadID", where);
        }

        public static int InsertGroup(string GroupName, int LeadID, int CompanyID)
        {
            SqlConnection conn = new SqlConnection(DataHelper.conn);
            string updateSql = "insert   GroupTable (GroupName,LeadID,CompanyID)  values (@GroupName,@LeadID,@CompanyID)";
            SqlCommand cmd = new SqlCommand(updateSql, conn);
            SqlParameter sp = new SqlParameter("@GroupName", SqlDbType.NVarChar) {
                Value = GroupName
            };
            SqlParameter sp1 = new SqlParameter("@LeadID", SqlDbType.Int) {
                Value = LeadID
            };
            SqlParameter sp3 = new SqlParameter("@CompanyID", SqlDbType.Int) {
                Value = CompanyID
            };
            cmd.Parameters.Add(sp);
            cmd.Parameters.Add(sp1);
            cmd.Parameters.Add(sp3);
            int id = 0;
            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                string sql = "select max(id) from GroupTable";
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

        public static void UpdateGroup(string GroupName, int LeadID, int ID)
        {
            SqlConnection conn = new SqlConnection(DataHelper.conn);
            string updateSql = "UPDATE   GroupTable SET  GroupName=@GroupName ,LeadID =@LeadID WHERE ID=@ID";
            SqlCommand cmd = new SqlCommand(updateSql, conn);
            SqlParameter sp = new SqlParameter("@ID", SqlDbType.Int) {
                Value = ID
            };
            SqlParameter sp1 = new SqlParameter("@LeadID", SqlDbType.Int) {
                Value = LeadID
            };
            SqlParameter sp2 = new SqlParameter("@GroupName", SqlDbType.NVarChar) {
                Value = GroupName
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

