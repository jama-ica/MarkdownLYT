using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MarkdownLYT.Note
{
	public interface INote
	{
		public string GetFullName();

		public string GetFileName();

		public string GetName();

		public string GetRelativePath(string currentDir);
	}
}
