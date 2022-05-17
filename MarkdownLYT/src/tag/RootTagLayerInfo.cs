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
		public RootTagLayerInfo()
			: base("", null)
		{
			
		}
		

		public override bool IsRoot()
		{
			return true;
		}
	}
}
