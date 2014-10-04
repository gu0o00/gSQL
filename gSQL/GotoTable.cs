using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gSQL
{
    class GotoTable : Dictionary<int, Dictionary<string, int>>
    {
        public void Add(int i, string A, int j)
        {
            if (this.ContainsKey(i) == false)
            {
                Dictionary<string, int> line = new Dictionary<string, int>();
                line.Add(A, j);
                this.Add(i, line);
            }
            else
            {
                if (this[i].Keys.Contains(A) == false)
                    this[i].Add(A, j);
            }
        }
        public int Get(int i, string Nzhongjiefu)
        {
            if (this.ContainsKey(i) == true && this[i].ContainsKey(Nzhongjiefu) == true)
            {
                int re ;
                this[i].TryGetValue(Nzhongjiefu, out re);
                return re;
            }
            return -1;
        }
        public void Show()
        {
            foreach (int i in this.Keys)
            {
                foreach (string Nzhongjiefu in this[i].Keys)
                {
                    Console.Write(i.ToString() + "  " +Nzhongjiefu + "   ");
                    Console.Write(this[i][Nzhongjiefu] + " ");
                    Console.WriteLine();
                }
            }
        }
    }
}
