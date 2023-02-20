Write-Host °Æ«Î ‰»ÎKey°Ø

#$key = Read-Host
$key = 'oy2cshoat5ip4vcgxzjqkv42v3djlrvetkebllzdaumgfu'
$dir = './_packages'

if(Test-Path -Path $dir){
    Remove-Item $dir -Recurse
}

dotnet build -c Release

Get-ChildItem -Path $dir | ForEach-Object -Process{
    dotnet nuget push $_.fullname -s https://api.nuget.org/v3/index.json -k $key  --skip-duplicate
}

pause
