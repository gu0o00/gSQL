using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gSQL
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
			slr = new SLR("create.txt");

			slr.GetZhongjiefu();
			slr.GetAllFirst();
			slr.GetAllFollow();
			slr.Closure();
			slr.GetItems();
			slr.MakeActionTable();
			slr.MakeGotoTable();
		}

		private void toolStripButton1_Click(object sender, EventArgs e)
		{
			MessageBox.Show("功能尚未添加");
		}

		private void inputBox_KeyUp(object sender, KeyEventArgs e)
		{
			if (sender.ToString().Contains("RichTextBox") &&
				e.KeyCode.ToString().Equals("Return") &&
				this.inputBox.Text.Trim(new char[] { '\n' }).EndsWith(";"))
			{
				string[] sql = this.inputBox.Text.Split(';');
				String asql = (sql[line++] + ";").Replace('\n', ' ').TrimStart();
				outputBox.Text += asql + "\n";
				char a = asql[asql.Length-1];
			}
		}
		static Int32 line = 0;
		private SLR slr;
	}
}
