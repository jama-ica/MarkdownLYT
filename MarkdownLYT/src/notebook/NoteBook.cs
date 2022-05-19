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

		public NoteBook(string path)
		{
			this.file = new FileInfo(path);
			this.tags = new List<TagInfo>();
		}

<<<<<<< Updated upstream
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
=======
		public bool Load()
		{
			return Load(this.file);
		}

		bool Load(FileInfo file)
		{
			if (!File.Exists(file.FullName))
			{
				Log.Error($"Notebook: file not found: {file.FullName}");
				return false;
			}

			Log.Info($"Notebook: load: {file.FullName}");
			LoadTag(file);
			return true;
>>>>>>> Stashed changes
		}

		public string GetFileName()
		{
			return this.file.Name;
		}

		public string GetName()
		{
			return this.file.Name[..^3];
		}

		public string GetPath()
		{
			return this.file.FullName;
		}

		public string GetRelativePath(string currentDir)
		{
			return Path.GetRelativePath(currentDir, this.file.FullName);
		}

		void LoadTag(FileInfo file)
		{
			this.tags.Clear();
			var tags = TagReader.Read(file);
			if (tags.Count == 0)
			{
				Log.Debug($"Notebook: no tag");
			}
			foreach (var tag in tags)
			{
				this.tags.Add(tag);
<<<<<<< Updated upstream
				Log.Debug($"Notebook add tag: {tag}");
=======
				Log.Debug($"Notebook: add tag: {tag.fullPath}");
>>>>>>> Stashed changes
			}
		}

	}
}
