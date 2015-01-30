namespace BLL
{
    using Common;
    using DAL;
    using System;
    using System.Data;

    public class BalanceBLL
    {
        private DAL.BalanceDAL BalanceDAL;
        private string tableName;

        public BalanceBLL(int companyID)
        {
            this.BalanceDAL = new DAL.BalanceDAL(companyID);
            this.tableName = Tools.GetBalanceTableName(companyID.ToString());
        }

        public void DeleteBalance(string IDs)
        {
            this.BalanceDAL.DeleteBalance(IDs);
        }

        public DataSet GetBalanceList(string where)
        {
            return this.BalanceDAL.GetBalanceList("BalanceDate,tbNewBalance", where);
        }

        public DataSet GetBalancePagingitems(int PageCount, int currentPage, string where)
        {
            string Fields = "*";
            string order = "desc";
            return DataHelper.GetPagingDataSet1(this.tableName, Fields, PageCount, currentPage, where, order);
        }

        public int GetBalanceTotalItems(string where)
        {
            return DataHelper.GetTableTotalItems(this.tableName, where);
        }
    }
}

