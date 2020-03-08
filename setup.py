from setuptools import setup, find_packages
import subprocess
import os

cwd = os.path.dirname(os.path.abspath(__file__))

try:
    with open(os.path.join(cwd,"VERSION")) as f:
        version = f.read()
except:
    version = "0.0.1+BETA"
commit_hash = subprocess.run(
    ['git','rev-parse','--short','HEAD'],
    cwd=cwd,
    stdout=subprocess.PIPE
).stdout.decode('ascii').strip()
version = version + F"+{commit_hash}"

try:
    with open(os.path.join(cwd,"README.md")) as f:
        long_desc = f.read()
except:
    long_desc = ""

try:
    with open(os.path.join(cwd,"requirements.txt")) as f:
        requirements = f.readlines()
except:
    requirements=[]

setup(
    name="transactioningestor-redsoxfantom",
    version=version,
    author="redsoxfantom",
    author_email="redsoxfantom@gmail.com",
    url="https://github.com/redsoxfantom/TransactionIngestor",
    description="Parse bank transaction history into budget trackers",
    long_description=long_desc,
    long_description_content_type="text/markdown",
    packages=find_packages(exclude="**/test/*"),
    install_requires=requirements,
    python_requires=">=3.7.3"
)