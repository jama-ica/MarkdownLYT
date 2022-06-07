using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using MarkdownLYT.Tag;

namespace MarkdownLYT
{
	class BreadcrumbTrail
	{
		public static void AddBreadcrumbTrail(FileInfo file, List<TagInfo> tags, RootNoteLayerInfo rootLayer)
		{
			if (tags == null)
			{
				throw new Exception("tag == null");
			}

			var breadcrumbTrails = new List<string>();
			foreach(var tag in tags)
			{
				var breadcrumbTrail = CreateBreadcrumbTrail(file, tag, rootLayer);
				breadcrumbTrails.Add(breadcrumbTrail);
			}
			
			string[] alllines = File.ReadAllLines(file.FullName, Encoding.UTF8);
			int line = 0;
			bool isComplate = true;;
			foreach(var breadcrumbTrail in breadcrumbTrails)
			{
				if(line >= alllines.Length)
				{
					isComplate = false;
					break;
				}
				if(alllines[line]!=breadcrumbTrail)
				{
					isComplate = false;
					break;
				}
				line++;
			}
			
			if(isComplate)
			{
				return;
			}
			
			using (var sw = new StreamWriter(file.FullName, append:false, Encoding.UTF8))
			{
				foreach(var breadcrumbTrail in breadcrumbTrails)
				{
					sw.WriteLine(breadcrumbTrail);
				}
				sw.WriteLine();
				foreach(var lineText in alllines)
				{
					if(lineText.StartsWith("[Home]("))
					{
						continue;
					}
					sw.WriteLine(lineText);
				}
			}
		}

		public static void AddBreadcrumbTrail(FileInfo file, NoteLayerInfo noteLayer, RootNoteLayerInfo rootLayer)
		{
			if (noteLayer == null)
			{
				throw new Exception("noteLayer is null");
			}

			var breadcrumbTrail = CreateBreadcrumbTrail(file, noteLayer);
			UpdateBreadcrumbTrail(breadcrumbTrail, file);
		}

		static string CreateBreadcrumbTrail(FileInfo file, TagInfo tag, RootNoteLayerInfo rootNoteLayer)
		{
			if (file == null)
			{
				throw new Exception("file == null");
			}
			if (!File.Exists(file.FullName))
			{
				throw new FileNotFoundException();
			}

			var sb = new StringBuilder();

			// Add top link
			var relativePath = rootNoteLayer.mocFile.GetRelativePath(file.DirectoryName);
			sb.Append($"[Home]({relativePath})");

			var layers = tag.layers;
			NoteLayerInfo parentLayer = rootNoteLayer;
			foreach (var layer in layers)
			{
				var noteLayer = parentLayer.FindChildNoteLayer(layer);
				if (noteLayer == null)
				{
					Logger.Warn($"BreadcrumbTrail: tag name: {layer} is not found");
					break;
				}

				relativePath = noteLayer.mocFile.GetRelativePath(file.DirectoryName);
				sb.Append($" / [{layer}]({relativePath})");

				parentLayer = noteLayer;
			}

			return sb.ToString();
		}

		public static string CreateBreadcrumbTrail(FileInfo file, NoteLayerInfo noteLayer)
		{
			var sb = new StringBuilder();

			var target = noteLayer;
			while (true)
			{
				var parent = target.parent;
				if (parent == null)
				{
					break;
				}

				if (sb.Length != 0)
				{
					sb.Insert(0, " / ");
				}

				var relativePath = parent.mocFile.GetRelativePath(file.DirectoryName);
				sb.Insert(0, $"[{parent.tagName}]({relativePath})");

				target = parent;
			}

			return sb.ToString();
		}

		static void UpdateBreadcrumbTrail(string breadcrumbTrail, FileInfo file)
		{
			E_BREADCRUMB_TRAIL_STATE state = CheckBreadcrumbTrail(breadcrumbTrail, file);

			switch (state)
			{
				case E_BREADCRUMB_TRAIL_STATE.NONE:
					AppendBreadcrumbTrail(breadcrumbTrail, file);
					break;
				case E_BREADCRUMB_TRAIL_STATE.INCORRECT:
					ReplaceBreadcrumbTrail(breadcrumbTrail, file);
					break;
				case E_BREADCRUMB_TRAIL_STATE.CORRECT:
					// nothing to do
					break;
				default:
					Logger.Error($"unexpect state ({state})");
					break;
			}
		}

		static void AppendBreadcrumbTrail(string breadcrumbTrail, FileInfo file)
		{
			var alltext = File.ReadAllText(file.FullName, Encoding.UTF8);

			using (var sw = new StreamWriter(file.FullName, append:false, Encoding.UTF8))
			{
				sw.WriteLine(breadcrumbTrail);
				sw.WriteLine("");
				sw.WriteLine(alltext);
			}
		}

		static void ReplaceBreadcrumbTrail(string breadcrumbTrail, FileInfo file)
		{
			string[] alllines = File.ReadAllLines(file.FullName, Encoding.UTF8);

			alllines[0] = breadcrumbTrail;

			using (var sw = new StreamWriter(file.FullName, append:false, Encoding.UTF8))
			{
				foreach (var line in alllines)
				{
					sw.WriteLine(line);
				}
			}
		}

		enum E_BREADCRUMB_TRAIL_STATE
		{
			NONE,
			INCORRECT,
			CORRECT,
		};

		static E_BREADCRUMB_TRAIL_STATE CheckBreadcrumbTrail(string breadcrumbTrail, FileInfo file)
		{
			E_BREADCRUMB_TRAIL_STATE state;
			using (var sr = new StreamReader(file.FullName))
			{
				var firstLine = sr.ReadLine();
				state = CheckBreadcrumbTrail(breadcrumbTrail, firstLine);
			}
			return state;
		}

		static E_BREADCRUMB_TRAIL_STATE CheckBreadcrumbTrail(string breadcrumbTrail, string? lineText)
		{
			if (lineText == null)
			{
				return E_BREADCRUMB_TRAIL_STATE.NONE;
			}
			if (!lineText.StartsWith("[Home]("))
			{
				return E_BREADCRUMB_TRAIL_STATE.NONE;
			}
			if (breadcrumbTrail == lineText)
			{
				return E_BREADCRUMB_TRAIL_STATE.CORRECT;
			}

			return E_BREADCRUMB_TRAIL_STATE.INCORRECT;
		}


	}
}
