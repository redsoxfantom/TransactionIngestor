import sys
from datetime import datetime
import os

def run():
    currtime = datetime.now()
    year = str(currtime.year)
    month = str(currtime.month)
    currdir = os.path.dirname(__file__)
    exe = os.path.join(currdir,"TransactionIngestor","TransactionIngestor.Console.exe")
    input_folder = os.path.join(currdir,"Inputs",year,month)
    output_file = os.path.join(input_folder,"output.json")
    first_run = True

    return True

if __name__ == "__main__":
    if not run():
        print("Run Failed")
        sys.exit(1)
    else:
        print("Run Completed")
