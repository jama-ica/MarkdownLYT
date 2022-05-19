using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace MarkdownLYT.Tag
{
	internal class RootTagLayerInfo : TagLayerInfo
	{
		// Constructor
		public RootTagLayerInfo(string path)
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
