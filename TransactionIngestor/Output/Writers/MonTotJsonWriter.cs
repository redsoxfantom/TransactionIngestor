using System;
using TransactionIngestor.Interfaces;
using Newtonsoft.Json;
using System.IO;
using TransactionIngestor.Output.OutputObjects;

namespace TransactionIngestor.Output.Writers
{
	public class MonTotJsonWriter : IWriter
	{
		public MonTotJsonWriter ()
		{
		}

		public string FileToWriteTo
		{
			set;
			private get;
		}
		public IDataProducer<object> Producer
		{
			set;
			private get;
		}

		public void Start()
		{
			JsonSerializer ser = new JsonSerializer();

			using (StreamWriter writer = new StreamWriter(FileToWriteTo))
			using (JsonWriter jsonWriter = new JsonTextWriter(writer))
			{
				jsonWriter.Formatting = Formatting.Indented;
				jsonWriter.WriteStartArray();
				foreach (MonthlyTotal record in Producer.GetRecords())
				{
					ser.Serialize(jsonWriter, record);
				}
				jsonWriter.WriteEndArray();
			}
		}
	}
}

