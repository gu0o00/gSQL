using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gSQL
{
    class ItemList : List<Closure>
    {
        new public bool Contains(Closure t)
        {
            
            foreach (Closure i in this)
            {
                if (t.Equals(i) == true)
                    return true;
            }
            return false;
        }
    }
}
