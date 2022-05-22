using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MarkdownLYT
{
	internal class SettingFile
	{
		public static readonly string fileName = "setting.dat";

		static SettingFile? Instance = null;

		SettingDataObj? dataObj;

		private SettingFile()
		{
			this.dataObj = null;
		}

		public static SettingFile GetInstance()
		{
			if (Instance == null)
			{
				Instance = new SettingFile();
			}
			return Instance;
		}

		public static SettingDataObj? GetData()
		{
			return GetInstance().dataObj;
		}

		public bool Load()
		{
			if (!File.Exists(fileName))
			{
				return false;
			}

			var text = File.ReadAllText(fileName);
			var deserializer = new YamlDotNet.Serialization.Deserializer();
			this.dataObj = deserializer.Deserialize<SettingDataObj>(text);
			return true;
		}

		public void Save()
		{
			if (this.dataObj == null)
			{
				Logger.Error("dataObj is null");
				return;
			}
			Save(this.dataObj);
		}

		public void Save(SettingDataObj data)
		{
			this.dataObj = data;
			using TextWriter writer = File.CreateText(fileName);
			var serializer = new YamlDotNet.Serialization.Serializer();
			serializer.Serialize(writer, data);
		}

		public SettingDataObj CreateDefaultData()
		{
			var data = new SettingDataObj
			{
				workspace = new SettingDataObj.WorkspaceData
				{
					path = String.Empty,
				}
			};
			return data;
		}

	}
}
