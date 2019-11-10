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

Get-ChildItem -Directory $inputfolder | ForEach-Object {
    $fileType = "$_"
    $containingFolder = $_.FullName
    Write-Output "Processing files of type $fileType in folder $containingFolder"
    
    Get-ChildItem $containingFolder | ForEach-Object {
        $filename = $_.FullName
        Write-Output "Processing $filename..."
        $combineTag = If ($firstRun) {""} Else {"--combine"}
        $args = "--input ""{0}"" --inputType {1} --output ""{2}"" {3} --ignoreExtraneousTag IGNORE" -f $filename,$fileType,$outputFile,$combineTag
        Invoke-Expression "$exe $args"
        if( -not $? )
        {
            Write-Error "Failed to process input file $filename"
            exit 1
        }
        $firstRun = $false
    }
}

$args = "--input ""{0}"" --inputType STANDARD_FORMAT_JSON --output ""{1}"" --outputType MONTHLY_TOTALS_CSV --ignoreExtraneousTag IGNORE" -f $outputFile,[IO.Path]::Combine($inputfolder,"month.csv")
Invoke-Expression "$exe $args"
if( -not $? )
{
    Write-Error "Failed to process input file $filename"
    exit 1
}

cmd /c pause