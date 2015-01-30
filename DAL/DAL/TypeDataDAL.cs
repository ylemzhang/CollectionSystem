namespace DAL
{
    using System;
    using System.Data;
    using System.Data.SqlClient;

    public class TypeDataDAL
    {
        private string insertFields = "Description,FTypeValue,TypeID,inx";
        private string insertPrams = "@Description,@FTypeValue,@TypeID,@Index";
        private string TableName = "TypeData";
        private string updateFields = "Description=@Description, FTypeValue=@FTypeValue,TypeID=@TypeID,inx=@Index";

        public void DeleteTypeDataByIDs(string IDs)
        {
            DataHelper.DeleteByIDs(this.TableName, IDs);
        }

        public DataSet GetTypeDataListByTypeID(string typeid)
        {
            return DataHelper.GetList(this.TableName, "TypeID=" + typeid);
        }

        public void InsertTypeData(string Description, string FTypeValue, string TypeID)
        {
            string sql = "insert   [{0}] ({1}) values ({2})";
            sql = string.Format(sql, this.TableName, this.insertFields, this.insertPrams);
            SqlConnection conn = new SqlConnection(DataHelper.conn);
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlParameter sp1 = new SqlParameter("@Description", SqlDbType.NVarChar) {
                Value = Description
            };
            SqlParameter sp2 = new SqlParameter("@FTypeValue", SqlDbType.NVarChar) {
                Value = FTypeValue
            };
            SqlParameter sp4 = new SqlParameter("@TypeID", SqlDbType.NVarChar) {
                Value = TypeID
            };
            SqlParameter sp5 = new SqlParameter("@Index", SqlDbType.NVarChar) {
                Value = 0
            };
            cmd.Parameters.Add(sp1);
            cmd.Parameters.Add(sp2);
            cmd.Parameters.Add(sp5);
            cmd.Parameters.Add(sp4);
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

        public void UpdateTypeData(string Description, string FTypeValue, string TypeID, string TypeDataID)
        {
            string updateSql = string.Format("UPDATE   [{1}] SET  {0}  WHERE ID =" + TypeDataID, this.updateFields, this.TableName);
            SqlConnection conn = new SqlConnection(DataHelper.conn);
            SqlCommand cmd = new SqlCommand(updateSql, conn);
            SqlParameter sp1 = new SqlParameter("@Description", SqlDbType.NVarChar) {
                Value = Description
            };
            SqlParameter sp2 = new SqlParameter("@FTypeValue", SqlDbType.NVarChar) {
                Value = FTypeValue
            };
            SqlParameter sp4 = new SqlParameter("@TypeID", SqlDbType.NVarChar) {
                Value = TypeID
            };
            SqlParameter sp5 = new SqlParameter("@Index", SqlDbType.NVarChar) {
                Value = 0
            };
            cmd.Parameters.Add(sp1);
            cmd.Parameters.Add(sp2);
            cmd.Parameters.Add(sp5);
            cmd.Parameters.Add(sp4);
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

