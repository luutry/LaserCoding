using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuickCoding
{
    class TemplateVar
    {
        public RowVar[] rowVar = new RowVar[5] { new RowVar(), new RowVar(), new RowVar(), new RowVar(), new RowVar() };
        private ushort _number;
        private string _name;
        private ushort _spacing;

        public RowVar[] RowVar
        {
            get { return rowVar; }
            set { rowVar = value; }
        }
        public string Name
        {
            get { return _name; }
            set { _name = value; }
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
