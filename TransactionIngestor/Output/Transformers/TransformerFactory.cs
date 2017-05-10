﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransactionIngestor.Enums;

namespace TransactionIngestor.Output.Transformers
{
    public class TransformerFactory
    {
        public static ITransformer<object> CreateTransformer(OutputType outputType)
        {
            switch(outputType)
            {
                default:
                    return new PassThruTransformer();
            }
        }
    }
}