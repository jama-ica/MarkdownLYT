﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using MarkdownLYT.Note;
using MarkdownLYT.Tag;
using System.IO;

namespace MarkdownLYT
{
	internal class RootNoteLayerInfo : NoteLayerInfo
	{
		// Constructor
		public RootNoteLayerInfo(string dirFullname)
			: base(dirFullname, "Home", null)
		{
		}


		public override MocFile CreateMocFile(string dirFullname, string tagName)
		{
			return new MocFile(Path.Combine(dirFullname, $"{tagName}.md"));
		}
		
		public override bool IsRoot()
		{
			return true;
		}

		public void AddLayer(Notebook note)
		{
			foreach (var tag in note.tags)
			{
				AddLayer(tag.fullName, note);
			}
		}

		public List<string> GetAllTags()
		{
			var allTags = new List<string>();

			foreach (var child in chilidren)
			{
				child.GetAllTags(ref allTags);
			}

			return allTags;
		}

		public NoteLayerInfo SearchNoteLayer(TagInfo tag)
		{
			var layers = tag.layers;

			if (0 >= layers.Count)
			{
				throw new Exception($"layers.Count is {layers.Count}");
			}

			var childLayer = FindChildNoteLayer(layers[0]);
			if (childLayer == null)
			{
				throw new Exception($"Layer not fond. Tag name is {layers[0]}");
			}

			if (1 == layers.Count)
			{
				return childLayer;
			}
			else
			{
				List<string> tempLayers = new List<string>(layers);
				tempLayers.RemoveAt(0);
				return childLayer.SearchNoteLayer(tempLayers);
			}
		}
	}
}
