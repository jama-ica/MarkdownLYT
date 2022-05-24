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

		public void Backup(string sourceDirName)
		{
			var today = DateTime.Now;
			var newDirectoryName = @$"{this.path}{Path.DirectorySeparatorChar}note_{today.ToString("yyyyMMdd_HHmmss")}";
			DirectoryUtil.SafeCopyTo(sourceDirName, newDirectoryName);
		}

		//public void CleanUp()
		//{
		//	var noteDirectoryName = GetNoteDirectoryName();
		//	var fullnames = Directory.EnumerateFiles(noteDirectoryName, "*.md", SearchOption.AllDirectories);
		//	foreach (string fullname in fullnames)
		//	{
		//		if (DiaryNote.IsDiaryFile(fullname))
		//		{
		//			continue;
		//		}
		//		if (MocFile.IsMocFile(fullname))
		//		{
		//			File.Delete(fullname);
		//		}
		//		if (HomeFile.IsHome(fullname))
		//		{
		//			File.Delete(fullname);
		//		}
		//		if (TagsFile.IsTagsFile(fullname))
		//		{
		//			File.Delete(fullname);
		//		}
		//	}
		//}

		public void CleanUpMoc()
		{
			var mocDirectoryName = GetMocDirectoryName();
			var fullnames = Directory.EnumerateFiles(mocDirectoryName, "*.md", SearchOption.AllDirectories);
			foreach (string fullname in fullnames)
			{
				File.Delete(fullname);
			}
		}


		public bool LoadNotebooks()
		{
			noteBooks.Clear();

			var directoryName = GetNoteDirectoryName();

			if (!Directory.Exists(directoryName))
			{
				Logger.Error($"Workspace: load notes: {directoryName} is not found");
				return false;
			}
			
			var fullnames = Directory.EnumerateFiles(directoryName, "*.md", SearchOption.AllDirectories);
			foreach (string fullname in fullnames)
			{
				//if (DiaryNote.IsDiaryFile(fullname))
				//{
				//	continue;
				//}
				//if (MocFile.IsMocFile(fullname))
				//{
				//	continue;
				//}
				//if (HomeFile.IsHome(fullname))
				//{
				//	continue;
				//}
				//if (TagsFile.IsTagsFile(fullname))
				//{
				//	continue;
				//}
				try
				{
					var notes = new NoteBook(fullname);
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
				Logger.Error("rootNoteLayer == null");
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
			
			var noTagNotes = new List<NoteBook>();
			foreach(var note in noteBooks)
			{
				if(0 == note.tags.Count)
				{
					noTagNotes.Add(note);
				}
			}

			var homeFile = new HomeFile(this.rootNoteLayer.mocFile.GetFullName());
			homeFile.UpdateFile(this.rootNoteLayer, noTagNotes);

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
			var tagsFile = new TagsFile($@"{GetMocDirectoryName()}{Path.DirectorySeparatorChar}tags.md");
			tagsFile.UpdateFile(tags);
		}

		public RootNoteLayerInfo CreateRootlTagLayer(List<NoteBook> notes)
		{
			var rootTagLater = new RootNoteLayerInfo(GetMocDirectoryName());

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

		public string GetMocDirectoryName()
		{
			return $@"{this.path}{Path.DirectorySeparatorChar}moc";
		}
	}
}
