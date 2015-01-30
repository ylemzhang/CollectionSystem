namespace BLL
{
    using Common;
    using DAL;
    using System;
    using System.Data;
    using System.Text;
    using System.Web;

    public class AdminBLL
    {
        public static bool ChangePassword(string UserName, string oldPass, string Password)
        {
            if (!Login(UserName, oldPass))
            {
                return false;
            }
            Password = Encode(Password);
            UserDAL.UpdateUserPassword(UserName, Password);
            return true;
        }

        private static bool CheckSameUserName(string ID, string UserName)
        {
            string where;
            if (ID == string.Empty)
            {
                where = " UserName=N'{0}' ";
                where = string.Format(where, UserName);
            }
            else
            {
                where = " UserName=N'{0}' and ID <> {1} ";
                where = string.Format(where, UserName, ID);
            }
            if (DataHelper.GetList("Usertable", "ID", where).Tables[0].Rows.Count == 0)
            {
                return false;
            }
            return true;
        }

        public static string Decode(string str)
        {
            if (str == string.Empty)
            {
                return "";
            }
            byte[] c = Convert.FromBase64String(str);
            return Encoding.Default.GetString(c);
        }

        public static void DeleteUser(string IDs)
        {
            UserDAL.DeleteUser(IDs);
        }

        public static string Encode(string str)
        {
            if (str == string.Empty)
            {
                return "";
            }
            return Convert.ToBase64String(Encoding.Default.GetBytes(str));
        }

        public static DataSet GetAllUser(string field)
        {
            return UserDAL.GetUserList(field, "1=1");
        }

        public static DataSet GetDicAdminUserList()
        {
            return UserDAL.GetUserList("RoleID=4 or RoleID=3");
        }

        private static DataSet getEmptyDataSet()
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            DataColumn dc = new DataColumn("ID");
            dt.Columns.Add(dc);
            dc = new DataColumn("RoleName");
            dt.Columns.Add(dc);
            ds.Tables.Add(dt);
            return ds;
        }

        public static DataSet GetledCompanys(string userid)
        {
            return DataHelper.GetList("Select ID,CompanyName  from CompanyTable where Owner=" + userid);
        }

        public static DataSet GetledGroups(string userid)
        {
            return DataHelper.GetList("Select ID,GroupName  from GroupTable where LeadID=" + userid);
        }

        public static DataSet GetReadCompanys(string userid)
        {
            return DataHelper.GetList("select c.Id,C.companyName from companyUser u inner join companyTable c on u.companyID=c.ID where u.UserID=" + userid);
        }

        public static DataSet GetRoleAll()
        {
            DataSet ds = getEmptyDataSet();
            DataTable dt = ds.Tables[0];
            DataRow dr = dt.NewRow();
            dr[0] = "1";
            dr[1] = StrTable.GetStr("administrator");
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr[0] = "2";
            dr[1] = StrTable.GetStr("lead");
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr[0] = "3";
            dr[1] = StrTable.GetStr("operator");
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr[0] = "4";
            dr[1] = StrTable.GetStr("readonlyUser");
            dt.Rows.Add(dr);
            return ds;
        }

        public static DataSet GetRoleList()
        {
            DataSet ds = getEmptyDataSet();
            DataTable dt = ds.Tables[0];
            DataRow dr = dt.NewRow();
            dr[0] = "3";
            dr[1] = StrTable.GetStr("operator");
            dt.Rows.Add(dr);
            return ds;
        }

        public static SystemUser GetUserByID(string id)
        {
            return GetUserFromRow(UserDAL.GetUserList("ID=" + id).Tables[0].Rows[0]);
        }

        private static SystemUser GetUserFromRow(DataRow dr)
        {
            SystemUser user = new SystemUser {
                ID = dr["ID"].ToString(),
                UserName = dr["UserName"].ToString()
            };
            string pass = dr["Password"].ToString();
            user.Password = Decode(pass);
            user.RoleID = dr["RoleID"].ToString();
            user.RealName = dr["RealName"].ToString();
            user.Phone = dr["Phone"].ToString();
            user.Mobile = dr["Mobile"].ToString();
            user.Email = dr["Email"].ToString();
            user.Gender = dr["Gender"].ToString();
            user.Para3 = dr["Para3"].ToString();
            user.Para1 = dr["Para1"].ToString();
            user.Para2 = dr["Para2"].ToString();
            return user;
        }

        public static DataSet GetUserList()
        {
            return UserDAL.GetUserList();
        }

        public static DataSet GetUserList(string field, string where)
        {
            return UserDAL.GetUserList(field, where);
        }

        public static DataSet GetUserListWithOutAdmin(string field)
        {
            return UserDAL.GetUserList(field, "ID>1");
        }

        public static bool Login(string UserName, string Password)
        {
            UserName = UserName.Replace("'", "''");
            Password = Password.Replace("'", "''");
            Password = Encode(Password);
            string where = "UserName =N'{0}' and password='{1}'";
            DataSet ds = UserDAL.GetUserList(string.Format(where, UserName, Password));
            if ((ds.Tables.Count == 0) || (ds.Tables[0].Rows.Count != 1))
            {
                return false;
            }
            HttpContext.Current.Session["CurrentUser"] = GetUserFromRow(ds.Tables[0].Rows[0]);
            return true;
        }

        public static void UpdateCurrentUser(SystemUser user)
        {
            string ID = user.ID;
            string RealName = user.RealName;
            string Phone = user.Phone;
            string Mobile = user.Mobile;
            string Email = user.Email;
            string Gender = user.Gender;
            string Para1 = user.Para1;
            string Para2 = user.Para2;
            string Para3 = user.Para3;
            UserDAL.UpdateUser(RealName, Phone, Mobile, Email, Gender, Para1, Para2, Para3, ID);
        }

        public static int UpdateUser(SystemUser user)
        {
            string ID = user.ID;
            string UserName = user.UserName;
            string Password = Encode(user.Password);
            string RoleID = user.RoleID;
            string RealName = user.RealName;
            string Phone = user.Phone;
            string Mobile = user.Mobile;
            string Email = user.Email;
            string Gender = user.Gender;
            string Para3 = user.Para3;
            if (CheckSameUserName(ID, UserName))
            {
                return 0;
            }
            if (ID == string.Empty)
            {
                UserDAL.InsertUser(UserName, Password, RoleID, RealName, Phone, Mobile, Email, Gender, Para3);
            }
            else
            {
                UserDAL.UpdateUser(UserName, Password, RoleID, RealName, Phone, Mobile, Email, Gender, Para3, ID);
            }
            HttpContext.Current.Session["UserDS"] = GetAllUser("ID,UserName,RoleID,RealName");
            return 1;
        }

        public static SystemUser CurrentSystemUser
        {
            get
            {
                return (HttpContext.Current.Session["CurrentUser"] as SystemUser);
            }
        }

        public static string TempFileName
        {
            get
            {
                return CurrentSystemUser.ID;
            }
        }
    }
}

