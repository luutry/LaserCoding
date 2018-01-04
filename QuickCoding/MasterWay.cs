using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows;
using Modbus;
using HDNing;

namespace QuickCoding
{
    public class MasterWay
    {
       public  ModbusManager CM = new ModbusManager();
    
        /// <summary>
        /// 查询当前模板下所有变量
        /// </summary>
        public VarByCurrentTemp varbycurrentTemp;
        public List<VarByCurrentTemp> varbycurrentTempList;
        public List<VarByCurrentTemp> findAllVarByCurrentTemp()
        {
            varbycurrentTempList = new List<VarByCurrentTemp>();
            for(int i = 0; i < 25; i++){
                varbycurrentTemp = new VarByCurrentTemp();
                ushort[] rownum = CM.ReadInputRegisters((ushort)(10000 + i * 100), 1);
                ushort a = rownum[0];
                ushort[] varnum = CM.ReadInputRegisters((ushort)(10001 + i * 100), 1);
                ushort[] name = CM.ReadInputRegisters((ushort)(10002 + i * 100), 19);
                ushort[] content = CM.ReadInputRegisters((ushort)(10021 + i * 100), 50);
                ushort[] type = CM.ReadInputRegisters((ushort)(10071 + i * 100), 10);

                varbycurrentTemp.RowNum = rownum[0];
                varbycurrentTemp.VarNum = varnum[0];
                varbycurrentTemp.VarName = name.ToProfaceString();
                varbycurrentTemp.VarContent = content.ToProfaceString();
                varbycurrentTemp.VarType = type.ToProfaceString();
                varbycurrentTempList.Add(varbycurrentTemp);
            }
            return varbycurrentTempList;
        }


       //读取已有的模板列表
       public ushort[] GetAvailablesTemp()
        {
            ushort[] read = CM.ReadInputRegisters(1000, 120);
            return read;
        }

        //由模板序号取得模板名称
        public string GetTemplateName(ushort Number)
        {
            //ushort indextoset = (ushort)int.Parse(lbAvailables.SelectedItem.ToString());
            ushort indextoset = Number;
            CM.WriteMultipleRegisters(2000, new ushort[] { indextoset });
            CM.WriteSingleCoil(13, true);
            ushort currentcount = CM.ExecutionCount;
            ushort result;

            if (SpinWait.SpinUntil(() => CM.ExecutionCount == currentcount + 1, 5000))
            {
                result = CM.TemplateStatus;
                //  MessageBox.Show(string.Format("次数 = {0}\n结果 = {1} ({2})", CM.ExecutionCount, result, (StatusCode)result), "查询");
                if (result == 1)
                {
                    string command = Class1.StringFromUShortArray(CM.ReadInputRegisters((ushort)(2000), 120));
                    //解析结果
                    Regex _IndexRegex = new Regex(@"^\d+(?=;)");
                    Regex _CommandRegex = new Regex(@"(?<=;).*?(?=;)");
                    Match indexMatch = _IndexRegex.Match(command);
                    MatchCollection commandMatches = _CommandRegex.Matches(command);

                    int index;
                    string name = null;

                    if (int.TryParse(indexMatch.Value, out index) == false)
                        MessageBox.Show("错误");
                    else if (commandMatches.Count != 1)
                        MessageBox.Show("错误");
                    else
                    {
                        name = commandMatches[0].Value;
                    }
                    return name;
                }
                return "";

            }
                return "";    
        }

       
        //查询所有二维码变量
        public List<QRcodeVar> qrCodeVarList ;
        public List<QRcodeVar> findAllQRcodeVar()
        {
            qrCodeVarList = new List<QRcodeVar>();
            ushort[] read = CM.ReadInputRegisters(41000, 120);
            for (int i = 0; i < read.Length && read[i] != 0; i++)
            {
                CM.WriteMultipleRegisters(42000, new ushort[] { read[i] });
                CM.WriteSingleCoil(40003, true);
                ushort currentcount = CM.ExecutionCount;
                if (SpinWait.SpinUntil(() => CM.ExecutionCount == currentcount + 1, 5000))
                {
                }
                else
                {
                    MessageBox.Show("TimeOut1");
                    return null;
                }
                string command = Class1
                    .StringFromUShortArray(CM.ReadInputRegisters((ushort)(42000), 120));
                //解析结果
                Regex _IndexRegex = new Regex(@"^\d+(?=;)");
                Regex _CommandRegex = new Regex(@"(?<=;).*?(?=;)");
                Match indexMatch = _IndexRegex.Match(command);
                MatchCollection commandMatches = _CommandRegex.Matches(command);
                QRcodeVar qrCodeVar = new QRcodeVar();
                qrCodeVar.OrderNum = Convert.ToUInt16(indexMatch.Value);
                qrCodeVar.Name = commandMatches[0].Value;
                qrCodeVar.Content = commandMatches[1].Value;
                qrCodeVar.Count = commandMatches[2].Value;
                qrCodeVar.Shape = commandMatches[3].Value;
                qrCodeVar.Code = commandMatches[4].Value;
                qrCodeVar.Width = commandMatches[5].Value;
                qrCodeVarList.Add(qrCodeVar);
            }
            return qrCodeVarList;
        }

        //得到当前行的参数（字体，字号，行间距）
        List<ushort[]> rowParams;
        public List<ushort[]> showRow(int index)
        {
            rowParams = new List<ushort[]>();
            ushort[] row= CM.ReadInputRegisters((ushort)(200 + (index - 1) * 50), 15);
            ushort[] fontfamily = CM.ReadInputRegisters((ushort)(215 + (index - 1) * 50), 30);
            ushort[] fontsize_rowspacing = CM.ReadInputRegisters((ushort)(245 + (index - 1) * 50), 2);
            //rowParams.Add(row);
            rowParams.Add(fontfamily);
            rowParams.Add(fontsize_rowspacing);
            return rowParams;
        }

 
    }
}
