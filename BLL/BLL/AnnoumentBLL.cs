namespace BLL
{
    using DAL;
    using System;
    using System.Data;

    public class AnnoumentBLL
    {
        public static void DeleteAnnouncement(string IDs)
        {
            AnnouncementDAL.DeleteAnnouncement(IDs);
        }

        public static DataSet GetAnnouncementByID(string id)
        {
            return AnnouncementDAL.GetAnnouncementList("ID=" + id);
        }

        public static DataSet GetAnnouncementList(string where)
        {
            return AnnouncementDAL.GetAnnouncementList(where);
        }

        public static void UpdateAnnouncement(string ID, string title, string body, DateTime expiredate, string Attachment, string currentuser)
        {
            body = body.Replace("\r\n", "");
            if (ID == string.Empty)
            {
                AnnouncementDAL.InsertAnnouncement(title, body, expiredate, currentuser, Attachment);
            }
            else
            {
                AnnouncementDAL.UpdateAnnouncement(title, body, expiredate, currentuser, ID, Attachment);
            }
        }
    }
}

