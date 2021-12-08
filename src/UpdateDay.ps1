param (
    [Parameter(Mandatory=$true)]
    [int]$Year,
    [Parameter(Mandatory=$true)]
    [int]$Day,
    [string]$Part1Answer,
    [string]$Part2TestAnswer,
    [string]$Part2Answer
 )

# Create Part 1 Answer if exists
$Part1AnswerPath = "./Jag.AdventOfCode/Answers/${Year}Day${Day}Part1.txt"
if (Test-Path $Part1AnswerPath) {
    Write-Warning "Part 1 answer file already exists"
} elseif([string]::IsNullOrWhiteSpace($Part1Answer)){
    Write-Verbose "No part 1 answer value provided"
} else {
    New-Item -Path $Part1AnswerPath -Value $Part1Answer| Out-Null
}

# Create Part 2 Test Answer if exists
$Part2TestAnswerPath = "./Jag.AdventOfCode/Answers/${Year}Day${Day}Part2Test.txt"
if (Test-Path $Part2TestAnswerPath) {
    Write-Warning "Part 2 Test answer file already exists"
} elseif([string]::IsNullOrWhiteSpace($Part2TestAnswer)){
    Write-Verbose "No part 2 test answer value provided"
} else {
    New-Item -Path $Part2TestAnswerPath -Value $Part2TestAnswer| Out-Null
}

# Create Part 2 Answer if exists
$Part2AnswerPath = "./Jag.AdventOfCode/Answers/${Year}Day${Day}Part2.txt"
if (Test-Path $Part2AnswerPath) {
    Write-Warning "Part 2 answer file already exists"
} elseif([string]::IsNullOrWhiteSpace($Part2Answer)){
    Write-Verbose "No part 2 answer value provided"
} else {
    New-Item -Path $Part2AnswerPath -Value $Part2Answer| Out-Null
}