using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkdownLYT
{
	internal class DailyNote
	{
		public static void Create(DateTime dateTime)
		{
			var year = today.Year;
			var month = today.
			var dt = System.DateTime.ParseExact("201709", "yyyyMM", null);
			//TODO
			FileUtil.SafeCreateFile();

		}

		public static void Open(DateTime dateTime)
		{
			var proc = new System.Diagnostics.Process();
			proc.StartInfo.FileName = @"c:\MySpace\memo.txt";
			proc.StartInfo.UseShellExecute = true;
			proc.Start();
		}

		static string GetFileFullName(DateTime dateTime)
		{
			SettingFile.GetData().workspace.path;
			return "";
		}
	}
}
