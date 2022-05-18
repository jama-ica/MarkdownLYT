using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MarkdownLYT
{
	internal class SettingData
	{
		public static readonly string fileName = "setting.dat";

		static SettingData Instance = null;

		SettingDataObj dataObj;

		private SettingData()
		{
			this.dataObj = null;
		}

		public static SettingData GetInstance()
		{
			return Instance;
		}

		public static SettingDataObj GetData()
		{
			if(Instance == null)
			{
				Instance = new SettingData();
			}
			return Instance.dataObj;
		}

		public SettingDataObj Load()
		{
			if (!File.Exists(fileName))
			{
				return null;
			}

			var deserializer = new YamlDotNet.Serialization.Deserializer();
			return deserializer.Deserialize<SettingDataObj>(fileName);
		}

		public void Save()
		{
			Save(this.dataObj);
		}

		public void Save(SettingDataObj data)
		{
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
