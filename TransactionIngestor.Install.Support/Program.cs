using System;
using CommandLine;
using TransactionIngestor.Enums;

namespace TransactionIngestor.Install.Support
{
    class Program
    {
        static void Main(string[] args)
        {
            CommandLine.Parser.Default.ParseArguments<Options>(args)
            .WithParsed<Options>(options=>{
                String req = options.Request;
                switch(req)
                {
                    case "GET_INPUT_TYPES":
                        var inputTypes = (InputType[])Enum.GetValues(typeof(InputType));
                        Console.WriteLine(String.Join(",",inputTypes));
                    break;
                }
            })
            .WithNotParsed<Options>(err=>{
                Console.WriteLine(String.Format("Could not parse Arguments: {0}\n{1}",err,String.Join(",",args)));
                Environment.Exit(1);
            });
        }
    }

    class Options
    {
        [Option("request",Required=true)]
        public String Request{get;set;}
    }
}
