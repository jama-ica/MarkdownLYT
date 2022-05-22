using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using MarkdownLYT.Tag;

namespace MarkdownLYT.Note
{
	class NoteBook : BaseNote
	{
		public List<TagInfo> tags { get; }

		public NoteBook(string path)
			: base(path)
		{
			this.tags = new List<TagInfo>();
		}

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
		}

		public void UpdateBreadcrumbTrail(RootNoteLayerInfo rootNoteLayer)
		{
			if (0 == this.tags.Count)
			{
				throw new Exception("tags count is 0");
			}
			BreadcrumbTrail.AddBreadcrumbTrail(this.file, tags, rootNoteLayer);
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
				Log.Debug($"Notebook: add tag: {tag.fullName}");
			}
		}

		public void ReplaceNote(NoteLayerInfo noteLayer)
		{
			var newFullname = $@"{noteLayer.mocFile.GetDirectoryName()}{Path.DirectorySeparatorChar}{GetFileName()}";
			this.file.MoveTo(newFullname);
		}

	}
}
