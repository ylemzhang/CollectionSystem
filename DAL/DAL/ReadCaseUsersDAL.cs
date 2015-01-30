namespace DAL
{
    using System;
    using System.Data;
    using System.Data.SqlClient;

    public class ReadCaseUsersDAL
    {
        public static void DeleteReadCaseUsers(string IDs)
        {
            DataHelper.DeleteByIDs("ReadCaseUsers", IDs);
        }

        public static DataSet GetReadCaseUsersList()
        {
            return DataHelper.GetAll("ReadCaseUsers");
        }

        public static DataSet GetReadCaseUsersList(string where)
        {
            return DataHelper.GetList("ReadCaseUsers", where);
        }

        public static DataSet GetReadCaseUsersList(string field, string where)
        {
            return DataHelper.GetList("ReadCaseUsers", field, where);
        }

        public static void InsertReadCaseUsers(string[] UserIDs, string CaseIDs, string companyID)
        {
            SqlConnection conn = new SqlConnection(DataHelper.conn);
            string updateSql = "delete  ReadCaseUsers where companyid= {0} and CaseID in ({1})";
            SqlCommand cmd = new SqlCommand(string.Format(updateSql, companyID, CaseIDs), conn);
            string insertSql = "insert ReadCaseUsers (UserID,CompanyID,CaseID) values ({0},{1},{2})";
            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                foreach (string userID in UserIDs)
                {
                    if (userID.Trim() != string.Empty)
                    {
                        string[] caseIDStrs = CaseIDs.Split(new char[] { ',' });
                        foreach (string caseID in caseIDStrs)
                        {
                            new SqlCommand(string.Format(insertSql, userID, companyID, caseID), conn).ExecuteNonQuery();
                        }
                    }
                }
            }
            finally
            {
                conn.Close();
                conn = null;
            }
        }
    }
}

