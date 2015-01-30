namespace BLL
{
    using DAL;
    using System;
    using System.Data;

    public class ReadCaseUsersBLL
    {
        public static void DeleteReadCaseUsers(string IDs)
        {
            ReadCaseUsersDAL.DeleteReadCaseUsers(IDs);
        }

        public static DataSet GetReadCaseUsersByID(string id)
        {
            return ReadCaseUsersDAL.GetReadCaseUsersList("ID=" + id);
        }

        public static DataSet GetReadCaseUsersList()
        {
            return ReadCaseUsersDAL.GetReadCaseUsersList();
        }

        public static DataSet GetReadCaseUsersList(string where)
        {
            return ReadCaseUsersDAL.GetReadCaseUsersList(where);
        }

        public static void InsertReadCaseUsers(string[] UserIDs, string CaseID, string CompanyID)
        {
            ReadCaseUsersDAL.InsertReadCaseUsers(UserIDs, CaseID, CompanyID);
        }
    }
}

