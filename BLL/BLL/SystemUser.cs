namespace BLL
{
    using System;

    public class SystemUser
    {
        private string email;
        private string gender;
        private string id;
        private string mobile;
        private string para1;
        private string para2;
        private string para3 = "";
        private string password;
        private string phone;
        private string realname;
        private string roleid;
        private string username;

        public int AlertDays
        {
            get
            {
                if (this.para3 != string.Empty)
                {
                    return int.Parse(this.para3);
                }
                return 7;
            }
            set
            {
                this.para3 = value.ToString();
            }
        }

        public int CaseDisplayColumn
        {
            get
            {
                if (this.para2 != string.Empty)
                {
                    return int.Parse(this.para2);
                }
                return 4;
            }
            set
            {
                this.para2 = value.ToString();
            }
        }

        public string Email
        {
            get
            {
                return this.email;
            }
            set
            {
                this.email = value;
            }
        }

        public string Gender
        {
            get
            {
                return this.gender;
            }
            set
            {
                this.gender = value;
            }
        }

        public string ID
        {
            get
            {
                return this.id;
            }
            set
            {
                this.id = value;
            }
        }

        public string Mobile
        {
            get
            {
                return this.mobile;
            }
            set
            {
                this.mobile = value;
            }
        }

        public int PageCount
        {
            get
            {
                if (this.para1 != string.Empty)
                {
                    return int.Parse(this.para1);
                }
                return 20;
            }
            set
            {
                this.para1 = value.ToString();
            }
        }

        public string Para1
        {
            get
            {
                return this.para1;
            }
            set
            {
                this.para1 = value;
            }
        }

        public string Para2
        {
            get
            {
                if (this.para2 == string.Empty)
                {
                    return "0";
                }
                return this.para2;
            }
            set
            {
                this.para2 = value;
            }
        }

        public string Para3
        {
            get
            {
                return this.para3;
            }
            set
            {
                this.para3 = value;
            }
        }

        public string Password
        {
            get
            {
                return this.password;
            }
            set
            {
                this.password = value;
            }
        }

        public string Phone
        {
            get
            {
                return this.phone;
            }
            set
            {
                this.phone = value;
            }
        }

        public string RealName
        {
            get
            {
                return this.realname;
            }
            set
            {
                this.realname = value;
            }
        }

        public string RoleID
        {
            get
            {
                return this.roleid;
            }
            set
            {
                this.roleid = value;
            }
        }

        public string UserName
        {
            get
            {
                return this.username;
            }
            set
            {
                this.username = value;
            }
        }
    }
}

