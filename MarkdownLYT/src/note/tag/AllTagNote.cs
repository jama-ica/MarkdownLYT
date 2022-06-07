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
				foreach (var child in rootNoteLayer.chilidren)
				{
				}
			}
		}

		void WriteLinkToTag(StreamWriter sw, NoteLayerInfo noteLayer, int layerLevel)
		{
			sw.WriteLine(noteLayer.tagName);
			//TODO
		}


	}
}
