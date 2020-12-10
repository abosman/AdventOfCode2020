using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

var sw = new Stopwatch();
sw.Start();
var lines = File.ReadAllLines(@"Input.txt");
var numbers = lines.Select(line => int.Parse(line)).ToList();
numbers.Add(0);
numbers.Add(numbers.Max()+3);
numbers.Sort();

Part1();
Part2();
Console.WriteLine($"Total time: {sw.Elapsed.TotalMilliseconds}");

void Part1()
{
    var oneJoltDifferences = 0;
    var threeJoltDifferences = 0;
    for (var i = 1; i < numbers.Count; i++)
    {
        var diff = numbers[i] - numbers[i - 1];
        if (diff == 1)
        {
            oneJoltDifferences++;
        }
        else
        {
            threeJoltDifferences++;
        }
    }
    Console.WriteLine($"Number of 1-jolt differences: {oneJoltDifferences} and number of 3-jolt differences: {threeJoltDifferences}");
    Console.WriteLine($"{oneJoltDifferences} x {threeJoltDifferences} = {oneJoltDifferences * threeJoltDifferences}"); // 1755
}

void Part2()
{
    var diffs = new List<int>();
    for (var i = 1; i < numbers.Count; i++)
    {
        var diff = numbers[i] - numbers[i - 1];
        diffs.Add(diff);
    }

    var combinations = 1L;
    var consecutiveOnesCount = 0;
    foreach (var diff in diffs)
    {
        if (diff == 1)
        {
            consecutiveOnesCount++;
        }
        else
        {
            var factor = consecutiveOnesCount switch
            {
                1 => 1,
                2 => 2,
                3 => 4,
                4 => 7,
                _ => 1
            };
            combinations *= factor;
            consecutiveOnesCount = 0;
        }
    }
    Console.WriteLine($"Number of combinations: {combinations}"); // 4049565169664
}