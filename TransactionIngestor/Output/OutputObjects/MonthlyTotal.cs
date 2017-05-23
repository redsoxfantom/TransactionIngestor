using System;
using System.Collections.Generic;

namespace TransactionIngestor.Output.OutputObjects
{
	public class MonthlyTotal
	{
		public int Year{ get; set;}
		public String MonthName{ get; set;}
		public Dictionary<String,TransTotals> Totals{get;set;}

	}

	public class TransTotals
	{
		public int NumTransactions;
		public decimal Average;
		public decimal Sum;
	}
}

