using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using MarkdownLYT.Tag;
using MarkdownLYT.Home;

namespace MarkdownLYT
{
	internal class WorkSpace
	{

		string path;
		List<NoteBook> notes;


		public WorkSpace()
		{
			this.path = String.Empty;
			this.notes = new List<NoteBook>();
		}

		public bool Load(string path)
		{
			this.path = path;
			this.notes.Clear();

			if (!Directory.Exists(path))
			{
				Log.Error($"path {path} is not found");
				return false;
			}
			
			Log.Info($"Workspace load: {path}");
			
			var filePaths = Directory.EnumerateFiles(path, "*.md", SearchOption.AllDirectories);
			foreach (string filePath in filePaths)
			{
				try
				{
					var notes = new NoteBook();
					notes.Load(filePath);
					this.notes.Add(notes);
					Log.Info($"Load: {filePath}");
				}
				catch (FileNotFoundException)
				{
					continue;
				}
			}
			return true;
		}

		public List<TagInfo> GetAllTags()
		{
			var allTags = new List<Tag.TagInfo>();

			foreach(var lytFile in this.notes )
			{
				var tags = lytFile.tags;
				foreach (var tag in tags)
				{
					if (allTags.Contains(tag))
					{
						continue;
					}
					allTags.Add(tag);
				}
			}

			allTags.OrderBy(tag => tag.fullPath);
			return allTags;
		}

		public void UpdateHomeFile()
		{
			var homeFile = new HomeFile(path + @"\home.md");
			homeFile.UpdateFile(CreateRootlTagLayer());
		}

		public void UpdateTagFile()
		{
		}

		public TagLayerInfo CreateRootlTagLayer()
		{
			var rootTagLater = new RootTagLayerInfo();

			foreach (var note in this.notes)
			{
				rootTagLater.AddLayer(note);
			}

			return rootTagLater;
		}
	}
}
