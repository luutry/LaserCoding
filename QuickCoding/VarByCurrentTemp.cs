using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickCoding
{
 
    public class VarByCurrentTemp
    {
        private ushort _rowNum;

        private ushort _VarNum;

        private string _VarName;

        private string _VarContent;

        private string _VarType;

        public string VarType
        {
            get { return _VarType; }
            set { _VarType = value; }
        }



        public string VarContent
        {
            get { return _VarContent; }
            set { _VarContent = value; }
        }

        public string VarName
        {
            get { return _VarName; }
            set { _VarName = value; }
        }

        public ushort VarNum
        {
            get { return _VarNum; }
            set { _VarNum = value; }
        }


        public ushort RowNum
        {
            get { return _rowNum; }
            set { _rowNum = value; }
        }
    }
}
