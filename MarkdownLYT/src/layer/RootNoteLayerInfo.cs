using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using MarkdownLYT.Note;

namespace MarkdownLYT
{
	internal class RootNoteLayerInfo : NoteLayerInfo
	{
		// Constructor
		public RootNoteLayerInfo(string path)
			: base(path, "Home", null)
		{
		}

		public override bool IsRoot()
		{
			return true;
		}
		
		public void AddLayer(NoteBook note)
		{
			foreach (var tag in note.tags)
			{
				AddLayer(tag.fullPath, note);
			}
		}
	}
}
