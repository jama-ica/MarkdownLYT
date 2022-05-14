using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkdownLYT.src
{
	internal class Tag
	{
		public Tag()
			: this("")
		{
		}

		public Tag(string name)
		{
			this.name = name;
		}

		public string name { get; set; }
	}
}
