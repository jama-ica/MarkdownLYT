﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MarkdownLYT.Note
{
	internal class TagsFile : BaseNote
	{

		public TagsFile(string path)
			: base(path)
		{
		}

		public void UpdateFile(List<string> tags)
		{
			Logger.Debug("HomeFile: update file");

			if (!this.file.Exists)
			{
				FileUtil.SafeCreateFile(GetFullName());
			}

			using (var sw = new StreamWriter(GetFullName(), append:false, Encoding.UTF8))
			{
				foreach (var tag in tags)
				{
					sw.WriteLine(tag);
				}
			}
		}

		public static bool IsTagsFile(string fullname)
		{
			var splits = fullname.Split(Path.DirectorySeparatorChar);
			return (splits[splits.Length - 1] == "tags.md");
		}

	}
}
