using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuickCoding
{
    public class TextVar
    {
        private ushort _orderNum;

        private string _name;

        private string _content;

        private string _space;

        private string _equalWidth;  // 1: 等宽 . 0 :不等宽

        private ushort _byTempNum;

        private ushort _byTempRow;

        private ushort _byTempUnit;

        public ushort ByTempUnit
        {
            get { return _byTempUnit; }
            set { _byTempUnit = value; }
        }

        public ushort ByTempRow
        {
            get { return _byTempRow; }
            set { _byTempRow = value; }
        }

        public ushort ByTempNum
        {
            get { return _byTempNum; }
            set { _byTempNum = value; }
        }

        public string Space
        {
            get { return _space; }
            set { _space = value; }
        }

        public string EqualWidth
        {
            get { return _equalWidth; }
            set { _equalWidth = value; }
        }
        public string Content
        {
            get { return _content; }
            set { _content = value; }
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
