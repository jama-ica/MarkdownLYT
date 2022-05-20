using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MarkdownLYT
{
	internal class FileUtil
	{
		public static void SafeCreateFile(string path)
		{
			var file = new FileInfo(path);
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
	}
}
