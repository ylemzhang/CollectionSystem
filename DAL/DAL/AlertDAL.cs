namespace DAL
{
    using System;
    using System.Data;
    using System.Data.SqlClient;

    public class AlertDAL
    {
        public static void DeleteAlert(string IDs)
        {
            DataHelper.DeleteByIDs("Alerttable", IDs);
        }

        public static DataSet GetAlertList()
        {
            return DataHelper.GetAll("Alerttable");
        }

        public static DataSet GetAlertList(string where)
        {
            return DataHelper.GetList("Alerttable", where);
        }

        public static DataSet GetAlertList(string field, string where)
        {
            return DataHelper.GetList("Alerttable", field, where);
        }

        public static void InsertAlert(int AlertType, int CaseID, decimal Num1, string Str1, string Person, DateTime Date1, int CompanyID, string CaseOwnerID)
        {
            SqlConnection conn = new SqlConnection(DataHelper.conn);
            string updateSql = "insert   Alerttable (AlertType,CaseID,Num1,Str1,Person,Date1,CompanyID,CaseOwnerID)  values (@AlertType,@CaseID,@Num1,@Str1,@Person,@Date1,@CompanyID,@CaseOwnerID)";
            SqlCommand cmd = new SqlCommand(updateSql, conn);
            SqlParameter sp1 = new SqlParameter("@AlertType", SqlDbType.Int) {
                Value = AlertType
            };
            SqlParameter sp2 = new SqlParameter("@CompanyID", SqlDbType.Int) {
                Value = CompanyID
            };
            SqlParameter sp3 = new SqlParameter("@CaseID", SqlDbType.Int) {
                Value = CaseID
            };
            SqlParameter sp4 = new SqlParameter("@Num1", SqlDbType.Decimal) {
                Value = Num1
            };
            SqlParameter sp5 = new SqlParameter("@Str1", SqlDbType.NVarChar) {
                Value = Str1
            };
            SqlParameter sp6 = new SqlParameter("@Person", SqlDbType.NVarChar) {
                Value = Person
            };
            SqlParameter sp7 = new SqlParameter("@Date1", SqlDbType.DateTime);
            if (Date1 == DateTime.MinValue)
            {
                sp7.Value = DBNull.Value;
            }
            else
            {
                sp7.Value = Date1;
            }
            SqlParameter sp8 = new SqlParameter("@CaseOwnerID", SqlDbType.Int);
            if (CaseOwnerID == "")
            {
                sp8.Value = DBNull.Value;
            }
            else
            {
                sp8.Value = int.Parse(CaseOwnerID);
            }
            cmd.Parameters.Add(sp1);
            cmd.Parameters.Add(sp2);
            cmd.Parameters.Add(sp3);
            cmd.Parameters.Add(sp4);
            cmd.Parameters.Add(sp5);
            cmd.Parameters.Add(sp6);
            cmd.Parameters.Add(sp7);
            cmd.Parameters.Add(sp8);
            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            finally
            {
                conn.Close();
                conn = null;
            }
        }

        public static void UpdateAlert(decimal Num1, string Str1, string Person, DateTime Date1, string AlertID)
        {
            SqlConnection conn = new SqlConnection(DataHelper.conn);
            SqlCommand cmd = new SqlCommand("update   Alerttable set Num1=@Num1,Str1=@Str1,Person=@Person,Date1=@Date1 where ID=" + AlertID, conn);
            SqlParameter sp4 = new SqlParameter("@Num1", SqlDbType.Decimal) {
                Value = Num1
            };
            SqlParameter sp5 = new SqlParameter("@Str1", SqlDbType.NVarChar) {
                Value = Str1
            };
            SqlParameter sp6 = new SqlParameter("@Person", SqlDbType.NVarChar) {
                Value = Person
            };
            SqlParameter sp7 = new SqlParameter("@Date1", SqlDbType.DateTime);
            if (Date1 == DateTime.MinValue)
            {
                sp7.Value = DBNull.Value;
            }
            else
            {
                sp7.Value = Date1;
            }
            cmd.Parameters.Add(sp4);
            cmd.Parameters.Add(sp5);
            cmd.Parameters.Add(sp6);
            cmd.Parameters.Add(sp7);
            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            finally
            {
                conn.Close();
                conn = null;
            }
        }
    }
}

