param (
    [String] $TransactionsDirectory = "<TRANSACTIONSDIRCTORY>",
    [String] $ExecutableDirectory = "<EXEDIRECTORY>"
)

$date = (Get-Date).AddMonths(-1)
$year = $date.Year
$month = $date.Month
$exe = [IO.Path]::Combine($ExecutableDirectory,'TransactionIngestor.Console.exe')
$inputfolder = [IO.Path]::Combine($TransactionsDirectory,$year,$month)
$outputFile = [IO.Path]::Combine($inputfolder,"output.json")
$firstRun = $true
$dirs = Get-ChildItem -Directory $inputfolder

& $exe