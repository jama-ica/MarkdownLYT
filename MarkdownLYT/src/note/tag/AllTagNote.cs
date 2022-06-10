using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MarkdownLYT.Note
{
	class AllTagNote : BaseNote
	{
		public static string GetFileFullName(string dirName)
		{
			return Path.Combine(dirName, "AllTags.md");
		}

		// Constructor
		public AllTagNote(string path)
			: base(path)
		{
		}

		public void UpdateFile(RootNoteLayerInfo rootNoteLayer)
		{
			if (!this.file.Exists)
			{
				FileUtil.SafeCreateFile(GetFullName());
			}

			using (var sw = new StreamWriter(GetFullName(), append: false, Encoding.UTF8))
			{
				AppendTagLink(sw, rootNoteLayer);
			}
		}

		void AppendTagLink(StreamWriter sw, NoteLayerInfo noteLayer)
		{
			foreach (var child in noteLayer.chilidren)
			{
				var tagFullName = child.GetFullTagName();
				var relativePath = child.mocFile.GetRelativePath(GetDirectoryName());
				sw.WriteLine($"[{tagFullName}]({relativePath})");

				AppendTagLink(sw, child);
			}
		}
	}
}
