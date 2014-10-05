using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gSQL
{
	class FenCi : IFenCi
	{
		public FenCi()
		{
			Key = new ArrayList();
			ID = new ArrayList();
			Num = new ArrayList();
			Symbol = new ArrayList();
		}
		public void handle(string strSQL)
		{
			throw new NotImplementedException();
			for (int i = 0; i < strSQL.Length; i++)
			{

			}
		}
		public ArrayList Key;
		public ArrayList ID;
		public ArrayList Num;
		public ArrayList Symbol;
	}
}
