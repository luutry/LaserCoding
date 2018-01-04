using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows;
using HDNing;

namespace QuickCoding
{
    public partial class keyboard : Form
    {
        bool Shift = false;
        string content = null;
        string currentPage;

        public keyboard(String pageSympol)
        {
            currentPage = pageSympol;
            InitializeComponent();
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
        }
   
        private void btnLeftShift_Click(object sender, EventArgs e)
        {
            if (Shift)
            {
                Shift = false;
                btnLeftShift.BackColor = System.Drawing.Color.Gainsboro;
                btnRightShift.BackColor = System.Drawing.Color.Gainsboro;
                btnBackQuote.Text = "`";
                btnOne.Text = "1";
                btnTwo.Text = "2";
                btnThree.Text = "3";
                btnFour.Text = "4";
                btnFive.Text = "5";
                btnSix.Text = "6";
                btnSeven.Text = "7";
                btnEight.Text = "8";
                btnNine.Text = "9";
                btnZero.Text = "0";
                btnMinus.Text = "-";
                btnEqual.Text = "=";
                btnLeftBracket.Text = "[";
                btnRightBracket.Text = "]";
                btnRightSlash.Text = "\\";
                btnSemicolon.Text = ";";
                btnQuote.Text = "'";
                btnComma.Text = ",";
                btnStop.Text = ".";
                btnLeftSlash.Text = "/";
                btnA.Text = "a";
                btnB.Text = "b";
                btnC.Text = "c";
                btnD.Text = "d";
                btnE.Text = "e";
                btnF.Text = "f";
                btnG.Text = "g";
                btnH.Text = "h";
                btnI.Text = "i";
                btnJ.Text = "j";
                btnK.Text = "k";
                btnL.Text = "l";
                btnM.Text = "m";
                btnN.Text = "n";
                btnO.Text = "o";
                btnP.Text = "p";
                btnQ.Text = "q";
                btnR.Text = "r";
                btnS.Text = "s";
                btnT.Text  = "t";
                btnV.Text = "v";
                btnU.Text = "u";
                btnW.Text = "w";
                btnX.Text = "x";
                btnY.Text = "y";
                btnZ.Text = "z";
            }
            else
            {
                Shift = true;
                btnLeftShift.BackColor = System.Drawing.Color.LightBlue;
                btnRightShift.BackColor = System.Drawing.Color.LightBlue;
                btnBackQuote.Text = "~";
                btnOne.Text = "!";
                btnTwo.Text = "@";
                btnThree.Text = "#";
                btnFour.Text = "$";
                btnFive.Text = "%";
                btnSix.Text = "^";
                btnSeven.Text = "&&";
                btnEight.Text = "*";
                btnNine.Text = "(";
                btnZero.Text = ")";
                btnMinus.Text = "_";
                btnEqual.Text = "+";
                btnLeftBracket.Text = "{";
                btnRightBracket.Text = "}";
                btnRightSlash.Text = "|";
                btnSemicolon.Text = ":";
                btnQuote.Text = "\"";
                btnComma.Text = "<";
                btnStop.Text = ">";
                btnLeftSlash.Text = "?";
                btnA.Text = "A";
                btnB.Text = "B";
                btnC.Text = "C";
                btnD.Text = "D";
                btnE.Text = "E";
                btnF.Text = "F";
                btnG.Text = "G";
                btnH.Text = "H";
                btnI.Text = "I";
                btnJ.Text = "J";
                btnK.Text = "K";
                btnL.Text = "L";
                btnM.Text = "M";
                btnN.Text = "N";
                btnO.Text = "O";
                btnP.Text = "P";
                btnQ.Text = "Q";
                btnR.Text = "R";
                btnS.Text = "S";
                btnT.Text = "T";
                btnV.Text = "V";
                btnU.Text = "U";
                btnW.Text = "W";
                btnX.Text = "X";
                btnY.Text = "Y";
                btnZ.Text = "Z";
            }          
        }

        private void getBackQuoteValue_Click(object sender, EventArgs e)
        {
            if (Shift)
            {
                content += "~";
                tbxInputString.Text = content;
            }
            else
            {
                content += "`";
                tbxInputString.Text = content;
            }
        }

        private void getOneValue_Click(object sender, EventArgs e)
        {
            if (Shift)
            {
                content += "!";
                tbxInputString.Text = content;
            }
            else
            {
                content += "1";
                tbxInputString.Text = content;
            }
        }

        private void getTwoValue_Click(object sender, EventArgs e)
        {
            if (Shift)
            {
                content += "@";
                tbxInputString.Text = content;
            }
            else
            {
                content += "2";
                tbxInputString.Text = content;
            }
        }

        private void getThreeValue_Click(object sender, EventArgs e)
        {
            if (Shift)
            {
                content += "#";
                tbxInputString.Text = content;
            }
            else
            {
                content += "3";
                tbxInputString.Text = content;
            }
        }

        private void get4Value_Click(object sender, EventArgs e)
        {
            if (Shift)
            {
                content += "$";
                tbxInputString.Text = content;
            }
            else
            {
                content += "4";
                tbxInputString.Text = content;
            }
        }

        private void get5Value_Click(object sender, EventArgs e)
        {
            if (Shift)
            {
                content += "%";
                tbxInputString.Text = content;
            }
            else
            {
                content += "5";
                tbxInputString.Text = content;
            }
        }

        private void get6Value_Click(object sender, EventArgs e)
        {
            if (Shift)
            {
                content += "^";
                tbxInputString.Text = content;
            }
            else
            {
                content += "6";
                tbxInputString.Text = content;
            }
        }

        private void get7Value_Click(object sender, EventArgs e)
        {
            if (Shift)
            {
                content += "&";
                tbxInputString.Text = content;
            }
            else
            {
                content += "7";
                tbxInputString.Text = content;
            }
        }

        private void get8Value_Click(object sender, EventArgs e)
        {
            if (Shift)
            {
                content += "*";
                tbxInputString.Text = content;
            }
            else
            {
                content += "8";
                tbxInputString.Text = content;
            }
        }

        private void get9Value_Click(object sender, EventArgs e)
        {
            if (Shift)
            {
                content += "(";
                tbxInputString.Text = content;
            }
            else
            {
                content += "9";
                tbxInputString.Text = content;
            }
        }

        private void get0Value_Click(object sender, EventArgs e)
        {
            if (Shift)
            {
                content += ")";
                tbxInputString.Text = content;
            }
            else
            {
                content += "0";
                tbxInputString.Text = content;
            }
        }

        private void getMinusValue_Click(object sender, EventArgs e)
        {
            if (Shift)
            {
                content += "_";
                tbxInputString.Text = content;
            }
            else
            {
                content += "-";
                tbxInputString.Text = content;
            }
        }

        private void getEqualValue_Click(object sender, EventArgs e)
        {
            if (Shift)
            {
                content += "+";
                tbxInputString.Text = content;
            }
            else
            {
                content += "=";
                tbxInputString.Text = content;
            }
        }

        private void DeductValue_Click(object sender, EventArgs e)
        {
            if (content != null && content != "")
            {
                content = content.Substring(0, content.Length - 1);
                tbxInputString.Text = content;
            }     
            else
                content = null;         
        }

        private void getQ_Click(object sender, EventArgs e)
        {
            if (Shift)
            {
                content += "Q";
                tbxInputString.Text = content;
            }
            else
            {
                content += "q";
                tbxInputString.Text = content;
            }
        }

        private void getW_Click(object sender, EventArgs e)
        {
            if (Shift)
            {
                content += "W";
                tbxInputString.Text = content;
            }
            else
            {
                content += "w";
                tbxInputString.Text = content;
            }
        }

        private void getE_Click(object sender, EventArgs e)
        {
            if (Shift)
            {
                content += "E";
                tbxInputString.Text = content;
            }
            else
            {
                content += "e";
                tbxInputString.Text = content;
            }
        }

        private void getR_Click(object sender, EventArgs e)
        {
            if (Shift)
            {
                content += "R";
                tbxInputString.Text = content;
            }
            else
            {
                content += "r";
                tbxInputString.Text = content;
            }
        }

        private void getT_Click(object sender, EventArgs e)
        {
            if (Shift)
            {
                content += "T";
                tbxInputString.Text = content;
            }
            else
            {
                content += "t";
                tbxInputString.Text = content;
            }
        }

        private void getY_Click(object sender, EventArgs e)
        {
            if (Shift)
            {
                content += "Y";
                tbxInputString.Text = content;
            }
            else
            {
                content += "y";
                tbxInputString.Text = content;
            }
        }

        private void getU_Click(object sender, EventArgs e)
        {
            if (Shift)
            {
                content += "U";
                tbxInputString.Text = content;
            }
            else
            {
                content += "u";
                tbxInputString.Text = content;
            }
        }

        private void getI_Click(object sender, EventArgs e)
        {
            if (Shift)
            {
                content += "I";
                tbxInputString.Text = content;
            }
            else
            {
                content += "i";
                tbxInputString.Text = content;
            }
        }

        private void getO_Click(object sender, EventArgs e)
        {
            if (Shift)
            {
                content += "O";
                tbxInputString.Text = content;
            }
            else
            {
                content += "o";
                tbxInputString.Text = content;
            }
        }

        private void getP_Click(object sender, EventArgs e)
        {
            if (Shift)
            {
                content += "P";
                tbxInputString.Text = content;
            }
            else
            {
                content += "p";
                tbxInputString.Text = content;
            }
        }

        private void getLeftBracket_Click(object sender, EventArgs e)
        {
            if (Shift)
            {
                content += "{";
                tbxInputString.Text = content;
            }
            else
            {
                content += "[";
                tbxInputString.Text = content;
            }
        }

        private void getRightBracket_Click(object sender, EventArgs e)
        {
            if (Shift)
            {
                content += "}";
                tbxInputString.Text = content;
            }
            else
            {
                content += "]";
                tbxInputString.Text = content;
            }
        }

        private void getRightSlash_Click(object sender, EventArgs e)
        {
            if (Shift)
            {
                content += "|";
                tbxInputString.Text = content;
            }
            else
            {
                content += "\\";
                tbxInputString.Text = content;
            }
        }

        private void getA_Click(object sender, EventArgs e)
        {
            if (Shift)
            {
                content += "A";
                tbxInputString.Text = content;
            }
            else
            {
                content += "a";
                tbxInputString.Text = content;
            }
        }

        private void getS_Click(object sender, EventArgs e)
        {
            if (Shift)
            {
                content += "S";
                tbxInputString.Text = content;
            }
            else
            {
                content += "s";
                tbxInputString.Text = content;
            }
        }

        private void getD_Click(object sender, EventArgs e)
        {
            if (Shift)
            {
                content += "D";
                tbxInputString.Text = content;
            }
            else
            {
                content += "d";
                tbxInputString.Text = content;
            }
        }

        private void getF_Click(object sender, EventArgs e)
        {
            if (Shift)
            {
                content += "F";
                tbxInputString.Text = content;
            }
            else
            {
                content += "f";
                tbxInputString.Text = content;
            }
        }

        private void getG_Click(object sender, EventArgs e)
        {
            if (Shift)
            {
                content += "G";
                tbxInputString.Text = content;
            }
            else
            {
                content += "g";
                tbxInputString.Text = content;
            }
        }

        private void getH_Click(object sender, EventArgs e)
        {
            if (Shift)
            {
                content += "H";
                tbxInputString.Text = content;
            }
            else
            {
                content += "h";
                tbxInputString.Text = content;
            }
        }

        private void getJ_Click(object sender, EventArgs e)
        {
            if (Shift)
            {
                content += "J";
                tbxInputString.Text = content;
            }
            else
            {
                content += "j";
                tbxInputString.Text = content;
            }
        }

        private void getK_Click(object sender, EventArgs e)
        {
            if (Shift)
            {
                content += "K";
                tbxInputString.Text = content;
            }
            else
            {
                content += "k";
                tbxInputString.Text = content;
            }
        }

        private void getL_Click(object sender, EventArgs e)
        {
            if (Shift)
            {
                content += "L";
                tbxInputString.Text = content;
            }
            else
            {
                content += "l";
                tbxInputString.Text = content;
            }
        }

        private void getSemicolon_Click(object sender, EventArgs e)
        {
            if (Shift)
            {
                content += ":";
                tbxInputString.Text = content;
            }
            else
            {
                content += ";";
                tbxInputString.Text = content;
            }
        }

        private void getQuoo_Click(object sender, EventArgs e)
        {
            if (Shift)
            {
                content += "\"";
                tbxInputString.Text = content;
            }
            else
            {
                content += "'";
                tbxInputString.Text = content;
            }
        }

        private void getZ_Click(object sender, EventArgs e)
        {
            if (Shift)
            {
                content += "Z";
                tbxInputString.Text = content;
            }
            else
            {
                content += "z";
                tbxInputString.Text = content;
            }
        }

        private void getX_Click(object sender, EventArgs e)
        {
            if (Shift)
            {
                content += "X";
                tbxInputString.Text = content;
            }
            else
            {
                content += "x";
                tbxInputString.Text = content;
            }
        }

        private void getC_Click(object sender, EventArgs e)
        {
            if (Shift)
            {
                content += "C";
                tbxInputString.Text = content;
            }
            else
            {
                content += "c";
                tbxInputString.Text = content;
            }
        }

        private void getV_Click(object sender, EventArgs e)
        {
            if (Shift)
            {
                content += "V";
                tbxInputString.Text = content;
            }
            else
            {
                content += "v";
                tbxInputString.Text = content;
            }
        }

        private void getB_Click(object sender, EventArgs e)
        {
            if (Shift)
            {
                content += "B";
                tbxInputString.Text = content;
            }
            else
            {
                content += "b";
                tbxInputString.Text = content;
            }
        }

        private void getN_Click(object sender, EventArgs e)
        {
            if (Shift)
            {
                content += "N";
                tbxInputString.Text = content;
            }
            else
            {
                content += "n";
                tbxInputString.Text = content;
            }
        }

        private void getM_Click(object sender, EventArgs e)
        {
            if (Shift)
            {
                content += "M";
                tbxInputString.Text = content;
            }
            else
            {
                content += "m";
                tbxInputString.Text = content;
            }
        }

        private void getComma_Click(object sender, EventArgs e)
        {
            if (Shift)
            {
                content += "<";
                tbxInputString.Text = content;
            }
            else
            {
                content += ",";
                tbxInputString.Text = content;
            }
        }

        private void getstop_Click(object sender, EventArgs e)
        {
            if (Shift)
            {
                content += ">";
                tbxInputString.Text = content;
            }
            else
            {
                content += ".";
                tbxInputString.Text = content;
            }
        }

        private void getLeftSlash_Click(object sender, EventArgs e)
        {
            if (Shift)
            {
                content += "?";
                tbxInputString.Text = content;
            }
            else
            {
                content += "/";
                tbxInputString.Text = content;
            }
        }

        private void getSpace_Click(object sender, EventArgs e)
        {
            content += " ";
            tbxInputString.Text = content;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (currentPage == "text")
            {
                InputText it = (InputText)this.Owner;
                it.inputResultTextByKey = tbxInputString.Text;
            }
            if (currentPage == "home")
            {
                MainForm mf = (MainForm)this.Owner;
                mf.inputResult = tbxInputString.Text;
            }
            if (currentPage == "date")
            {
                InputDate id = (InputDate)this.Owner;
                id.inputResult = tbxInputString.Text;
            }
            if (currentPage == "serial")
            {
                InputSerial isl = (InputSerial)this.Owner;
                isl.inputResult = tbxInputString.Text;
            }
            if (currentPage == "newTemp")
            {
                NewTemplate nt = (NewTemplate)this.Owner;
                nt.inputResult = tbxInputString.Text;
            }
         
            Close();
        }
       

        private void btnError_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            tbxInputString.Clear();
        }

      

    }
}
