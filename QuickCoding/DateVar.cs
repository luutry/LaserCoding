using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuickCoding
{
   public class DateVar
    {
        private ushort _orderNum;

        private string _name;

        private string _separator;

        private string _dateFormat;

        private string _equalWidth;  // 1: 等宽 . 0 :不等宽

        public string Separator
        {
            get { return _separator; }
            set { _separator = value; }
        }

        public string EqualWidth
        {
            get { return _equalWidth; }
            set { _equalWidth = value; }
        }
        public string DateFormat
        {
            get { return _dateFormat; }
            set { _dateFormat = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public ushort OrderNum
        {
            get { return _orderNum; }
            set { _orderNum = value; }
        }

    }
}
