using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace gSQL
{
	class SLR
	{
		public SLR(String garFile)
		{
			ReadGrammar(garFile);
		}
		public void ReadGrammar(string file )
		{
			StreamReader fs = null;
			try
			{
				fs = new StreamReader(file);
			}catch(Exception ){
				Console.WriteLine("打开文件出现问题！");
				return;
			}
			while (fs.EndOfStream != true)
			{
				String aline = fs.ReadLine();
				String[] part = aline.Split("->".ToCharArray());
				String lBufen = part[0];
				String[] rBufen = part[2].Split(' ');
				TuidaoShi newTuidaoshi = new TuidaoShi();
				newTuidaoshi.Add(lBufen, rBufen);
				garmmarList.Add(newTuidaoshi);
				//add into allFuhao
				if (allFuhao.Contains(lBufen) == false)
					allFuhao.Add(lBufen);
				foreach (string a in rBufen)
					if (allFuhao.Contains(a) == false)
						allFuhao.Add(a);
			}
		}

		public void GetZhongjiefu()
		{
			foreach (string fuhao in allFuhao)
			{
				foreach (Dictionary<string, string[]> i in garmmarList)
				{
					if (i.Keys.Contains(fuhao) == true && Nzhongjiefu.Contains(fuhao) == false)
						Nzhongjiefu.Add(fuhao);
				}
			}
			foreach (string fuhao in allFuhao)
			{
				if (Nzhongjiefu.Contains(fuhao) == false && zhongjiefu.Contains(fuhao) == false)
				{
					zhongjiefu.Add(fuhao);
				}
			}
		}
		public List<string> GetFirst(string x)
		{
			List<string> first = new List<string>();
			if (zhongjiefu.Contains(x) == true && first.Contains(x) == false)    //如果x是终结符，直接将x放入first
			{
				first.Add(x);
				return first;
			}
			GrammarList allGarmmar = garmmarList.GetAllTuidaoShi(x);
			foreach (Dictionary<string, string[]> i in allGarmmar)
			{
				if (i.ElementAt(0).Value[0].Equals(x))
					continue;
				List<string> res = GetFirst(i.ElementAt(0).Value[0]);
				foreach (string t in res)
				{
					if (first.Contains(t) == false)
						first.Add(t);
				}

			}
			return first;
		}
		public void GetAllFirst()
		{
			List<string> first;
			foreach (string x in Nzhongjiefu)
			{
				first = new List<string>();
				first = GetFirst(x);
				firstList.Add(x, first);
			}
		}
		public List<string> GetFollow(string x)
		{

			if (zhongjiefu.Contains(x) == true)     //如果x是终结符
			{
				return follow;
			}
			GrammarList allGarmmar = garmmarList.GetYoubuHad(x);
			foreach (TuidaoShi i in allGarmmar)
			{
				int index = GetIndexFromArry(i.ElementAt(0).Value, x);
				if (index >= i.ElementAt(0).Value.Length - 1
					&& i.ElementAt(0).Key.Equals(i.ElementAt(0).Value[i.ElementAt(0).Value.Length - 1]) == false
					&& jilu.Contains(i.ElementAt(0).Key) == false)   //x的后面没有东西了
				{
					if (jilu.Contains(i.ElementAt(0).Key))
						continue;
					jilu.Add(i.ElementAt(0).Key);
					List<string> res = GetFollow(i.ElementAt(0).Key);
					foreach (string t in res)
					{
						if (follow.Contains(t) == false)
						{
							follow.Add(t);
						}
					}
				}
				else if (index < i.ElementAt(0).Value.Length - 1)   //x的后面还有符号
				{
					string nextFuhao = i.ElementAt(0).Value[index + 1];
					List<string> res = new List<string>();
					if (Nzhongjiefu.Contains(nextFuhao) == true)
						firstList.TryGetValue(nextFuhao, out res);
					else
						res.Add(nextFuhao);
					if (res != null)
					{
						foreach (string t in res)
						{
							if (follow.Contains(t) == false)
								follow.Add(t);
						}
					}
				}
			}
			return follow;
		}
		public List<string> GetAllFollow()
		{
			List<string> allfollow = new List<string>();
			foreach (string Nzjf in Nzhongjiefu)
			{
				follow = new List<string>();
				follow.Add("$");
				jilu = new List<string>();
				List<string> res = GetFollow(Nzjf);
				followList.Add(Nzjf, res);
				string X = Nzjf+": ";
				foreach (var i in res)
					X = X + " " + i;
				allfollow.Add(X);
			}
			return allfollow;
		}
		public void Closure(Item start = null)
		{
			Item aItem;
			if (start == null)
				aItem = new Item(garmmarList[0].ElementAt(0).Key, garmmarList[0].ElementAt(0).Value);
			else
				aItem = start;

			ClosureList.Add(aItem);
			for(int i =0;i<ClosureList.Count;i++)
			{
				Item a = ClosureList[i];
				int len = a.rStr.Length;
				String aFuhao = a.rStr[0];
				GrammarList allTuidaoShi = garmmarList.GetAllTuidaoShi(aFuhao);
				if (allTuidaoShi.Count == 0)
					continue;
				foreach (TuidaoShi aTuidaoShi in allTuidaoShi)
				{
					Item tmpItem = new Item(aTuidaoShi.ElementAt(0).Key, aTuidaoShi.ElementAt(0).Value);
					if (ClosureList.Contains(tmpItem) == false)
					{
						ClosureList.Add(tmpItem);
					}
				}
			}
		}
		public Closure Goto(Closure I, String X)//闭包就是项目的表
		{
			Closure list = new Closure();
			foreach (Item a in I)
			{
				if (a.point >= a.rStr.Length)   //如果点已经移到最后，则处理闭包中的下一个项目
					continue;
				if (a.rStr[a.point] == X)       //如果X刚好在点后，则开始求闭包
				{
					list.Add(new Item(a.lStr, a.rStr, a.point + 1));

					if ((a.point + 1) >= a.rStr.Length)
						continue;
					GrammarList allTuidaoShi = garmmarList.GetAllTuidaoShi(a.rStr[a.point + 1]);
					if (allTuidaoShi.Count == 0)
						continue;
					foreach (TuidaoShi aTuidaoShi in allTuidaoShi)      //把符合的推导式变成项目放入list
					{
						Item aItem = new Item(aTuidaoShi.ElementAt(0).Key, aTuidaoShi.ElementAt(0).Value, 0);
						if (list.Contains(aItem) == false)
							list.Add(aItem);
					}

					for (int i = 1; i < list.Count; i++)
					{
						Item aItem = list[i];
						int len = aItem.rStr.Length;
						String aFuhao = aItem.rStr[0];
						GrammarList all_TuidaoShi = garmmarList.GetAllTuidaoShi(aFuhao);
						if (all_TuidaoShi.Count == 0)
							continue;
						foreach (TuidaoShi aTuidaoShi in all_TuidaoShi)
						{
							//TuidaoShi to Item
							Item tmpItem = new Item(aTuidaoShi.ElementAt(0).Key, aTuidaoShi.ElementAt(0).Value);
							if (list.Contains(tmpItem) == false)
							{
								list.Add(tmpItem);
							}
						}
					}
				}
			}
			return list;
		}
		public void GetItems()
		{
			Items.Add(ClosureList);     //I0 Add into Items
			for (int i = 0; i < Items.Count; i++)
			{
				Closure aItemList = Items[i];
				foreach (string X in allFuhao)
				{
					Closure newItemList = Goto(aItemList, X);
					if (newItemList.Count != 0 && Items.Contains(newItemList) == false)
					{
						Items.Add(newItemList);
						Items[i].go.Add(X, Items.Count - 1);
					}
					else
					{
						for (int j = 0; j < Items.Count; j++)
						{
							if (Items[j].Equals(newItemList) == true)
							{
								Items[i].go.Add(X, j);
							}
						}
					}
				}
			}   
		}
		public int GetIndexFromArry(string[] array, string x)
		{
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i].Equals(x) == true)
					return i;
			}
			return -1;
		}
		public void MakeActionTable()
		{
			int i,j;
			for(i=0 ;i<Items.Count;i++)
			{
				Closure aItemList = Items[i];       //aItemList是一个项目集
				Console.WriteLine(i.ToString()+": ");
				for(j =0;j<aItemList.Count;j++)
				{
					Item aItem = aItemList[j];      //aItem是项目集中的一个项目
					if (aItem.point < aItem.rStr.Length)        //点还没有移动到最后
					{
						string aFuhao = aItem.rStr[aItem.point];
						if (zhongjiefu.Contains(aFuhao) == false)   //如果aFuhao不是终结符，这里可以优化
							continue;
						int tar;
						aItemList.go.TryGetValue(aFuhao, out tar);

						actionTable.Add(i, aFuhao, new string[] { "s", tar.ToString() });
					}
					else if (aItem.point >= aItem.rStr.Length)  //点在最后了
					{
						if(aItem.Equals(new Item(garmmarList[0].ElementAt(0).Key,
							garmmarList[0].ElementAt(0).Value,
							garmmarList[0].ElementAt(0).Value.Length)))
						{
							actionTable.Add(i,"$",new string[]{"acc"});
						}
						else{       //上面的是实现规则c，下面是实现规则b             
							string preFuhao = aItem.lStr;
							jilu = new List<string>();
							follow = new List<string>();
							follow.Add("$");
							
							List<string> followlist = new List<string>(); 
							followList.TryGetValue(preFuhao, out followlist);
							for (int t = 0; t < followlist.Count; t++)
							{
								string aFuhao = followlist[t];
								for (int n = 0; n < garmmarList.Count; n++)
								{
									if(aItem.EqualsWithOutP (new Item(garmmarList[n].ElementAt(0).Key,garmmarList[n].ElementAt(0).Value)))
										actionTable.Add(i, aFuhao, new string[] { "r", n.ToString() });
								}
							}
						}
					}
				}
			}
		}
		public void MakeGotoTable()
		{
			for (int i = 0; i < Items.Count; i++)
			{
				Closure aItemList = Items[i];
				int go;
				foreach (string aNzhongjiefu in Nzhongjiefu)
				{
					aItemList.go.TryGetValue(aNzhongjiefu, out go);
					gotoTable.Add(i, aNzhongjiefu, go);
				}
			}
		}
		public bool Drive(string file,out string output)//file 是总符号表
		{
			output = "";
			StreamReader fs = new StreamReader(file);
			List<string> stack = new List<string>();
			stack.Add("0");
			fs.ReadLine();  //第一行标题，没用
			string[] aLine = fs.ReadLine().Split('\t');
			string aFuhao = aLine[0];

			while ( true)
			{
				int stackstatus =Convert.ToInt32( stack.ElementAt(stack.Count-1));
				string[] status = actionTable.Get(stackstatus, aFuhao);

				if (status == null)
				{
					Console.WriteLine("错误。。");
					output += "错误。。。\n";
					fs.Close();
					return false;
				}
				else if (status[0].Equals("s"))
				{
					string info=null;
					stack.Add(aFuhao);
					stack.Add(status[1]);

					aLine = fs.ReadLine().Split('\t');
					aFuhao = aLine[0];

					foreach (string i in stack)
					{
						info += i + " ";
					}
					info += "移动进栈\t\t"+aFuhao+"\n";
					output += info;
					Console.WriteLine(info);

					if (aFuhao == "NUM")
					{
						int i = Convert.ToInt16(aLine[1]);
						shuStack.Push(numTable[i]);
					}
					else if (aFuhao == "ID")
					{
						int i = Convert.ToInt16(aLine[1]);
						shuStack.Push(idTable[i]);
					}
					else if (aFuhao != "(" && aFuhao != ")" && aFuhao != ";" && aFuhao != "{" && aFuhao != "}")
						shuStack.Push(aFuhao);
				}
				else if (status[0].Equals("r"))
				{
					string info=null;
					int n = Convert.ToInt32(status[1]);
					TuidaoShi needTuidaoshi = garmmarList[n];
					int len = needTuidaoshi.ElementAt(0).Value.Length;
					for (int i = 0; i < len * 2; i++)
					{
						stack.RemoveAt(stack.Count - 1);
					}
					stackstatus = Convert.ToInt32(stack.ElementAt(stack.Count - 1));
					stack.Add(needTuidaoshi.ElementAt(0).Key);
					int go = gotoTable.Get(stackstatus, needTuidaoshi.ElementAt(0).Key);
					stack.Add(go.ToString());

					foreach (string i in stack)
					{
						info += i + " ";
					}
					info += "用 ";
					info += needTuidaoshi.ToString();
					info += " 进行归约\t";
					info += aFuhao+"\n";
					output += info;
					//////////////////////////////////////////////////////////////

				}
				else if (status[0].Equals("acc"))
				{
					Console.WriteLine("yes");
					output += "yes" + "\n";
					fs.Close();
					return true;
				}
				else
				{
					Console.WriteLine("no");
					output += "no" + "\n";
					fs.Close();
					return false;
				}
			}
		}

		private GrammarList garmmarList = new GrammarList();
		public List<string> zhongjiefu = new List<string>();
		public List<string> Nzhongjiefu = new List<string>();
		private Closure ClosureList = new Closure();
		public ItemList Items = new ItemList();
		private List<string> allFuhao = new List<string>();
		public ActionTable actionTable = new ActionTable();
		public GotoTable gotoTable = new GotoTable();
		private Dictionary<string,List<string>> followList = new Dictionary<string,List<string>>();
		private Dictionary<string, List<string>> firstList = new Dictionary<string, List<string>>();

		private List<string> numTable = new List<string>();
		private List<string> idTable = new List<string>();
		Stack<string> shuStack = new Stack<string>();
		Stack<int> remakeStart = new Stack<int>();
		CStack remakeEnd = new CStack();
		Stack<Si> forStack = new Stack<Si>();

		List<string> follow = new List<string>();
		List<string> jilu = new List<string>();
	}
}
