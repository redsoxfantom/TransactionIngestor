using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TransactionIngestor.Data;
using TransactionIngestor.Enums;
using TransactionIngestor.Input;
using TransactionIngestor.Output;

namespace TransactionIngestor
{
    public class TransactionIngestorManager
    {
        InputManager iMgr;
        OutputManager oMgr;

        public TransactionIngestorManager(Func<DataRecord, Tuple<DataRecord, Regex>> RawConversionNotFound, 
            InputType inputType, string inputFile, OutputType outputType, string outputFile, bool combine)
        {
            iMgr = new InputManager(RawConversionNotFound, inputFile, inputType);
            oMgr = new OutputManager(outputFile, combine, outputType);

            oMgr.Producer = iMgr;
        }

        public void Process()
        {
            oMgr.Start();
        }
    }
}
