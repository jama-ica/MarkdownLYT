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
		//List<NoteBook> notebooks;
		RootNoteLayerInfo? rootNoteLayer;

		public WorkSpace()
		{
			this.path = String.Empty;
			//this.notebooks = new List<NoteBook>();
			this.rootNoteLayer = null;
		}

		public bool Load(string path)
		{
			this.path = path;
			var notebooks = new List<NoteBook>();

			if (!Directory.Exists(path))
			{
				Log.Error($"Workspace: load: {path} is not found");
				return false;
			}
			
			var filePaths = Directory.EnumerateFiles(path, "*.md", SearchOption.AllDirectories);
			foreach (string filePath in filePaths)
			{
				if (DiaryNote.IsDiaryFile(filePath))
				{
					continue;
				}
				if (MocFile.IsMocFile(filePath))
				{
					continue;
				}
				if (HomeFile.IsHome(filePath))
				{
					continue;
				}
				try
				{
					var notes = new NoteBook(filePath);
					notes.Load();
					notebooks.Add(notes);
				}
				catch (FileNotFoundException)
				{
					continue;
				}
			}

			this.rootNoteLayer = CreateRootlTagLayer(notebooks);
			return true;
		}

		List<string> GetAllTags()
		{
			if (this.rootNoteLayer == null)
			{
				Log.Error("rootNoteLayer == null");
				return null;
			}

			var allTags = new List<string>();
			this.rootNoteLayer.GetAllTags(allTags);
			return allTags;
		}

		public void UpdateAllMocFiles()
		{
			if (this.rootNoteLayer == null)
			{
				throw new Exception("rootNoteLayer == null");
			}

			var homeFile = new HomeFile(this.rootNoteLayer.mocFile.path);
			homeFile.UpdateFile(this.rootNoteLayer);

			foreach (var childLayer in this.rootNoteLayer.chilidren)
			{
				UpdateMocFiles(childLayer);
			}
		}

		void UpdateMocFiles(NoteLayerInfo noteLayer)
		{
			var mocFile = new MocFile(noteLayer.mocFile.path);
			mocFile.UpdateFile(noteLayer);

			foreach (var child in noteLayer.chilidren)
			{
				UpdateMocFiles(child);
			}
		}

		public void UpdateTagFile()
		{
			var tags = GetAllTags();
			var tagsFile = new TagsFile($@"{GetNoteDirPath()}{Path.DirectorySeparatorChar}tags.md");
			tagsFile.UpdateFile(tags);
		}

		public RootNoteLayerInfo CreateRootlTagLayer(List<NoteBook> notes)
		{
			var rootTagLater = new RootNoteLayerInfo(GetNoteDirPath());

			foreach (var note in notes)
			{
				rootTagLater.AddLayer(note);
			}

			return rootTagLater;
		}

		public void ReplaceAllNotes()
		{
			if (this.rootNoteLayer == null)
			{
				throw new Exception("rootNoteLayer == null");
			}
			//TODO
		}

		public void UpdateAllNoteBooks()
		{
			if (this.rootNoteLayer == null)
			{
				throw new Exception("rootNoteLayer == null");
			}
			foreach (var note in this.rootNoteLayer.notes)
			{
				note.UpdateBreadcrumbTrail(this.rootNoteLayer);
			}
			foreach (var child in this.rootNoteLayer.chilidren)
			{
				UpdateNoteBooks(child);
			}
		}


		void UpdateNoteBooks(NoteLayerInfo noteLayer)
		{
			if (noteLayer == null)
			{
				throw new Exception("rootNoteLayer == null");
			}

			foreach (var note in noteLayer.notes)
			{
				note.UpdateBreadcrumbTrail(noteLayer);
			}
			foreach (var child in noteLayer.chilidren)
			{
				UpdateNoteBooks(child);
			}
		}

		string GetNoteDirPath()
		{
			return $@"{this.path}{Path.DirectorySeparatorChar}note";
		}
	}
}
