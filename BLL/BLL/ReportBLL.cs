namespace BLL
{
    using DAL;
    using System;
    using System.Data;

    public class ReportBLL
    {
        private string CompanyID;

        public ReportBLL(string companyID)
        {
            this.CompanyID = companyID;
        }

        public static DataSet GetCollectonDetail(string companyID, string where)
        {
            string sql = "select  u.UserName as '业务员', c.tbName as '客户',  p.patchName as '批号', (case n.noteType when 1 then N'电催' else N'拜访' end) as '催收方式',n.body as '备注',Str1 as '联系电话',\r\n            n.contactorType as '联系对象',n.contractResult as '是否可联',n.contactor as '联系对象姓名',Num1 as '路费', createon as '日期' from  notetable n ,companycase_{0} c ,patch p, usertable u where n.caseID=c.id  and p.id=c.PatchID and c.ownerID=u.ID and notetype>0 and {1}";
            return DataHelper.GetList(string.Format(sql, companyID, where));
        }

        public static DataSet GetCollectonDetail(string companyID, string where, string fields)
        {
            string sql = "select {2} from\r\n notetable n ,companycase_{0} c ,patch p, usertable u\r\n where n.caseID=c.id \r\nand p.id=c.PatchID\r\nand c.ownerID=u.ID\r\nand notetype>0\r\nand {1}\r\n";
            return DataHelper.GetList(string.Format(sql, companyID, where, fields));
        }

        public static DataSet GetDataSet(string sql)
        {
            return DataHelper.GetList(sql);
        }

        public DataSet GetTotalCaseNumOfUsers(string PatchID)
        {
            string sql = "select ownerID,count(*),sum(tbBalance)  from companycase_{0} \r\n            where patchid={1} group by ownerID ";
            sql = string.Format(sql, this.CompanyID, PatchID);
            if ((PatchID == "-1") || (PatchID == ""))
            {
                sql = "select ownerID,count(*),sum(tbBalance)  from companycase_{0} \r\n            where patchid in (select ID  from patch where CompanyID={0} and expiredate>=getdate()) group by ownerID ";
                sql = string.Format(sql, this.CompanyID);
            }
            return DataHelper.GetList(sql);
        }

        public static DataSet GetTotalCollectonByUsers(string where)
        {
            string sql = " select createby,count(*), sum(num1) ,notetype from Notetable where {0} group by createby ,notetype ";
            return DataHelper.GetList(string.Format(sql, where));
        }

        public DataSet GetTotalPaymentAllUsers(string where)
        {
            string sql = "select OwnerID,tbExpireStatus,sum(tbPayment)  from \r\n(select c.id,c.tbExpireStatus,c.tbkey, c.OwnerID as OwnerID, p.tbPayment from companypayment_{0} p inner join  companycase_{0}  c on p.tbkey=c.tbkey where {1}) s\r\n group by OwnerID,tbkey,tbExpireStatus";
            return DataHelper.GetList(string.Format(sql, this.CompanyID, where));
        }
    }
}

