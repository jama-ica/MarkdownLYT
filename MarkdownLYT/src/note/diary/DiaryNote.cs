using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MarkdownLYT.Note
{
	internal class DiaryNote
	{
		public static void Create(DateTime dateTime)
		{
			var fullname = GetFileFullName(dateTime);
			if (File.Exists(fullname))
			{
				return;
			}
			FileUtil.SafeCreateFile(fullname);
		}

		public static void Open(DateTime dateTime)
		{
			var fullname = GetFileFullName(dateTime);
			if (!File.Exists(fullname))
			{
				Log.Error($"file {fullname} is not found");
				return;
			}
			var proc = new System.Diagnostics.Process();
			proc.StartInfo.FileName = fullname;
			proc.StartInfo.UseShellExecute = true;
			proc.Start();
		}

		static string GetFileFullName(DateTime dateTime)
		{
			var workspacePath = SettingFile.GetData().workspace.path;
			var yyyy = dateTime.ToString("yyyy");
			var mmdd = dateTime.ToString("MM-dd");
			var week = GetWeekText(dateTime);
			return $@"{workspacePath}\dialy\{yyyy}\{yyyy}-{mmdd}-{week}.md";
		}

		static string GetWeekText(DateTime dateTime)
		{
			var week = dateTime.DayOfWeek;
			switch (week)
			{
				case DayOfWeek.Monday: return "mon";
				case DayOfWeek.Tuesday: return "tue";
				case DayOfWeek.Wednesday: return "wed";
				case DayOfWeek.Thursday: return "thu";
				case DayOfWeek.Friday: return "fri";
				case DayOfWeek.Saturday: return "sat";
				case DayOfWeek.Sunday: return "sun";
				default:
					throw new Exception($"unknow week {week}");
			}
		}

		public static bool IsDiaryFile(string filePath)
		{
			var splits = filePath.Split(Path.DirectorySeparatorChar);
			return splits[splits.Length - 3] == "dialy";
		}
	}
}
