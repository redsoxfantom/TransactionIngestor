using System;
using System.Collections.Generic;

namespace TransactionIngestor.Output.OutputObjects
{
	public class MonthlyTotal
	{
		public int Year{ get; set;}
		public String MonthName{ get; set;}
		public Dictionary<String,decimal> Totals{get;set;}
	}
}

