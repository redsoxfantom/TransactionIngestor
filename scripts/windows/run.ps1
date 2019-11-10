param (
    [String] $TransactionsDirectory = "<TRANSACTIONSDIRCTORY>"
)

$date = (Get-Date).AddMonths(-1)
$year = $date.Year
$month = $date.Month
$currentdir = (Get-Item -Path ".\").FullName
$exe = [IO.Path]::Combine($currentdir,'TransactionIngestor','TransactionIngestor.Console.exe')
$inputfolder = [IO.Path]::Combine($TransactionsDirectory,$year,$month)
$outputFile = [IO.Path]::Combine($inputfolder,"output.json")
$firstRun = $true
$dirs = Get-ChildItem -Directory $inputfolder

& ./TransactionIngestor/TransactionIngestor.Console.exe