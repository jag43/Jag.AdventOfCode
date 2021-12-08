param (
    [Parameter(Mandatory=$true)]
    [int]$Year,
    [Parameter(Mandatory=$true)]
    [int]$Day,
    [Parameter(Mandatory=$true)]
    [string]$TestInputData,
    [Parameter(Mandatory=$true)]
    [string]$InputData,
    [Parameter(Mandatory=$true)]
    [string]$Part1TestAnswer,
    [string]$Part1Answer,
    [string]$Part2TestAnswer,
    [string]$Part2Answer
 )
# Create Test Input
$TestInputPath = "./Jag.AdventOfCode/Input/${Year}Day${Day}Test.txt";
if (!(Test-Path $TestInputPath)) {
    New-Item -Path $TestInputPath -Value $TestInputData
} else {
    Write-Warning "Test input file already exists"
}

# Create Input
$InputPath = "./Jag.AdventOfCode/Input/${Year}Day${Day}.txt"
if (!(Test-Path $InputPath)) {
    New-Item -Path $InputPath -Value $InputData
}
else {
    Write-Warning "Input file already exists"
}

# Create Part 1 Test Answer
$Part1TestAnswerPath = "./Jag.AdventOfCode/Answers/${Year}Day${Day}Part1Test.txt"
if (Test-Path $Part1TestAnswerPath) {
    Write-Warning "Part 1 test answer file already exists"
} else {
    New-Item -Path $Part1TestAnswerPath -Value $Part1TestAnswer
}

.\UpdateDay.ps1 -Part1Answer $Part1Answer -Part2TestAnswer $Part2TestAnswer -Part2Answer $Part2Answer

# Create Solver
New-Item -Path "./Jag.AdventOfCode/Y${Year}/Day${Day}" -ItemType "Directory"
New-Item -Path "./Jag.AdventOfCode/Y${Year}/Day${Day}" -Name "Solver.cs" -ItemType "Directory" -Value "using System;

namespace Jag.AdventOfCode.Y${Year}.Day${Day}
{
    public class Solver : ISolver
    {
        public int Year => ${Year};

        public int Day => ${Day};

        public string SolvePart1(string input)
        {
            throw new NotImplementedException();
        }

        public string SolvePart2(string input)
        {
            throw new NotImplementedException();
        }
    }
}";

# Create Test class
New-Item -Path "./Jag.AdventOfCode.Tests/Days/Day${Day}Tests" -ItemType "Directory"
New-Item -Path "./Jag.AdventOfCode/Y${Year}/Day${Day}" -Name "Solver.cs" -ItemType "Directory" -Value "";