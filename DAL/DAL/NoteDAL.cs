namespace DAL
{
    using System;
    using System.Data;
    using System.Data.SqlClient;

    public class NoteDAL
    {
        public static void DeleteNote(string IDs)
        {
            DataHelper.DeleteByIDs("NoteTable", IDs);
        }

        public static string GetLatestNoteTime(string companyID, string CaseID)
        {
            string sql = "select max(createon)  from notetable where companyid={0} and caseID={1}";
            return DataHelper.ExecuteScalar(string.Format(sql, companyID, CaseID)).ToString();
        }

        public static DataSet GetNoteList()
        {
            return DataHelper.GetAll("NoteTable");
        }

        public static DataSet GetNoteList(string where)
        {
            return DataHelper.GetListByDesc("NoteTable", where);
        }

        public static DataSet GetNoteList(string field, string where)
        {
            return DataHelper.GetList("NoteTable", field, where);
        }

        public static int GetNoteTotalItems(string where)
        {
            string sql = "select count(*) from NoteTable ";
            if (where != "")
            {
                sql = sql + " where " + where;
            }
            return (int) DataHelper.ExecuteScalar(sql);
        }

        public static int InsertNote(string Body, string CreateBy, int NoteType, int CaseID, decimal Num1, string Str1, string Str2, DateTime Date1, DateTime CreateOn, int CompanyID, string contactor, string contactorType, string contractResult)
        {
            SqlConnection conn = new SqlConnection(DataHelper.conn);
            string updateSql = "insert   NoteTable (Body,CreateBy,NoteType,CaseID,Num1,Str1,Str2,Date1,CreateOn,CompanyID,contactor,contactorType,contractResult)  values (@Body,@CreateBy,@NoteType,@CaseID,@Num1,@Str1,@Str2,@Date1,@CreateOn,@CompanyID,@contactor,@contactorType,@contractResult)";
            SqlCommand cmd = new SqlCommand(updateSql, conn);
            SqlParameter sp = new SqlParameter("@Body", SqlDbType.NVarChar) {
                Value = Body
            };
            SqlParameter sp1 = new SqlParameter("@Num1", SqlDbType.Decimal) {
                Value = Num1
            };
            SqlParameter sp2 = new SqlParameter("@CompanyID", SqlDbType.Int) {
                Value = CompanyID
            };
            SqlParameter sp3 = new SqlParameter("@CaseID", SqlDbType.Int) {
                Value = CaseID
            };
            SqlParameter sp4 = new SqlParameter("@CreateBy", SqlDbType.NVarChar) {
                Value = CreateBy
            };
            SqlParameter sp5 = new SqlParameter("@Str1", SqlDbType.NVarChar) {
                Value = Str1
            };
            SqlParameter sp6 = new SqlParameter("@Str2", SqlDbType.NVarChar) {
                Value = Str2
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
            SqlParameter sp8 = new SqlParameter("@CreateOn", SqlDbType.DateTime) {
                Value = CreateOn
            };
            SqlParameter sp9 = new SqlParameter("@NoteType", SqlDbType.Int) {
                Value = NoteType
            };
            SqlParameter sp10 = new SqlParameter("@contactor", SqlDbType.NVarChar) {
                Value = contactor
            };
            SqlParameter sp11 = new SqlParameter("@contactorType", SqlDbType.NVarChar) {
                Value = contactorType
            };
            SqlParameter sp12 = new SqlParameter("@contractResult", SqlDbType.NVarChar) {
                Value = contractResult
            };
            cmd.Parameters.Add(sp);
            cmd.Parameters.Add(sp1);
            cmd.Parameters.Add(sp2);
            cmd.Parameters.Add(sp3);
            cmd.Parameters.Add(sp4);
            cmd.Parameters.Add(sp5);
            cmd.Parameters.Add(sp6);
            cmd.Parameters.Add(sp7);
            cmd.Parameters.Add(sp8);
            cmd.Parameters.Add(sp9);
            cmd.Parameters.Add(sp10);
            cmd.Parameters.Add(sp11);
            cmd.Parameters.Add(sp12);
            int id = 0;
            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                string sql = "select max(id) from noteTable";
                cmd = new SqlCommand(sql, conn);
                id = (int) cmd.ExecuteScalar();
            }
            finally
            {
                conn.Close();
                conn = null;
            }
            return id;
        }

        public static void UpdateNote(string ID, string body)
        {
            string sql = "Update NoteTable set body='{0}' where ID={1}";
            DataHelper.ExecuteQuerys(string.Format(sql, body, ID));
        }

        public static void UpdateNote(string ID, string body, string tel, string phoneType, string contactor, string contactorType, string contractResult)
        {
            string sql = "Update NoteTable set body='{0}',Str1='{1}', Str2='{2}',contactor='{4}',contactorType='{5}', contractResult='{6}' where ID={3}";
            DataHelper.ExecuteQuerys(string.Format(sql, new object[] { body, tel, phoneType, ID, contactor, contactorType, contractResult }));
        }

        public static void UpdateVisitNote(string ID, string body, decimal num, string contactor, string contactorType, string contractResult)
        {
            string sql = "Update NoteTable set body='{0}',Num1='{1}',contactor='{3}',contactorType='{4}', contractResult='{5}' where ID={2}";
            DataHelper.ExecuteQuerys(string.Format(sql, new object[] { body, num, ID, contactor, contactorType, contractResult }));
        }
    }
}

