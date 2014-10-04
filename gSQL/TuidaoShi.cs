using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gSQL
{
    class TuidaoShi : Dictionary<string, string[]>
    {
        public bool Equals(TuidaoShi i)
        {
            if (i.ElementAt(0).Key.Equals(this.ElementAt(0).Key) == true 
                && i.ElementAt(0).Value.Length == this.ElementAt(0).Value.Length)
            {
                for (int k = 0; k < this.ElementAt(0).Value.Length; k++)
                {
                    if (i.ElementAt(0).Value[k].Equals(this.ElementAt(0).Value[k]) == false)
                        return false;
                }
                return true;
            }
            return false;
        }
        override public string ToString()
        {
            string info = this.ElementAt(0).Key+"-->";
            for (int i = 0; i < this.ElementAt(0).Value.Length; i++)
            {
                info += this.ElementAt(0).Value[i]+"空";
            }
            return info;
        }
    }
}
