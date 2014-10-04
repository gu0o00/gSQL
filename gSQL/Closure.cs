using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gSQL
{
    class Closure : List<Item>
    {
        public Closure()
        {
            go = new Dictionary<string, int>();
        }
        new public bool Contains(Item t)
        {
            foreach (Item a in this)
            {
                if (a.Equals(t) == true)
                    return true;
            }
            return false;
        }
        public bool Equals(Closure t)
        {
            if (t.Count != this.Count)
                return false;
            for (int i = 0; i < this.Count; i++)
            {
                if (this[i].Equals(t[i]) == false)
                    return false;
            }
            return true;
        }
        public void Show()
        {
            foreach (Item aItem in this)
            {
                aItem.Show();
            }
        }
        public Dictionary<string, int> go;
    }
}
