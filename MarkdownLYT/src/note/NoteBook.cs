using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using MarkdownLYT.Tag;

namespace MarkdownLYT.Note
{
	class Notebook : BaseNote
	{
		public List<TagInfo> tags { get; }

		public Notebook(string path)
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
				Logger.Error($"Notebook: file not found: {file.FullName}");
				return false;
			}

			Logger.Info($"Notebook: load: {file.FullName}");
			LoadTag(file);
			return true;
		}

		public void UpdateBreadcrumbTrail(RootNoteLayerInfo rootNoteLayer)
		{
			if (0 == this.tags.Count)
			{
				// No tag file
				return;
			}
			BreadcrumbTrail.AddBreadcrumbTrail(this.file, tags, rootNoteLayer);
		}


		void LoadTag(FileInfo file)
		{
			this.tags.Clear();
			var tags = TagReader.Read(file);
			if (tags.Count == 0)
			{
				Logger.Debug($"Notebook: no tag");
			}
			foreach (var tag in tags)
			{
				this.tags.Add(tag);
				Logger.Debug($"Notebook: add tag: {tag.fullName}");
			}
		}

		public void ReplaceNote(RootNoteLayerInfo rootNoteLayer)
		{
			if (this.tags.Count == 0)
			{
				SafeMoveTo($"{rootNoteLayer.mocFile.GetDirectoryName()}{Path.DirectorySeparatorChar}NoTags");
				return;
			}

			int num = 0;

			foreach (var tag in this.tags)
			{
				var noteLayer = rootNoteLayer.SearchNoteLayer(tag);
				if (num == 0)
				{
					SafeMoveTo(noteLayer.mocFile.GetDirectoryName());
				}
				else
				{
					SafeCreateSymbolicLink(noteLayer.mocFile.GetDirectoryName());
				}
				num++;
			}

		}

	}
}
