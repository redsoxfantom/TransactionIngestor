using System;
using TransactionIngestor.Interfaces;
using ExcelLibrary.SpreadSheet;
using TransactionIngestor.Output.OutputObjects;

namespace TransactionIngestor.Output.Writers
{
	public class MonTotExcelWriter : IWriter
	{
		public MonTotExcelWriter ()
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
			Workbook wb = new Workbook ();
			Worksheet ws = new Worksheet ("Budget");

			int baseX = 0;
			int baseY = 0;
			foreach(MonthlyTotal month in Producer.GetRecords())
			{
				int incomeRow = 2;
				int expenseRow = 2;
				ws.Cells [baseX, baseY] = new Cell (month.MonthName);
				ws.Cells [baseX + 1, baseY] = new Cell ("Income");
				ws.Cells [baseX + 1, baseY+1] = new Cell ("Income Amount");
				ws.Cells [baseX + 1, baseY+3] = new Cell ("Expenses");
				ws.Cells [baseX + 1, baseY+4] = new Cell ("Expense Amount");

				foreach(var total in month.Totals)
				{
					if(total.Value.Sum >= 0)
					{
						ws.Cells [baseX + incomeRow, baseY] = new Cell (total.Key);
						ws.Cells [baseX + incomeRow, baseY+1] = new Cell (total.Value.Sum);
						incomeRow++;
					}
					else
					{
						ws.Cells [baseX + expenseRow, baseY+3] = new Cell (total.Key);
						ws.Cells [baseX + expenseRow, baseY+4] = new Cell (total.Value.Sum);
						expenseRow++;
					}
				}

				baseY += 6;
			}

			wb.Worksheets.Add (ws);
			wb.Save (FileToWriteTo);
		}
	}
}

