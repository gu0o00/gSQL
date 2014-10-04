using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gSQL
{
    class Si
    {
        public Si(string opr, string op1, string op2)
        {
            this.opr = opr;
            this.op1 = op1;
            this.op2 = op2;
            this.result = "t" + num;
            num++;
            this.addr = num;
        }
        public Si(string opr, string op1, string op2,string res)
        {
            this.opr = opr;
            this.op1 = op1;
            this.op2 = op2;
            this.result = res;
            num++;
            this.addr = num;
        }
        override public string ToString()
        {
            return ""  + "(" + opr + "," + op1 + "," + op2 + "," + result + ")";
        }
        public int addr;
        public string opr;
        public string op1;
        public string op2;
        public string result;
        public static int num = 0;
    }
}
