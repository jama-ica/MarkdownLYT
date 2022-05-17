using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkdownLYT.Tag
{
	internal class RootTagLayerInfo : TagLayerInfo
	{
		public RootTagLayerInfo()
			: base("", null)
		{
			
		}
		
		public void AddLytFile(LYTFile lytFile)
		{
			var tags = lytFile.tags;
			foreach (var tag in tags)
			{
				AddLytFile(tag.fullPath, lytFile);
			}
		}

		public void AddLytFile(string path, LYTFile lytFile)
		{
			string[] layers = TagPath.GetLayers(path);

			if (1 == layers.Length)
			{
				this.lytFiles.Add(lytFile);
			}
			else if(1 < layers.Length)
			{

			}

		}

		public override bool IsRoot()
		{
			return true;
		}
	}
}
