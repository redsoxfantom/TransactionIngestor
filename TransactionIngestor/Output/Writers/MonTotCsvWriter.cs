using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransactionIngestor.Interfaces;
using TransactionIngestor.Output.OutputObjects;

namespace TransactionIngestor.Output.Writers
{
    public class MonTotCsvWriter : IWriter
    {
        public string FileToWriteTo { set; private get; }
        public IDataProducer<object> Producer { set; private get; }

        public void Start()
        {
            using (StreamWriter writer = new StreamWriter(FileToWriteTo))
            {
                foreach (MonthlyTotal month in Producer.GetRecords())
                {
                    writer.WriteLine(month.MonthName);

                    var incomes = month.Totals.Where((total) => { return total.Value.Sum > 0; });
                    decimal totalIncome = incomes.Sum((total) => { return total.Value.Sum; });
                    var expenses = month.Totals.Where((total) => { return total.Value.Sum <= 0; });
                    decimal totalExpenses = Math.Abs(expenses.Sum((total) => { return total.Value.Sum; }));
                    decimal bigPurchases = totalIncome * 0.05m;
                    decimal emergencies = totalIncome * 0.15m;
                    decimal totalSavings = bigPurchases + emergencies;
                    decimal monthlyTotalFree = totalIncome - totalExpenses;

                    string[] totalSavingsEntries = new string[] 
                    {
                        "Total Savings",
                        totalSavings.ToString(),
                        "Total Income",
                        totalIncome.ToString(),
                        "Total Expenses",
                        totalExpenses.ToString(),
                        "Monthly Total Free",
                        monthlyTotalFree.ToString(),
                        "Savings Below Free?",
                        monthlyTotalFree > totalSavings ? "1" : "0"
                    };
                    string[] savingsAmountEntries = new string[]
                    {
                        "Savings Amount",
                        bigPurchases.ToString(),
                        emergencies.ToString()
                    };
                    string[] savingsEntries = new string[]
                    {
                        "Savings",
                        "5% Big Purchases",
                        "15% Emergencies"
                    };
                    List<string> expensesAmountEntries = new List<string>()
                    {
                        "Expense Amount"
                    };
                    List<string> expensesEntries = new List<string>()
                    {
                        "Expenses"
                    };
                    foreach (var expense in expenses)
                    {
                        expensesAmountEntries.Add(Math.Abs(expense.Value.Sum).ToString());
                        expensesEntries.Add(expense.Key);
                    }
                    List<string> incomeAmountEntries = new List<string>()
                    {
                        "Income Amount"
                    };
                    List<string> incomeEntries = new List<string>()
                    {
                        "Income"
                    };
                    foreach (var income in incomes)
                    {
                        incomeAmountEntries.Add(income.Value.Sum.ToString());
                        incomeEntries.Add(income.Key);
                    }

                    int largestListLength = totalSavingsEntries.Length;
                    if (savingsEntries.Length > largestListLength)
                        largestListLength = savingsEntries.Length;
                    if (expensesEntries.Count > largestListLength)
                        largestListLength = expensesEntries.Count;
                    if (incomeEntries.Count > largestListLength)
                        largestListLength = incomeEntries.Count;

                    for(int i = 0; i < largestListLength; i++)
                    {
                        writer.WriteLine(String.Format("{0},{1},{2},{3},{4},{5},{6}",
                            incomeEntries.Count > i ? incomeEntries[i] : "",
                            incomeAmountEntries.Count > i ? incomeAmountEntries[i] : "",
                            expensesEntries.Count > i ? expensesEntries[i] : "",
                            expensesAmountEntries.Count > i ? expensesAmountEntries[i] : "",
                            savingsEntries.Length > i ? savingsEntries[i] : "",
                            savingsAmountEntries.Length > i ? savingsAmountEntries[i] : "",
                            totalSavingsEntries.Length > i ? totalSavingsEntries[i] : ""
                        ));
                    }
                }
            }
        }
    }
}
