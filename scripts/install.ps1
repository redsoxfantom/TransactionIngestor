param (
    [String] $TransactionsDirectory = ""
)

if([String]::IsNullOrEmpty($TransactionsDirectory))
{
    $Documents = [System.Environment]::GetFolderPath("MyDocuments")
    $TransactionsDirectory = Join-Path -Path $Documents -ChildPath "Transactions"
}

if( -not (Test-Path $TransactionsDirectory))
{
    Write-Output "Creating $TransactionsDirectory..."
    New-Item $TransactionsDirectory -ItemType "directory" | Out-Null
    if( -not $?)
    {
        Write-Error "Failed to create $TransactionDirectory"
        exit 1
    }
}

if(Test-Path $TransactionsDirectory/TransactionIngestor)
{
    Write-Output "Deleting $TransactionsDirectory/TransactionIngestor..."
    Remove-Item $TransactionsDirectory/TransactionIngestor -Recurse
    if ( -not $?)
    {
        Write-Error "Failed to delete $TransactionsDirectory/TransactionIngestor folder"
        exit 1
    }
}

if(Test-Path "../output")
{
    Write-Output "Deleting old compilation outputs..."
    Remove-Item "../output" -Recurse
    if ( -not $?)
    {
        Write-Error "Failed to delete output folder"
        exit 1
    }
}

& dotnet.exe restore ../TransactionIngestor.sln 
if ( -not $?)
{
    Write-Error "dotnet restore failed"
    exit 1
}

& dotnet.exe publish -o "../output/" ../TransactionIngestor.sln 
if ( -not $?)
{
    Write-Error "dotnet publish failed"
    exit 1
}

Write-Output "Copying scripts from windows and combined folders..."

Copy-Item "./windows/*" -Destination "$TransactionsDirectory"
if ( -not $?)
{
    Write-Error "Windows Script Copy Failed"
    exit 1
}

Copy-Item "./combined/*" -Destination "$TransactionsDirectory"
if ( -not $?)
{
    Write-Error "Combined Script Copy Failed"
    exit 1
}

Write-Output "Copying compiled output to $TransactionsDirectory/TransactionIngestor"
Copy-Item -Path ../output -Recurse -Destination $TransactionsDirectory/TransactionIngestor -Container
if ( -not $?)
{
    Write-Error "Compiled Output Copy Failed"
    exit 1
}

$ValidInputs = & $TransactionsDirectory/TransactionIngestor/TransactionIngestor.Install.Support.exe --request GET_INPUT_TYPES
if ( -not $?)
{
    Write-Error "Failed to get input types"
    exit 1
}
$ValidInputs = $ValidInputs.Split(', ')

Write-Output "Updating $TransactionsDirectory/README.md and $TransactionsDirectory/run.ps1"

((Get-Content "$TransactionsDirectory/README.md" -Raw) -replace '<TRANSACTIONSDIRCTORY>',"$TransactionsDirectory/Inputs" -replace '<INPUTTYPES>',"$ValidInputs") | Set-Content "$TransactionsDirectory/README.md"
if ( -not $?)
{
    Write-Error "Failed to replace text in README.md"
    exit 1
}

((Get-Content "$TransactionsDirectory/run.ps1" -Raw) -replace '<EXEDIRECTORY>',"$TransactionsDirectory/TransactionIngestor" -replace '<TRANSACTIONSDIRCTORY>',"$TransactionsDirectory/Inputs") | Set-Content "$TransactionsDirectory/run.ps1"
if ( -not $?)
{
    Write-Error "Failed to replace text in run.ps1"
    exit 1
}

Write-Output "`r`nInstallation completed`r`nRun $TransactionsDirectory/run.ps1 to ingest transactions`r`n"