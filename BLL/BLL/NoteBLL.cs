namespace BLL
{
    using DAL;
    using System;
    using System.Data;

    public class NoteBLL
    {
        public static void DeleteNote(string IDs)
        {
            NoteDAL.DeleteNote(IDs);
        }

        public static DataSet GetCompanyNoteListByCompanyID(string CompanyID)
        {
            string where = "CompanyID={0}";
            return NoteDAL.GetNoteList(string.Format(where, CompanyID));
        }

        public static DataSet GetNoteByID(string id)
        {
            return NoteDAL.GetNoteList("ID=" + id);
        }

        public static DataSet GetNoteList()
        {
            return NoteDAL.GetNoteList();
        }

        public static DataSet GetNoteList(string where)
        {
            return NoteDAL.GetNoteList(where);
        }

        public static DataSet GetNoteListByCaseID(string CompanyID, string CaseID)
        {
            string where = "CompanyID= {1} and CaseID={0}";
            return NoteDAL.GetNoteList(string.Format(where, CaseID, CompanyID));
        }

        public static DataSet GetNoteListByCaseType(string CompanyID, string CaseID, int type)
        {
            string where = "CompanyID= {1} and CaseID={0} and NoteType={2} ";
            return NoteDAL.GetNoteList(string.Format(where, CaseID, CompanyID, type));
        }

        public static DataSet GetNotePagingitems(int PageCount, int currentPage, string where)
        {
            string Fields = "*";
            string tablename = "NoteTable";
            string order = "desc";
            return DataHelper.GetPagingDataSet1(tablename, Fields, PageCount, currentPage, where, order);
        }

        public static int GetNoteTotalItems(string where)
        {
            return NoteDAL.GetNoteTotalItems(where);
        }

        public static string GetTime(string companyID, string caseID)
        {
            return DataHelper.ExecuteScalar(string.Format("select max(createon)  from notetable where companyid={0} and caseid={1}", companyID, caseID)).ToString();
        }

        public static int InsertNote(string Body, string CreateBy, int NoteType, int CaseID, decimal Num1, string Str1, string Str2, DateTime Date1, DateTime CreateOn, int CompanyID, string contactor, string contactorType, string contractResult)
        {
            return NoteDAL.InsertNote(Body, CreateBy, NoteType, CaseID, Num1, Str1, Str2, Date1, CreateOn, CompanyID, contactor, contactorType, contractResult);
        }

        public static void UpdateNote(string ID, string body)
        {
            NoteDAL.UpdateNote(ID, body);
        }

        public static void UpdateNote(string ID, string body, string tel, string phoneType, string contactor, string contactorType, string contractResult)
        {
            NoteDAL.UpdateNote(ID, body, tel, phoneType, contactor, contactorType, contractResult);
        }

        public static void UpdateVisitNote(string ID, string body, decimal num, string contactor, string contactorType, string contractResult)
        {
            NoteDAL.UpdateVisitNote(ID, body, num, contactor, contactorType, contractResult);
        }
    }
}

