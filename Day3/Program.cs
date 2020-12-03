using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Day3;

var lines = File.ReadLines(@"Input.txt").ToArray();
var columns = lines[0].Length;

Part1();
Part2();

void Part1()
{
    var treeCount = TraverseMap(3,1);
    Console.WriteLine($"Number of encountered trees: {treeCount}"); // 232
}

void Part2()
{
    var slopes = new List<Slope>
    {
        new Slope(1, 1),
        new Slope(3, 1),
        new Slope(5, 1),
        new Slope(7, 1),
        new Slope(1, 2)
    };
    var result = 0L;
    foreach (var (rightSteps, downSteps) in slopes)
    {
        var treeCount = TraverseMap(rightSteps, downSteps);
        Console.WriteLine($"Number of encountered trees: {treeCount}, with slope Right: {rightSteps}, Down: {downSteps}");
        result = result == 0 ? treeCount : result * treeCount;
    }
    Console.WriteLine($"Multiplication of encountered trees: {result}"); // 3952291680
}

int TraverseMap(int rightSteps, int downSteps)
{
    var row = 0;
    var column = 0;
    var treeCount = 0;
    while (true)
    {
        row += downSteps;
        if (row >= lines.Length)
        {
            return treeCount;
        }
        column = (column + rightSteps) % columns;
        if (lines[row][column] == '#')
        {
            treeCount++;
        }
    }
}

namespace Day3
{
    internal record Slope(int RightSteps, int DownSteps);
}






