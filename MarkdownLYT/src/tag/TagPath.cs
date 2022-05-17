using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkdownLYT.Tag
{
	internal class TagPath
	{
		public static string[] GetLayers(string path)
		{
			return path.Split('/');
		}

		public static string RemoveTopLayer(string path)
		{
			int pos = path.IndexOf('/');
			if (pos == -1)
			{
				return "";
			}
			else
			{
				return path[pos..];
			}
		}

		public static string RemoveTopLayer(string[] layers)
		{
			StringBuilder sb = new StringBuilder();
			for (int i = 1; i < layers.Length; i++)
			{
				sb.Append(layers[i]);
				if (i + 1 >= layers.Length)
				{
					sb.Append("/");
				}
			}
			return sb.ToString();
		}
	}
}
