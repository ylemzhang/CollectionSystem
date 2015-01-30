namespace DAL
{
    using System;
    using System.Collections;
    using System.Data;
    using System.Data.SqlClient;

    public class FieldDAL
    {
        private static string checkSameFieldName(DataSet ds)
        {
            ArrayList ar = new ArrayList();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                if (ar.Contains(dr[1].ToString().ToLower()))
                {
                    return dr[1].ToString();
                }
                ar.Add(dr[1].ToString().ToLower());
            }
            return "";
        }

        public static void DeleteField(string IDs)
        {
            DataHelper.DeleteByIDs("Field", IDs);
        }

        public static DataSet GetFieldList()
        {
            return DataHelper.GetAll("Field");
        }

        public static DataSet GetFieldList(string where)
        {
            return DataHelper.GetList("Field", where);
        }

        public static DataSet GetFieldList(string field, string where)
        {
            return DataHelper.GetList("Field", field, where);
        }

        public static string InsertByDataSet(DataSet ds, string CompanyID, string tableType)
        {
            int count = ds.Tables[0].Rows.Count;
            if (ds.Tables[0].Columns.Count != 7)
            {
                return "excel文件栏目不正确";
            }
            string samefield = checkSameFieldName(ds);
            if (samefield != "")
            {
                return ("数据库字段名" + samefield + "重复");
            }
            string select = "select * from Field where 1=0";
            string conn = DataHelper.conn;
            SqlDataAdapter adapter = new SqlDataAdapter(select, conn);
            SqlCommandBuilder Sqlcb = new SqlCommandBuilder(adapter);
            DataSet dsStrctue = new DataSet();
            adapter.Fill(dsStrctue);
            DataTable dbnew = dsStrctue.Tables[0];
            for (int i = 0; i < count; i++)
            {
                DataRow dr = ds.Tables[0].Rows[i];
                DataRow newRow = dbnew.NewRow();
                if ((dr[0].ToString().Trim() != "") && (dr[1].ToString().Trim() != ""))
                {
                    newRow[1] = dr.ItemArray.GetValue(0);
                    newRow[2] = dr.ItemArray.GetValue(1);
                    newRow[3] = dr.ItemArray.GetValue(2);
                    newRow[4] = dr.ItemArray.GetValue(3);
                    newRow[5] = dr.ItemArray.GetValue(4);
                    newRow[6] = dr.ItemArray.GetValue(5);
                    newRow[7] = dr.ItemArray.GetValue(6);
                    newRow["CompanyID"] = CompanyID;
                    newRow["TableType"] = tableType;
                    dbnew.Rows.Add(newRow);
                }
            }
            adapter.Update(dsStrctue);
            adapter = null;
            return "";
        }

        public static void InsertField(string FieldName, string FName, string CompanyID, string TableType, string FieldType, string FieldLength, string Misk, string IsDisplay)
        {
            SqlConnection conn = new SqlConnection(DataHelper.conn);
            string updateSql = "insert   Field (FieldName,FName,  CompanyID,  TableType,  FieldType,  FieldLength, Misk,IsDisplay)  values (@FieldName,@FName,  @CompanyID,  @TableType,  @FieldType,  @FieldLength,@Misk,@IsDisplay)";
            SqlCommand cmd = new SqlCommand(updateSql, conn);
            SqlParameter sp = new SqlParameter("@FieldName", SqlDbType.NVarChar) {
                Value = FieldName
            };
            SqlParameter sp1 = new SqlParameter("@FName", SqlDbType.NVarChar) {
                Value = FName
            };
            SqlParameter sp2 = new SqlParameter("@CompanyID", SqlDbType.NVarChar) {
                Value = CompanyID
            };
            SqlParameter sp3 = new SqlParameter("@TableType", SqlDbType.NVarChar) {
                Value = TableType
            };
            SqlParameter sp4 = new SqlParameter("@FieldType", SqlDbType.NVarChar) {
                Value = FieldType
            };
            SqlParameter sp5 = new SqlParameter("@FieldLength", SqlDbType.NVarChar) {
                Value = FieldLength
            };
            SqlParameter sp6 = new SqlParameter("@Misk", SqlDbType.NVarChar) {
                Value = Misk
            };
            SqlParameter sp7 = new SqlParameter("@IsDisplay", SqlDbType.NVarChar) {
                Value = IsDisplay
            };
            cmd.Parameters.Add(sp);
            cmd.Parameters.Add(sp1);
            cmd.Parameters.Add(sp2);
            cmd.Parameters.Add(sp3);
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
            }
        }

        public static void UpdateField(string FieldName, string ID)
        {
            SqlConnection conn = new SqlConnection(DataHelper.conn);
            string updateSql = "UPDATE  Field SET   FieldName=@FieldName WHERE ID = @ID ";
            SqlCommand cmd = new SqlCommand(updateSql, conn);
            SqlParameter sp = new SqlParameter("@ID", SqlDbType.Int) {
                Value = ID
            };
            SqlParameter sp2 = new SqlParameter("@FieldName", SqlDbType.NVarChar) {
                Value = FieldName
            };
            cmd.Parameters.Add(sp);
            cmd.Parameters.Add(sp2);
            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            finally
            {
                conn.Close();
            }
        }

        public static void UpdateField(string FName, string Misk, string IsDisplay, string ID)
        {
            SqlConnection conn = new SqlConnection(DataHelper.conn);
            string updateSql = "UPDATE  Field SET  FName=@FName,Misk=@Misk,IsDisplay=@IsDisplay WHERE ID = @ID ";
            SqlCommand cmd = new SqlCommand(updateSql, conn);
            SqlParameter sp = new SqlParameter("@ID", SqlDbType.Int) {
                Value = ID
            };
            SqlParameter sp1 = new SqlParameter("@FName", SqlDbType.NVarChar) {
                Value = FName
            };
            SqlParameter sp6 = new SqlParameter("@Misk", SqlDbType.NVarChar) {
                Value = Misk
            };
            SqlParameter sp7 = new SqlParameter("@IsDisplay", SqlDbType.NVarChar) {
                Value = IsDisplay
            };
            cmd.Parameters.Add(sp);
            cmd.Parameters.Add(sp1);
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
            }
        }
    }
}

