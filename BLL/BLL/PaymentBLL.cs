namespace BLL
{
    using Common;
    using DAL;
    using System;
    using System.Data;

    public class PaymentBLL
    {
        private DAL.PaymentDAL PaymentDAL;
        private string tableName;

        public PaymentBLL(int companyID)
        {
            this.PaymentDAL = new DAL.PaymentDAL(companyID);
            this.tableName = Tools.GetPaymentTableName(companyID.ToString());
        }

        public void DeletePayment(string IDs)
        {
            this.PaymentDAL.DeletePayment(IDs);
        }

        public static DataSet GetAlertPaymentList(string sql)
        {
            return DataHelper.GetList(sql);
        }

        public DataSet GetPaymentList(string where)
        {
            return this.PaymentDAL.GetPaymentList("*", where);
        }

        public DataSet GetPaymentPagingitems(int PageCount, int currentPage, string where)
        {
            string Fields = "*";
            string order = "desc";
            return DataHelper.GetPagingDataSet1(this.tableName, Fields, PageCount, currentPage, where, order);
        }

        public int GetPaymentTotalItems(string where)
        {
            return DataHelper.GetTableTotalItems(this.tableName, where);
        }
    }
}

