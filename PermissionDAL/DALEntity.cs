/*
 * 
 * 定义数据库访问实体。
 * 
 * */

namespace DAL
{
    /// <summary>
    /// 数据库访问实体。
    /// </summary>
    public partial class DALEntity
    {
        private DALEntity()
        {

        }

        public static DALEntity GetInstance()
        {
            return new DALEntity();
        }
    }
}
