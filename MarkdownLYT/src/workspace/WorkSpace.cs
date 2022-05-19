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
		RootTagLayerInfo? rootTagLayer;

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
<<<<<<< Updated upstream
				Log.Error($"path {path} is not found");
				return false;
			}
			
			Log.Info($"Workspace load: {path}");
=======
				Log.Error($"Workspace: load: {path} is not found");
				return false;
			}
			
			Log.Info($"Workspace: load: {path}");
>>>>>>> Stashed changes
			
			var filePaths = Directory.EnumerateFiles(path, "*.md", SearchOption.AllDirectories);
			foreach (string filePath in filePaths)
			{
				try
				{
<<<<<<< Updated upstream
					var notes = new NoteBook();
					notes.Load(filePath);
					this.notes.Add(notes);
					Log.Info($"Load: {filePath}");
=======
					var notes = new NoteBook(filePath);
					notes.Load();
					this.notes.Add(notes);
>>>>>>> Stashed changes
				}
				catch (FileNotFoundException)
				{
					continue;
				}
			}
<<<<<<< Updated upstream
=======

			this.rootTagLayer = CreateRootlTagLayer(this.notes);
>>>>>>> Stashed changes
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

		void UpdateMocFiles(TagLayerInfo tagLayer)
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

		public RootTagLayerInfo CreateRootlTagLayer(List<NoteBook> notes)
		{
			var rootTagLater = new RootTagLayerInfo(this.path);

			foreach (var note in notes)
			{
				rootTagLater.AddLayer(note);
			}

			return rootTagLater;
		}
	}
}
