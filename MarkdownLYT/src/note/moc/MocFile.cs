using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using MarkdownLYT.Tag;

namespace MarkdownLYT.Note
{
	internal class MocFile : INote
	{
		protected FileInfo file;

		public MocFile(string path)
		{
			this.file = new FileInfo(path);
		}

		public void UpdateFile(NoteLayerInfo noteLayer)
		{

			if (!File.Exists(this.file.FullName))
			{
				FileUtil.SafeCreateFile(this.file.FullName);
			}

			using (var sw = new StreamWriter(this.file.FullName, append:false, Encoding.UTF8))
			{
				// add breadcrumb trail
				var breadcrumb = BreadcrumbTrail.CreateBreadcrumbTrail(noteLayer);
				sw.WriteLine(breadcrumb);
				sw.WriteLine();

				// add title
				sw.WriteLine($"# {noteLayer.tagName}");
				sw.WriteLine();

				// add note link
				if (0 < noteLayer.notes.Count)
				{
					sw.WriteLine("notes");
					sw.WriteLine();
				}
				foreach (var note in noteLayer.notes)
				{
					var relativePath = note.GetRelativePath(noteLayer.directory);
					sw.WriteLine($"[{note.GetName()}]({relativePath})");
				}
				if (0 < noteLayer.notes.Count)
				{
					sw.WriteLine();
				}

				// add moc link
				if (0 < noteLayer.chilidren.Count)
				{
					sw.WriteLine("moc");
					sw.WriteLine();
				}
				foreach (var child in noteLayer.chilidren)
				{
					var relativePath = child.mocFile.GetRelativePath(noteLayer.directory);
					sw.WriteLine($"[{child.tagName}]({relativePath})");
				}
				if (0 < noteLayer.chilidren.Count)
				{
					sw.WriteLine();
				}
			}
		}

		public static bool IsMocFile(string filePath)
		{
			var splits = filePath.Split(Path.DirectorySeparatorChar);
			return (splits[splits.Length - 2] == splits[splits.Length - 1][..^3]);
		}

		public string GetFullName()
		{
			return this.file.FullName;
		}

		public string GetFileName()
		{
			return this.file.Name;
		}

		public string GetName()
		{
			return this.file.Name[..^3];
		}

		public string GetRelativePath(string currentDir)
		{
			return Path.GetRelativePath(currentDir, GetFullName());
		}
	}
}
