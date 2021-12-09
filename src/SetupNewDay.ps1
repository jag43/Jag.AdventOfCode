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
    New-Item -Path $TestInputPath -Value $TestInputData| Out-Null
} else {
    Write-Warning "Test input file already exists"
}

# Create Input
$InputPath = "./Jag.AdventOfCode/Input/${Year}Day${Day}.txt"
if (!(Test-Path $InputPath)) {
    New-Item -Path $InputPath -Value $InputData| Out-Null
}
else {
    Write-Warning "Input file already exists"
}

# Create Part 1 Test Answer
$Part1TestAnswerPath = "./Jag.AdventOfCode/Answers/${Year}Day${Day}Part1Test.txt"
if (Test-Path $Part1TestAnswerPath) {
    Write-Warning "Part 1 test answer file already exists"
} else {
    New-Item -Path $Part1TestAnswerPath -Value $Part1TestAnswer| Out-Null
}

.\UpdateDay.ps1 -Year $Year -Day $Day -Part1Answer $Part1Answer -Part2TestAnswer $Part2TestAnswer -Part2Answer $Part2Answer

# Create Solver
$SolverDirectory = "./Jag.AdventOfCode/Y${Year}/Day${Day}";
$SolverPath = "${SolverDirectory}/Solver.cs";
if (!(Test-Path $SolverDirectory)) {
    New-Item -Path $SolverDirectory -ItemType "Directory"| Out-Null
}
if (Test-Path $SolverPath) {
    Write-Warning "Solver file already exists"
} else {
    New-Item -Path $SolverPath -Value "using System;

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
}"| Out-Null;
}

# Create Test class
$TestClassDirectory = "./Jag.AdventOfCode.Tests/Days/Day${Day}Tests"
$TestClassPath = "${TestClassDirectory}/Day${Day}Tests.cs"
if (!(Test-Path $TestClassDirectory)) {
    New-Item -Path $TestClassDirectory -ItemType "Directory"| Out-Null
}
if (Test-Path $TestClassPath) {
    Write-Warning "Test class file already exists"
} else {
    New-Item -Path $TestClassPath -Value "using System.Threading.Tasks;

using Jag.AdventOfCode.Tests.Traits;
using Jag.AdventOfCode.Y${Year}.Day${Day};
using Xunit;

namespace Jag.AdventOfCode.Tests.Y${Year}
{
    [Year(${Year}), Day(${Day})]
    public class Day${Day}Tests : TestBase
    {
        public Day${Day}Tests()
        : base (new Solver(), new InputRepository(), new AnswerRepository())
        {
        }

        //[Part(1), Input(true)]
        [Fact]
        public async Task Part1Test()
        {
            await base.Test(solver.Year, solver.Day, 1, true);
        }

        //[Part(1), Input(false)]
        [Fact]
        public async Task Part1()
        {
            await base.Test(solver.Year, solver.Day, 1, false);
        }

        //[Part(2), Input(true)]
        [Fact]
        public async Task Part2Test()
        {
            await base.Test(solver.Year, solver.Day, 2, true);
        }

        //[Part(2), Input(false)]
        [Fact]
        public async Task Part2()
        {
            await base.Test(solver.Year, solver.Day, 2, false);
        }
    }
}"| Out-Null;
}