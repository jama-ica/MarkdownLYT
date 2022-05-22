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

		public static string GetName(string path)
		{
			var pos = path.LastIndexOf('/');
			if (pos == -1)
			{
				return path;
			}
			return path[pos..];
		}

		public static string GetTopLayerName(string path)
		{
			int pos = path.IndexOf('/');
			if (pos == -1)
			{
				return path;
			}
			return path[..pos];
		}

		public static string RemoveTopLayer(string path)
		{
			int pos = path.IndexOf('/');
			if (pos == -1)
			{
				return "";
			}
			pos++;
			if (path.Length <= pos)
			{
				Logger.Warn($"unexpect path = {path}");
				return "";
			}
			return path[pos..];
		}

	}
}
