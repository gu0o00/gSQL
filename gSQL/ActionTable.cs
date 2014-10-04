using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace gSQL
{
    class ActionTable : Dictionary<int,Dictionary<string,string[]>>
    {
        public void Add(int i, string zhongjiefu, string[] result)
        {
            if (this.ContainsKey(i) == false)
            {
                Dictionary<string, string[]> line = new Dictionary<string, string[]>();
                line.Add(zhongjiefu, result);
                this.Add(i, line);
            }
            else
            {
                if(this[i].Keys.Contains(zhongjiefu) == false)
                    this[i].Add(zhongjiefu, result);
            }
        }
        public string[] Get(int i, string zhongjiefu)
        {
            if (this.ContainsKey(i) == true && this[i].ContainsKey(zhongjiefu) == true)
            {
                string[] re = new string[]{};
                this[i].TryGetValue(zhongjiefu,out re);
                return re;
            }
            return null;
        }
        public void Show()
        {
            StreamWriter fs = new StreamWriter("action.txt");
            foreach (int i in this.Keys)
            {
                foreach (string zhongjiefu in this[i].Keys)
                {
                    //Console.Write(i.ToString() + "  " + zhongjiefu + "   ");
                    fs.Write(i.ToString() + "  " + zhongjiefu + "   ");
                    foreach (string t in this[i][zhongjiefu])
                    {
                        //Console.Write(t + " ");
                        fs.Write(t + " ");
                    }
                    //Console.WriteLine();
                    fs.WriteLine();
                }
            }
            fs.Close();
        }
    }
}
