using System;
using TransactionIngestor.Interfaces;
using TransactionIngestor.Data;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace TransactionIngestor.Output.Transformers
{
	public class MonTotHumReadTransform : ITransformer
	{
		private Dictionary<Tuple<String,int>,Tuple<StreamWriter,JsonWriter>> CachedFiles;

		public MonTotHumReadTransform()
		{
			CachedFiles = new Dictionary<Tuple<string, int>, Tuple<StreamWriter, JsonWriter>> ();
		}

		public IDataProducer<DataRecord> Producer
		{
			set;
			private get;
		}

		public IEnumerable<object> GetRecords()
		{
			foreach(var record in Producer.GetRecords())
			{
				yield return record;
			}
		}
	}
}

