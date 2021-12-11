param (
    [Parameter(Mandatory=$true, Position=0, ValueFromPipeline)]
    [string]$AdventOfCodeSessionCookie)

Push-Location $PSScriptRoot

dotnet user-secrets --project ../Jag.AdventOfCode.Runner/ set "AdventOfCode:Http:SessionCookie" "53616c7465645f5f8296401d97490ccce8bbbc8ff820b44b6d06d9f27e6727b574ad47eb9507349f30d06e45b67d8f4c"

Pop-Location