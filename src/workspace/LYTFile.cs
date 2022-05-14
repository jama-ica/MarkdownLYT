﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using MarkdownLYT.Tag;

namespace MarkdownLYT
{
	internal class LYTFile
	{
		FileInfo file;
		List<TagInfo> tags;

		public LYTFile()
		{
			this.file = null;
			this.tags = null;
		}

		public void Load(string path)
		{
			var file = new FileInfo(path);
			Load(file);
		}

		public void Load(FileInfo file)
		{
			if (!File.Exists(file.FullName))
			{
				throw new FileNotFoundException(file.FullName);
			}

			this.file = file;
			LoadTag(this.file);
		}

		public void LoadTag(FileInfo file)
		{
			this.tags = TagReader.Read(file);
		}

	}
}
