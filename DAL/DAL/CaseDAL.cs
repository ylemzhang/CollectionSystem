namespace DAL
{
    using Common;
    using System;
    using System.Data;
    using System.Data.SqlClient;

    public class CaseDAL
    {
        private string TableName;

        public CaseDAL(int CompanyID)
        {
            this.TableName = Tools.GetCaseTableName(CompanyID.ToString());
        }

        public void DeleteCase(string IDs)
        {
            DataHelper.DeleteByIDs(this.TableName, IDs);
        }

        public DataSet GetCaseList()
        {
            return DataHelper.GetAll(this.TableName);
        }

        public DataSet GetCaseList(string where)
        {
            return DataHelper.GetList(this.TableName, where);
        }

        public DataSet GetCaseList(string field, string where)
        {
            return DataHelper.GetList(this.TableName, field, where);
        }

        public DataSet GetCaseList(string field, string where, string orderStr)
        {
            return DataHelper.GetList(this.TableName, field, where, orderStr);
        }

        public void MarkRepeatedCase(int Repeated, string IDs)
        {
            string sql = "Update {0} set Repeated={1} where ID in ({2})";
            DataHelper.ExecuteQuerys(string.Format(sql, this.TableName, Repeated, IDs));
        }

        public void UpdateCaseOwer(int ownerID, string IDs)
        {
            string sql = "Update {0} set OwnerID={1} where ID in ({2})";
            DataHelper.ExecuteQuerys(string.Format(sql, this.TableName, ownerID, IDs));
        }

        public void UpdateCasePhoned(int Phoned, int ID)
        {
            SqlConnection conn = new SqlConnection(DataHelper.conn);
            string updateSql = "UPDATE   {0} SET  Phoned =@Phoned WHERE ID=@ID";
            SqlCommand cmd = new SqlCommand(string.Format(updateSql, this.TableName), conn);
            SqlParameter sp = new SqlParameter("@ID", SqlDbType.Int) {
                Value = ID
            };
            SqlParameter sp1 = new SqlParameter("@Phoned", SqlDbType.Int) {
                Value = Phoned
            };
            cmd.Parameters.Add(sp);
            cmd.Parameters.Add(sp1);
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

        public void UpdateCasePromiseDate(DateTime PromisedDate, int ID)
        {
            SqlConnection conn = new SqlConnection(DataHelper.conn);
            string updateSql = "UPDATE   {0} SET  PromisedDate =@PromisedDate WHERE ID=@ID";
            SqlCommand cmd = new SqlCommand(string.Format(updateSql, this.TableName), conn);
            SqlParameter sp = new SqlParameter("@ID", SqlDbType.Int) {
                Value = ID
            };
            SqlParameter sp1 = new SqlParameter("@PromisedDate", SqlDbType.DateTime) {
                Value = PromisedDate
            };
            cmd.Parameters.Add(sp);
            cmd.Parameters.Add(sp1);
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

        public void UpdateCaseVisited(int Visited, int ID)
        {
            SqlConnection conn = new SqlConnection(DataHelper.conn);
            string updateSql = "UPDATE   {0} SET  Visited =@Visited WHERE ID=@ID";
            SqlCommand cmd = new SqlCommand(string.Format(updateSql, this.TableName), conn);
            SqlParameter sp = new SqlParameter("@ID", SqlDbType.Int) {
                Value = ID
            };
            SqlParameter sp1 = new SqlParameter("@Visited", SqlDbType.Int) {
                Value = Visited
            };
            cmd.Parameters.Add(sp);
            cmd.Parameters.Add(sp1);
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

