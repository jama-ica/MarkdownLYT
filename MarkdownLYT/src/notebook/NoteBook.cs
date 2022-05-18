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

		public bool Load(string path)
		{
			var file = new FileInfo(path);
			return Load(file);
		}

		public bool Load(FileInfo file)
		{
			if (!File.Exists(file.FullName))
			{
				Log.Error($"Notebook: file not found: {file.FillName});
				return false;
			}

			Log.Info($"Notebook: load file: {file.FullName}");
			this.file = file;
			LoadTag(this.file);
			teturn true;
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
			this.tags.Clear();
			var tags = TagReader.Read(file);
			foreach (var tag in tags)
			{
				this.tags.Add(tag);
				Log.Debug($"Notebook add tag: {tag}");
			}
		}

	}
}
