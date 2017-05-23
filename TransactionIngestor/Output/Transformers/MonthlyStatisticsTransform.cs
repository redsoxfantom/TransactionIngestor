using System;
using TransactionIngestor.Interfaces;
using TransactionIngestor.Data;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Newtonsoft.Json;
using TransactionIngestor.Output.OutputObjects;

namespace TransactionIngestor.Output.Transformers
{
	public class MonthlyStatisticsTransform : ITransformer
	{
		private Dictionary<int,MonthlyTotal> runningTotals;

		public MonthlyStatisticsTransform()
		{
			runningTotals = new Dictionary<int, MonthlyTotal> ();
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
				int year = record.TransactionDate.Year;
				String month = record.TransactionDate.ToString ("MMMM");
				int monthKey = int.Parse (string.Format ("{0}{1}",year,record.TransactionDate.Month));

				if(!runningTotals.ContainsKey(monthKey))
				{
					runningTotals.Add (monthKey, new MonthlyTotal (){ Year = year, MonthName = month, Totals = new Dictionary<string, TransTotals>() });
				}

				var thisMonthsTotals = runningTotals [monthKey];
				if(!thisMonthsTotals.Totals.ContainsKey(record.ParsedTransactionType))
				{
					thisMonthsTotals.Totals.Add (record.ParsedTransactionType, new TransTotals(){Sum = record.TransactionAmount, NumTransactions = 1});
				}
				else
				{
					var total = thisMonthsTotals.Totals [record.ParsedTransactionType];
					thisMonthsTotals.Totals [record.ParsedTransactionType].NumTransactions++;
					thisMonthsTotals.Totals [record.ParsedTransactionType].Sum += record.TransactionAmount;
				}
			}

			foreach(var thisMonthsTotals in runningTotals.Values)
			{
				foreach(var transTotal in thisMonthsTotals.Totals.Values)
				{
					transTotal.Average = transTotal.Sum / transTotal.NumTransactions;
				}
			}

			var keysList = runningTotals.Keys.ToList ();
			keysList.Sort ();
			foreach(var key in keysList)
			{
				yield return runningTotals [key];
			}
		}
	}
}

