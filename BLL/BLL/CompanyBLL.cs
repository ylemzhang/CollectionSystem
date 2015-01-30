namespace BLL
{
    using Common;
    using DAL;
    using System;
    using System.Data;
    using System.Web;

    public class CompanyBLL
    {
        public static void DeleteCompany(string IDs)
        {
            CompanyDAL.DeleteCompany(IDs);
        }

        public static DataSet GetALLCompanysByUserID(string userID)
        {
            return DataHelper.GetList("select  companyID from companyUser where userID =" + userID);
        }

        public static DataSet GetALLCompanysIDandNameByUserID(string userID)
        {
            string sql = "select id, companyName from companytable where id in (select distinct companyid from companyuser where userid={0})";
            return DataHelper.GetList(string.Format(sql, userID));
        }

        public static DataSet GetCacheFields(string CompanyID, string tableType)
        {
            if (HttpContext.Current.Cache[Tools.GetCashName(CompanyID, tableType)] == null)
            {
                string where = "CompanyID='{0}' and TableType ='{1}'";
                DataSet ds = FieldDAL.GetFieldList(string.Format(where, CompanyID, tableType));
                if (ds.Tables[0].Rows.Count == 0)
                {
                    return null;
                }
                HttpContext.Current.Cache[Tools.GetCashName(CompanyID, tableType)] = ds;
                return ds;
            }
            return (HttpContext.Current.Cache[Tools.GetCashName(CompanyID, tableType)] as DataSet);
        }

        public static DataSet GetCompanyByID(string id)
        {
            return CompanyDAL.GetCompanyList("ID=" + id);
        }

        public static DataSet GetCompanyList()
        {
            return CompanyDAL.GetCompanyList();
        }

        public static DataSet GetCompanyUsers(string CompanyID)
        {
            return DataHelper.GetList("Select distinct UserID from CompanyUser where CompanyID=" + CompanyID);
        }

        public static DataSet GetGroupUsersByID(string ID)
        {
            return DataHelper.GetList("Select UserID from CompanyUser where GroupID=" + ID);
        }

        public static string GetIfHasBalanceTable(string id)
        {
            return CompanyDAL.GetIfHasBalanceTable("where ID=" + id);
        }

        public static string ImportBalanceRecords(string filename, string CompanyID, DateTime balanceDate)
        {
            try
            {
                DataSet importData = Tools.GetDataFromFile(filename, false);
                DataSet fieldsTable = GetCacheFields(CompanyID, Tools.BalanceTableType);
                if (importData.Tables[0].Rows.Count < 1)
                {
                    return "文件没有内容";
                }
                if (importData.Tables[0].Columns.Count != fieldsTable.Tables[0].Rows.Count)
                {
                    return "excel文件栏目不正确";
                }
                return CompanyDAL.InsertBalanceRecordByDataSet(importData, CompanyID, balanceDate);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public static string ImportCompanyCaseRecords(string filename, string CompanyID, string PatchName, string ExpireDate)
        {
            try
            {
                DataSet importData = Tools.GetDataFromFile(filename, false);
                DataSet fieldsTable = GetCacheFields(CompanyID, Tools.CaseTableType);
                if (importData.Tables[0].Rows.Count < 1)
                {
                    return "文件没有内容";
                }
                if (importData.Tables[0].Columns.Count != fieldsTable.Tables[0].Rows.Count)
                {
                    return "excel文件栏目不正确";
                }
                int patchid = PatchBLL.InsertPatch(PatchName, DateTime.Parse(ExpireDate), DateTime.Now, int.Parse(CompanyID));
                return CompanyDAL.InsertCaseRecordByDataSet(importData, CompanyID, patchid);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public static string ImportFieldsTable(string filename, string CompanyID, string ImportTableType)
        {
            try
            {
                string message = SaveImportTable(Tools.GetDataFromFile(filename, false), CompanyID, ImportTableType);
                if (message == "")
                {
                    CompanyDAL.CreateTableColumns(CompanyID, ImportTableType, HttpContext.Current.Cache[Tools.GetCashName(CompanyID, ImportTableType)] as DataSet);
                }
                return message;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public static string ImportPaymentRecords(string filename, string CompanyID)
        {
            try
            {
                DataSet importData = Tools.GetDataFromFile(filename, false);
                DataSet fieldsTable = GetCacheFields(CompanyID, Tools.PaymentTableType);
                if (importData.Tables[0].Rows.Count < 1)
                {
                    return "文件没有内容";
                }
                if (importData.Tables[0].Columns.Count != fieldsTable.Tables[0].Rows.Count)
                {
                    return "excel文件栏目不正确";
                }
                return CompanyDAL.InsertPaymentRecordByDataSet(importData, CompanyID);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public static int InsertCompany(string CompanyName, string Discription, string HasBalanceTable)
        {
            return CompanyDAL.InsertCompany(CompanyName, Discription, HasBalanceTable);
        }

        public static void InsertGroupUsers(string[] UserIDs, string groupID, string companyID)
        {
            CompanyDAL.InsertGroupUsers(UserIDs, groupID, companyID);
        }

        private static string SaveImportTable(DataSet ds, string CompanyID, string ImportTableType)
        {
            if (ds.Tables[0].Rows.Count < 1)
            {
                return "文件没有内容";
            }
            return FieldBLL.InsertByDataSet(ds, CompanyID, ImportTableType);
        }

        public static void UpdateCompany(string ID, string CompanyName, string Discription, string HasBalanceTable)
        {
            CompanyDAL.UpdateCompany(CompanyName, Discription, HasBalanceTable, ID);
        }

        public static void UpdateTbKey(string CompanyID, string tableType, string newKey)
        {
            DataSet ds = HttpContext.Current.Cache[Tools.GetCashName(CompanyID, tableType)] as DataSet;
            string tableName = CompanyDAL.GetTableName(CompanyID, tableType);
            DataTable dt = ds.Tables[0];
            string newKeyID = dt.Select("FieldName='" + newKey + "'")[0]["ID"].ToString();
            DataRow[] drsTbkey = dt.Select("FieldName='tbKey'");
            if (drsTbkey.Length == 0)
            {
                DataHelper.ExecuteQuerys(string.Format(" EXEC sp_rename '{0}.{1}', 'tbKey', 'column' ", tableName, newKey));
                FieldBLL.UpdateField(newKeyID, "tbKey", CompanyID, tableType);
            }
            else
            {
                string oldKeyID = drsTbkey[0]["ID"].ToString();
                string oldFieldName = "tb_" + DateTime.Now.Ticks.ToString();
                DataHelper.ExecuteQuerysArray(new string[] { string.Format("EXEC sp_rename '{0}.tbKey', '{1}', 'column' ", tableName, oldFieldName), string.Format("EXEC sp_rename '{0}.{1}', 'tbKey', 'column' ", tableName, newKey) });
                FieldBLL.UpdateField(oldKeyID, oldFieldName, CompanyID, tableType);
                FieldBLL.UpdateField(newKeyID, "tbKey", CompanyID, tableType);
            }
        }
    }
}

