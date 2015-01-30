using System.Collections.Generic;
using System.Linq.Expressions;

namespace System.Data
{
    /// <summary>
    /// DataSet扩展类。
    /// </summary>
    internal static class DataSetExtend
    {
        /// <summary>
        /// DataSet扩展涉及到的基本类型。
        /// </summary>
        private static HashSet<Type> _primitiveType = new HashSet<Type>()
        {
            typeof(object),
            typeof(string),
            typeof(char),
            typeof(int),
            typeof(double),
            typeof(decimal),
            typeof(float),
            typeof(long),
            typeof(bool),
            typeof(DateTime)
        };

        /// <summary>
        /// DataSet扩展涉及到的基本类型。
        /// </summary>
        public static HashSet<Type> PrimitiveType
        {
            get
            {
                return DataSetExtend._primitiveType;
            }
        }

        /// <summary>
        /// 将集合转换为数据集。
        /// </summary>
        /// <typeparam name="T">集合中元素的类型。</typeparam>
        /// <param name="list">集合。</param>
        /// <returns>转换后的数据集。</returns>
        public static DataSet ToDataSet<T>(this IEnumerable<T> list)
        {
            Type t = typeof(T);
            DataSet ds = new DataSet("data");
            ds.Tables.Add(t.Name);
            //如果集合中元素为DataSet扩展涉及到的基本类型时，进行特殊转换。
            if (DataSetExtend.PrimitiveType.Contains(t))
            {
                ds.Tables[0].TableName = t.Name;
                ds.Tables[0].Columns.Add("value");
                if (list == null)
                {
                    return ds;
                }
                foreach (var i in list)
                {
                    DataRow addRow = ds.Tables[0].NewRow();
                    addRow[t.Name] = i;
                    ds.Tables[0].Rows.Add(addRow);
                }
                return ds;
            }
            //处理模型的字段和属性。
            var fields = t.GetFields();
            var properties = t.GetProperties();
            foreach (var j in fields)
            {
                if (!ds.Tables[0].Columns.Contains(j.Name))
                {
                    ds.Tables[0].Columns.Add(j.Name, j.FieldType);
                }
            }
            foreach (var j in properties)
            {
                if (!ds.Tables[0].Columns.Contains(j.Name))
                {
                    ds.Tables[0].Columns.Add(j.Name, j.PropertyType);
                }
            }
            if (list == null)
            {
                return ds;
            }
            foreach (var i in list)
            {
                DataRow addRow = ds.Tables[0].NewRow();
                foreach (var j in fields)
                {
                    MemberExpression field = Expression.Field(Expression.Constant(i), j.Name);
                    LambdaExpression lambda = Expression.Lambda(field, new ParameterExpression[] { });
                    ////.NET Framework4.0用（效率高）
                    //dynamic func = lambda.Compile();
                    //dynamic value = func();
                    //addRow[j.Name] = value;
                    //兼容.NET Framework3.5用
                    addRow[j.Name] = lambda.Compile().DynamicInvoke();
                }
                foreach (var j in properties)
                {
                    MemberExpression property = Expression.Property(Expression.Constant(i), j);
                    LambdaExpression lambda = Expression.Lambda(property, new ParameterExpression[] { });
                    ////.NET Framework4.0用（效率高）
                    //dynamic func = lambda.Compile();
                    //dynamic value = func();
                    //addRow[j.Name] = value;
                    //兼容.NET Framework3.5用
                    addRow[j.Name] = lambda.Compile().DynamicInvoke();
                }
                ds.Tables[0].Rows.Add(addRow);
            }
            return ds;
        }

        /// <summary>
        /// 获取DataSet第一表，第一行，第一列的值。
        /// </summary>
        /// <param name="ds">DataSet数据集。</param>
        /// <returns>值。</returns>
        public static object GetData(this DataSet ds)
        {
            if (ds == null
                || ds.Tables.Count == 0)
            {
                return string.Empty;
            }
            else
            {
                return ds.Tables[0].GetData();
            }
        }

        /// <summary>
        /// 获取DataTable第一行，第一列的值。
        /// </summary>
        /// <param name="ds">DataTable数据集表。</param>
        /// <returns>值。</returns>
        public static object GetData(this DataTable dt)
        {
            if (dt.Columns.Count == 0
                || dt.Rows.Count == 0)
            {
                return string.Empty;
            }
            else
            {
                return dt.Rows[0][0];
            }
        }
        /// <summary>
        /// 获取DataSet第一个匹配columnName的值。
        /// </summary>
        /// <param name="ds">数据集。</param>
        /// <param name="columnName">列名。</param>
        /// <returns>值。</returns>
        public static object GetData(this DataSet ds, string columnName)
        {
            if (ds == null
                || ds.Tables.Count == 0)
            {
                return string.Empty;
            }
            foreach (DataTable dt in ds.Tables)
            {
                object obj = ds.Tables[0].GetData(columnName);
                if (obj != null)
                {
                    return obj;
                }
            }
            return string.Empty;
        }

        /// <summary>
        /// 获取DataTable第一个匹配columnName的值。
        /// </summary>
        /// <param name="dt">数据表。</param>
        /// <param name="columnName">列名。</param>
        /// <returns>值。</returns>
        public static object GetData(this DataTable dt, string columnName)
        {
            if (string.IsNullOrEmpty(columnName))
            {
                return GetData(dt);
            }
            if (dt.Columns.Count == 0
                || dt.Columns.IndexOf(columnName) == -1
                || dt.Rows.Count == 0)
            {
                return string.Empty;
            }
            return dt.Rows[0][columnName];
        }
    }
}