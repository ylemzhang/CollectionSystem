namespace BLL
{
    using DAL;
    using System;
    using System.Data;

    public class CaseTypeBLL
    {
        public static void DeleteCaseType(string IDs)
        {
            CaseTypeDAL.DeleteCaseType(IDs);
        }

        public static DataSet GetCaseTypeByID(string id)
        {
            return CaseTypeDAL.GetCaseTypeList("ID=" + id);
        }

        public static DataSet GetCaseTypeList()
        {
            return CaseTypeDAL.GetCaseTypeList();
        }

        public static DataSet GetCaseTypeList(string Fields, string where)
        {
            return CaseTypeDAL.GetCaseTypeList(Fields, where);
        }

        public static void UpdateCaseType(string UserID, string CaseTypeName, string isdisplay, string caseTypeID)
        {
            if (caseTypeID == "")
            {
                CaseTypeDAL.InsertCaseType(CaseTypeName, UserID, isdisplay);
            }
            else
            {
                CaseTypeDAL.UpdateCaseType(CaseTypeName, isdisplay, caseTypeID);
            }
        }
    }
}

