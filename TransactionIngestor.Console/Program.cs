using CommandLine;
using CommandLine.Text;
using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TransactionIngestor.Data;
using TransactionIngestor.Enums;
using TransactionIngestor.Interfaces;

namespace TransactionIngestor.Console
{
    class Program
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(Program));

        static void Main(string[] args)
        {
            CommandLine.Parser.Default.ParseArguments<Options>(args)
            .WithParsed<Options>(options=>{
                if (options.OutputFile == null)
                {
                    options.OutputFile = Path.Combine(Directory.GetCurrentDirectory(), "output");
                }
                log.InfoFormat("Processing input file {0} as type {1} into output file {2} as type {3}",
                    options.InputFile, options.InputType, options.OutputFile, options.OutputType);

                TransactionIngestorManager mgr = new TransactionIngestorManager(PromptForConversion, options.InputType, 
					options.InputFile, options.OutputType, options.OutputFile, options.Combine, options.IgnoreExtraneousTag);

                mgr.Process();
            })
            .WithNotParsed<Options>(Error=>{
                log.Error("Failed to parse arguments");
                Environment.Exit(1);
            });

            Environment.ExitCode = 0;
            log.Info("Done!");
        }

        private static Tuple<DataRecord,Regex> PromptForConversion(DataRecord record)
        {
            System.Console.WriteLine(String.Format("An unrecognized transaction type was found:\n{0}\nHow should this be categorized?",record.RawTransactionType));
            record.ParsedTransactionType = System.Console.ReadLine();
            if(String.IsNullOrEmpty(record.ParsedTransactionType))
            {
                record.ParsedTransactionType = record.RawTransactionType;
            }

			bool regularExpressionMatches = false;

			Regex reg = null;
			while (!regularExpressionMatches)
			{
				System.Console.WriteLine ("Enter a Regular Expression that we can use to recognize this transaction type in the future:");
				String newRegex = System.Console.ReadLine ();
				if (String.IsNullOrEmpty (newRegex))
				{
					newRegex = record.RawTransactionType;
				}

				reg = new Regex (newRegex, RegexOptions.Compiled);
				if(!reg.IsMatch(record.RawTransactionType))
				{
					System.Console.WriteLine (String.Format ("The regular expression {0} does not match the transaction type {1}",reg,record.RawTransactionType));
				}
				else
				{
					regularExpressionMatches = true;
				}
			}

            return new Tuple<DataRecord, Regex>(record, reg);
        }
    }

    class Options
    {
        [Option("input", Required = true, HelpText = "File to be read")]
        public String InputFile { get; set; }

        [Option("inputType", Required = true, HelpText = "Type of input file this is")]
        public InputType InputType { get; set; }

        [Option("output", Required = false, Default = null, HelpText = "The path to the output file this should tool should generate. Defaults to <current directory>/output")]
        public String OutputFile { get; set; }

        [Option("combine", Required = false, Default = false, HelpText = "If the output file already exists, deterimines whether or not we should add this file to it or just wipe it out")]
        public Boolean Combine { get; set; }

        [Option("outputType", Required = false, Default = OutputType.STANDARD_FORMAT_JSON, HelpText = "The type of output file this ingestor should create")]
        public OutputType OutputType { get; set; }

		[Option("ignoreExtraneousTag", Required = false, HelpText="If set, will configure the output report to ignore any Transation Types beginning with this tag")]
		public String IgnoreExtraneousTag{ get; set; }
    }
}
