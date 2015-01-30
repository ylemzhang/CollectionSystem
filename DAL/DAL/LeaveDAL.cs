namespace DAL
{
    using System;
    using System.Data;
    using System.Data.SqlClient;

    public class LeaveDAL
    {
        public static void DeleteLeave(string IDs)
        {
            DataHelper.DeleteByIDs("LeaveManagement", IDs);
        }

        public static DataSet GetLeaveList()
        {
            return DataHelper.GetAll("LeaveManagement");
        }

        public static DataSet GetLeaveList(string where)
        {
            return DataHelper.GetList("LeaveManagement", where);
        }

        public static DataSet GetLeaveList(string field, string where)
        {
            return DataHelper.GetList("LeaveManagement", field, where);
        }

        public static void InsertLeave(int UserID, string UserName, DateTime PunchIn)
        {
            SqlConnection conn = new SqlConnection(DataHelper.conn);
            string updateSql = "insert   LeaveManagement (UserID,UserName,PunchIn)  values (@UserID,@UserName,@PunchIn)";
            SqlCommand cmd = new SqlCommand(updateSql, conn);
            SqlParameter sp = new SqlParameter("@UserID", SqlDbType.Int) {
                Value = UserID
            };
            SqlParameter sp1 = new SqlParameter("@UserName", SqlDbType.NVarChar) {
                Value = UserName
            };
            SqlParameter sp2 = new SqlParameter("@PunchIn", SqlDbType.DateTime) {
                Value = PunchIn
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

        public static void UpdateLeave(DateTime PunchOut, int UserID)
        {
            SqlConnection conn = new SqlConnection(DataHelper.conn);
            string updateSql = "UPDATE   LeaveManagement SET  PunchOut=@PunchOut  WHERE UserID = @UserID and  datediff(day,getdate(),punchIn)=0";
            SqlCommand cmd = new SqlCommand(updateSql, conn);
            SqlParameter sp = new SqlParameter("@UserID", SqlDbType.Int) {
                Value = UserID
            };
            SqlParameter sp1 = new SqlParameter("@PunchOut", SqlDbType.DateTime) {
                Value = PunchOut
            };
            cmd.Parameters.Add(sp);
            cmd.Parameters.Add(sp1);
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

        public static void UpdateLeave(DateTime PunchIn, DateTime PunchOut, int ID)
        {
            SqlConnection conn = new SqlConnection(DataHelper.conn);
            string updateSql = "UPDATE   LeaveManagement SET  PunchOut=@PunchOut ,PunchIn =@PunchIn WHERE ID=@ID";
            SqlCommand cmd = new SqlCommand(updateSql, conn);
            SqlParameter sp = new SqlParameter("@ID", SqlDbType.Int) {
                Value = ID
            };
            SqlParameter sp1 = new SqlParameter("@PunchOut", SqlDbType.DateTime);
            if (PunchOut != DateTime.MinValue)
            {
                sp1.Value = PunchOut;
            }
            else
            {
                sp1.Value = DBNull.Value;
            }
            SqlParameter sp2 = new SqlParameter("@PunchIn", SqlDbType.DateTime);
            if (PunchIn != DateTime.MinValue)
            {
                sp2.Value = PunchIn;
            }
            else
            {
                sp2.Value = DBNull.Value;
            }
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

