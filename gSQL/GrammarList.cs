using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gSQL
{
    class GrammarList : List<TuidaoShi>
    {
        public GrammarList GetAllTuidaoShi(string str)    //返回文法中所有以str为推导式左部的推导式
        {
            GrammarList NeedList = new GrammarList();
            foreach (var i in this)
            {
                if (i.Keys.Contains<string>(str) )//&& NeedList.Contains(i) == false)
                {
                    if (NeedList.Count == 0)
                    {
                        NeedList.Add(i);
                    }
                    else
                    {
                        for (int k = 0; k < NeedList.Count; k++)
                        {
                            TuidaoShi t = NeedList[k];
                                if (t.Equals(i) == false)
                                    NeedList.Add(i);
                        }
                    }
                   // NeedList.Add(i);
                }
                
                
            }
            return NeedList;
        }
        public GrammarList GetYoubuHad(string str)
        {
            GrammarList NeedList = new GrammarList();
            foreach (TuidaoShi i in this)
            {
                if (i.ElementAt(0).Value.Contains(str) == true && NeedList.Contains(i) == false)
                {
                    NeedList.Add(i);
                }
            }
            return NeedList;
        }
        public bool Contains(Dictionary<string, string[]> aTuidaoshi)
        {
            foreach (Dictionary<string, string[]> i in this)
            {
                if (i.ElementAt(0).Key.Equals(aTuidaoshi.ElementAt(0).Key) && i.ElementAt(0).Value.Length == aTuidaoshi.ElementAt(0).Value.Length)
                {
                    for (int j = 0; j < i.ElementAt(0).Value.Length; j++)
                    {
                        if (i.ElementAt(0).Value[j].Equals(aTuidaoshi.ElementAt(0).Value[j]) == false)
                            return false;
                    }
                    return true;
                }
            }
            return false;
        }
    }
}
