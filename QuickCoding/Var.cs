using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuickCoding
{
    // 变量元素
    class Var
    {
        private ushort _number;
        private ushort _type;
        private ushort _spacing;

        public ushort Type
        {
            get { return _type; }
            set { _type = value; }
        }

        public ushort Spacing
        {
            get { return _spacing; }
            set { _spacing = value; }
        }

        public ushort Number
        {
            get { return _number; }
            set { _number = value; }
        }
    }
}
