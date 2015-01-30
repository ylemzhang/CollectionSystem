namespace BLL
{
    using Common;
    using DAL;
    using System;
    using System.Data;
    using System.Web;

    public class FieldBLL
    {
        public static void DeleteField(string IDs)
        {
            FieldDAL.DeleteField(IDs);
        }

        public static void DeleteField(string CompanyID, string ID, string FieldName, string TableType)
        {
            DataHelper.ExecuteQuerys("ALTER TABLE " + CompanyDAL.GetTableName(CompanyID, TableType) + " Drop column " + FieldName);
            FieldDAL.DeleteField(ID);
            string where = "CompanyID='{0}' and TableType ='{1}'";
            DataSet ds = FieldDAL.GetFieldList(string.Format(where, CompanyID, TableType));
            HttpContext.Current.Cache[Tools.GetCashName(CompanyID, TableType)] = ds;
        }

        public static DataSet GetFieldByID(string id)
        {
            return FieldDAL.GetFieldList("ID=" + id);
        }

        public static DataSet GetFieldList()
        {
            return FieldDAL.GetFieldList();
        }

        public static string InsertByDataSet(DataSet ds, string CompanyID, string tableType)
        {
            string message = FieldDAL.InsertByDataSet(ds, CompanyID, tableType);
            if (message == "")
            {
                string where = "CompanyID='{0}' and TableType ='{1}'";
                where = string.Format(where, CompanyID, tableType);
                HttpContext.Current.Cache.Insert(Tools.GetCashName(CompanyID, tableType), FieldDAL.GetFieldList(where));
            }
            return message;
        }

        public static void InsertField(string FieldName, string FName, string CompanyID, string TableType, string FieldType, string FieldLength, string Misk, string IsDispaly)
        {
            if (FieldLength.Trim() == "")
            {
                FieldLength = "300";
            }
            FieldDAL.InsertField(FieldName, FName, CompanyID, TableType, FieldType, FieldLength, Misk, IsDispaly);
            string tableName = CompanyDAL.GetTableName(CompanyID, TableType);
            DataHelper.ExecuteQuerys("ALTER TABLE " + tableName + " ADD " + FieldName + " NVARCHAR(" + FieldLength + ") NULL");
            string where = "CompanyID='{0}' and TableType ='{1}'";
            DataSet ds = FieldDAL.GetFieldList(string.Format(where, CompanyID, TableType));
            HttpContext.Current.Cache[Tools.GetCashName(CompanyID, TableType)] = ds;
        }

        public static void UpdateField(string ID, string FieldName, string companyID, string tableType)
        {
            FieldDAL.UpdateField(FieldName, ID);
            DataSet fieldsDS = CompanyBLL.GetCacheFields(companyID, tableType);
            DataRow[] drs = fieldsDS.Tables[0].Select("ID=" + ID);
            if (drs.Length == 1)
            {
                drs[0]["FieldName"] = FieldName;
            }
            fieldsDS.AcceptChanges();
        }

        public static void UpdateField(string ID, string FName, string Misk, string IsDispaly, string companyID, string tableType)
        {
            FieldDAL.UpdateField(FName, Misk, IsDispaly, ID);
            DataSet fieldsDS = CompanyBLL.GetCacheFields(companyID, tableType);
            DataRow[] drs = fieldsDS.Tables[0].Select("ID=" + ID);
            if (drs.Length == 1)
            {
                drs[0]["FName"] = FName;
                drs[0]["Misk"] = Misk;
                drs[0]["IsDisplay"] = IsDispaly;
            }
            fieldsDS.AcceptChanges();
        }
    }
}

