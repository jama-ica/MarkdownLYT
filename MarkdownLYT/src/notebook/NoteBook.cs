using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using MarkdownLYT.Tag;

namespace MarkdownLYT
{
	internal class NoteBook
	{
		FileInfo file;
		public List<TagInfo> tags { get; }

		public NoteBook()
		{
			this.file = null;
			this.tags = new List<TagInfo>();
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

		public string GetFileName()
		{
			return this.file.Name;
		}

		public string GetName()
		{
			return this.file.Name[..^3];
		}

		public string GetFullName()
		{
			return this.file.FullName;
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
