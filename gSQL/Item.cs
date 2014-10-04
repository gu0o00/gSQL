using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gSQL
{
    class Item
    {
        public Item(string lStr, string[] rStr)
        {
            this.lStr = lStr;
            this.rStr = rStr;
            this.point = 0;
        }
        public Item(string lStr, string[] rStr,int p)
        {
            this.lStr = lStr;
            this.rStr = rStr;
            this.point = p;
        }
        public bool Equals(Item t)
        {
            if (t.lStr.Equals(this.lStr) && this.point == t.point && this.rStr.Length == t.rStr.Length)
            {
                for (int i = 0; i < this.rStr.Length; i++)
                {
                    if (this.rStr[i].Equals(t.rStr[i]) == false)
                        return false;
                }
                return true;
            }
            return false;
        }
        public bool EqualsWithOutP(Item t)
        {
            if (t.lStr.Equals(this.lStr) && this.rStr.Length == t.rStr.Length)
            {
                for (int i = 0; i < this.rStr.Length; i++)
                {
                    if (this.rStr[i].Equals(t.rStr[i]) == false)
                        return false;
                }
                return true;
            }
            return false;
        }
        public void Show()
        {
            Console.Write(this.lStr + "-->");
            foreach (string a in this.rStr)
            {
                Console.Write(a + " ");
            }
            Console.WriteLine(this.point);
        }
        public void ShowOnlyGrammar()
        {
            Console.Write(this.lStr + "-->");
            foreach (string a in this.rStr)
            {
                Console.Write(a + " ");
            }
        }
        public String lStr;
        public String[] rStr; 
        public int point;
    }
}
