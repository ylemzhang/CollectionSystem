namespace DAL
{
    using System;
    using System.Data;
    using System.Data.SqlClient;

    public class UserDAL
    {
        public static void DeleteUser(string IDs)
        {
            DataHelper.DeleteByIDs("Usertable", IDs);
        }

        public static DataSet GetUserList()
        {
            return DataHelper.GetAll("Usertable");
        }

        public static DataSet GetUserList(string where)
        {
            return DataHelper.GetList("Usertable", where);
        }

        public static DataSet GetUserList(string field, string where)
        {
            return DataHelper.GetList("Usertable", field, where);
        }

        public static void InsertUser(string UserName, string Password, string RoleID, string RealName, string Phone, string Mobile, string Email, string Gender, string Para3)
        {
            SqlConnection conn = new SqlConnection(DataHelper.conn);
            string updateSql = "insert   Usertable (UserName,Password,RoleID,RealName,Phone,Mobile,Email,Gender,Para3)  values (@UserName,@Password,@RoleID,@RealName,@Phone,@Mobile,@Email,@Gender,@Para3)";
            string addSql = "   insert User_Data (Account,UserName,Password,Ban) values(@@IDENTITY,@UserName,@Password,0) ";
            updateSql += addSql;
            SqlCommand cmd = new SqlCommand(updateSql, conn);
            SqlParameter sp = new SqlParameter("@UserName", SqlDbType.NVarChar) {
                Value = UserName
            };
            SqlParameter sp1 = new SqlParameter("@Password", SqlDbType.NVarChar) {
                Value = Password
            };
            SqlParameter sp2 = new SqlParameter("@RoleID", SqlDbType.Int) {
                Value = int.Parse(RoleID)
            };
            SqlParameter sp3 = new SqlParameter("@RealName", SqlDbType.NVarChar) {
                Value = RealName
            };
            SqlParameter sp4 = new SqlParameter("@Phone", SqlDbType.NVarChar) {
                Value = Phone
            };
            SqlParameter sp5 = new SqlParameter("@Mobile", SqlDbType.NVarChar) {
                Value = Mobile
            };
            SqlParameter sp6 = new SqlParameter("@Email", SqlDbType.NVarChar) {
                Value = Email
            };
            SqlParameter sp7 = new SqlParameter("@Gender", SqlDbType.NVarChar) {
                Value = Gender
            };
            SqlParameter sp8 = new SqlParameter("@Para3", SqlDbType.NVarChar) {
                Value = Para3
            };
            cmd.Parameters.Add(sp);
            cmd.Parameters.Add(sp1);
            cmd.Parameters.Add(sp2);
            cmd.Parameters.Add(sp3);
            cmd.Parameters.Add(sp4);
            cmd.Parameters.Add(sp5);
            cmd.Parameters.Add(sp6);
            cmd.Parameters.Add(sp7);
            cmd.Parameters.Add(sp8);
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

        public static void UpdateUser(string RealName, string Phone, string Mobile, string Email, string Gender, string Para1, string Para2, string Para3, string ID)
        {
            SqlConnection conn = new SqlConnection(DataHelper.conn);
            string updateSql = "UPDATE   Usertable SET  RealName=@RealName,Para1=@Para1,Para2=@Para2,Para3=@Para3,\r\n           Phone = @Phone, Mobile = @Mobile,Email=@Email ,Gender=@Gender    WHERE ID = @ID";
            SqlCommand cmd = new SqlCommand(updateSql, conn);
            SqlParameter sp3 = new SqlParameter("@RealName", SqlDbType.NVarChar) {
                Value = RealName
            };
            SqlParameter sp4 = new SqlParameter("@Phone", SqlDbType.NVarChar) {
                Value = Phone
            };
            SqlParameter sp5 = new SqlParameter("@Mobile", SqlDbType.NVarChar) {
                Value = Mobile
            };
            SqlParameter sp6 = new SqlParameter("@Email", SqlDbType.NVarChar) {
                Value = Email
            };
            SqlParameter sp7 = new SqlParameter("@Gender", SqlDbType.NVarChar) {
                Value = Gender
            };
            SqlParameter sp8 = new SqlParameter("@ID", SqlDbType.Int) {
                Value = int.Parse(ID)
            };
            SqlParameter sp9 = new SqlParameter("@Para1", SqlDbType.NVarChar) {
                Value = Para1
            };
            SqlParameter sp10 = new SqlParameter("@Para2", SqlDbType.NVarChar) {
                Value = Para2
            };
            SqlParameter sp11 = new SqlParameter("@Para3", SqlDbType.NVarChar) {
                Value = Para3
            };
            cmd.Parameters.Add(sp3);
            cmd.Parameters.Add(sp4);
            cmd.Parameters.Add(sp5);
            cmd.Parameters.Add(sp6);
            cmd.Parameters.Add(sp7);
            cmd.Parameters.Add(sp8);
            cmd.Parameters.Add(sp9);
            cmd.Parameters.Add(sp10);
            cmd.Parameters.Add(sp11);
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

        public static void UpdateUser(string UserName, string Password, string RoleID, string RealName, string Phone, string Mobile, string Email, string Gender, string Para3, string ID)
        {
            SqlConnection conn = new SqlConnection(DataHelper.conn);
            string updateSql = "UPDATE   Usertable SET  UserName = @UserName, Password = @Password,RoleID=@RoleID ,RealName=@RealName,Para3=@Para3,\r\n           Phone = @Phone, Mobile = @Mobile,Email=@Email ,Gender=@Gender    WHERE ID = @ID";
            SqlCommand cmd = new SqlCommand(updateSql, conn);
            SqlParameter sp = new SqlParameter("@UserName", SqlDbType.NVarChar) {
                Value = UserName
            };
            SqlParameter sp1 = new SqlParameter("@Password", SqlDbType.NVarChar) {
                Value = Password
            };
            SqlParameter sp2 = new SqlParameter("@RoleID", SqlDbType.Int) {
                Value = int.Parse(RoleID)
            };
            SqlParameter sp3 = new SqlParameter("@RealName", SqlDbType.NVarChar) {
                Value = RealName
            };
            SqlParameter sp4 = new SqlParameter("@Phone", SqlDbType.NVarChar) {
                Value = Phone
            };
            SqlParameter sp5 = new SqlParameter("@Mobile", SqlDbType.NVarChar) {
                Value = Mobile
            };
            SqlParameter sp6 = new SqlParameter("@Email", SqlDbType.NVarChar) {
                Value = Email
            };
            SqlParameter sp7 = new SqlParameter("@Gender", SqlDbType.NVarChar) {
                Value = Gender
            };
            SqlParameter sp8 = new SqlParameter("@ID", SqlDbType.Int) {
                Value = int.Parse(ID)
            };
            SqlParameter sp9 = new SqlParameter("@Para3", SqlDbType.NVarChar) {
                Value = Para3
            };
            cmd.Parameters.Add(sp);
            cmd.Parameters.Add(sp1);
            cmd.Parameters.Add(sp2);
            cmd.Parameters.Add(sp3);
            cmd.Parameters.Add(sp4);
            cmd.Parameters.Add(sp5);
            cmd.Parameters.Add(sp6);
            cmd.Parameters.Add(sp7);
            cmd.Parameters.Add(sp8);
            cmd.Parameters.Add(sp9);
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

        public static void UpdateUserPassword(string UserName, string Password)
        {
            SqlConnection conn = new SqlConnection(DataHelper.conn);
            string updateSql = "UPDATE   Usertable SET  Password = @Password  WHERE UserName = @UserName";
            SqlCommand cmd = new SqlCommand(updateSql, conn);
            SqlParameter sp = new SqlParameter("@UserName", SqlDbType.NVarChar) {
                Value = UserName
            };
            SqlParameter sp1 = new SqlParameter("@Password", SqlDbType.NVarChar) {
                Value = Password
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

