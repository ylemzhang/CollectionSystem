using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;

namespace BLL
{
    /// <summary>
    /// BLL层访问外部统一入口。
    /// </summary>
    internal class BLLAccess
    {
        private BLLAccess()
        {

        }

        public static DALEntity GetInstance()
        {
            return DALEntity.GetInstance();
        }
    }
}
