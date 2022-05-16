using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkdownLYT.Tag
{
	internal class TagInfo
	{
		public Tag tag { get; }
		public List<LYTFile> lytFiles { get; }

		public TagInfo(Tag tag)
		{
			this.lytFiles = new List<LYTFile>();
		}

		public void AddLYTFile(LYTFile lytFile)
		{
			this.lytFiles.Add(lytFile);
		}
	}
}
