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
    New-Item $TransactionsDirectory -ItemType "directory" | Out-Null
    if( -not $?)
    {
        Write-Error "Failed to create $TransactionDirectory"
        exit 1
    }
}

if(Test-Path "../output")
{
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

& dotnet.exe publish -o ../output/TransactionIngestor ../TransactionIngestor.sln 
if ( -not $?)
{
    Write-Error "dotnet publish failed"
    exit 1
}

Copy-Item "./windows/*" -Destination "../output/"
if ( -not $?)
{
    Write-Error "Script Copy Failed"
    exit 1
}

Copy-Item "./combined/*" -Destination "../output/"
if ( -not $?)
{
    Write-Error "Script Copy Failed"
    exit 1
}

$ValidInputs = & ../output/TransactionIngestor/TransactionIngestor.Install.Support.exe --request "GET_INPUT_TYPES"
if ( -not $?)
{
    Write-Error "Failed to get input types"
    exit 1
}
$ValidInputs = $ValidInputs.Split(', ')
((Get-Content "../output/README.md" -Raw) -replace '<TRANSACTIONSDIRCTORY>',"$TransactionsDirectory" -replace '<INPUTTYPES>',"$ValidInputs") | Set-Content "../output/README.md"
if ( -not $?)
{
    Write-Error "Failed to replace text in README.md"
    exit 1
}

((Get-Content "../output/run.ps1" -Raw) -replace '<TRANSACTIONSDIRCTORY>',"$TransactionsDirectory") | Set-Content "../output/run.ps1"
if ( -not $?)
{
    Write-Error "Failed to replace text in run.ps1"
    exit 1
}