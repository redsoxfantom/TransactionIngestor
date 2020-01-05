import sys
from datetime import datetime,timedelta
import os

def run():
    lastmonth = datetime.now().replace(day=1) - timedelta(days=1)
    year = str(lastmonth.year)
    month = str(lastmonth.month)
    currdir = os.path.abspath(os.path.dirname(__file__))
    exe = os.path.join(currdir,"TransactionIngestor","TransactionIngestor.Console.exe")
    input_folder = os.path.join(currdir,"Inputs",year,month)
    output_file = os.path.join(input_folder,"output.json")
    first_run = True

    for folder in os.listdir(input_folder):
        containing_folder = os.path.join(input_folder,folder)
        if os.path.isdir(containing_folder):
            input_type = folder
            print(F"Processing files of type {input_type} in folder {containing_folder}")
            for input_file in os.listdir(containing_folder):
                input_file = os.path.join(containing_folder,input_file)
                print(input_file)

    return True

if __name__ == "__main__":
    if not run():
        print("Run Failed")
        sys.exit(1)
    else:
        print("Run Completed")
