using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkdownLYT.Tag
{
	internal class TagLayerInfo
	{
		public string name { get;  }

		public TagLayerInfo parent { get; }
		public List<TagLayerInfo> chilidren { get; }

		public List<LYTFile> lytFiles { get; }

		public TagLayerInfo()
			: this("", null)
		{
			// this is root
		}

		public TagLayerInfo(string name, TagLayerInfo parent)
		{
			this.name = name;
			this.parent = parent;
			this.chilidren = new List<TagLayerInfo>();
			this.lytFiles = new List<LYTFile>();
		}

		public void AddChildLayer(TagLayerInfo tagLayerInfo)
		{
			this.chilidren.Add(tagLayerInfo);
		}

		public void AddLytFile(LYTFile lytFile)
		{
			this.lytFiles.Add(lytFile);
		}

		public bool IsRoot()
		{
			return this.parent == null;	
		}
	}
}
