using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuickCoding
{
    public class SerialVar
    {
        private ushort _orderNum;

        private String _name;

        private String _startValue;

        private String _endValue;

        private String _addValue;

        private String _repeatNum;

        private String _minBitNum;

        private String _spacing;

        private String _equalWidth;

        public String EqualWidth
        {
            get { return _equalWidth; }
            set { _equalWidth = value; }
        }

        public String Spacing
        {
            get { return _spacing; }
            set { _spacing = value; }
        }

        public String MinBitNum
        {
            get { return _minBitNum; }
            set { _minBitNum = value; }
        }

        public String RepeatNum
        {
            get { return _repeatNum; }
            set { _repeatNum = value; }
        }

        public String AddValue
        {
            get { return _addValue; }
            set { _addValue = value; }
        }

        public String EndValue
        {
            get { return _endValue; }
            set { _endValue = value; }
        }



        public String StartValue
        {
            get { return _startValue; }
            set { _startValue = value; }
        }

        public String Name
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
