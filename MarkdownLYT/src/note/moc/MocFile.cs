using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using MarkdownLYT.Tag;

namespace MarkdownLYT.Moc
{
	internal class MocFile
	{
		public string path { get; }

		public MocFile(string path)
		{
			this.path = path;
		}

		public void UpdateFile(NoteLayerInfo noteLayer)
		{
			Log.Debug($"MocFile: update file: {noteLayer.tagName}");

			if (!File.Exists(this.path))
			{
				FileUtil.SafeCreateFile(this.path);
			}

			using (var sw = new StreamWriter(this.path, append: false, Encoding.UTF8))
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

		public string GetRelativePath(string currentDir)
		{
			return Path.GetRelativePath(currentDir, this.path);
		}

		public static bool IsMocFile(string filePath)
		{
			var splits = filePath.Split(Path.DirectorySeparatorChar);
			return (splits[splits.Length - 2] == splits[splits.Length - 1][..^3]);
		}

	}
}
