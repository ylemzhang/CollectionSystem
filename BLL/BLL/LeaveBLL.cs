namespace BLL
{
    using DAL;
    using System;
    using System.Data;

    public class LeaveBLL
    {
        public static void DeleteLeave(string IDs)
        {
            LeaveDAL.DeleteLeave(IDs);
        }

        public static DataSet GetLeaveByID(string id)
        {
            return LeaveDAL.GetLeaveList("ID=" + id);
        }

        public static DataSet GetLeaveList(string where)
        {
            return LeaveDAL.GetLeaveList(where);
        }

        public static void InsertLeave(int UserID, string userName, DateTime punchIn)
        {
            LeaveDAL.InsertLeave(UserID, userName, punchIn);
        }

        public static void UpdateLeave(int UserID, DateTime punchOut)
        {
            LeaveDAL.UpdateLeave(punchOut, UserID);
        }

        public static void UpdateLeave(int ID, DateTime punchIn, DateTime punchOut)
        {
            LeaveDAL.UpdateLeave(punchIn, punchOut, ID);
        }
    }
}

