namespace BLL
{
    using DAL;
    using System;
    using System.Data;

    public class GroupBLL
    {
        public static void DeleteGroup(string IDs)
        {
            GroupDAL.DeleteGroup(IDs);
        }

        public static DataSet GetGroupByID(string id)
        {
            return GroupDAL.GetGroupList("ID=" + id);
        }

        public static DataSet GetGroupList()
        {
            return GroupDAL.GetGroupList();
        }

        public static DataSet GetGroupList(string where)
        {
            return GroupDAL.GetGroupList(where);
        }

        public static DataSet GetLeadID(string userID, string CompanyID)
        {
            return GroupDAL.GetLeadID(userID, CompanyID);
        }

        public static int InsertGroup(string GroupName, int leadID, int CompanyID)
        {
            return GroupDAL.InsertGroup(GroupName, leadID, CompanyID);
        }

        public static void UpdateGroup(string ID, string GroupName, int leadID)
        {
            GroupDAL.UpdateGroup(GroupName, leadID, int.Parse(ID));
        }
    }
}

