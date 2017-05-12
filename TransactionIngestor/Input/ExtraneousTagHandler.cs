using System;
using TransactionIngestor.Interfaces;
using TransactionIngestor.Data;
using System.Collections.Generic;

namespace TransactionIngestor
{
	public class ExtraneousTagHandler : IDataRecordConsumer, IDataRecordProducer
	{
		public IDataProducer<DataRecord> Producer { set; private get; }
		private string  extraneousDataTag;

		public ExtraneousTagHandler (string extraneousDataTag)
		{
			this.extraneousDataTag = extraneousDataTag;
		}

		public IEnumerable<DataRecord> GetRecords()
		{
			foreach (var record in Producer.GetRecords()) 
			{
				if (String.IsNullOrEmpty (extraneousDataTag)) 
				{
					yield return record;
				} 
				else 
				{
					if (!record.ParsedTransactionType.StartsWith (extraneousDataTag))
					{
						yield return record;
					}
				}
			}
		}
	}
}

