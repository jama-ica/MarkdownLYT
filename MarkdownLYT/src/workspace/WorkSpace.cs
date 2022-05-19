using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using MarkdownLYT.Tag;
using MarkdownLYT.Moc;

namespace MarkdownLYT
{
	internal class WorkSpace
	{

		string path;
		List<NoteBook> notes;
		RootNoteLayerInfo? rootTagLayer;

		public WorkSpace()
		{
			this.path = String.Empty;
			this.notes = new List<NoteBook>();
			this.rootTagLayer = null;
		}

		public bool Load(string path)
		{
			this.path = path;
			this.notes.Clear();

			if (!Directory.Exists(path))
			{
				Log.Error($"Workspace: load: {path} is not found");
				return false;
			}
			
			Log.Info($"Workspace: load: {path}");
			
			var filePaths = Directory.EnumerateFiles(path, "*.md", SearchOption.AllDirectories);
			foreach (string filePath in filePaths)
			{
				try
				{
					var notes = new NoteBook(filePath);
					notes.Load();
					this.notes.Add(notes);
				}
				catch (FileNotFoundException)
				{
					continue;
				}
			}

			this.rootTagLayer = CreateRootlTagLayer(this.notes);
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

		public void UpdateAllMocFiles()
		{
			if (this.rootTagLayer == null)
			{
				Log.Error("Workspace: not initialzed");
			}

			var homeFile = new HomeMocFile(this.rootTagLayer.mocFile.path);
			homeFile.UpdateFile(this.rootTagLayer);

			foreach (var childLayer in this.rootTagLayer.chilidren)
			{
				UpdateMocFiles(childLayer);
			}
		}

		void UpdateMocFiles(NoteLayerInfo tagLayer)
		{
			var mocFile = new MocFile(tagLayer.mocFile.path);
			mocFile.UpdateFile(tagLayer);

			foreach (var child in tagLayer.chilidren)
			{
				UpdateMocFiles(child);
			}
		}

		public void UpdateTagFile()
		{
		}

		public RootNoteLayerInfo CreateRootlTagLayer(List<NoteBook> notes)
		{
			var rootTagLater = new RootNoteLayerInfo(this.path);

			foreach (var note in notes)
			{
				rootTagLater.AddLayer(note);
			}

			return rootTagLater;
		}
	}
}
