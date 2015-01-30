namespace BLL
{
    using DAL;
    using System;
    using System.Data;

    public class OpenedCaseBLL
    {
        public static void DeleteOpenedCase(string IDs)
        {
            OpenedCaseDAL.DeleteOpenedCase(IDs);
        }

        public static DataSet GetOpenedCaseByID(string id)
        {
            return OpenedCaseDAL.GetOpenedCaseList("ID=" + id);
        }

        public static DataSet GetOpenedCaseList()
        {
            return OpenedCaseDAL.GetOpenedCaseList();
        }

        public static DataSet GetOpenedCaseList(string Fields, string where)
        {
            return OpenedCaseDAL.GetOpenedCaseList(Fields, where);
        }

        public static void InsertOpenedCase(string UserID, string CaseID, string CompanyID)
        {
            OpenedCaseDAL.InsertOpenedCase(CaseID, UserID, CompanyID);
        }

        public static void UpdateOpenedCase(string IDs, string CaseID, string CompanyID)
        {
            OpenedCaseDAL.DeleteOpenedCaseByCaseIDandCompanyID(CompanyID, CaseID);
            if (IDs != "")
            {
                string[] users = IDs.Split(new char[] { ',' });
                foreach (string user in users)
                {
                    if (user.Trim() != "")
                    {
                        OpenedCaseDAL.InsertOpenedCase(CaseID, user, CompanyID);
                    }
                }
            }
        }
    }
}

