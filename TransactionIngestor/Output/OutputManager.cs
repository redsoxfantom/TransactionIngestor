using System;
using System.Collections.Generic;
using System.IO;
using TransactionIngestor.Data;
using TransactionIngestor.Enums;
using TransactionIngestor.Interfaces;
using TransactionIngestor.Output.Combiners;
using TransactionIngestor.Output.Transformers;
using TransactionIngestor.Output.Writers;

namespace TransactionIngestor.Output
{
    public class OutputManager : IDataRecordConsumer, IDataRecordProducer
    {
        ICombiner mCombiner = null;
        ITransformer mTransformer = null;
        IWriter mWriter = null;
        string combinedScratchFile = null;

        public IDataProducer<DataRecord> Producer
        {
            set;
            private get;
        }

        public OutputManager(string outputFile, bool combine, OutputType outputType)
        {
            mTransformer = TransformerFactory.CreateTransformer(outputType);
            mWriter = WriterFactory.CreateWriter(outputType);
            mWriter.FileToWriteTo = outputFile;
            if(combine)
            {
                combinedScratchFile = outputFile + ".combiner";
                mCombiner = CombinerFactory.CreateCombiner(outputType);
                File.Copy(outputFile, combinedScratchFile);
                mCombiner.FileToCombineWith = combinedScratchFile;
            }
            else
            {
                mCombiner = new PassThruCombiner();
            }

            mTransformer.Producer = this;
            mCombiner.Producer = mTransformer;
            mWriter.Producer = mCombiner;
        }

        public IEnumerable<DataRecord> GetRecords()
        {
            foreach(var record in Producer.GetRecords())
            {
                yield return record;
            }
        }

        public void Start()
        {
            mWriter.Start();

            if(File.Exists(combinedScratchFile))
            {
                File.Delete(combinedScratchFile);
            }
        }
    }
}
