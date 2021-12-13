Push-Location $PSScriptRoot

$AdventOfCodeSessionCookie = Read-Host -Prompt "Enter AdventOfCode session cookie or press enter to skip" -AsSecureString

if ($AdventOfCodeSessionCookie.Length -gt 0) {
    dotnet user-secrets --project ../Jag.AdventOfCode.Runner/ set "AdventOfCode:Http:SessionCookie" $AdventOfCodeSessionCookie
}

$srcDir = Join-Path $PSScriptRoot "..\" | Resolve-Path
$inputDir = Join-Path $srcDir "Jag.AdventOfCode\Input"
$answersDir = Join-Path $srcDir "Jag.AdventOfCode\Answers"

dotnet user-secrets --project ../Jag.AdventOfCode.Runner/ set "AdventOfCode:SourceRootDirectory:InputDirectory" $inputDir
dotnet user-secrets --project ../Jag.AdventOfCode.Runner/ set "AdventOfCode:SourceRootDirectory:AnswersDirectory" $answersDir

Pop-Location