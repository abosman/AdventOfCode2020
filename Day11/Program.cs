using System;
using System.Diagnostics;
using System.IO;

var sw = new Stopwatch();
sw.Start();
var inputFileName = "Input.txt";
var map = ReadInput(inputFileName);
Part1();
map = ReadInput(inputFileName);
Part2();
Console.WriteLine($"Total time: {sw.Elapsed.TotalMilliseconds}");

void Part1()
{
    var change = true;
    while (change)
    {
        change = false;
        var prevMap = map.Clone() as char[,];
        for (var i = 0; i <= map.GetUpperBound(0); i++)
        {
            for (var j = 0; j <= map.GetUpperBound(1); j++)
            {
                var point = prevMap[i, j];
                if (point == 'L' && OccupiedAdjacentSeats(prevMap, i, j) == 0)
                {
                    map[i, j] = '#';
                    change = true;
                }
                else if (point == '#' && OccupiedAdjacentSeats(prevMap, i, j) >= 4)
                {
                    map[i, j] = 'L';
                    change = true;
                }
            }
        }
    }
    Console.WriteLine($"Number of occupied seats: {NumberOfOccupiedSeats(map)}"); // 2489
}
void Part2()
{
    var change = true;
    while (change)
    {
        change = false;
        var prevMap = map.Clone() as char[,];
        for (int i = 0; i <= map.GetUpperBound(0); i++)
        {
            for (int j = 0; j <= map.GetUpperBound(1); j++)
            {
                var point = prevMap[i, j];
                if (point == 'L' && OccupiedSeats(prevMap, i, j) == 0)
                {
                    map[i, j] = '#';
                    change = true;

                }
                else if (point == '#' && OccupiedSeats(prevMap, i, j) >= 5)
                {
                    map[i, j] = 'L';
                    change = true;
                }
            }
        }
        //PrintGrid(map);
    }
    Console.WriteLine($"Number of occupied seats: {NumberOfOccupiedSeats(map)}"); // 2180
}

int OccupiedAdjacentSeats(char[,] map, int row, int col)
{
    var occupiedSeats = 0;
    var startColumn = Math.Max(col - 1, 0);
    var endColumn = Math.Min(col + 1, map.GetUpperBound(1));
    var startRow = Math.Max(row - 1, 0);
    var endRow = Math.Min(row + 1, map.GetUpperBound(0));

    for (int i = startRow; i <= endRow; i++)
    {
        for (int j = startColumn; j <= endColumn; j++)
        {
            if (i == row && j == col)
            {
                continue;
            }
            if (map[i, j] == '#')
            {
                occupiedSeats++;
            }
        }
    }
    return occupiedSeats;
}

int OccupiedSeats(char[,] map, int row, int col)
{
    var occupiedSeats = 0;
    for (int i = -1; i < 2; i++)
    {
        for (int j = -1; j < 2; j++)
        {
            if (i == 0 && j == 0)
            {
                continue;
            }
            if (FindOccupiedSeat(map,row, col, i, j))
            {
                occupiedSeats++;
            }
        }
    }
    return occupiedSeats;
}

bool FindOccupiedSeat(char[,] map, int currentRow, int currentCol, int rowOffset, int colOffset)
{

    var row = currentRow + rowOffset;
    var endRow = map.GetUpperBound(0);
    var col = currentCol + colOffset;
    var endColumn = map.GetUpperBound(1);

    while (true)
    {
        if (row < 0 || row > endRow || col < 0 || col > endColumn)
        {
            return false;
        }
        switch (map[row, col])
        {
            case '#':
                return true;
            case 'L':
                return false;
        }
        col += colOffset;
        row += rowOffset;
    }
}

int NumberOfOccupiedSeats(char[,] map)
{
    var numberOfOccupiedSeats = 0;
    for (var i = 0; i <= map.GetUpperBound(0); i++)
    {
        for (var j = 0; j <= map.GetUpperBound(1); j++)
        {
            if (map[i, j] == '#')
            {
                numberOfOccupiedSeats++;
            }
        }
    }
    return numberOfOccupiedSeats;
}

static void PrintGrid(char[,] map)
{
    for (int i = 0; i <= map.GetUpperBound(0); i++)
    {
        for (int j = 0; j <= map.GetUpperBound(1); j++)
        {
            Console.Write(map[i, j]);
        }

        Console.WriteLine();
    }

    Console.WriteLine();
}


char[,] ReadInput(string fileName)
{
    var lines = File.ReadAllLines(fileName);
    var map = new char[lines.Length, lines[0].Length];
    for (int i = 0; i < lines.Length; i++)
    {
        var line = lines[i];
        for (int j = 0; j < line.Length; j++)
        {
            map[i, j] = line[j];
        }
    }
    return map;
}