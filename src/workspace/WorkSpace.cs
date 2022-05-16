using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using MarkdownLYT.Tag;
using MarkdownLYT.Home;

namespace MarkdownLYT
{
	internal class WorkSpace
	{

		WorkSpaceSetting setting;

		string path;
		List<LYTFile> lytFiles;


		public WorkSpace()
		{
			this.lytFiles = new List<LYTFile>();
		}

		public void Load(string path)
		{
			this.path = path;
			this.lytFiles.Clear();

			if (!Directory.Exists(path))
			{
				throw new DirectoryNotFoundException(path);
			}

			var files = Directory.EnumerateFiles(path, "*.md", SearchOption.AllDirectories);
			foreach (string file in files)
			{
				try
				{
					var lytFile = new LYTFile();
					lytFile.Load(file);
					this.lytFiles.Add(lytFile);
					Console.WriteLine(file);
				}
				catch (FileNotFoundException)
				{
					continue;
				}
			}

		}

		public List<Tag.Tag> GetAllTags()
		{
			var allTags = new List<Tag.Tag>();

			foreach(var lytFile in this.lytFiles )
			{
				var tags = lytFile.tags;
				foreach (var tag in tags)
				{
					if (allTags.Contains(tag))
					{
						continue;
					}
					allTags.Add(tag);
				}
			}

			allTags.OrderBy(tag => tag.text);
			return allTags;
		}

		public void UpdateHomeFile()
		{
			var homeFile = new HomeFile(path + @"\home.md");
			homeFile.UpdateFile(GetAllTags());
		}

		public void UpsateTagFile()
		{
		}
	}
}
