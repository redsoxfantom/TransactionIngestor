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
            BasicConfigurator.Configure();

            var options = new Options();
            if (CommandLine.Parser.Default.ParseArguments(args, options))
            {
                if (options.OutputFile == null)
                {
                    options.OutputFile = Path.Combine(Directory.GetCurrentDirectory(), "output");
                }
                log.InfoFormat("Processing input file {0} as type {1} into output file {2} as type {3}",
                    options.InputFile, options.InputType, options.OutputFile, options.OutputType);

                TransactionIngestorManager mgr = new TransactionIngestorManager(PromptForConversion, options.InputType, 
                    options.InputFile, options.OutputType, options.OutputFile, options.Combine);

                mgr.Process();
            }
            else
            {
                log.Error("Failed to parse arguments");
                Environment.Exit(1);
            }

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

            System.Console.WriteLine("Enter a Regular Expression that we can use to recognize this transaction type in the future:");
            String newRegex = System.Console.ReadLine();
            if(String.IsNullOrEmpty(newRegex))
            {
                newRegex = record.RawTransactionType;
            }

            return new Tuple<DataRecord, Regex>(record, new Regex(newRegex, RegexOptions.Compiled));
        }
    }

    class Options
    {
        [Option("input", Required = true, HelpText = "File to be read")]
        public String InputFile { get; set; }

        [Option("inputType", Required = true, HelpText = "Type of input file this is")]
        public InputType InputType { get; set; }

        [Option("output", Required = false, DefaultValue = null, HelpText = "The path to the output file this should tool should generate. Defaults to <current directory>/output")]
        public String OutputFile { get; set; }

        [Option("combine", Required = false, DefaultValue = false, HelpText = "If the output file already exists, deterimines whether or not we should add this file to it or just wipe it out")]
        public Boolean Combine { get; set; }

        [Option("outputType", Required = false, DefaultValue = OutputType.STANDARD_FORMAT_JSON, HelpText = "The type of output file this ingestor should create")]
        public OutputType OutputType { get; set; }

        [HelpOption]
        public String Usage()
        {
            return HelpText.AutoBuild(this, (HelpText current) =>
                HelpText.DefaultParsingErrorsHandler(this, current));
        }
    }
}
