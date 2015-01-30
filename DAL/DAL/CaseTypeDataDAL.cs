namespace DAL
{
    using System;
    using System.Data;

    public class CaseTypeDataDAL
    {
        public static DataSet GetCaseTypeDataList()
        {
            return DataHelper.GetAll("CaseTypeData");
        }

        public static DataSet GetCaseTypeDataList(string where)
        {
            return DataHelper.GetList("CaseTypeData", where);
        }

        public static DataSet GetCaseTypeDataList(string field, string where)
        {
            return DataHelper.GetList("CaseTypeData", field, where);
        }

        public static void InsertCaseTypeData(string CaseID, string CompanyID, string CaseTypeID)
        {
            string sql = "insert CaseTypeData values( {0},{1},{2})";
            DataHelper.ExecuteQuerys(string.Format(sql, CaseID, CompanyID, CaseTypeID));
        }
    }
}

