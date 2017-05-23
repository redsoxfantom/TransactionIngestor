using System;
using TransactionIngestor.Output.Writers;
using TransactionIngestor.Interfaces;
using System.IO;
using TransactionIngestor.Output.OutputObjects;

namespace TransactionIngestor.Output.Writers
{
	public class MonTotHumReadWriter : IWriter
	{
		private const String HEADER_FORMAT = "===={0} {1}====";
		private const String TRANSACTION_FORMAT = "{0}${1}";

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
			using (StreamWriter writer = new StreamWriter (FileToWriteTo))
			{
				foreach (MonthlyTotal total in Producer.GetRecords())
				{
					writer.WriteLine (String.Format (HEADER_FORMAT, total.MonthName, total.Year));
					writer.WriteLine();

					int largestLineLength = 0;
					foreach(var transTotal in total.Totals)
					{
						if(transTotal.Key.Length > largestLineLength)
						{
							largestLineLength = transTotal.Key.Length;
						}
					}

					writer.WriteLine ("==INCOME==");
					foreach(var transTotal in total.Totals)
					{
						if(transTotal.Value.Sum >= 0)
						{
							writer.WriteLine (String.Format (TRANSACTION_FORMAT, transTotal.Key.PadRight(largestLineLength+5,'.'), transTotal.Value.Sum));
						}
					}
					writer.WriteLine ();
					writer.WriteLine ("==EXPENSES==");
					foreach(var transTotal in total.Totals)
					{
						if(transTotal.Value.Sum < 0)
						{
							writer.WriteLine (String.Format (TRANSACTION_FORMAT, transTotal.Key.PadRight(largestLineLength+5,'.'), transTotal.Value.Sum));
						}
					}
					writer.WriteLine ();
				}
			}
		}
	}
}

