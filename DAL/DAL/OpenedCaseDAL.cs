namespace DAL
{
    using System;
    using System.Data;

    public class OpenedCaseDAL
    {
        public static void DeleteOpenedCase(string IDs)
        {
            DataHelper.DeleteByIDs("OpenedCase", IDs);
        }

        public static void DeleteOpenedCaseByCaseIDandCompanyID(string companyID, string caseID)
        {
            string sql = "Delete OpenedCase where companyID={0} and caseID={1}";
            DataHelper.ExecuteQuerys(string.Format(sql, companyID, caseID));
        }

        public static DataSet GetOpenedCaseList()
        {
            return DataHelper.GetAll("OpenedCase");
        }

        public static DataSet GetOpenedCaseList(string where)
        {
            return DataHelper.GetList("OpenedCase", where);
        }

        public static DataSet GetOpenedCaseList(string field, string where)
        {
            return DataHelper.GetList("OpenedCase", field, where);
        }

        public static void InsertOpenedCase(string CaseID, string UserID, string CompanyID)
        {
            string sql = "insert openedcase values( {0},{1},{2})";
            DataHelper.ExecuteQuerys(string.Format(sql, UserID, CompanyID, CaseID));
        }
    }
}

