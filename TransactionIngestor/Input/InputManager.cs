using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TransactionIngestor.Data;
using TransactionIngestor.Enums;
using TransactionIngestor.Input.Ingestors;
using TransactionIngestor.Interfaces;

namespace TransactionIngestor.Input
{
    public class InputManager : IDataRecordProducer
    {
        private IIngestor ingestor;
        private RawConverter converter;
		private ExtraneousTagHandler extraTagHandler;

		public InputManager(Func<DataRecord, Tuple<DataRecord, Regex>> RawConverterUpdateNeeded, String InputFileName, InputType inputType, string ignoreExtraneousTag)
        {
            ingestor = IngestorFactory.CreateIngestor(inputType);
            ingestor.InputFileName = InputFileName;

            converter = new RawConverter(RawConverterUpdateNeeded);
            converter.Producer = ingestor;

			extraTagHandler = new ExtraneousTagHandler (ignoreExtraneousTag);
			extraTagHandler.Producer = converter;
        }

        public IEnumerable<DataRecord> GetRecords()
        { 
			foreach(var record in extraTagHandler.GetRecords())
            {
                yield return record;
            }
        }
    }
}
