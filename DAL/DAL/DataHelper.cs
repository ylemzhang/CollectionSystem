namespace DAL
{
    using System;
    using System.Collections;
    using System.Data;
    using System.Data.SqlClient;
    using System.Configuration;

    public class DataHelper
    {
        /// <summary>
        /// 连接字符串
        /// </summary>
        public static string conn = ConfigurationManager.ConnectionStrings["connSQL"].ConnectionString;

        public static void AddStrColumnToTable(string tableName, string field, string length)
        {
            string sql = "ALTER TABLE {0} ADD {1} nvarchar({2}) NULL ";
            ExecuteQuerys(string.Format(sql, tableName, field, length));
        }

        public static void DeleteByIDs(string table, string IDs)
        {
            ExecuteQuerys(string.Format("Delete   [{1}]  WHERE ID in ({0})", IDs, table));
        }

        public static void DropColumnFromTable(string tableName, string field)
        {
            string sql = "ALTER TABLE {0} DROP COLUMN {1} ";
            ExecuteQuerys(string.Format(sql, tableName, field));
        }

        public static int ExecuteQuerys(string sql)
        {
            int cs2;
            SqlConnection conn = new SqlConnection(DataHelper.conn);
            SqlCommand cmd = new SqlCommand(sql, conn);
            try
            {
                conn.Open();
                cs2 = cmd.ExecuteNonQuery();
            }
            finally
            {
                conn.Close();
            }
            return cs2;
        }

        public static void ExecuteQuerysArray(ArrayList ar)
        {
            SqlConnection conn = new SqlConnection(DataHelper.conn);
            try
            {
                conn.Open();
                for (int i = 0; i < ar.Count; i++)
                {
                    new SqlCommand(ar[i].ToString(), conn).ExecuteNonQuery();
                }
            }
            finally
            {
                conn.Close();
            }
        }

        public static void ExecuteQuerysArray(string[] sqls)
        {
            SqlConnection conn = new SqlConnection(DataHelper.conn);
            try
            {
                conn.Open();
                foreach (string sql in sqls)
                {
                    new SqlCommand(sql, conn).ExecuteNonQuery();
                }
            }
            finally
            {
                conn.Close();
            }
        }

        public static void ExecuteQueryWithParas(string sql, ArrayList sc)
        {
            SqlConnection conn = new SqlConnection(DataHelper.conn);
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddRange(sc.ToArray());
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

        public static object ExecuteScalar(string sql)
        {
            object cs2;
            SqlConnection conn = new SqlConnection(DataHelper.conn);
            SqlCommand cmd = new SqlCommand(sql, conn);
            try
            {
                conn.Open();
                cs2 = cmd.ExecuteScalar();
            }
            finally
            {
                conn.Close();
            }
            return cs2;
        }

        public static DataSet GetAll(string table)
        {
            string sql = "SELECT  * FROM [{0}] ";
            sql = string.Format(sql, table);
            DataSet ds = new DataSet();
            new SqlDataAdapter(sql, conn).Fill(ds);
            return ds;
        }

        public static DataSet GetAllByDesc(string table)
        {
            string sql = "SELECT  * FROM [{0}] order by id desc ";
            sql = string.Format(sql, table);
            DataSet ds = new DataSet();
            new SqlDataAdapter(sql, conn).Fill(ds);
            return ds;
        }

        public static DataSet GetList(string sql)
        {
            DataSet ds = new DataSet();
            new SqlDataAdapter(sql, conn).Fill(ds);
            return ds;
        }

        public static DataSet GetList(string table, string condition)
        {
            string sql = "SELECT  * FROM [{0}] where {1} ";
            sql = string.Format(sql, table, condition);
            DataSet ds = new DataSet();
            new SqlDataAdapter(sql, conn).Fill(ds);
            return ds;
        }

        public static DataSet GetList(string table, string fileds, string condition)
        {
            string sql;
            if (condition == string.Empty)
            {
                sql = "SELECT  {1} FROM [{0}]";
                sql = string.Format(sql, table, fileds);
            }
            else
            {
                sql = "SELECT  {2} FROM [{0}] where {1}";
                sql = string.Format(sql, table, condition, fileds);
            }
            DataSet ds = new DataSet();
            new SqlDataAdapter(sql, conn).Fill(ds);
            return ds;
        }

        public static DataSet GetList(string table, string fileds, string condition, string orderStr)
        {
            string sql;
            if (condition == string.Empty)
            {
                sql = "SELECT  {1} FROM [{0}] {2}";
                sql = string.Format(sql, table, fileds, orderStr);
            }
            else
            {
                sql = "SELECT  {2} FROM [{0}] where {1}   {3}";
                sql = string.Format(sql, new object[] { table, condition, fileds, orderStr });
            }
            DataSet ds = new DataSet();
            new SqlDataAdapter(sql, conn).Fill(ds);
            return ds;
        }

        public static DataSet GetListByDesc(string table, string condition)
        {
            string sql = "SELECT  * FROM [{0}] where {1} order by id desc ";
            sql = string.Format(sql, table, condition);
            DataSet ds = new DataSet();
            new SqlDataAdapter(sql, conn).Fill(ds);
            return ds;
        }

        public static DataSet GetListByDesc(string table, string field, string condition)
        {
            string sql = "SELECT  {2} FROM [{0}] where {1} order by id desc ";
            sql = string.Format(sql, table, condition, field);
            DataSet ds = new DataSet();
            new SqlDataAdapter(sql, conn).Fill(ds);
            return ds;
        }

        public static DataSet GetPagingDataSet1(string tablename, string Fields, int PageCount, int currentPage, string where, string order)
        {
            string sql;
            int beginCount = PageCount * (currentPage - 1);
            if (currentPage == 1)
            {
                sql = "select top {0} {3} from {1}  where   {2} order by id {4}";
                sql = string.Format(sql, new object[] { PageCount, tablename, where, Fields, order });
                if (where == "searchSameRecord")
                {
                    sql = "SELECT {1} from {0}  where word in (select  word from {0} group by word having count(*)>1 )  order by word ";
                    sql = string.Format(sql, tablename, Fields);
                }
            }
            else
            {
                string sign;
                if (order == "desc")
                {
                    sign = "<";
                }
                else
                {
                    sign = ">";
                }
                sql = "select top {0} {4} from {1}  where id {6} all(select top {2} id from {1}  where {3} order by id {5}) and  {3} order by id {5}";
                sql = string.Format(sql, new object[] { PageCount, tablename, beginCount, where, Fields, order, sign });
            }
            return GetList(sql);
        }

        public static int GetTableTotalItems(string TableName, string where)
        {
            string sql = "select count(*) from " + TableName;
            if (where != "")
            {
                sql = sql + " where " + where;
            }
            return (int) ExecuteScalar(sql);
        }
    }
}

