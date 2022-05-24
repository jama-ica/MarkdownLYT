﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MarkdownLYT
{
	internal class FileUtil
	{
		public static void SafeCreateFile(string fullname)
		{
			var file = new FileInfo(fullname);
			if (file.Exists)
			{
				return;
			}

			var dir = file.Directory;
			if (dir == null)
			{
				throw new Exception("directory is null");
			}

			if (!dir.Exists)
			{
				dir.Create();
			}

			using (FileStream fs = file.Create()) ;
		}

		public static void SafeMoveTo(FileInfo oldFile, string newFullname)
		{
			if(oldFile.FullName == newFullname)
			{
				return;
			}

			var file = new FileInfo(newFullname);
			if (file.Exists)
			{
				file.Delete();
				//TODO
				//throw new Exception("file already exist");
			}

			var dir = file.Directory;
			if (dir == null)
			{
				throw new Exception("directory is null");
			}

			if (!dir.Exists)
			{
				dir.Create();
			}

			oldFile.MoveTo(newFullname);
		}

		public static FileInfo SafeCopyTo(FileInfo oldFile, string newFullname)
		{
			var file = new FileInfo(newFullname);
			if (file.Exists)
			{
				throw new Exception("file already exist");
			}

			var dir = file.Directory;
			if (dir == null)
			{
				throw new Exception("directory is null");
			}

			if (!dir.Exists)
			{
				dir.Create();
			}

			return oldFile.CopyTo(newFullname);
		}

		public static void SafeCreateSymbolicLink(FileInfo originalFile, string linkFileFullname)
		{
		#if NET6_0
			if (!originalFile.Exists)
			{
				throw new Exception("originalFile does not exist");
			}

			var file = new FileInfo(linkFileFullname);
			if (file.Exists)
			{
				throw new Exception("file already exist");
			}

			var dir = file.Directory;
			if (dir == null)
			{
				throw new Exception("directory is null");
			}

			if (!dir.Exists)
			{
				dir.Create();
			}
			File.CreateSymbolicLink(linkFileFullname, originalFile.FullName);
		#endif
		}
	}
}
