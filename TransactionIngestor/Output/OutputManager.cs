using TransactionIngestor.Data;
using TransactionIngestor.Enums;
using TransactionIngestor.Interfaces;
using TransactionIngestor.Output.Combiners;
using TransactionIngestor.Output.Transformers;
using TransactionIngestor.Output.Writers;

namespace TransactionIngestor.Output
{
    public class OutputManager : IDataRecordConsumer
    {
        ICombiner<object> mCombiner = null;
        ITransformer<object> mTransformer = null;
        IWriter<object> mWriter = null;

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
                mCombiner = CombinerFactory.CreateCombiner(outputType);
                mCombiner.FileToCombineWith = outputFile;
            }
            else
            {
                mCombiner = new PassThruCombiner();
            }
            
            mCombiner.Producer = mTransformer;
            mWriter.Producer = mCombiner;
        }
    }
}
