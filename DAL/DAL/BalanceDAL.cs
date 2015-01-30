namespace DAL
{
    using Common;
    using System;
    using System.Data;

    public class BalanceDAL
    {
        private string TableName;

        public BalanceDAL(int CompanyID)
        {
            this.TableName = Tools.GetBalanceTableName(CompanyID.ToString());
        }

        public void DeleteBalance(string IDs)
        {
            DataHelper.DeleteByIDs(this.TableName, IDs);
        }

        public DataSet GetBalanceList()
        {
            return DataHelper.GetAll(this.TableName);
        }

        public DataSet GetBalanceList(string where)
        {
            return DataHelper.GetListByDesc(this.TableName, where);
        }

        public DataSet GetBalanceList(string field, string where)
        {
            return DataHelper.GetListByDesc(this.TableName, field, where);
        }
    }
}

