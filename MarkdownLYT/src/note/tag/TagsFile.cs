using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MarkdownLYT
{
	internal class TagsFile
	{
		string path;

		public TagsFile(string path)
		{
			this.path = path;
		}

		public void UpdateFile(List<string> tags)
		{
			Log.Debug("HomeFile: update file");

			if (!File.Exists(this.path))
			{
				FileUtil.SafeCreateFile(this.path);
			}

			using (var sw = new StreamWriter(path, append:false, Encoding.UTF8))
			{
				foreach (var tag in tags)
				{
					sw.WriteLine(tag);
				}
			}
		}

	}
}
