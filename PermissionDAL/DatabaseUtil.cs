using System.Collections.Generic;
using System.Data.Common;
using System.Data.OleDb;
using System.Data.OracleClient;
using System.Data.SqlClient;
using System.Text;

namespace System.Data
{
    /// <summary>
    /// 数据库处理工具。
    /// </summary>
    internal class DatabaseUtil
    {
        /// <summary>
        /// 数据库工具实例集合。
        /// </summary>
        private static readonly Dictionary<Enum_DatabaseVersion, DatabaseUtil> _entitySet = new Dictionary<Enum_DatabaseVersion, DatabaseUtil>();

        /// <summary>
        /// 数据库工具实例集合。
        /// </summary>
        private static Dictionary<Enum_DatabaseVersion, DatabaseUtil> EntitySet
        {
            get
            {
                return DatabaseUtil._entitySet;
            }
        }

        /// <summary>
        /// 创造一个数据库工具实例。
        /// </summary>
        /// <param name="databaseVersion">指定数据库版本。</param>
        /// <returns>数据库工具实例。</returns>
        public static DatabaseUtil GetInstance(Enum_DatabaseVersion databaseVersion)
        {
            if (!EntitySet.ContainsKey(databaseVersion))
            {
                EntitySet[databaseVersion] = new DatabaseUtil()
                {
                    DatabaseVersion = databaseVersion
                };
            }
            return EntitySet[databaseVersion];
        }

        /// <summary>
        /// 创造一个数据库工具实例。（若不指定数据库版本，会屏蔽某些自动功能。）
        /// </summary>
        /// <returns>数据库工具实例。</returns>
        public static DatabaseUtil GetInstance()
        {
            return GetInstance(Enum_DatabaseVersion.Default);
        }

        /// <summary>
        /// 隐藏默认构造方法
        /// </summary>
        private DatabaseUtil()
        {
        }

        /// <summary>
        /// 依据不同的数据库版本，创造对应的适配器。
        /// </summary>
        private Dictionary<Enum_DatabaseVersion, Func<DbDataAdapter>> _dbDataAdapterSet = new Dictionary<Enum_DatabaseVersion, Func<DbDataAdapter>>() {
        { Enum_DatabaseVersion.Default, new Func<DbDataAdapter>(() => { return new SqlDataAdapter(); }) },
        { Enum_DatabaseVersion.OleDB, new Func<DbDataAdapter>(() => { return new OleDbDataAdapter(); }) },
        { Enum_DatabaseVersion.SqlServer2000, new Func<DbDataAdapter>(() => { return new SqlDataAdapter(); }) },
        { Enum_DatabaseVersion.SqlServer2005, new Func<DbDataAdapter>(() => { return new SqlDataAdapter(); }) },
        { Enum_DatabaseVersion.SqlServer2008, new Func<DbDataAdapter>(() => { return new SqlDataAdapter(); }) },
        { Enum_DatabaseVersion.Oracle, new Func<DbDataAdapter>(() => { return new OracleDataAdapter(); }) }
        };

        /// <summary>
        /// 依据不同的数据库版本，创造对应的适配器。
        /// </summary>
        private Dictionary<Enum_DatabaseVersion, Func<DbDataAdapter>> DbDataAdapterSet
        {
            get
            {
                return _dbDataAdapterSet;
            }
        }

        /// <summary>
        /// 构建适配器。
        /// </summary>
        /// <returns></returns>
        public DbDataAdapter CreateDataAdapter()
        {
            return DbDataAdapterSet[DatabaseVersion]();
        }



        /// <summary>
        /// 依据不同的数据库版本，创造对应的数据库链接。
        /// </summary>
        private Dictionary<Enum_DatabaseVersion, Func<DbConnection>> _dbConnectionSet = new Dictionary<Enum_DatabaseVersion, Func<DbConnection>>() {
        { Enum_DatabaseVersion.Default, new Func<DbConnection>(() => { return new SqlConnection(); }) },
        { Enum_DatabaseVersion.OleDB, new Func<DbConnection>(() => { return new OleDbConnection(); }) },
        { Enum_DatabaseVersion.SqlServer2000, new Func<DbConnection>(() => { return new SqlConnection(); }) },
        { Enum_DatabaseVersion.SqlServer2005, new Func<DbConnection>(() => { return new SqlConnection(); }) },
        { Enum_DatabaseVersion.SqlServer2008, new Func<DbConnection>(() => { return new SqlConnection(); }) },
        { Enum_DatabaseVersion.Oracle, new Func<DbConnection>(() => { return new OracleConnection(); }) }
        };

        /// <summary>
        /// 依据不同的数据库版本，创造对应的数据库链接。
        /// </summary>
        private Dictionary<Enum_DatabaseVersion, Func<DbConnection>> DbConnectionSet
        {
            get
            {
                return _dbConnectionSet;
            }
        }

        /// <summary>
        /// 构建数据库链接。
        /// </summary>
        /// <returns></returns>
        public DbConnection CreateConnection(string connectionString)
        {
            var dbConnection = DbConnectionSet[DatabaseVersion]();
            dbConnection.ConnectionString = connectionString;
            return dbConnection;
        }

        /// <summary>
        /// 依据不同的数据库版本，创造对应的命令行。
        /// </summary>
        private Dictionary<Enum_DatabaseVersion, Func<DbCommand>> _dbCommandSet = new Dictionary<Enum_DatabaseVersion, Func<DbCommand>>() {
        { Enum_DatabaseVersion.Default, new Func<DbCommand>(() => { return new SqlCommand(); }) },
        { Enum_DatabaseVersion.OleDB, new Func<DbCommand>(() => { return new OleDbCommand(); }) },
        { Enum_DatabaseVersion.SqlServer2000, new Func<DbCommand>(() => { return new SqlCommand(); }) },
        { Enum_DatabaseVersion.SqlServer2005, new Func<DbCommand>(() => { return new SqlCommand(); }) },
        { Enum_DatabaseVersion.SqlServer2008, new Func<DbCommand>(() => { return new SqlCommand(); }) },
        { Enum_DatabaseVersion.Oracle, new Func<DbCommand>(() => { return new OracleCommand(); }) }
        };

        /// <summary>
        /// 依据不同的数据库版本，创造对应的命令行。
        /// </summary>
        private Dictionary<Enum_DatabaseVersion, Func<DbCommand>> DbCommandSet
        {
            get
            {
                return _dbCommandSet;
            }
        }

        /// <summary>
        /// 构建命令行。
        /// </summary>
        /// <returns></returns>
        public DbCommand CreateCommand()
        {
            return DbCommandSet[DatabaseVersion]();
        }

        /// <summary>
        /// 构建命令行。
        /// </summary>
        /// <returns></returns>
        public DbCommand CreateCommand(string sql, string connectionString, IEnumerable<DbParameter> dbParameter, CommandType commandType)
        {
            DbCommand comm = CreateCommand();
            comm.CommandText = sql;
            comm.CommandType = commandType;
            comm.Connection = CreateConnection(connectionString);
            if (dbParameter != null)
            {
                foreach (DbParameter i in dbParameter)
                {
                    comm.Parameters.Add(i);
                }
            }
            return comm;
        }

        /// <summary>
        /// 构建命令行。
        /// </summary>
        /// <returns></returns>
        public DbCommand CreateCommand(SelectBuilder selectBuilder)
        {
            if (selectBuilder == null)
            {
                return CreateCommand();
            }
            else
            {
                return CreateCommand(selectBuilder.ToString(), selectBuilder.ConnectionString, selectBuilder.Parameters, CommandType.Text);
            }
        }

        /// <summary>
        /// 当前工具对应的数据库版本。
        /// </summary>
        public Enum_DatabaseVersion DatabaseVersion { get; private set; }

        /// <summary>
        /// 对Sql语句进行分页处理。
        /// </summary>
        /// <param name="viewSql">待处理的Sql语句。</param>
        /// <param name="pageIndex">分页页码。</param>
        /// <param name="dataCount">每页显示数据数量。</param>
        /// <param name="sortBy">排序规则。</param>
        /// <param name="viewName">生成子视图名称。</param>
        /// <param name="rowIndexName">生成行标记序列名称。</param>
        /// <param name="databaseVersion">数据库版本。</param>
        /// <returns>处理后的分页语句。</returns>
        public static string PagingView(string viewSql, int pageIndex, int dataCount, string sortBy, string viewName, string rowIndexName, Enum_DatabaseVersion databaseVersion)
        {
            if (string.IsNullOrEmpty(viewSql))
            {
                return viewSql;
            }
            if (pageIndex < 1)
            {
                pageIndex = 1;
            }
            if (dataCount < 1)
            {
                dataCount = 1;
            }
            if (string.IsNullOrEmpty(viewName))
            {
                viewName = string.Format("View_{0}", Guid.NewGuid().ToString().Replace("-", string.Empty));
            }
            if (string.IsNullOrEmpty(rowIndexName))
            {
                rowIndexName = string.Format("RowIndex_{0}", Guid.NewGuid().ToString().Replace("-", string.Empty));
            }
            switch (databaseVersion)
            {
                case Enum_DatabaseVersion.Default:
                case Enum_DatabaseVersion.OleDB:
                    {
                        return viewSql;
                    }
                case Enum_DatabaseVersion.SqlServer2000:
                    {
                        return string.Format("select top {3} * from ({0}) as {1}_A where id not in (select top {4} id from ({0}) as {1}_B order by {2}) order by {2};select Count(*) as Amount from ({0}) as {1}_C"
                            , viewSql
                            , viewName
                            , sortBy
                            , dataCount
                            , ((pageIndex - 1) * dataCount)
                            );
                    }
                case Enum_DatabaseVersion.SqlServer2005:
                case Enum_DatabaseVersion.SqlServer2008:
                    {
                        return string.Format("select * from (select Row_Number() over(order by {3}) as {2},* from ({0}) as {1}_A) as {1}_B where {2} between {4} and {5};select Count(*) as Amount from ({0}) as {1}_C"
                            , viewSql
                            , viewName
                            , rowIndexName
                            , sortBy
                            , ((pageIndex - 1) * dataCount + 1)
                            , (pageIndex * dataCount)
                            );
                    }
                case Enum_DatabaseVersion.Oracle:
                    {
                        return string.Format("select * from (select RowNum {2},* from ({0} order by {3}) {1}_A where RowNum >= {4} ) {1}_B where {1}_B.{2} <= {5};select Count(*) as Amount from ({0}) as {1}_C"
                            , viewSql
                            , viewName
                            , rowIndexName
                            , sortBy
                            , ((pageIndex - 1) * dataCount + 1)
                            , (pageIndex * dataCount)
                            );
                    }
                default: goto case Enum_DatabaseVersion.Default;
            }
        }

        /// <summary>
        /// 对Sql语句进行分页处理。
        /// </summary>
        /// <param name="viewSql">待处理的Sql语句。</param>
        /// <param name="pageIndex">分页页码。</param>
        /// <param name="dataCount">每页显示数据数量。</param>
        /// <param name="sortBy">排序规则。</param>
        /// <param name="databaseVersion">数据库版本。</param>
        /// <returns>处理后的分页语句。</returns>
        public static string PagingView(string viewSql, int pageIndex, int dataCount, string sortBy, Enum_DatabaseVersion databaseVersion)
        {
            return PagingView(viewSql, pageIndex, dataCount, sortBy, string.Empty, string.Empty, databaseVersion);
        }

        /// <summary>
        /// 对Sql语句进行分页处理。
        /// </summary>
        /// <param name="viewSql">待处理的Sql语句。</param>
        /// <param name="pageIndex">分页页码。</param>
        /// <param name="dataCount">每页显示数据数量。</param>
        /// <param name="sortBy">排序规则。</param>
        /// <returns>处理后的分页语句。</returns>
        public string PagingView(string viewSql, int pageIndex, int dataCount, string sortBy)
        {
            return PagingView(viewSql, pageIndex, dataCount, sortBy, string.Empty, string.Empty, DatabaseVersion);
        }

        /// <summary>
        /// 使用适配器按照指定参数进行数据集填充。
        /// </summary>
        /// <param name="sql">需要进行查询的sql字符串。</param>
        /// <param name="connectionString">需要进行查询的数据库连接字符串。</param>
        /// <returns>填充后的数据集</returns>
        public DataSet ExecuteDataAdapter(string sql, string connectionString)
        {
            return ExecuteDataAdapter(sql, connectionString, new DbParameter[] { });
        }

        /// <summary>
        /// 使用适配器按照指定参数进行数据集填充。
        /// </summary>
        /// <param name="sql">需要进行查询的sql字符串。</param>
        /// <param name="connectionString">需要进行查询的数据库连接字符串。</param>
        /// <param name="dbParameter">需要进行查询的dbParameter参数组。</param>
        /// <returns>填充后的数据集。</returns>
        public DataSet ExecuteDataAdapter(string sql, string connectionString, IEnumerable<DbParameter> dbParameter)
        {
            return ExecuteDataAdapter(sql, connectionString, dbParameter, CommandType.Text);
        }

        /// <summary>
        /// 使用适配器按照指定参数进行数据集填充。
        /// </summary>
        /// <param name="sql">需要进行查询的sql字符串。</param>
        /// <param name="connectionString">需要进行查询的数据库连接字符串。</param>
        /// <param name="dbParameter">需要进行查询的dbParameter参数组。</param>
        /// <param name="commandType">数据查询命令Command类型。</param>
        /// <returns>填充后的数据集。</returns>
        public DataSet ExecuteDataAdapter(string sql, string connectionString, IEnumerable<DbParameter> dbParameter, CommandType commandType)
        {
            DbCommand select = CreateCommand(sql, connectionString, dbParameter, commandType);
            FormatSelectCommand(select);
            return ExecuteDataAdapter(select);
        }

        /// <summary>
        /// 使用适配器按照指定参数进行数据集填充。
        /// </summary>
        /// <param name="select">查询构造器。</param>
        /// <returns>填充后的数据集。</returns>
        public DataSet ExecuteDataAdapter(SelectBuilder selectBuilder)
        {
            DbCommand select = CreateCommand(selectBuilder);
            FormatSelectCommand(select);
            return ExecuteDataAdapter(select);
        }

        /// <summary>
        /// 使用适配器按照指定参数进行数据集填充。
        /// </summary>
        /// <param name="select">查询命令集。</param>
        /// <returns>填充后的数据集。</returns>
        public DataSet ExecuteDataAdapter(DbCommand select)
        {
            return ExecuteDataAdapter(new DataSet("data"), select);
        }

        /// <summary>
        /// 使用适配器按照指定参数进行数据集填充。
        /// </summary>
        /// <param name="data">待填充的数据集。</param>
        /// <param name="select">查询命令集。</param>
        /// <returns>填充后的数据集。</returns>
        public DataSet ExecuteDataAdapter(DataSet data, DbCommand select)
        {
            return ExecuteDataAdapter(data
                , select
                , null
                , null
                , null);
        }

        /// <summary>
        /// 使用适配器按照指定参数进行数据集填充。
        /// </summary>
        /// <param name="data">待填充的数据集。</param>
        /// <param name="select">查询命令集。</param>
        /// <param name="insert">新增命令集。</param>
        /// <param name="delete">删除命令集。</param>
        /// <param name="update">修改命令集。</param>
        /// <returns>填充后的数据集。</returns>
        public DataSet ExecuteDataAdapter(DataSet data, DbCommand select, DbCommand insert, DbCommand delete, DbCommand update)
        {
            if (data == null
                || (
                    (select == null || string.IsNullOrEmpty(select.CommandText))
                    && (insert == null || string.IsNullOrEmpty(insert.CommandText))
                    && (delete == null || string.IsNullOrEmpty(delete.CommandText))
                    && (update == null || string.IsNullOrEmpty(update.CommandText))
                    )
                )
            {
                return data;
            }
            DbDataAdapter da = DbDataAdapterSet[DatabaseVersion]();
            da.SelectCommand = select;
            da.InsertCommand = insert;
            da.DeleteCommand = delete;
            da.UpdateCommand = update;
            if (
                (insert != null && !string.IsNullOrEmpty(insert.CommandText)) ||
                (delete != null && !string.IsNullOrEmpty(delete.CommandText)) ||
                (update != null && !string.IsNullOrEmpty(update.CommandText))
                )
            {
                bool insertFlag = ForeachSqlCommand(insert);
                bool deleteFlag = ForeachSqlCommand(delete);
                bool updateFlag = ForeachSqlCommand(update);
                DbTransaction insertTran = null;
                DbTransaction deleteTran = null;
                DbTransaction updateTran = null;
                if (insertFlag)
                {
                    if (insert.Connection.State == ConnectionState.Closed)
                    {
                        insert.Connection.Open();
                    }
                    insertTran = insert.Connection.BeginTransaction();
                    insert.Transaction = insertTran;
                }
                if (deleteFlag)
                {
                    if (delete.Connection.State == ConnectionState.Closed)
                    {
                        delete.Connection.Open();
                    }
                    deleteTran = delete.Connection.BeginTransaction();
                    delete.Transaction = deleteTran;
                }
                if (updateFlag)
                {
                    if (update.Connection.State == ConnectionState.Closed)
                    {
                        update.Connection.Open();
                    }
                    updateTran = update.Connection.BeginTransaction();
                    update.Transaction = updateTran;
                }
                try
                {
                    foreach (DataTable dt in data.Tables)
                    {
                        da.Update(dt);
                    }
                }
                catch (Exception ex)
                {
                    if (updateFlag && updateTran != null)
                    {
                        updateTran.Rollback();
                    }
                    if (deleteFlag && deleteTran != null)
                    {
                        deleteTran.Rollback();
                    }
                    if (insertFlag && insertTran != null)
                    {
                        insertTran.Rollback();
                    }
                    throw ex;
                }
                if (updateFlag && updateTran != null)
                {
                    updateTran.Commit();
                }
                if (deleteFlag && deleteTran != null)
                {
                    deleteTran.Commit();
                }
                if (insertFlag && insertTran != null)
                {
                    insertTran.Commit();
                }
            }
            if (select != null && !string.IsNullOrEmpty(select.CommandText))
            {
                bool selectFlag = ForeachSqlCommand(select);
                DbTransaction selectTran = null;
                if (selectFlag)
                {
                    if (select.Connection.State == ConnectionState.Closed)
                    {
                        select.Connection.Open();
                    }
                    selectTran = select.Connection.BeginTransaction();
                    select.Transaction = selectTran;
                }
                try
                {
                    select.CommandTimeout = 600;
                    da.Fill(data);
                }
                catch (Exception ex)
                {
                    if (selectFlag && selectTran != null)
                    {
                        selectTran.Rollback();
                    }
                    throw ex;
                }
                if (selectFlag && selectTran != null)
                {
                    selectTran.Commit();
                }
            }
            return data;
        }

        /// <summary>
        /// 重新整理DbCommand，并且验证是否存在事务参数。
        /// </summary>
        /// <param name="comm"></param>
        /// <param name="sql"></param>
        public static bool ForeachSqlCommand(DbCommand comm)
        {
            if (comm == null)
            {
                return false;
            }

            if (string.IsNullOrEmpty(comm.CommandText))
            {
                comm.CommandText = string.Empty;
            }

            bool tranFlag = false;

            StringBuilder parametersString = new StringBuilder();
            foreach (DbParameter p in comm.Parameters)
            {
                if (p.Value == null)
                {
                    p.Value = DBNull.Value;
                }
                if (
                    p.ParameterName.Equals("@Transaction") &&
                    (p.Value.Equals(true) || p.Value.ToString().ToLower().Equals("true"))
                    )
                {
                    tranFlag = true;
                }
            }
            return tranFlag;
        }

        /// <summary>
        /// 重新整理SelectCommand。
        /// </summary>
        /// <param name="selectCommand"></param>
        /// <param name="sql"></param>
        public void FormatSelectCommand(DbCommand selectCommand)
        {
            if (selectCommand == null)
            {
                return;
            }
            if (string.IsNullOrEmpty(selectCommand.CommandText))
            {
                selectCommand.CommandText = string.Empty;
            }
            bool[] pagingArgsFlag = new bool[] { false, false, false };
            foreach (DbParameter p in selectCommand.Parameters)
            {
                if (p.Value == null)
                {
                    p.Value = DBNull.Value;
                }
                if ("@PageIndex".Equals(p.ParameterName))
                {
                    pagingArgsFlag[0] = true;
                }
                if ("@DataCount".Equals(p.ParameterName))
                {
                    pagingArgsFlag[1] = true;
                }
                if ("@SortBy".Equals(p.ParameterName))
                {
                    pagingArgsFlag[2] = true;
                }
            }
            bool pagingFlag = pagingArgsFlag[0] && pagingArgsFlag[1] && pagingArgsFlag[2];
            if (
                selectCommand.CommandType == CommandType.Text &&
                pagingFlag
                )
            {
                int pageIndex = 0;
                int.TryParse(selectCommand.Parameters["@PageIndex"].Value.ToString(), out pageIndex);
                int dataCount = 0;
                int.TryParse(selectCommand.Parameters["@DataCount"].Value.ToString(), out dataCount);
                selectCommand.CommandText = PagingView(selectCommand.CommandText
                      , pageIndex
                      , dataCount
                      , selectCommand.Parameters["@SortBy"].Value.ToString()
                      );
            }
        }

        /// <summary>
        /// 格式化数据库参数值中的特殊符号。escape '\'
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string FormatSqlParameterValue(string value)
        {
            return new StringBuilder(value)
                .Replace(@"\", @"\\")
                .Replace("[", @"\[")
                .Replace("]", @"\]")
                .Replace(@"%", @"\%")
                .Replace("_", @"\_")
                .Replace(@"  ", @"_")
                .Replace(@" ", @"%")
                .ToString();
        }

        /// <summary>
        /// 格式化数据库字符串值中的特殊符号。escape '\'
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string FormatSqlString(string value)
        {
            return new StringBuilder(value)
                .Replace(@"\", @"\\")
                .Replace("[", @"\[")
                .Replace("]", @"\]")
                .Replace(@"%", @"\%")
                .Replace("_", @"\_")
                .Replace("'", @"\''")
                .Replace(@"  ", @"_")
                .Replace(@" ", @"%")
                .ToString();
        }

        /// <summary>
        /// 数据库版本。
        /// </summary>
        public enum Enum_DatabaseVersion
        {
            /// <summary>
            /// 默认数据库。
            /// </summary>
            Default,

            /// <summary>
            /// 泛指OleDB。
            /// </summary>
            OleDB,

            /// <summary>
            /// SqlServer2000。
            /// </summary>
            SqlServer2000,

            /// <summary>
            /// SqlServer2005。
            /// </summary>
            SqlServer2005,

            /// <summary>
            /// SqlServer2008。
            /// </summary>
            SqlServer2008,

            /// <summary>
            /// Oracle。
            /// </summary>
            Oracle
        }

        /// <summary>
        /// 基本查询语句构建器。
        /// </summary>
        public class SelectBuilder
        {
            /// <summary>
            /// 私有化实例方法。
            /// </summary>
            private SelectBuilder()
            {

            }

            /// <summary>
            /// 获取基本查询语句构建器实例。
            /// </summary>
            /// <returns>实例。</returns>
            public static SelectBuilder GetInstance()
            {
                return new SelectBuilder();
            }

            /// <summary>
            /// 头部文档。
            /// </summary>
            private StringBuilder _header = new StringBuilder();

            /// <summary>
            /// 头部文档。
            /// </summary>
            public StringBuilder Header
            {
                get
                {
                    return _header;
                }
            }

            /// <summary>
            /// 脚部文档。
            /// </summary>
            private StringBuilder _footer = new StringBuilder();

            /// <summary>
            /// 脚部文档。
            /// </summary>
            public StringBuilder Footer
            {
                get
                {
                    return _footer;
                }
            }

            /// <summary>
            /// 基本查询语句select部分。
            /// </summary>
            private StringBuilder _select = new StringBuilder();

            /// <summary>
            /// 基本查询语句select部分。
            /// </summary>
            public StringBuilder Select
            {
                get
                {
                    return _select;
                }
            }

            /// <summary>
            /// 基本查询语句发from部分。
            /// </summary>
            private StringBuilder _from = new StringBuilder();

            /// <summary>
            /// 基本查询语句发from部分。
            /// </summary>
            public StringBuilder From
            {
                get
                {
                    return _from;
                }
            }

            /// <summary>
            /// 基本查询语句where部分。
            /// </summary>
            private StringBuilder _where = new StringBuilder();

            /// <summary>
            /// 基本查询语句where部分。
            /// </summary>
            public StringBuilder Where
            {
                get
                {
                    return _where;
                }
            }

            /// <summary>
            /// 基本查询语句group by部分。
            /// </summary>
            private StringBuilder _groupBy = new StringBuilder();

            /// <summary>
            /// 基本查询语句group by部分。
            /// </summary>
            public StringBuilder GroupBy
            {
                get
                {
                    return _groupBy;
                }
            }

            /// <summary>
            /// 基本查询语句having部分。
            /// </summary>
            private StringBuilder _having = new StringBuilder();

            /// <summary>
            /// 基本查询语句having部分。
            /// </summary>
            public StringBuilder Having
            {
                get
                {
                    return _having;
                }
            }

            /// <summary>
            /// 基本查询语句order by部分。
            /// </summary>
            private StringBuilder _orderBy = new StringBuilder();

            /// <summary>
            /// 基本查询语句order by部分。
            /// </summary>
            public StringBuilder OrderBy
            {
                get
                {
                    return _orderBy;
                }
            }

            /// <summary>
            /// 基本查询语句参数集合。
            /// </summary>
            private HashSet<DbParameter> _parameters = new HashSet<DbParameter>();

            public HashSet<DbParameter> Parameters
            {
                get { return _parameters; }
            }

            /// <summary>
            /// 数据库链接字符串。
            /// </summary>
            private string _connectionString = string.Empty;

            /// <summary>
            /// 数据库链接字符串。
            /// </summary>
            public string ConnectionString
            {
                get
                {
                    return _connectionString;
                }
                set
                {
                    if (string.IsNullOrEmpty(value))
                    {
                        return;
                    }
                    _connectionString = value;
                }
            }

            public override string ToString()
            {
                return string.Format(@"
{0}
{1}
{2}
{3}
{4}
{5}
{6}
{7}
", Header, Select, From, Where, GroupBy, Having, OrderBy, Footer);
            }
        }
    }
}
