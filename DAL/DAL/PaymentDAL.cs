namespace DAL
{
    using Common;
    using System;
    using System.Data;

    public class PaymentDAL
    {
        private string TableName;

        public PaymentDAL(int CompanyID)
        {
            this.TableName = Tools.GetPaymentTableName(CompanyID.ToString());
        }

        public void DeletePayment(string IDs)
        {
            DataHelper.DeleteByIDs(this.TableName, IDs);
        }

        public DataSet GetPaymentList()
        {
            return DataHelper.GetAll(this.TableName);
        }

        public DataSet GetPaymentList(string where)
        {
            return DataHelper.GetList(this.TableName, where);
        }

        public DataSet GetPaymentList(string field, string where)
        {
            return DataHelper.GetList(this.TableName, field, where);
        }
    }
}

