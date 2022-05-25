using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MarkdownLYT
{
	internal class AppSettingFile
	{
		public static readonly string fileName = "setting.dat";

		static AppSettingFile? Instance = null;

		AppSettingObj? dataObj;

		private AppSettingFile()
		{
			this.dataObj = null;
		}

		public static AppSettingFile GetInstance()
		{
			if (Instance == null)
			{
				Instance = new AppSettingFile();
			}
			return Instance;
		}

		public static AppSettingObj? GetData()
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
			this.dataObj = deserializer.Deserialize<AppSettingObj>(text);
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

		public void Save(AppSettingObj data)
		{
			this.dataObj = data;
			using TextWriter writer = File.CreateText(fileName);
			var serializer = new YamlDotNet.Serialization.Serializer();
			serializer.Serialize(writer, data);
		}

		public AppSettingObj CreateDefaultData()
		{
			var data = new AppSettingObj
			{
				workspace = new AppSettingObj.WorkspaceData
				{
					path = String.Empty,
				}
			};
			return data;
		}

	}
}
