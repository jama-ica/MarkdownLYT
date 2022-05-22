using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MarkdownLYT.Note
{
	public abstract class BaseNote
	{
		protected FileInfo file;

		public BaseNote(string path)
		{
			this.file = new FileInfo(path);
		}

		public string GetDirectoryName()
		{
			return this.file.DirectoryName;
		}

		public string GetFullName()
		{
			return this.file.FullName;
		}

		public string GetFileName()
		{
			return this.file.Name;
		}

		public string GetName()
		{
			return this.file.Name[..^3];
		}

		public string GetRelativePath(string currentDir)
		{
			var path = Path.GetRelativePath(currentDir, GetFullName());
			path = path.Replace(Path.DirectorySeparatorChar, '/');
			return path;
		}

		protected void MoveTo(string directoryName)
		{
			var newFullname = $@"{directoryName}{Path.DirectorySeparatorChar}{GetFileName()}";
			FileUtil.SafeMoveTo(this.file, newFullname);
		}

		protected void CreateSymbolicLink(string directoryName)
		{
			var newFullname = $@"{directoryName}{Path.DirectorySeparatorChar}{GetFileName()}";
			FileUtil.SafeCreateSymbolicLink(this.file, newFullname);
		}
	}
}
