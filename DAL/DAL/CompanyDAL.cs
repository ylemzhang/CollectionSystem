namespace DAL
{
    using Common;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Web;

    public class CompanyDAL
    {
        public static void CreateTableColumns(string CompanyID, string ImportTableType, DataSet ds)
        {
            DataHelper.ExecuteQuerys(GetAddTableColumnsSql(CompanyID, ImportTableType, ds));
        }

        public static void DeleteCompany(string IDs)
        {
            DataHelper.DeleteByIDs("Companytable", IDs);
            DeleteImportTable(IDs, Tools.CaseTableType);
            DeleteImportTable(IDs, Tools.PaymentTableType);
            DeleteImportTable(IDs, Tools.BalanceTableType);
            DeleteFields(IDs);
            DeletePatch(IDs);
            DeleteCompanyUsers(IDs);
            DeleteReadUsers(IDs);
            DeleteOpenedCase(IDs);
        }

        private static void DeleteCompanyUsers(string IDs)
        {
            string sql = "Delete CompanyUser where CompanyID in ({0})";
            DataHelper.ExecuteQuerys(string.Format(sql, IDs));
        }

        private static void DeleteFields(string IDs)
        {
            string sql = "Delete Field where CompanyID in ({0})";
            DataHelper.ExecuteQuerys(string.Format(sql, IDs));
        }

        private static void DeleteImportTable(string IDs, string ImportType)
        {
            string[] idstr = IDs.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            SqlConnection conn = new SqlConnection(DataHelper.conn);
            string dropSql = "if exists (select * from dbo.sysobjects where id = object_id(N'[{0}]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)\r\ndrop table [{0}]";
            string prefix = "";
            switch (ImportType)
            {
                case "1":
                    prefix = Tools.CompanyCasePrefix;
                    break;

                case "2":
                    prefix = Tools.CompanyPaymentPrefix;
                    break;

                case "3":
                    prefix = Tools.CompanyBalancePrefix;
                    break;

                default:
                    prefix = Tools.CompanyCasePrefix;
                    break;
            }
            try
            {
                conn.Open();
                foreach (string id in idstr)
                {
                    string tableName = Tools.GetImportTableName(prefix, id);
                    new SqlCommand(string.Format(dropSql, tableName), conn).ExecuteNonQuery();
                    if (HttpContext.Current.Cache[Tools.GetCashName(id, ImportType)] != null)
                    {
                        HttpContext.Current.Cache.Remove(Tools.GetCashName(id, ImportType));
                    }
                }
            }
            finally
            {
                conn.Close();
                conn = null;
            }
        }

        private static void DeleteOpenedCase(string IDs)
        {
            string sql = "Delete OpenedCase where CompanyID in ({0})";
            DataHelper.ExecuteQuerys(string.Format(sql, IDs));
        }

        private static void DeletePatch(string IDs)
        {
            string sql = "Delete Patch where CompanyID in ({0})";
            DataHelper.ExecuteQuerys(string.Format(sql, IDs));
        }

        private static void DeleteReadUsers(string IDs)
        {
            string sql = "Delete ReadCaseUsers where CompanyID in ({0})";
            DataHelper.ExecuteQuerys(string.Format(sql, IDs));
        }

        private static string GetAddTableColumnsSql(string CompanyID, string ImportTableType, DataSet ds)
        {
            string tableName = GetTableName(CompanyID, ImportTableType);
            string sql = "ALTER TABLE {0} ADD ";
            sql = string.Format(sql, tableName);
            string nvarchar = " NVARCHAR(300) NULL,";
            string money = " Decimal(18,2) NULL,";
            string datetime = " datetime NULL,";
            string inttype = " int NULL,";
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                string field = dr[2].ToString().Trim();
                string type = dr[3].ToString().Trim().ToLower();
                string len = dr[4].ToString().Trim();
                sql = sql + field;
                if (type == "money")
                {
                    sql = sql + money;
                }
                else if (type == "int")
                {
                    sql = sql + inttype;
                }
                else if (type == "datetime")
                {
                    sql = sql + datetime;
                }
                else if (len != "")
                {
                    string temp = nvarchar.Replace("50", len);
                    sql = sql + temp;
                }
                else
                {
                    sql = sql + nvarchar;
                }
            }
            return sql.Substring(0, sql.Length - 1);
        }

        public static DataSet GetCompanyList()
        {
            return DataHelper.GetAll("Companytable");
        }

        public static DataSet GetCompanyList(string where)
        {
            return DataHelper.GetList("Companytable", where);
        }

        public static DataSet GetCompanyList(string field, string where)
        {
            return DataHelper.GetList("Companytable", field, where);
        }

        private static string GetCreateBalanceTableSql(int id)
        {
            string tableName = Tools.GetBalanceTableName(id.ToString());
            string sql = "CREATE TABLE [{0}] (\r\n\t[ID] [int] IDENTITY (1, 1) NOT NULL ,\r\n\r\n[BalanceDate] [datetime]  ,\r\n\tCONSTRAINT [PK_{0}] PRIMARY KEY  CLUSTERED \r\n\t(\r\n\t\t[ID]\r\n\t)  ON [PRIMARY] \r\n) ON [PRIMARY]";
            return string.Format(sql, tableName);
        }

        private static string GetCreateCaseTableSql(int id)
        {
            string tableName = Tools.GetCaseTableName(id.ToString());
            string sql = "CREATE TABLE [{0}] (\r\n\t[ID] [int] IDENTITY (1, 1) NOT NULL ,\r\n[PatchID] [int]  ,\r\n[OwnerID] [int]  ,\r\n\r\n[PromisedDate] datetime NULL,\r\n[Phoned] [int] NULL CONSTRAINT [DF_CompanyCase_{0}_Phoned] DEFAULT (0),\r\n\r\n[Visited] [int] NULL CONSTRAINT [DF_CompanyCase_{0}_Visited] DEFAULT (0),\r\n[Repeated] [int] NULL CONSTRAINT [DF_CompanyCase_{0}_Repeated] DEFAULT (0),\r\n\tCONSTRAINT [PK_{0}] PRIMARY KEY  CLUSTERED \r\n\t(\r\n\t\t[ID]\r\n\t)  ON [PRIMARY] \r\n) ON [PRIMARY]";
            return string.Format(sql, tableName);
        }

        private static string GetCreatePaymentTableSql(int id)
        {
            string tableName = Tools.GetPaymentTableName(id.ToString());
            string sql = "CREATE TABLE [{0}] (\r\n\t[ID] [int] IDENTITY (1, 1) NOT NULL ,\r\n\r\n[ImportDate] [datetime]  ,\r\n\tCONSTRAINT [PK_{0}] PRIMARY KEY  CLUSTERED \r\n\t(\r\n\t\t[ID]\r\n\t)  ON [PRIMARY] \r\n) ON [PRIMARY]";
            return string.Format(sql, tableName);
        }

        public static string GetIfHasBalanceTable(string where)
        {
            return DataHelper.ExecuteScalar("select HasBalanceTable from CompanyTable " + where).ToString();
        }

        public static string GetTableName(string CompanyID, string ImportTableType)
        {
            string prefix = "";
            switch (ImportTableType)
            {
                case "1":
                    prefix = Tools.CompanyCasePrefix;
                    break;

                case "2":
                    prefix = Tools.CompanyPaymentPrefix;
                    break;

                case "3":
                    prefix = Tools.CompanyBalancePrefix;
                    break;

                default:
                    prefix = Tools.CompanyCasePrefix;
                    break;
            }
            return Tools.GetImportTableName(prefix, CompanyID);
        }

        public static string InsertBalanceRecordByDataSet(DataSet importds, string CompanyID, DateTime balancedate)
        {
            return InsertRecordByDataSet(importds, CompanyID, 0, Tools.CompanyBalancePrefix, 2, balancedate);
        }

        public static string InsertCaseRecordByDataSet(DataSet importds, string CompanyID, int patchID)
        {
            return InsertRecordByDataSet(importds, CompanyID, patchID, Tools.CompanyCasePrefix, 7, DateTime.MinValue);
        }

        public static int InsertCompany(string CompanyName, string Description, string HasBalanceTable)
        {
            SqlConnection conn = new SqlConnection(DataHelper.conn);
            string updateSql = "insert   Companytable (CompanyName,Description,HasBalanceTable)  values (@CompanyName,@Description,@HasBalanceTable)";
            SqlCommand cmd = new SqlCommand(updateSql, conn);
            SqlParameter sp = new SqlParameter("@CompanyName", SqlDbType.NVarChar) {
                Value = CompanyName
            };
            SqlParameter sp1 = new SqlParameter("@Description", SqlDbType.NVarChar) {
                Value = Description
            };
            SqlParameter sp2 = new SqlParameter("@HasBalanceTable", SqlDbType.NVarChar) {
                Value = HasBalanceTable
            };
            cmd.Parameters.Add(sp);
            cmd.Parameters.Add(sp1);
            cmd.Parameters.Add(sp2);
            int id = 0;
            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                string sql = "select max(id) from Companytable";
                cmd = new SqlCommand(sql, conn);
                id = (int) cmd.ExecuteScalar();
                if (id == 0)
                {
                    return id;
                }
                new SqlCommand(GetCreateCaseTableSql(id), conn).ExecuteNonQuery();
                new SqlCommand(GetCreatePaymentTableSql(id), conn).ExecuteNonQuery();
                if (HasBalanceTable == "1")
                {
                    new SqlCommand(GetCreateBalanceTableSql(id), conn).ExecuteNonQuery();
                }
            }
            finally
            {
                conn.Close();
                conn = null;
            }
            return id;
        }

        public static void InsertGroupUsers(string[] UserIDs, string GroupID, string companyID)
        {
            SqlConnection conn = new SqlConnection(DataHelper.conn);
            string updateSql = "delete  CompanyUser where companyid= {0} and groupID= {1}";
            SqlCommand cmd = new SqlCommand(string.Format(updateSql, companyID, GroupID), conn);
            string insertSql = "insert CompanyUser (UserID,CompanyID,GroupID) values ({0},{1},{2})";
            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                foreach (string userID in UserIDs)
                {
                    if (userID.Trim() != string.Empty)
                    {
                        new SqlCommand(string.Format(insertSql, userID, companyID, GroupID), conn).ExecuteNonQuery();
                    }
                }
            }
            finally
            {
                conn.Close();
                conn = null;
            }
        }

        public static string InsertPaymentRecordByDataSet(DataSet importds, string CompanyID)
        {
            return InsertRecordByDataSet(importds, CompanyID, 0, Tools.CompanyPaymentPrefix, 2, DateTime.Now);
        }

        private static string InsertRecordByDataSet(DataSet importds, string CompanyID, int patchID, string prefix, int AlredayColumns, DateTime date)
        {
            string tableName = Tools.GetImportTableName(prefix, CompanyID);
            string select = "select * from {0} where id=0";
            select = string.Format(select, tableName);
            string conn = DataHelper.conn;
            SqlDataAdapter adapter = new SqlDataAdapter(select, conn);
            SqlCommandBuilder Sqlcb = new SqlCommandBuilder(adapter);
            DataSet dsStrctue = new DataSet();
            adapter.Fill(dsStrctue);
            DataTable dbnew = dsStrctue.Tables[0];
            int columns = importds.Tables[0].Columns.Count;
            for (int i = 0; i < importds.Tables[0].Rows.Count; i++)
            {
                DataRow dr = importds.Tables[0].Rows[i];
                DataRow newRow = dbnew.NewRow();
                for (int j = 0; j < columns; j++)
                {
                    newRow[j + AlredayColumns] = dr[j];
                }
                if (patchID != 0)
                {
                    newRow[1] = patchID;
                }
                else
                {
                    newRow[1] = date;
                }
                if ((newRow["tbKey"].ToString().Trim() != "") || (newRow["tbName"].ToString().Trim() != ""))
                {
                    newRow["tbKey"] = newRow["tbKey"].ToString().Trim();
                    dbnew.Rows.Add(newRow);
                }
            }
            adapter.Update(dsStrctue);
            adapter = null;
            return "";
        }

        public static void UpdateCompany(string CompanyName, string Description, string HasBalanceTable, string ID)
        {
            SqlConnection conn = new SqlConnection(DataHelper.conn);
            string updateSql = "UPDATE  Companytable SET  CompanyName=@CompanyName,HasBalanceTable=@HasBalanceTable,Description=@Description WHERE ID = @ID ";
            SqlCommand cmd = new SqlCommand(updateSql, conn);
            SqlParameter sp = new SqlParameter("@CompanyName", SqlDbType.NVarChar) {
                Value = CompanyName
            };
            SqlParameter sp1 = new SqlParameter("@Description", SqlDbType.NVarChar) {
                Value = Description
            };
            SqlParameter sp2 = new SqlParameter("@HasBalanceTable", SqlDbType.NVarChar) {
                Value = HasBalanceTable
            };
            SqlParameter sp3 = new SqlParameter("@ID", SqlDbType.Int) {
                Value = int.Parse(ID)
            };
            cmd.Parameters.Add(sp);
            cmd.Parameters.Add(sp1);
            cmd.Parameters.Add(sp2);
            cmd.Parameters.Add(sp3);
            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                if (HasBalanceTable == "1")
                {
                    new SqlCommand(GetCreateBalanceTableSql(int.Parse(ID)), conn).ExecuteNonQuery();
                }
            }
            finally
            {
                conn.Close();
                conn = null;
            }
        }
    }
}

