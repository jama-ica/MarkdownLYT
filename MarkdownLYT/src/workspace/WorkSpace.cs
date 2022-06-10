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
		string dirFullName;

		WorkspaceSettingFile? workspaceSetting;
		List<Notebook> noteBooks;
		RootNoteLayerInfo? rootNoteLayer;

		public WorkSpace(string dirFullName)
		{
			this.dirFullName = dirFullName;
			this.workspaceSetting = null;
			this.noteBooks = new List<Notebook>();
			this.rootNoteLayer = null;
		}

		public void Start()
		{
			this.workspaceSetting = new WorkspaceSettingFile();
			this.workspaceSetting.Load(this.dirFullName);

			DirectoryUtil.SafeCreate(GetMocDirectoryName());
			DirectoryUtil.SafeCreate(GetNoteDirectoryName());

		}

		public void CleanUpMoc()
		{
			var dirFullname = GetMocDirectoryName();
			Directory.Delete(dirFullname, true);
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
				try
				{
					var notes = new Notebook(fullname);
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
			var allTags = new List<string>();
			foreach(var note in this.noteBooks)
			{
				foreach(var tag in note.tags)
				{
					if (allTags.Contains(tag.fullName))
					{
						continue;
					}
					allTags.Add(tag.fullName);
				}
			}
			return allTags;
		}

		public void UpdateAllMocFiles()
		{
			if (this.rootNoteLayer == null)
			{
				throw new Exception("rootNoteLayer == null");
			}
			
			var noTagNotes = new List<Notebook>();
			foreach(var note in noteBooks)
			{
				if(0 == note.tags.Count)
				{
					noTagNotes.Add(note);
				}
			}

			// Home
			var homeFile = new HomeFile(this.rootNoteLayer.mocFile.GetFullName());
			homeFile.UpdateFile(this.rootNoteLayer, noTagNotes);

			// Mocs
			foreach (var childLayer in this.rootNoteLayer.chilidren)
			{
				UpdateMocFiles(childLayer);
			}

			// Tags
			UpdateAllTagNote();
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

		public void UpdateAllTagNote()
		{
			string fileFullName = AllTagNote.GetFileFullName(GetMocDirectoryName());
			var allTagsNote = new AllTagNote(fileFullName);
			allTagsNote.UpdateFile(this.rootNoteLayer);
		}

		public void UpdateTagFile()
		{
			var tags = GetAllTags();
			var tagsFile = new TagsFile($@"{GetMocDirectoryName()}{Path.DirectorySeparatorChar}tags.md");
			tagsFile.UpdateFile(tags);
		}

		public RootNoteLayerInfo CreateRootlTagLayer(List<Notebook> notes)
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
			return Path.Combine(this.dirFullName, "note");
		}

		public string GetMocDirectoryName()
		{
			return Path.Combine(this.dirFullName, "moc");
		}

		public string CreateNewNote(string name)
		{
			return Notebook.Create(GetNoteDirectoryName(), name);
		}

		public void OpenWorkspace()
		{
			var startInfo = new System.Diagnostics.ProcessStartInfo()
			{
				FileName = this.dirFullName,
				UseShellExecute = true,
				CreateNoWindow = true,
			};
			System.Diagnostics.Process.Start(startInfo);
		}

	}
}
