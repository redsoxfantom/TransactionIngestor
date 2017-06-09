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
				decimal totalIncome = 0;
				int incomeRow = 2;
				int expenseRow = 2;
				ws.Cells [baseX, baseY] = new Cell (month.MonthName);
				ws.Cells [baseX + 1, baseY] = new Cell ("Income");
				ws.Cells [baseX + 1, baseY+1] = new Cell ("Income Amount");
				ws.Cells [baseX + 1, baseY+2] = new Cell ("Expenses");
				ws.Cells [baseX + 1, baseY+3] = new Cell ("Expense Amount");
				ws.Cells [baseX + 1, baseY+4] = new Cell ("Savings");
				ws.Cells [baseX + 1, baseY+5] = new Cell ("Savings Amount");

				foreach(var total in month.Totals)
				{
					if(total.Value.Sum >= 0)
					{
						ws.Cells [baseX + incomeRow, baseY] = new Cell (total.Key);
						ws.Cells [baseX + incomeRow, baseY+1] = new Cell (total.Value.Sum);
						incomeRow++;
						totalIncome += total.Value.Sum;
					}
					else
					{
						ws.Cells [baseX + expenseRow, baseY+2] = new Cell (total.Key);
						ws.Cells [baseX + expenseRow, baseY+3] = new Cell (Math.Abs(total.Value.Sum));
						expenseRow++;
					}
				}

				ws.Cells [baseX + 2, baseY + 4] = new Cell ("5% Big Purchases");
				ws.Cells [baseX + 2, baseY + 5] = new Cell (totalIncome * new decimal(0.05));
				ws.Cells [baseX + 3, baseY + 4] = new Cell ("15% Emergencies");
				ws.Cells [baseX + 3, baseY + 5] = new Cell (totalIncome * new decimal(0.15));

				baseY += 6;
			}

			wb.Worksheets.Add (ws);
			wb.Save (FileToWriteTo);
		}
	}
}

