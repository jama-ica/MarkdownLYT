using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MarkdownLYT.Tag
{
	internal class AllTagsFile
	{
		string path;

		public AllTagsFile(string path)
		{
			this.path = path;
		}

		public void UpdateFile(List<TagInfo> tags)
		{
			using (var sw = new StreamWriter(path, append:false, Encoding.UTF8))
			{
				foreach (var tag in tags)
				{
					sw.WriteLine(tag.fullPath);
				}
			}
		}

	}
}
