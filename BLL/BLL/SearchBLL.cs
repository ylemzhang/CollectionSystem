namespace BLL
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Text;

    public class SearchBLL
    {
        public static string[] numArr = new string[] { "=", ">", ">=", "<", "<=", "<>" };
        private const string split2 = ";";
        public static string[] strArr = new string[] { "包含", "=", "开头以", "结尾以", "<>" };

        public List<SearchRow> GetEmptySearchRow(int rowNum)
        {
            List<SearchRow> list = new List<SearchRow>(rowNum);
            for (int i = 0; i < rowNum; i++)
            {
                SearchRow row = new SearchRow {
                    AndOr = "And",
                    Field = "",
                    SearchOperator = "",
                    SearchValue = ""
                };
                list.Add(row);
            }
            return list;
        }

        public DataSet GetSearchFields(string companyID, string tableType)
        {
            return CompanyBLL.GetCacheFields(companyID, tableType);
        }

        public string GetSearchSql(List<SearchRow> list)
        {
            StringBuilder sb = new StringBuilder("1=1");
            foreach (SearchRow row in list)
            {
                string field = row.Field;
                string andor = row.AndOr;
                string text = row.SearchValue;
                string opstr = row.SearchOperator;
                string fieldType = row.FieldType;
                string expression = " {0} {1} {2} {3} ";
                string strFieldType = fieldType;
                if (strFieldType == null)
                {
                    goto Label_015B;
                }
                if (!(strFieldType == "money") && !(strFieldType == "int"))
                {
                    if (strFieldType == "datetime")
                    {
                        goto Label_00E7;
                    }
                    goto Label_015B;
                }
                if (text.Trim().ToLower() == "null")
                {
                    if (opstr == "<>")
                    {
                        opstr = "";
                        text = " is not null";
                    }
                    else
                    {
                        opstr = "";
                        text = " is  null";
                    }
                }
                goto Label_0303;
            Label_00E7:
                if (text.Trim().ToLower() == "null")
                {
                    if (opstr == "<>")
                    {
                        opstr = "";
                        text = " is not null";
                    }
                    else
                    {
                        opstr = "";
                        text = " is  null";
                    }
                }
                else
                {
                    text = "'" + text + "'";
                }
                goto Label_0303;
            Label_015B:
                if (text.Trim().ToLower() == "null")
                {
                    if (opstr == "<>")
                    {
                        text = "''";
                    }
                    else
                    {
                        opstr = "=";
                        text = "''";
                    }
                }
                else
                {
                    text = text.Replace("'", "''");
                    switch (opstr)
                    {
                        case null:
                            goto Label_0303;

                        case "=":
                            opstr = "=";
                            text = "N'" + text + "'";
                            goto Label_0303;

                        case "<>":
                            opstr = "<>";
                            text = "N'" + text + "'";
                            goto Label_0303;

                        case "开头以":
                            opstr = "like";
                            text = this.handleSpecialSigns(text);
                            text = "N'" + text + "%'";
                            goto Label_0303;

                        case "结尾以":
                            opstr = "like";
                            text = this.handleSpecialSigns(text);
                            text = "N'%" + text + "'";
                            goto Label_0303;

                        case "包含":
                            opstr = "like";
                            text = this.handleSpecialSigns(text);
                            text = "N'%" + text + "%'";
                            goto Label_0303;
                    }
                }
            Label_0303:;
                expression = string.Format(expression, new object[] { andor, field, opstr, text });
                sb.AppendLine(expression);
            }
            return sb.ToString();
        }

        private string handleSpecialSigns(string text)
        {
            return text.Replace("%", "[%]");
        }
    }
}

