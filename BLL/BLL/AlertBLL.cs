namespace BLL
{
    using DAL;
    using System;
    using System.Data;

    public class AlertBLL
    {
        public static void DeleteAlert(string IDs)
        {
            AlertDAL.DeleteAlert(IDs);
        }

        public static DataSet GetAlertByID(string id)
        {
            return AlertDAL.GetAlertList("ID=" + id);
        }

        public static DataSet GetAlertCompanys(string sql)
        {
            return DataHelper.GetList(sql);
        }

        public static DataSet GetAlertList()
        {
            return AlertDAL.GetAlertList();
        }

        public static DataSet GetAlertList(string where)
        {
            return AlertDAL.GetAlertList(where);
        }

        public static DataSet GetAlertListByCaseID(string CompanyID, string CaseID)
        {
            string where = "CompanyID= {1} and CaseID={0}";
            return AlertDAL.GetAlertList(string.Format(where, CaseID, CompanyID));
        }

        public static DataSet GetAlertPagingitems(int PageCount, int currentPage, string where)
        {
            string Fields = "*";
            string tablename = "AlertTable";
            string order = "desc";
            return DataHelper.GetPagingDataSet1(tablename, Fields, PageCount, currentPage, where, order);
        }

        public static DataSet GetCompanyAlertListByCompanyID(string CompanyID)
        {
            string where = "CompanyID={0}";
            return AlertDAL.GetAlertList(string.Format(where, CompanyID));
        }

        public static void UpdateAlert(string AlertID, int AlertType, int CaseID, decimal Num1, string Str1, string person, DateTime Date1, int CompanyID, string CaseOwnerID)
        {
            AlertDAL.InsertAlert(AlertType, CaseID, Num1, Str1, person, Date1, CompanyID, CaseOwnerID);
        }
    }
}

