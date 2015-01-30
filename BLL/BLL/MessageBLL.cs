namespace BLL
{
    using DAL;
    using System;
    using System.Data;

    public class MessageBLL
    {
        public static void DeleteMessage(string IDs)
        {
            MessageDAL.DeleteMessage(IDs);
        }

        public static void DeleteReceivedMessage(string IDs)
        {
            MessageDAL.DeleteReceivedMessage(IDs);
        }

        public static string GetAlertCommentCount(string sql)
        {
            return DataHelper.ExecuteScalar(sql).ToString();
        }

        public static DataSet GetMessageByID(string id)
        {
            return MessageDAL.GetMessageList("ID=" + id);
        }

        public static DataSet GetMessageList()
        {
            return MessageDAL.GetMessageList();
        }

        public static DataSet GetMessagePagingitems(int PageCount, int currentPage, string where)
        {
            string Fields = "*";
            string tablename = "messagetable";
            string order = "desc";
            return DataHelper.GetPagingDataSet1(tablename, Fields, PageCount, currentPage, where, order);
        }

        public static int GetMessageTotalItems(string where)
        {
            return MessageDAL.GetMessageTotalItems(where);
        }

        public static string GetNewEmailCount(string userName)
        {
            string sql = "select count(*) from MessageReceivedList where status ='0' and Owner= '{0}'";
            return DataHelper.ExecuteScalar(string.Format(sql, userName)).ToString();
        }

        public static DataSet GetReceivedMessageByID(string id)
        {
            return MessageDAL.GetReceivedMessageList("ID=" + id);
        }

        public static DataSet GetReceivedMessagePagingitems(int PageCount, int currentPage, string where)
        {
            string Fields = "*";
            string tablename = "MessageReceivedList";
            string order = "desc";
            return DataHelper.GetPagingDataSet1(tablename, Fields, PageCount, currentPage, where, order);
        }

        public static int GetReceivedMessageTotalItems(string where)
        {
            return MessageDAL.GetReceivedMessageTotalItems(where);
        }

        public static DataSet GetReportMessageList(string where, string order)
        {
            return MessageDAL.GetReportMessageList(where, order);
        }

        public static void SendMessage(string Title, string Body, string Sender, string Recipient, string Status, string Attachment, DateTime SentOn)
        {
            if (Recipient.Length >= 1)
            {
                int messageID = MessageDAL.InsertMessage(Title, Body, Sender, Recipient, Status, Attachment, SentOn);
                string[] users = Recipient.Split(new char[] { ';' });
                foreach (string user in users)
                {
                    string recep = user.Trim();
                    if (recep != "")
                    {
                        MessageDAL.InsertReceivedMessage(Title, Body, Sender, Recipient, Status, Attachment, SentOn, recep);
                    }
                }
            }
        }

        public static void UpdateReceivedMessageStatus(string status, string where)
        {
            MessageDAL.UpdateReceivedMessageStatus(status, where);
        }
    }
}

