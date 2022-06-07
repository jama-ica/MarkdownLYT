using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using MarkdownLYT.Tag;

namespace MarkdownLYT.Note
{
	internal class MocFile : BaseNote
	{

		public MocFile(string path)
			: base(path)
		{
		}

		public void UpdateFile(NoteLayerInfo noteLayer)
		{
			if (!File.Exists(this.file.FullName))
			{
				FileUtil.SafeCreateFile(this.file.FullName);
			}

			using (var sw = new StreamWriter(this.file.FullName, append: false, Encoding.UTF8))
			{
				// add breadcrumb trail
				var breadcrumb = BreadcrumbTrail.CreateBreadcrumbTrail(file, noteLayer);
				sw.WriteLine(breadcrumb);
				sw.WriteLine();

				// add title
				sw.WriteLine($"# MOC - {noteLayer.tagName}");
				sw.WriteLine();

				// add moc link
				if (0 < noteLayer.chilidren.Count)
				{
					sw.WriteLine("## MOC");
				}
				var sortedChildren = noteLayer.chilidren.OrderBy(x => x.tagName);
				foreach (var childLayer in sortedChildren)
				{
					var relativePath = childLayer.mocFile.GetRelativePath(file.DirectoryName);
					relativePath = Uri.EscapeDataString(relativePath);
					sw.WriteLine($@"[{childLayer.tagName}]({relativePath})");
				}
				if (0 < noteLayer.chilidren.Count)
				{
					sw.WriteLine();
				}

				// add note link
				if (0 < noteLayer.notes.Count)
				{
					sw.WriteLine("## Notes");
				}
				var sortedNote = noteLayer.notes.OrderBy(x => x.GetName());
				foreach (var note in sortedNote)
				{
					var relativePath = note.GetRelativePath(file.DirectoryName);
					relativePath = Uri.EscapeDataString(relativePath);
					sw.WriteLine($@"[{note.GetFileName()}]({relativePath})");
				}
				if (0 < noteLayer.notes.Count)
				{
					sw.WriteLine();
				}
			}
		}

		//public static bool IsMocFile(string filePath)
		//{
		//	var splits = filePath.Split(Path.DirectorySeparatorChar);
		//	return (splits[splits.Length - 2] == splits[splits.Length - 1][..^3]);
		//}

	}
}
