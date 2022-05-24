using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MarkdownLYT
{
	internal class DirectoryUtil
	{
		public static void SafeCreate(string dirName)
		{
			var dir = new DirectoryInfo(dirName);
			if (!dir.Exists)
			{
				dir.Create();
			}
		}

		public static void SafeCopyTo(string sourceDirName, string newDirName)
		{
			var sourceDir = new DirectoryInfo(sourceDirName);
			if (!sourceDir.Exists)
			{
				throw new Exception("{sourceDirName} is not found.");
			}

			var newDir = new DirectoryInfo(newDirName);
			if(!newDir.Exists)
			{
				newDir.Create();
			}

			foreach (var file in sourceDir.GetFiles())
			{
				file.CopyTo(newDir.FullName + @"\" + file.Name, overwrite:true);
			}

			foreach (var dir in sourceDir.GetDirectories())
			{
				SafeCopyTo(dir.FullName, newDir.FullName + @"\" + dir.Name);
			}

		}

	}
}
