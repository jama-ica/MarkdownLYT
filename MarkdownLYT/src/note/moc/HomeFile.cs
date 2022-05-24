using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using MarkdownLYT.Tag;

namespace MarkdownLYT.Note
{
	internal class HomeFile : MocFile
	{

		public HomeFile(string path)
			: base(path)
		{
		}

		public void UpdateFile(RootNoteLayerInfo rootTagLayer, List<NoteBook> noTagNotes)
		{

			if (!File.Exists(this.file.FullName))
			{
				FileUtil.SafeCreateFile(this.file.FullName);
			}

			using (var sw = new StreamWriter(this.file.FullName, append:false, Encoding.UTF8))
			{
				// add title
				sw.WriteLine("# Home");
				sw.WriteLine();

				// add moc link
				if (0 < rootTagLayer.chilidren.Count)
				{
					sw.WriteLine("## MOC");
				}
				var sortedChildren = rootTagLayer.chilidren.OrderBy(x => x.tagName);
				foreach (var childLayer in sortedChildren)
				{
					var relativePath = childLayer.mocFile.GetRelativePath(file.DirectoryName);
					relativePath = Uri.EscapeDataString(relativePath);
					sw.WriteLine($@"[{childLayer.tagName}]({relativePath})");
				}
				if (0 < rootTagLayer.chilidren.Count)
				{
					sw.WriteLine();
				}

				// add note link
				if (0 < rootTagLayer.notes.Count)
				{
					sw.WriteLine("## Notes");
					sw.WriteLine();
				}
				var sortedNote = rootTagLayer.notes.OrderBy(x => x.GetName());
				foreach (var note in sortedNote)
				{
					var relativePath = note.GetRelativePath(file.DirectoryName);
					relativePath = Uri.EscapeDataString(relativePath);
					sw.WriteLine($"[{note.GetFileName()}]({relativePath})");
				}
				if (0 < rootTagLayer.notes.Count)
				{
					sw.WriteLine();
				}

				// add note no tags
				if (0 < noTagNotes.Count)
				{
					sw.WriteLine("## No Tag");
				}
				foreach (var note in noTagNotes)
				{
					var relativePath = note.GetRelativePath(file.DirectoryName);
					relativePath = Uri.EscapeDataString(relativePath);
					sw.WriteLine($"[{note.GetFileName()}]({relativePath})");
				}
			}
		}


		public static bool IsHome(string filePath)
		{
			var splits = filePath.Split(Path.DirectorySeparatorChar);
			return splits[splits.Length - 1] == "Home.md";
		}
	}
}
