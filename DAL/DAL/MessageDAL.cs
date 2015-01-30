namespace DAL
{
    using System;
    using System.Data;
    using System.Data.SqlClient;

    public class MessageDAL
    {
        public static void DeleteMessage(string IDs)
        {
            string status = "1";
            string where = "id in ({0})";
            where = string.Format(where, IDs);
            UpdateMessageStatus(status, where);
        }

        public static void DeleteReceivedMessage(string IDs)
        {
            DataHelper.DeleteByIDs("MessageReceivedList", IDs);
        }

        public static DataSet GetMessageList()
        {
            return DataHelper.GetAll("Messagetable");
        }

        public static DataSet GetMessageList(string where)
        {
            return DataHelper.GetListByDesc("Messagetable", where);
        }

        public static int GetMessageTotalItems(string where)
        {
            string sql = "select count(*) from Messagetable ";
            if (where != "")
            {
                sql = sql + " where " + where;
            }
            return (int) DataHelper.ExecuteScalar(sql);
        }

        public static DataSet GetReceivedMessageList(string where)
        {
            return DataHelper.GetListByDesc("MessageReceivedList", where);
        }

        public static int GetReceivedMessageTotalItems(string where)
        {
            string sql = "select count(*) from MessageReceivedList ";
            if (where != "")
            {
                sql = sql + " where " + where;
            }
            return (int) DataHelper.ExecuteScalar(sql);
        }

        public static DataSet GetReportMessageList(string where, string order)
        {
            string sql = "select sender, recipient,senton,title,body, id from Messagetable where {0} {1}";
            return DataHelper.GetList(string.Format(sql, where, order));
        }

        public static int InsertMessage(string Title, string Body, string Sender, string Recipient, string Status, string Attachment, DateTime SentOn)
        {
            SqlConnection conn = new SqlConnection(DataHelper.conn);
            string updateSql = "insert   Messagetable (Title,Body,Sender,Recipient,Status,SentOn,Attachment) values (@Title,@Body,@Sender,@Recipient,@Status,@SentOn,@Attachment)";
            SqlCommand cmd = new SqlCommand(updateSql, conn);
            SqlParameter sp = new SqlParameter("@Title", SqlDbType.NVarChar) {
                Value = Title
            };
            SqlParameter sp3 = new SqlParameter("@Body", SqlDbType.NVarChar) {
                Value = Body
            };
            SqlParameter sp1 = new SqlParameter("@Sender", SqlDbType.NVarChar) {
                Value = Sender
            };
            SqlParameter sp2 = new SqlParameter("@SentOn", SqlDbType.DateTime) {
                Value = SentOn
            };
            SqlParameter sp4 = new SqlParameter("@Recipient", SqlDbType.NVarChar) {
                Value = Recipient
            };
            SqlParameter sp5 = new SqlParameter("@Status", SqlDbType.NVarChar) {
                Value = Status
            };
            SqlParameter sp6 = new SqlParameter("@Attachment", SqlDbType.NVarChar) {
                Value = Attachment
            };
            cmd.Parameters.Add(sp);
            cmd.Parameters.Add(sp1);
            cmd.Parameters.Add(sp2);
            cmd.Parameters.Add(sp3);
            cmd.Parameters.Add(sp4);
            cmd.Parameters.Add(sp5);
            cmd.Parameters.Add(sp6);
            int id = 0;
            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                string sql = "select max(id) from Messagetable";
                cmd = new SqlCommand(sql, conn);
                id = (int) cmd.ExecuteScalar();
            }
            finally
            {
                conn.Close();
            }
            return id;
        }

        public static void InsertReceivedMessage(string Title, string Body, string Sender, string Recipient, string Status, string Attachment, DateTime SentOn, string Owner)
        {
            SqlConnection conn = new SqlConnection(DataHelper.conn);
            string updateSql = "insert   MessageReceivedList (Title,Body,Sender,Recipient,Status,SentOn,Attachment,Owner) values (@Title,@Body,@Sender,@Recipient,@Status,@SentOn,@Attachment,@Owner)";
            SqlCommand cmd = new SqlCommand(updateSql, conn);
            SqlParameter sp = new SqlParameter("@Title", SqlDbType.NVarChar) {
                Value = Title
            };
            SqlParameter sp1 = new SqlParameter("@Sender", SqlDbType.NVarChar) {
                Value = Sender
            };
            SqlParameter sp2 = new SqlParameter("@SentOn", SqlDbType.DateTime) {
                Value = SentOn
            };
            SqlParameter sp3 = new SqlParameter("@Body", SqlDbType.NVarChar) {
                Value = Body
            };
            SqlParameter sp4 = new SqlParameter("@Recipient", SqlDbType.NVarChar) {
                Value = Recipient
            };
            SqlParameter sp5 = new SqlParameter("@Status", SqlDbType.NVarChar) {
                Value = Status
            };
            SqlParameter sp6 = new SqlParameter("@Attachment", SqlDbType.NVarChar) {
                Value = Attachment
            };
            SqlParameter sp7 = new SqlParameter("@Owner", SqlDbType.NVarChar) {
                Value = Owner
            };
            cmd.Parameters.Add(sp);
            cmd.Parameters.Add(sp1);
            cmd.Parameters.Add(sp2);
            cmd.Parameters.Add(sp3);
            cmd.Parameters.Add(sp4);
            cmd.Parameters.Add(sp5);
            cmd.Parameters.Add(sp6);
            cmd.Parameters.Add(sp7);
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

        public static void UpdateMessageStatus(string status, string where)
        {
            string sql = "Update Messagetable set status='{0}' where {1} ";
            DataHelper.ExecuteQuerys(string.Format(sql, status, where));
        }

        public static void UpdateReceivedMessageStatus(string status, string where)
        {
            string sql = "Update MessageReceivedList set status='{0}' where {1} ";
            DataHelper.ExecuteQuerys(string.Format(sql, status, where));
        }
    }
}

