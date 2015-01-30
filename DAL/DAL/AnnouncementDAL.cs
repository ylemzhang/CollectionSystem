namespace DAL
{
    using System;
    using System.Data;
    using System.Data.SqlClient;

    public class AnnouncementDAL
    {
        public static void DeleteAnnouncement(string IDs)
        {
            DataHelper.DeleteByIDs("Announcement", IDs);
        }

        public static DataSet GetAnnouncementList()
        {
            return DataHelper.GetAll("Announcement");
        }

        public static DataSet GetAnnouncementList(string where)
        {
            return DataHelper.GetListByDesc("Announcement", where);
        }

        public static void InsertAnnouncement(string title, string body, DateTime ExpireDate, string Createby, string Attachment)
        {
            SqlConnection conn = new SqlConnection(DataHelper.conn);
            string updateSql = "insert   Announcement (title,body,ExpireDate,Createby,Createon,Modifyby, Modifyon,Attachment) values (@title,@body,@ExpireDate,@Createby,@Createon,@Modifyby, @Modifyon,@Attachment)";
            SqlCommand cmd = new SqlCommand(updateSql, conn);
            SqlParameter sp = new SqlParameter("@title", SqlDbType.NVarChar) {
                Value = title
            };
            SqlParameter sp1 = new SqlParameter("@body", SqlDbType.NVarChar) {
                Value = body
            };
            SqlParameter sp2 = new SqlParameter("@ExpireDate", SqlDbType.DateTime);
            if (ExpireDate == DateTime.MinValue)
            {
                sp2.Value = DBNull.Value;
            }
            else
            {
                sp2.Value = ExpireDate;
            }
            SqlParameter sp4 = new SqlParameter("@Createby", SqlDbType.NVarChar) {
                Value = Createby
            };
            DateTime dt = DateTime.Now;
            SqlParameter sp5 = new SqlParameter("@Createon", SqlDbType.DateTime) {
                Value = dt
            };
            SqlParameter sp6 = new SqlParameter("@Modifyby", SqlDbType.NVarChar) {
                Value = Createby
            };
            SqlParameter sp7 = new SqlParameter("@Modifyon", SqlDbType.DateTime) {
                Value = dt
            };
            SqlParameter sp8 = new SqlParameter("@Attachment", SqlDbType.NVarChar) {
                Value = Attachment
            };
            cmd.Parameters.Add(sp);
            cmd.Parameters.Add(sp1);
            cmd.Parameters.Add(sp2);
            cmd.Parameters.Add(sp4);
            cmd.Parameters.Add(sp5);
            cmd.Parameters.Add(sp6);
            cmd.Parameters.Add(sp7);
            cmd.Parameters.Add(sp8);
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

        public static void UpdateAnnouncement(string title, string body, DateTime ExpireDate, string modifyBy, string ID, string Attachment)
        {
            SqlConnection conn = new SqlConnection(DataHelper.conn);
            string updateSql = "UPDATE   Announcement SET  title = @title,body = @body, ExpireDate = @ExpireDate ,ModifyBy=@ModifyBy,ModifyOn=@ModifyOn,Attachment=@Attachment WHERE ID = @ID";
            SqlCommand cmd = new SqlCommand(updateSql, conn);
            SqlParameter sp = new SqlParameter("@title", SqlDbType.NVarChar) {
                Value = title
            };
            SqlParameter sp1 = new SqlParameter("@body", SqlDbType.NVarChar) {
                Value = body
            };
            SqlParameter sp2 = new SqlParameter("@ExpireDate", SqlDbType.DateTime);
            if (ExpireDate == DateTime.MinValue)
            {
                sp2.Value = DBNull.Value;
            }
            else
            {
                sp2.Value = ExpireDate;
            }
            SqlParameter sp6 = new SqlParameter("@ModifyBy", SqlDbType.NVarChar) {
                Value = modifyBy
            };
            DateTime dt = DateTime.Now;
            SqlParameter sp7 = new SqlParameter("@ModifyOn", SqlDbType.DateTime) {
                Value = dt
            };
            SqlParameter sp3 = new SqlParameter("@ID", SqlDbType.Int) {
                Value = int.Parse(ID)
            };
            SqlParameter sp8 = new SqlParameter("@Attachment", SqlDbType.NVarChar) {
                Value = Attachment
            };
            cmd.Parameters.Add(sp);
            cmd.Parameters.Add(sp1);
            cmd.Parameters.Add(sp2);
            cmd.Parameters.Add(sp3);
            cmd.Parameters.Add(sp6);
            cmd.Parameters.Add(sp7);
            cmd.Parameters.Add(sp8);
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

