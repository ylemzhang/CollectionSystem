/*
 * 
 * 定义业务层处理实体。
 * 
 * */

namespace BLL
{
    /// <summary>
    /// 业务层处理实体。
    /// </summary>
    public partial class BLLEntity
    {
        private BLLEntity()
        {

        }

        public static BLLEntity GetInstance()
        {
            return new BLLEntity();
        }
    }
}
