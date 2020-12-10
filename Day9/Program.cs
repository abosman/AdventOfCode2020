using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

var sw = new Stopwatch();
sw.Start();
var lines = File.ReadAllLines(@"Input.txt");
var numbers = lines.Select(line => long.Parse(line)).ToList();
var preambleLength = 25;
var answerPart1 = 0L;
Part1();
Part2();
Console.WriteLine($"Total time: {sw.Elapsed.TotalMilliseconds}");

void Part1()
{
    for (int i = preambleLength; i < numbers.Count; i++)
    {
        List<long> preambleNumbers = numbers.GetRange(i - preambleLength, preambleLength);
        if (!IsValid(preambleNumbers, numbers[i]))
        {
            answerPart1 = numbers[i];
            Console.WriteLine($"First not valid number: {answerPart1}"); // 14144619
            break;
        }
    }
}

bool IsValid(List<long> preambleNumbers, long number)
{
    for (var i = 0; i < preambleNumbers.Count-1; i++)
    {
        for (int j = 1; j < preambleNumbers.Count; j++)
        {
            if (preambleNumbers[i] + preambleNumbers[j] == number)
            {
                return true;
            }
        }
    }
    return false;
}

void Part2()
{
    var startIndex = 0;
    var found = false;
    while (!found)
    {
        found = SumNumbers(startIndex);
        startIndex++;
    }
}

bool SumNumbers(int startIndex)
{
    var index = startIndex;
    var sum = 0L;
    while (sum < answerPart1)
    {
        sum += numbers[index];
        index++;
    }

    if (sum == answerPart1)
    {
        Console.WriteLine($"Sum of numbers from {startIndex} to {index-1} in list is: {answerPart1}");
        var min = numbers[startIndex];
        var max = numbers[startIndex];
        for (var i = startIndex + 1; i < index; i++)
        {
            min = Math.Min(min, numbers[i]);
            max = Math.Max(max, numbers[i]);
        }
        Console.WriteLine($"Sum of min and max in range are: {min} + {max} = {min + max}"); // 1766397
        return true;
    }

    return false;
}