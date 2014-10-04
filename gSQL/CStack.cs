using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gSQL
{
    class CStack : List<int>
    {
        public CStack()
        {

        }
        public void update(int n)
        {
            for (int i = 0; i < this.Count; i++)
            {
                this[i] = this[i] + n;
            }
        }
        public void Push(int n)
        {
            this.Add(n);
        }
        public int Pop()
        {
            int r = this[0];
            this.RemoveAt(0);
            return r;
        }

    }
}
