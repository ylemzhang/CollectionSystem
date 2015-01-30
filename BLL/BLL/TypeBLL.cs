namespace BLL
{
    using DAL;
    using System;
    using System.Data;

    public class TypeBLL
    {
        public void DeleteTypeData(string IDs)
        {
            new TypeDataDAL().DeleteTypeDataByIDs(IDs);
        }

        public DataSet GetTypeDataListByTypeID(string typeid)
        {
            return new TypeDataDAL().GetTypeDataListByTypeID(typeid);
        }

        public void InsertTypeData(string Description, string FTypeValue, string TypeID)
        {
            new TypeDataDAL().InsertTypeData(Description, FTypeValue, TypeID);
        }

        public void UpdateTypeData(string Description, string FTypeValue, string TypeID, string TypeDataID)
        {
            new TypeDataDAL().UpdateTypeData(Description, FTypeValue, TypeID, TypeDataID);
        }
    }
}

