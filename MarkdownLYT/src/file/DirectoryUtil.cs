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
				var newFileFullname = Path.Combine(newDir.FullName, file.Name);
				file.CopyTo(newFileFullname, overwrite:true);
			}

			foreach (var dir in sourceDir.GetDirectories())
			{
				var newDirFullname = Path.Combine(newDir.FullName, dir.Name);
				SafeCopyTo(dir.FullName, newDirFullname);
			}

		}

	}
}
