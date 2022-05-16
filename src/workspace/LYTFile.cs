using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using MarkdownLYT.Tag;

namespace MarkdownLYT
{
	internal class LYTFile
	{
		FileInfo file;
		public List<Tag.Tag> tags { get; }

		public LYTFile()
		{
			this.file = null;
			this.tags = new List<Tag.Tag>();
		}

		public void Load(string path)
		{
			var file = new FileInfo(path);
			Load(file);
		}

		public void Load(FileInfo file)
		{
			if (!File.Exists(file.FullName))
			{
				throw new FileNotFoundException(file.FullName);
			}

			this.file = file;
			LoadTag(this.file);
		}

		void LoadTag(FileInfo file)
		{
			var tags = TagReader.Read(file);
			foreach (var tag in tags)
			{
				this.tags.Add(tag);
			}
		}

	}
}
