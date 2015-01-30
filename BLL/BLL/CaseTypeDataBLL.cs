namespace BLL
{
    using DAL;
    using System;
    using System.Collections;
    using System.Data;

    public class CaseTypeDataBLL
    {
        public static DataSet GetCaseTypeDataByID(string id)
        {
            return CaseTypeDataDAL.GetCaseTypeDataList("ID=" + id);
        }

        public static DataSet GetCaseTypeDataList()
        {
            return CaseTypeDataDAL.GetCaseTypeDataList();
        }

        public static DataSet GetCaseTypeDataList(string Fields, string where)
        {
            return CaseTypeDataDAL.GetCaseTypeDataList(Fields, where);
        }

        public static void UpdateCaseTypeData(ArrayList sqls)
        {
            DataHelper.ExecuteQuerysArray(sqls);
        }
    }
}

