using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuickCoding
{
   public class QRcodeVar
    {
        private ushort _orderNum;

        private String _name;

        private String _content;

        private String _count;

        private String _shape;

        private String _code;

        private String _width;

        public String Width
        {
            get { return _width; }
            set { _width = value; }
        }

        public String Code
        {
            get { return _code; }
            set { _code = value; }
        }

        public String Shape
        {
            get { return _shape; }
            set { _shape = value; }
        }

        public String Count
        {
            get { return _count; }
            set { _count = value; }
        }

        public String Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public String Content
        {
            get { return _content; }
            set { _content = value; }
        }

        public ushort OrderNum
        {
            get { return _orderNum; }
            set { _orderNum = value; }
        }

    }
}
