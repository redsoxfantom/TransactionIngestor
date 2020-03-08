from transactioningestor import get_transaction_dir, TransactionIngestor
from transactioningestor.enums import InputType
import sys
import os.path
from glob import glob

transdir = get_transaction_dir()
possibleinputs = [str(i).split('.',1)[1] for i in InputType]

def parsefilesfromfolder(inputtype,inputfolder):
    for file in glob(os.path.join(inputfolder,"*")):
        print(F"Parsing {file}...")
    pass

def lastmonthrun():
    from datetime import datetime,timedelta
    lastmonth = datetime.utcnow().replace(day=1) - timedelta(days=1)
    year = str(lastmonth.year)
    month = str(lastmonth.month)
    searchdir = os.path.join(transdir,year,month)
    print(F"Searching for transactions in {searchdir}...")
    
    atleastoneinputfound = False
    for inputtype in possibleinputs:
        inputfolder = os.path.join(searchdir,inputtype)
        if os.path.isdir(inputfolder):
            atleastoneinputfound = True
            parsefilesfromfolder(inputtype,inputfolder)
    if not atleastoneinputfound:
        print(F"No transaction directories found. Possible directories: {','.join(possibleinputs)}")
        return 1

    return 0

print(F"Using {transdir} for transaction dir")
if len(sys.argv) == 1:
    sys.exit(lastmonthrun())

sys.exit(0)