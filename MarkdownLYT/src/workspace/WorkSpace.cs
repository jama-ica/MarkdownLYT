using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using MarkdownLYT.Tag;
using MarkdownLYT.Note;

namespace MarkdownLYT
{
	internal class WorkSpace
	{

		string path;
		List<NoteBook> noteBooks;
		RootNoteLayerInfo? rootNoteLayer;

		public WorkSpace(string path)
		{
			this.path = path;
			this.noteBooks = new List<NoteBook>();
			this.rootNoteLayer = null;
		}

		public DirectoryInfo Backup(string directoryName)
		{
			var dir = new DirectoryInfo(directoryName);
			var today = DateTime.Now;
			var newDirectoryName = @$"{this.path}{Path.DirectorySeparatorChar}note_{today.ToString("yyyyMMdd_HHmmss")}";
			dir.MoveTo(newDirectoryName);
			return dir;
		}

		public bool LoadNotebooks(string directoryName)
		{
			noteBooks.Clear();

			if (!Directory.Exists(directoryName))
			{
				Log.Error($"Workspace: load notes: {directoryName} is not found");
				return false;
			}
			
			var filePaths = Directory.EnumerateFiles(directoryName, "*.md", SearchOption.AllDirectories);
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
					this.noteBooks.Add(notes);
				}
				catch (FileNotFoundException)
				{
					continue;
				}
			}

			this.rootNoteLayer = CreateRootlTagLayer(this.noteBooks);
			return true;
		}

		public List<string> GetAllTags()
		{
			if (this.rootNoteLayer == null)
			{
				Log.Error("rootNoteLayer == null");
				return null;
			}

			var allTags = this.rootNoteLayer.GetAllTags();
			return allTags;
		}

		public void UpdateAllMocFiles()
		{
			if (this.rootNoteLayer == null)
			{
				throw new Exception("rootNoteLayer == null");
			}

			var homeFile = new HomeFile(this.rootNoteLayer.mocFile.GetFullName());
			homeFile.UpdateFile(this.rootNoteLayer, );

			foreach (var childLayer in this.rootNoteLayer.chilidren)
			{
				UpdateMocFiles(childLayer);
			}
		}

		void UpdateMocFiles(NoteLayerInfo noteLayer)
		{
			var mocFile = new MocFile(noteLayer.mocFile.GetFullName());
			mocFile.UpdateFile(noteLayer);

			foreach (var child in noteLayer.chilidren)
			{
				UpdateMocFiles(child);
			}
		}

		public void UpdateTagFile()
		{
			var tags = GetAllTags();
			var tagsFile = new TagsFile($@"{GetNoteDirectoryName()}{Path.DirectorySeparatorChar}tags.md");
			tagsFile.UpdateFile(tags);
		}

		public RootNoteLayerInfo CreateRootlTagLayer(List<NoteBook> notes)
		{
			var rootTagLater = new RootNoteLayerInfo(GetNoteDirectoryName());

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

			foreach (var note in this.noteBooks)
			{
				note.ReplaceNote(this.rootNoteLayer);
			}
		}

		public void UpdateAllNoteBooks()
		{
			if (this.rootNoteLayer == null)
			{
				throw new Exception("rootNoteLayer == null");
			}
			foreach (var note in this.noteBooks)
			{
				note.UpdateBreadcrumbTrail(this.rootNoteLayer);
			}
		}

		public string GetNoteDirectoryName()
		{
			return $@"{this.path}{Path.DirectorySeparatorChar}note";
		}
	}
}
