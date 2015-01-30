namespace BLL
{
    using DAL;
    using System;
    using System.Data;

    public class PatchBLL
    {
        public static void DeletePatch(string IDs, string CompanyID)
        {
            PatchDAL.DeletePatch(CompanyID, IDs);
        }

        public static DataSet GetCompanyPatchListByCompanyID(string CompanyID)
        {
            string where = "CompanyID={0} and ExpireDate > '{1}'";
            string date = DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00";
            return PatchDAL.GetPatchList(string.Format(where, CompanyID, date));
        }

        public static DataSet GetPatchByID(string id)
        {
            return PatchDAL.GetPatchList("ID=" + id);
        }

        public static DataSet GetPatchList()
        {
            return PatchDAL.GetPatchList();
        }

        public static DataSet GetPatchList(string where)
        {
            return PatchDAL.GetPatchList(where);
        }

        public static DataSet GetPatchPagingitems(int PageCount, int currentPage, string where)
        {
            string Fields = "*";
            string tablename = "Patch";
            string order = "desc";
            return DataHelper.GetPagingDataSet1(tablename, Fields, PageCount, currentPage, where, order);
        }

        public static int GetPatchTotalItems(string where)
        {
            return PatchDAL.GetPatchTotalItems(where);
        }

        public static int InsertPatch(string PatchName, DateTime ExprieDate, DateTime ImportTime, int CompanyID)
        {
            return PatchDAL.InsertPatch(PatchName, ExprieDate, ImportTime, CompanyID);
        }

        public static void UpdatePatch(string ID, string PatchName, DateTime ExprieDate)
        {
            PatchDAL.UpdatePatch(PatchName, ExprieDate, int.Parse(ID));
        }
    }
}

