using Day12;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

var sw = new Stopwatch();
sw.Start();
var inputFileName = "Input.txt";
var lines = File.ReadAllLines(inputFileName);
var instructions = lines.Select(line =>
    new Instruction(line[0].ToString(), int.Parse(line[1..]))).ToList();

Part1();
Part2();
Console.WriteLine($"Total time: {sw.Elapsed.TotalMilliseconds}");

void Part1()
{
    var position = new Tuple<int, int, Direction>(0, 0, Direction.East);
    position = instructions.Aggregate(position, (current, instruction) => instruction.Action switch
    {
        "N" => new Tuple<int, int, Direction>(current.Item1, current.Item2 + instruction.Step, current.Item3),
        "S" => new Tuple<int, int, Direction>(current.Item1, current.Item2 - instruction.Step, current.Item3),
        "W" => new Tuple<int, int, Direction>(current.Item1 - instruction.Step, current.Item2, current.Item3),
        "E" => new Tuple<int, int, Direction>(current.Item1 + instruction.Step, current.Item2, current.Item3),
        "F" => MoveForward(current, instruction.Step),
        "L" => new Tuple<int, int, Direction>(current.Item1, current.Item2, DetermineDirection(current.Item3, -instruction.Step)),
        "R" => new Tuple<int, int, Direction>(current.Item1, current.Item2, DetermineDirection(current.Item3, instruction.Step)),
        _ => throw new ArgumentOutOfRangeException()
    });
    var distance = Math.Abs(position.Item1) + Math.Abs(position.Item2);
    Console.WriteLine($"Manhattan distance = {distance}"); // 2879
}

Tuple<int, int, Direction> MoveForward(Tuple<int, int, Direction> currentPosition, int step)
{
    return currentPosition.Item3 switch
    {
        Direction.North => new Tuple<int, int, Direction>(currentPosition.Item1, currentPosition.Item2 + step,
            currentPosition.Item3),
        Direction.South => new Tuple<int, int, Direction>(currentPosition.Item1, currentPosition.Item2 - step,
            currentPosition.Item3),
        Direction.West => new Tuple<int, int, Direction>(currentPosition.Item1 - step, currentPosition.Item2,
            currentPosition.Item3),
        Direction.East => new Tuple<int, int, Direction>(currentPosition.Item1 + step, currentPosition.Item2,
            currentPosition.Item3),
        _ => throw new ArgumentOutOfRangeException()
    };
}


Direction DetermineDirection(Direction current, int degrees)
{
    return (Direction)(((int)current + (degrees / 90) + 4) % 4);
}

void Part2()
{
    var ship = new Tuple<int, int>(0, 0);
    var wayPoint = new Tuple<int, int>(10, 1);
    foreach (var instruction in instructions)
    {
        switch (instruction.Action)
        {
            case "F":
                ship = new Tuple<int, int>(ship.Item1 + wayPoint.Item1 * instruction.Step,
                    ship.Item2 + wayPoint.Item2 * instruction.Step);
                break;
            case "N":
                wayPoint =
                    new Tuple<int, int>(wayPoint.Item1, wayPoint.Item2 + instruction.Step);
                break;
            case "S":
                wayPoint =
                    new Tuple<int, int>(wayPoint.Item1, wayPoint.Item2 - instruction.Step);
                break;
            case "W":
                wayPoint =
                    new Tuple<int, int>(wayPoint.Item1 - instruction.Step, wayPoint.Item2);
                break;
            case "E":
                wayPoint =
                    new Tuple<int, int>(wayPoint.Item1 + instruction.Step, wayPoint.Item2);
                break;
            case "R":
                wayPoint = RotateWayPoint(wayPoint, instruction.Step);
                break;
            case "L":
                wayPoint = RotateWayPoint(wayPoint, -instruction.Step);
                break;
        }
    }
    var distance = Math.Abs(ship.Item1) + Math.Abs(ship.Item2);
    Console.WriteLine($"Manhattan distance = {distance}"); // 178986
}

Tuple<int, int> RotateWayPoint(Tuple<int, int> positionWayPoint, int degrees)
{
    var step = (degrees / 90 + 4) % 4;
    var point = positionWayPoint;
    for (var i = 0; i < step; i++)
    {
        point = new Tuple<int, int>(point.Item2, point.Item1 * -1);
    }
    return point;
}

namespace Day12
{
    record Instruction(string Action, int Step);

    enum Direction
    {
        East = 0,
        South = 1,
        West = 2,
        North = 3
    }
}