namespace BLL
{
    using System;

    public class SearchRow
    {
        private string andOr;
        private string field;
        private string fieldType;
        private string searchOperator;
        private string searchValue;
        private const string split1 = ",";
        private const string split1Replace = "CCCSPLIT1";
        private const string split2 = ";";
        private const string split2Replace = "SSSSPLIT2";

        public SearchRow()
        {
            this.fieldType = "";
        }

        public SearchRow(string str)
        {
            this.fieldType = "";
            string[] arrs = str.Split(new char[] { ',' });
            this.andOr = arrs[0];
            this.field = arrs[1];
            this.searchOperator = arrs[2];
            this.searchValue = this.decodeSpecialSigns(arrs[3]);
            this.fieldType = arrs[4];
        }

        private string decodeSpecialSigns(string text)
        {
            return text.Replace("CCCSPLIT1", ",").Replace("SSSSPLIT2", ";");
        }

        private string encodeSpecialSigns(string text)
        {
            return text.Replace(",", "CCCSPLIT1").Replace(";", "SSSSPLIT2");
        }

        public override string ToString()
        {
            string valueAfterHandle = this.encodeSpecialSigns(this.SearchValue);
            return (this.AndOr + "," + this.Field + "," + this.SearchOperator + "," + valueAfterHandle + "," + this.FieldType);
        }

        public string AndOr
        {
            get
            {
                return this.andOr;
            }
            set
            {
                this.andOr = value;
            }
        }

        public string Field
        {
            get
            {
                return this.field;
            }
            set
            {
                this.field = value;
            }
        }

        public string FieldType
        {
            get
            {
                return this.fieldType;
            }
            set
            {
                this.fieldType = value;
            }
        }

        public string SearchOperator
        {
            get
            {
                return this.searchOperator;
            }
            set
            {
                this.searchOperator = value;
            }
        }

        public string SearchValue
        {
            get
            {
                return this.searchValue;
            }
            set
            {
                this.searchValue = value;
            }
        }
    }
}

