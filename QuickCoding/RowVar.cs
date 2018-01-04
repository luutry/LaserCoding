using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuickCoding
{
    class RowVar
    {
        public TextVar[] textVar = new TextVar[5]{new TextVar(),new TextVar(),new TextVar(),new TextVar(),new TextVar()};
        public DateVar[] dateVar = new DateVar[5]{new DateVar(),new DateVar(),new DateVar(),new DateVar(),new DateVar()};
        public SerialVar[] serialVar = new SerialVar[5] { new SerialVar(), new SerialVar(), new SerialVar(), new SerialVar(), new SerialVar() };
        public ushort[] varSpacing = new ushort[5];

        public ushort[] VarType = new ushort[5];

        //Font
        private ushort[] _font; 
        //Fontsize
        private ushort _fontSize;

        private ushort _rowSpacing;

        //internal List<Var> VarList
        //{
        //    get { return varList; }
        //    set { varList = value; }
        //}
        public ushort[] Font
        {
            get { return _font; }
            set { _font = value; }
        }
     
        public ushort FontSize
        {
            get { return _fontSize; }
            set { _fontSize = value; }
        }

        public ushort RowSpacing
        {
            get { return _rowSpacing; }
            set { _rowSpacing = value; }
        }               
    }
}

  //if (row[0] == 1)
  //          {
  //              foreach (TextVar tv in varbyCurrentTemp)
  //              {
  //                  if (tv.OrderNum == row[1])
  //                  {
  //                      currentTemplate.rowVar[index].VarType[0] = 1;
  //                      currentTemplate.rowVar[index].textVar[0].Name = tv.Name;
  //                      currentTemplate.rowVar[index].textVar[0].Content = tv.Content;
  //                      currentTemplate.rowVar[index].textVar[0].OrderNum = tv.OrderNum;
  //                  }
  //              }
  //          }
  //          if (row[0] == 2)
  //          {
  //              foreach (DateVar dv in dateVarList)
  //              {
  //                  if (dv.OrderNum == row[1])
  //                  {
  //                      currentTemplate.rowVar[index].VarType[0] = 2;
  //                      currentTemplate.rowVar[index].dateVar[0].Name = dv.Name;
  //                      currentTemplate.rowVar[index].dateVar[0].OrderNum = dv.OrderNum;
  //                      currentTemplate.rowVar[index].dateVar[0].DateFormat = dv.DateFormat;
  //                  }
  //              }
  //          }
  //          if (row[0] == 3)
  //          {
  //              foreach (SerialVar sv in serialVarList)
  //              {
  //                  if (sv.OrderNum == row[1])
  //                  {
  //                      currentTemplate.rowVar[index].VarType[0] = 3;
  //                      currentTemplate.rowVar[index].serialVar[0].OrderNum = sv.OrderNum;
  //                      currentTemplate.rowVar[index].serialVar[0].StartValue = sv.StartValue;
  //                  }
  //              }
  //          }
  //          currentTemplate.rowVar[index].varSpacing[0] = row[2];

  //          //变量二
  //          comboBox2.SelectedIndex = row[3];
  //          if (row[3] == 1)
  //          {
  //              foreach (TextVar tv in textVarList)
  //              {
  //                  if (tv.OrderNum == row[4])
  //                  {
  //                      currentTemplate.rowVar[index].VarType[1] = 1;
  //                      currentTemplate.rowVar[index].textVar[1].Name = tv.Name;
  //                      currentTemplate.rowVar[index].textVar[1].Content = tv.Content;
  //                      currentTemplate.rowVar[index].textVar[1].OrderNum = tv.OrderNum;
  //                  }
  //              }
  //          }
  //          if (row[3] == 2)
  //          {
  //              foreach (DateVar dv in dateVarList)
  //              {
  //                  if (dv.OrderNum == row[4])
  //                  {
  //                      currentTemplate.rowVar[index].VarType[1] = 2;
  //                      currentTemplate.rowVar[index].dateVar[1].Name = dv.Name;
  //                      currentTemplate.rowVar[index].dateVar[1].OrderNum = dv.OrderNum;
  //                      currentTemplate.rowVar[index].dateVar[1].DateFormat = dv.DateFormat;
  //                  }
  //              }
  //          }
  //          if (row[3] == 3)
  //          {
  //              foreach (SerialVar sv in serialVarList)
  //              {
  //                  if (sv.OrderNum == row[4])
  //                  {
  //                      currentTemplate.rowVar[index].VarType[1] = 3;
  //                      currentTemplate.rowVar[index].serialVar[1].OrderNum = sv.OrderNum;
  //                      currentTemplate.rowVar[index].serialVar[1].StartValue = sv.StartValue;
  //                  }
  //              }
  //          }
  //          currentTemplate.rowVar[index].varSpacing[1] = row[5];

  //          //变量三
  //          comboBox3.SelectedIndex = row[6];
  //          if (row[6] == 1)
  //          {
  //              foreach (TextVar tv in textVarList)
  //              {
  //                  if (tv.OrderNum == row[7])
  //                  {
  //                      currentTemplate.rowVar[index].VarType[2] = 1;
  //                      currentTemplate.rowVar[index].textVar[2].Name = tv.Name;
  //                      currentTemplate.rowVar[index].textVar[2].Content = tv.Content;
  //                      currentTemplate.rowVar[index].textVar[2].OrderNum = tv.OrderNum;
  //                  }
  //              }
  //          }
  //          if (row[6] == 2)
  //          {
  //              foreach (DateVar dv in dateVarList)
  //              {
  //                  if (dv.OrderNum == row[7])
  //                  {
  //                      currentTemplate.rowVar[index].VarType[2] = 2;
  //                      currentTemplate.rowVar[index].dateVar[2].Name = dv.Name;
  //                      currentTemplate.rowVar[index].dateVar[2].OrderNum = dv.OrderNum;
  //                      currentTemplate.rowVar[index].dateVar[2].DateFormat = dv.DateFormat;
  //                  }
  //              }
  //          }
  //          if (row[6] == 3)
  //          {
  //              foreach (SerialVar sv in serialVarList)
  //              {
  //                  if (sv.OrderNum == row[7])
  //                  {
  //                      currentTemplate.rowVar[index].VarType[2] = 3;
  //                      currentTemplate.rowVar[index].serialVar[2].OrderNum = sv.OrderNum;
  //                      currentTemplate.rowVar[index].serialVar[2].StartValue = sv.StartValue;
  //                  }
  //              }
  //          }

  //          currentTemplate.rowVar[index].varSpacing[2] = row[8];

  //          //变量四
  //          comboBox4.SelectedIndex = row[9];
  //          if (row[9] == 1)
  //          {
  //              foreach (TextVar tv in textVarList)
  //              {
  //                  if (tv.OrderNum == row[10])
  //                  {
  //                      currentTemplate.rowVar[index].VarType[3] = 1;
  //                      currentTemplate.rowVar[index].textVar[3].Name = tv.Name;
  //                      currentTemplate.rowVar[index].textVar[3].Content = tv.Content;
  //                      currentTemplate.rowVar[index].textVar[3].OrderNum = tv.OrderNum;
  //                  }
  //              }
  //          }
  //          if (row[9] == 2)
  //          {
  //              foreach (DateVar dv in dateVarList)
  //              {
  //                  if (dv.OrderNum == row[10])
  //                  {
  //                      currentTemplate.rowVar[index].VarType[3] = 2;
  //                      currentTemplate.rowVar[index].dateVar[3].Name = dv.Name;
  //                      currentTemplate.rowVar[index].dateVar[3].OrderNum = dv.OrderNum;
  //                      currentTemplate.rowVar[index].dateVar[3].DateFormat = dv.DateFormat;
  //                  }
  //              }
  //          }
  //          if (row[9] == 3)
  //          {
  //              foreach (SerialVar sv in serialVarList)
  //              {
  //                  if (sv.OrderNum == row[10])
  //                  {
  //                      currentTemplate.rowVar[index].VarType[3] = 3;
  //                      currentTemplate.rowVar[index].serialVar[3].OrderNum = sv.OrderNum;
  //                      currentTemplate.rowVar[index].serialVar[3].StartValue = sv.StartValue;
  //                  }
  //              }
  //          }

  //          currentTemplate.rowVar[index].varSpacing[3] = row[11];

  //          //变量五
  //          comboBox5.SelectedIndex = row[12];
  //          if (row[12] == 1)
  //          {
  //              foreach (TextVar tv in textVarList)
  //              {
  //                  if (tv.OrderNum == row[13])
  //                  {
  //                      currentTemplate.rowVar[index].VarType[4] = 1;
  //                      currentTemplate.rowVar[index].textVar[4].Name = tv.Name;
  //                      currentTemplate.rowVar[index].textVar[4].Content = tv.Content;
  //                      currentTemplate.rowVar[index].textVar[4].OrderNum = tv.OrderNum;
  //                  }
  //              }
  //          }
  //          if (row[12] == 2)
  //          {
  //              foreach (DateVar dv in dateVarList)
  //              {
  //                  if (dv.OrderNum == row[13])
  //                  {
  //                      currentTemplate.rowVar[index].VarType[4] = 2;
  //                      currentTemplate.rowVar[index].dateVar[4].Name = dv.Name;
  //                      currentTemplate.rowVar[index].dateVar[4].OrderNum = dv.OrderNum;
  //                      currentTemplate.rowVar[index].dateVar[4].DateFormat = dv.DateFormat;
  //                  }

  //              }
  //          }
  //          if (row[12] == 3)
  //          {
  //              foreach (SerialVar sv in serialVarList)
  //              {
  //                  if (sv.OrderNum == row[13])
  //                  {
  //                      currentTemplate.rowVar[index].VarType[4] = 3;
  //                      currentTemplate.rowVar[index].serialVar[4].OrderNum = sv.OrderNum;
  //                      currentTemplate.rowVar[index].serialVar[4].StartValue = sv.StartValue;
  //                  }

  //              }