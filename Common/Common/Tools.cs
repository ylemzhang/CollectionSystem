namespace Common
{
    using System;
    using System.Data;
    using System.Data.OleDb;
    using System.Configuration;

    public class Tools
    {
        public static string UploadPath = ConfigurationManager.AppSettings["UploadPath"];
        public static string AttachMentPath = (UploadPath + @"Attachment\");
        public static string BalanceTableType = "3";
        public static string BcpPwd = ConfigurationManager.AppSettings["Bcppwd"];
        public static string CaseTableType = "1";
        public static string CompanyBalancePrefix = "CompanyBalance_";
        public static string CompanyCasePrefix = "CompanyCase_";        
        public static string CompanyInfoPath = (UploadPath + @"CompanyInfo\");
        public static string CompanyPaymentPrefix = "CompanyPayment_";
        public static string HtmlUrl = ConfigurationManager.AppSettings["HtmlUrl"];
        public static string langPath = (UploadPath + @"Lang\");
        public static string PaymentTableType = "2";        

        public static string GetBalanceFieldsCacheName(string CompanyID)
        {
            return GetCashName(CompanyID, BalanceTableType);
        }

        public static string GetBalanceTableName(string CompanyID)
        {
            return (CompanyBalancePrefix + CompanyID);
        }

        public static string GetCaseFieldsCacheName(string CompanyID)
        {
            return GetCashName(CompanyID, CaseTableType);
        }

        public static string GetCaseTableName(string CompanyID)
        {
            return (CompanyCasePrefix + CompanyID);
        }

        public static string GetCashName(string CompanyID, string ImportType)
        {
            string companyCasePrefix = "";
            switch (ImportType)
            {
                case "1":
                    companyCasePrefix = CompanyCasePrefix;
                    break;

                case "2":
                    companyCasePrefix = CompanyPaymentPrefix;
                    break;

                case "3":
                    companyCasePrefix = CompanyBalancePrefix;
                    break;

                default:
                    companyCasePrefix = CompanyCasePrefix;
                    break;
            }
            return (companyCasePrefix + CompanyID + "_" + ImportType);
        }

        public static string GetCheckFieldsCacheName(string CompanyID)
        {
            return GetCashName(CompanyID, PaymentTableType);
        }

        public static DataSet GetDataFromFile(string filename, bool noTitle)
        {
            string selectConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filename + ";Extended Properties='Excel 8.0;HDR=yes;IMEX=1'";
            string selectCommandText = "select * from [Sheet1$]";
            OleDbDataAdapter adapter = new OleDbDataAdapter(selectCommandText, selectConnectionString);
            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet);
            for (int i = dataSet.Tables[0].Columns.Count - 1; i > 0; i--)
            {
                if (dataSet.Tables[0].Columns[i].Caption.Trim() == "")
                {
                    dataSet.Tables[0].Columns.RemoveAt(i);
                }
            }
            return dataSet;
        }

        public static string GetImportTableName(string prefix, string CompanyID)
        {
            return (prefix + CompanyID);
        }

        public static string GetPaymentTableName(string CompanyID)
        {
            return (CompanyPaymentPrefix + CompanyID);
        }
    }
}

