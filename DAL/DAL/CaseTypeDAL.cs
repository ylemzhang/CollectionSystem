namespace DAL
{
    using System;
    using System.Data;

    public class CaseTypeDAL
    {
        public static void DeleteCaseType(string IDs)
        {
            DataHelper.DeleteByIDs("CaseType", IDs);
        }

        public static DataSet GetCaseTypeList()
        {
            return DataHelper.GetAll("CaseType");
        }

        public static DataSet GetCaseTypeList(string where)
        {
            return DataHelper.GetList("CaseType", where);
        }

        public static DataSet GetCaseTypeList(string field, string where)
        {
            return DataHelper.GetList("CaseType", field, where);
        }

        public static void InsertCaseType(string CaseTypeName, string UserID, string isdisplay)
        {
            string sql = "insert CaseType values( {0},'{1}',{2})";
            DataHelper.ExecuteQuerys(string.Format(sql, UserID, CaseTypeName, isdisplay));
        }

        public static void UpdateCaseType(string CaseTypeName, string isdisplay, string caseTypeID)
        {
            string sql = "update CaseType set CaseTypeName= '{0}',isDisplay={1} where ID={2} ";
            DataHelper.ExecuteQuerys(string.Format(sql, CaseTypeName, isdisplay, caseTypeID));
        }
    }
}

