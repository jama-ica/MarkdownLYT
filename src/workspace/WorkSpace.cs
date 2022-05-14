using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MarkdownLYT
{
	internal class WorkSpace
	{
		List<LYTFile> lytFiles;

		public WorkSpace()
		{
			this.lytFiles = new List<LYTFile>();
		}

		public void Load(string path)
		{
			this.lytFiles.Clear();

			if (!Directory.Exists(path))
			{
				throw new DirectoryNotFoundException(path);
			}

			var files = Directory.EnumerateFiles(path, "*.md", SearchOption.AllDirectories);
			foreach (string file in files)
			{
				try
				{
					var lytFile = new LYTFile();
					lytFile.Load(file);
					this.lytFiles.Add(lytFile);
					Console.WriteLine(file);
				}
				catch (FileNotFoundException)
				{
					continue;
				}
			}

		}

	}
}
