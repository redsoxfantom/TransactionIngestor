using System;
using TransactionIngestor.Output.Writers;
using TransactionIngestor.Interfaces;
using System.IO;
using TransactionIngestor.Output.OutputObjects;

namespace TransactionIngestor.Output.Writers
{
	public class MonTotHumReadWriter : IWriter
	{
		private const String HEADER_FORMAT = "{0} {1}";
		private const String TRANSACTION_FORMAT = "{0}\t${1}";

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
			using (FileStream writer = new FileStream (FileToWriteTo))
			using (StringWriter sw = new StringWriter(writer))
			{
				foreach (MonthlyTotal total in Producer.GetRecords())
				{
					sw.WriteLine (String.Format (HEADER_FORMAT, total.MonthName, total.Year));
					sw.WriteLine();

					sw.WriteLine ("INCOME");
					foreach(var transTotal in total.Totals)
					{
						if(transTotal.Value >= 0)
						{
							sw.WriteLine (String.Format (TRANSACTION_FORMAT, transTotal.Key, transTotal.Value));
						}
					}
					sw.WriteLine ();
					sw.WriteLine ("EXPENSES");
					foreach(var transTotal in total.Totals)
					{
						if(transTotal.Value < 0)
						{
							sw.WriteLine (String.Format (TRANSACTION_FORMAT, transTotal.Key, transTotal.Value));
						}
					}
					sw.WriteLine ();
				}
			}
		}
	}
}

