from transactioningestor.input.ingestors.wellsfargoingestor import WellsFargoIngestor
from transactioningestor.input.ingestors.stdfmtjsoningestor import StandardFormatJsonIngestor
from transactioningestor.enums import ImputType
from os import path

class IngestorException(Exception):
    pass

def createingestor(ingestortype, inputfile):
    if not path.isfile(inputfile):
        raise IngestorException(F"Input file {inputfile} not found")

    if ingestortype == InputType.WELLS_FARGO_CSV.name:
        return WellsFargoIngestor(inputfile)
    if ingestortype == InputType.STANDARD_FORMAT_JSON.name:
        return StandardFormatJsonIngestor(inputfile)
    raise IngestorException(F"IngestorType {ingestortype} not recognized")