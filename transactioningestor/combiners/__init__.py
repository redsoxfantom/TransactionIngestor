from transactioningestor.enums import OutputType
from transactioningestor.combiners.stdfmtjsoncombiner import StandardFormatJsonCombiner
from transactioningestor.combiners.passthrucombiner import PassThruCombiner

def createcombiner(combinertype,dataproducer,filetocombinewith = None):
    if combinertype == OutputType.STANDARD_FORMAT_JSON.name and filetocombinewith:
        return StandardFormatJsonCombiner(dataproducer,filetocombinewith)
    return PassThruCombiner(dataproducer)