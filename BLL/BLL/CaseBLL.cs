namespace BLL
{
    using Common;
    using DAL;
    using System;
    using System.Data;

    public class CaseBLL
    {
        private CaseDAL caseDAL;
        private string tableName;

        public CaseBLL(int companyID)
        {
            this.caseDAL = new CaseDAL(companyID);
            this.tableName = Tools.GetCaseTableName(companyID.ToString());
        }

        public void DeleteCase(string IDs)
        {
            this.caseDAL.DeleteCase(IDs);
        }

        public DataSet GetCaseByID(string id)
        {
            return this.caseDAL.GetCaseList("ID=" + id);
        }

        public DataSet GetCaseList(string where)
        {
            return this.caseDAL.GetCaseList("ID,tbName,Phoned,Visited,PromisedDate,Repeated", where);
        }

        public DataSet GetCaseList(string where, string filterField, string order)
        {
            string orderStr = " Order by " + filterField + " " + order;
            if (order == "")
            {
                orderStr = "";
            }
            return this.caseDAL.GetCaseList("ID,tbName,Phoned,Visited,PromisedDate,Repeated", where, orderStr);
        }

        public DataSet GetCaseListByPatchID(string PatchID)
        {
            return this.caseDAL.GetCaseList("ID,tbName", "PatchID =" + PatchID);
        }

        public DataSet GetCasePagingitems(int PageCount, int currentPage, string where)
        {
            string Fields = "ID,OwnerID,tbName,tbKey,tbBalance";
            string order = "desc";
            return DataHelper.GetPagingDataSet1(this.tableName, Fields, PageCount, currentPage, where, order);
        }

        public int GetCaseTotalItems(string where)
        {
            return DataHelper.GetTableTotalItems(this.tableName, where);
        }

        public DataSet GetReportCaseList(string where)
        {
            return this.caseDAL.GetCaseList("*", where);
        }

        public static DataSet GetSearchCaseList(string sql)
        {
            return DataHelper.GetList(sql);
        }

        public void MarkRepeatedCase(int repeated, string IDs)
        {
            this.caseDAL.MarkRepeatedCase(repeated, IDs);
        }

        public void UpdateCaseOwer(int ownerID, string IDs)
        {
            this.caseDAL.UpdateCaseOwer(ownerID, IDs);
        }

        public void UpdateCasePhoned(int Phoned, int ID)
        {
            this.caseDAL.UpdateCasePhoned(Phoned, ID);
        }

        public void UpdateCasePromiseDate(DateTime PromisedDate, int ID)
        {
            this.caseDAL.UpdateCasePromiseDate(PromisedDate, ID);
        }

        public void UpdateCaseVisited(int Visited, int ID)
        {
            this.caseDAL.UpdateCaseVisited(Visited, ID);
        }
    }
}

