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

((Get-Content "../output/run.ps1" -Raw) -replace '<TRANSACTIONSDIRCTORY>',"$TransactionsDirectory") | Set-Content "../output/run.ps1"